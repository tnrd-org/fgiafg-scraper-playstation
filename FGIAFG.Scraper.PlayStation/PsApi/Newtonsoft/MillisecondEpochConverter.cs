#region License

// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Globalization;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FGIAFG.Scraper.PlayStation.PsApi.Newtonsoft
{
    public class MillisecondEpochConverter : DateTimeConverterBase
    {
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long seconds;

            if (value is DateTime dateTime)
            {
                seconds = (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
#if HAVE_DATE_TIME_OFFSET
            else if (value is DateTimeOffset dateTimeOffset)
            {
                seconds = (long)(dateTimeOffset.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
            }
#endif
            else
            {
                throw new JsonSerializationException("Expected date object value.");
            }

            if (seconds < 0)
            {
                throw new JsonSerializationException(
                    "Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            }

            writer.WriteValue(seconds);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            bool nullable = ReflectionUtils.IsNullable(objectType);
            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw CreateJsonSerializationException(reader,
                        "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
                }

                return null;
            }

            long milliSeconds;

            if (reader.TokenType == JsonToken.Integer)
            {
                milliSeconds = (long)reader.Value!;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (!long.TryParse((string)reader.Value!, out milliSeconds))
                {
                    throw CreateJsonSerializationException(reader,
                        "Cannot convert invalid value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
                }
            }
            else
            {
                throw CreateJsonSerializationException(reader,
                    "Unexpected token parsing date. Expected Integer or String, got {0}.".FormatWith(
                        CultureInfo.InvariantCulture, reader.TokenType));
            }

            if (milliSeconds >= 0)
            {
                DateTime d = UnixEpoch.AddMilliseconds(milliSeconds);

#if HAVE_DATE_TIME_OFFSET
                Type t = (nullable)
                    ? Nullable.GetUnderlyingType(objectType)
                    : objectType;
                if (t == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(d, TimeSpan.Zero);
                }
#endif
                return d;
            }
            else
            {
                throw CreateJsonSerializationException(reader,
                    "Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {0}."
                        .FormatWith(CultureInfo.InvariantCulture, objectType));
            }
        }

        private JsonSerializationException CreateJsonSerializationException(JsonReader reader, string message)
        {
            MethodInfo method =
                typeof(JsonSerializationException).GetMethod("Create", new[]
                {
                    typeof(IJsonLineInfo), typeof(string), typeof(string), typeof(Exception)
                }, new[]
                {
                    new ParameterModifier(4)
                });

            return (JsonSerializationException)method.Invoke(null, new object[] { reader, message });
        }
    }
}
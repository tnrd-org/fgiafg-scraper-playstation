﻿using Microsoft.EntityFrameworkCore;

namespace FGIAFG.Scraper.PlayStation.Database;

internal class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public DbSet<GameModel> Games { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameModel>()
            .HasIndex(x => x.Hash).IsUnique();
    }
}

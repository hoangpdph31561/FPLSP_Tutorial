﻿using FPLSP_Tutorial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FPLSP_Tutorial.Infrastructure.Database.AppDbContext;

public class ExampleReadWriteDbContext : DbContext
{
    public ExampleReadWriteDbContext()
    {
    }

    public ExampleReadWriteDbContext(DbContextOptions<ExampleReadWriteDbContext> options) : base(options)
    {
    }

    #region DbSet

    public DbSet<ExampleEntity> Examples { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExampleReadWriteDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(
                "Server=<SERVER>;Database=<DATABASE>;User Id=<USER>;Password=<PASSWORD>;Trust Server Certificate=true;");
    }
}
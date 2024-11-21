﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class DB : DbContext
{
    public DB(DbContextOptions<DB> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogTag> BlogTags { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Blog__3214EC0752F3AB1F");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.Blogs).HasConstraintName("FK__Blog__UserId__29572725");
        });

        modelBuilder.Entity<BlogTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BlogTag__3214EC07FAA9413F");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogTags)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__BlogTag__BlogId__2E1BDC42");

            entity.HasOne(d => d.Tag).WithMany(p => p.BlogTags)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__BlogTag__TagId__2F10007B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC078A561F4A");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tag__3214EC07052D45C1");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0780A5C1BD");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasConstraintName("FK__Users__RoleId__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
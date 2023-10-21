using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_PRUEBAS.Models;

public partial class DbIotContext : DbContext
{
    public DbIotContext()
    {
    }

    public DbIotContext(DbContextOptions<DbIotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Eventos__3214EC070E516D11");

            entity.Property(e => e.Evento1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Evento");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Eventos__Usuario__300424B4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07DFCB23C8");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

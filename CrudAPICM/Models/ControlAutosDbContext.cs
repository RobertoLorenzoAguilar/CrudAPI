using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace CrudAPICM.Models;

public partial class ControlAutosDbContext : DbContext
{
    public ControlAutosDbContext()
    {
    }

    public ControlAutosDbContext(DbContextOptions<ControlAutosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DestinoRuta> DestinoRuta { get; set; }

    public virtual DbSet<Ruta> Ruta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DestinoRuta>(entity =>
        {
            entity.HasKey(e => e.IdDestino).HasName("PK__DestinoR__55FFB3D5543829FF");

            entity.Property(e => e.NombreDestino)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RutaDestino)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.oRuta).WithMany(p => p.DestinoRuta)
                .HasForeignKey(d => d.IdRuta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cargo");
        });

        modelBuilder.Entity<Ruta>(entity =>
        {
            entity.HasKey(e => e.IdRuta).HasName("PK__Ruta__887538FEB57DBEC4");

            entity.Property(e => e.NombreRuta)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.RutaInicio)
                .HasMaxLength(60)
                .IsUnicode(false);
        });


        //modelBuilder.Entity<Ruta>()
        //   .HasMany(e => e.DestinoRuta).
        //   .WithRequired()
        //   .WillCascadeOnDelete(true); // Configura la actualización en cascada



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

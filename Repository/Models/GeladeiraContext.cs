using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class GeladeiraContext : DbContext
{
    public GeladeiraContext()
    {
    }

    public GeladeiraContext(DbContextOptions<GeladeiraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Andar> Andars { get; set; }

    public virtual DbSet<Classificacao> Classificacaos { get; set; }

    public virtual DbSet<Container> Containers { get; set; }

    public virtual DbSet<Geladeira> Geladeiras { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Geladeira;Uid=sa;Pwd=123;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Andar>(entity =>
        {
            entity.HasKey(e => e.IdAndar);

            entity.ToTable("Andar");

            entity.Property(e => e.IdAndar).HasColumnName("ID_Andar");
            entity.Property(e => e.Andar1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Andar");
            entity.Property(e => e.IdClassificacao).HasColumnName("ID_Classificacao");

            entity.HasOne(d => d.IdClassificacaoNavigation).WithMany(p => p.Andars)
                .HasForeignKey(d => d.IdClassificacao)
                .HasConstraintName("FK_Andar_Classificacao");
        });

        modelBuilder.Entity<Classificacao>(entity =>
        {
            entity.HasKey(e => e.IdClassificacao);

            entity.ToTable("Classificacao");

            entity.Property(e => e.IdClassificacao).HasColumnName("ID_Classificacao");
            entity.Property(e => e.Classificacao1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Classificacao");
        });

        modelBuilder.Entity<Container>(entity =>
        {
            entity.HasKey(e => e.IdContainer);

            entity.ToTable("Container");

            entity.Property(e => e.IdContainer).HasColumnName("ID_container");
            entity.Property(e => e.IdAndar).HasColumnName("ID_Andar");

            entity.HasOne(d => d.IdAndarNavigation).WithMany(p => p.Containers)
                .HasForeignKey(d => d.IdAndar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Container_Andar1");
        });

        modelBuilder.Entity<Geladeira>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Geladeira");

            entity.Property(e => e.IdContainer).HasColumnName("ID_Container");
            entity.Property(e => e.IdItem).HasColumnName("ID_Item");

            entity.HasOne(d => d.IdContainerNavigation).WithMany()
                .HasForeignKey(d => d.IdContainer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Geladeira_Container");

            entity.HasOne(d => d.IdItemNavigation).WithMany()
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Geladeira_Item1");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IdItem);

            entity.ToTable("Item");

            entity.Property(e => e.IdItem).HasColumnName("ID_ITem");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdClassificacao).HasColumnName("ID_Classificacao");
            entity.Property(e => e.UnidadeQuantidade)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClassificacaoNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdClassificacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Item_ID_Classificacao_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StacktimApi.Models;

namespace StacktimApi.Data;

public partial class StacktimDbContext : DbContext
{
    public StacktimDbContext()
    {
    }

    public StacktimDbContext(DbContextOptions<StacktimDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=StacktimDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.IdPlayers).HasName("PK__Players__8FEDD67CEDA33B98");

            entity.HasIndex(e => e.Email, "UQ__Players__A9D10534DF4884C7").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Players__F1433CEF2E1786C8").IsUnique();

            entity.Property(e => e.IdPlayers).HasColumnName("Id_Players");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RankPlayer)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RankPlayer");
            entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.IdTeams).HasName("PK__Teams__BA144B35EB16B07F");

            entity.HasIndex(e => e.Name, "UQ__Teams__737584F63DA0668A").IsUnique();

            entity.HasIndex(e => e.Tag, "UQ__Teams__C4516413C3FF4937").IsUnique();

            entity.Property(e => e.IdTeams).HasColumnName("Id_Teams");
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tag)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Captain).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CaptainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teams_Captain");
        });

        modelBuilder.Entity<TeamPlayer>(entity =>
        {
            entity.HasKey(e => new { e.PlayerId, e.TeamId }).HasName("PK__TeamPlay__CB6DDAB180571EB5");

            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Player).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__TeamPlaye__Playe__46E78A0C");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK__TeamPlaye__TeamI__47DBAE45");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

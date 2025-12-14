using Itzin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itzin.Infrastructure.Data;

public class ItzinDbContext : DbContext
{
    public ItzinDbContext(DbContextOptions<ItzinDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Hexagram> Hexagrams { get; set; }
    public DbSet<Consultation> Consultations { get; set; }
    public DbSet<HexagramRuDescription> HexagramRuDescriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.PreferredLanguage).HasMaxLength(10);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Timezone).HasMaxLength(100);
        });

        modelBuilder.Entity<Hexagram>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Number).IsUnique();
            entity.Property(e => e.Number).IsRequired();
            entity.Property(e => e.ChineseName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Pinyin).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EnglishName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.RussianName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Binary).IsRequired().HasMaxLength(6);
            entity.Property(e => e.Unicode).IsRequired().HasMaxLength(10);
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired();
            entity.Property(e => e.TossResults).IsRequired().HasMaxLength(50);
            entity.Property(e => e.QuestionLanguage).HasMaxLength(10);
            entity.Property(e => e.AdditionalChangingHexagrams).HasMaxLength(200);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Consultations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.PrimaryHexagram)
                .WithMany(h => h.PrimaryConsultations)
                .HasForeignKey(e => e.PrimaryHexagramId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.RelatingHexagram)
                .WithMany(h => h.RelatingConsultations)
                .HasForeignKey(e => e.RelatingHexagramId)
                .OnDelete(DeleteBehavior.Restrict);

            // Advanced Consultation Navigation Properties
            entity.HasOne(e => e.AntiHexagram)
                .WithMany()
                .HasForeignKey(e => e.AntiHexagramId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ChangingHexagram)
                .WithMany()
                .HasForeignKey(e => e.ChangingHexagramId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HexagramRuDescription>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.HexagramId).IsUnique();
            entity.Property(e => e.Short).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ImageRow).HasMaxLength(500);
            entity.Property(e => e.Symbol).HasMaxLength(100);
            
            entity.HasOne(e => e.Hexagram)
                .WithOne(h => h.RuDescription)
                .HasForeignKey<HexagramRuDescription>(e => e.HexagramId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

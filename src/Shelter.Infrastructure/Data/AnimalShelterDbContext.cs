using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Shelter.Infrastructure.Data;

public partial class AnimalShelterDbContext : DbContext
{
    public AnimalShelterDbContext()
    {
    }

    public AnimalShelterDbContext(DbContextOptions<AnimalShelterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalHealthRecord> AnimalHealthRecords { get; set; }

    public virtual DbSet<AnimalMedium> AnimalMedia { get; set; }

    public virtual DbSet<AnimalTreatment> AnimalTreatments { get; set; }

    public virtual DbSet<Shelter> Shelters { get; set; }

    public virtual DbSet<ShelterZone> ShelterZones { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    public virtual DbSet<VolunteerShift> VolunteerShifts { get; set; }

    public virtual DbSet<VolunteerShiftAssignment> VolunteerShiftAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Shelter;User Id=sa;Password=PaSSW0RD;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07E44F8BBC");

            entity.ToTable("Animal");

            entity.Property(e => e.AdmissionDate).HasColumnType("datetime");
            entity.Property(e => e.AdoptionDate).HasColumnType("datetime");
            entity.Property(e => e.HealthStatus).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Species).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UbicationName).HasMaxLength(50);

            entity.HasOne(d => d.Shelter).WithMany(p => p.Animals)
                .HasForeignKey(d => d.ShelterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Animal__ShelterI__5EBF139D");
        });

        modelBuilder.Entity<AnimalHealthRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AnimalHe__3214EC076A304884");

            entity.ToTable("AnimalHealthRecord");

            entity.Property(e => e.Diagnosis).HasMaxLength(255);
            entity.Property(e => e.Medication).HasMaxLength(255);
            entity.Property(e => e.Treatment).HasMaxLength(255);
            entity.Property(e => e.VeterinarianName).HasMaxLength(100);
            entity.Property(e => e.VisitDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Animal).WithMany(p => p.AnimalHealthRecords)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalHealthRecord_Animal");
        });

        modelBuilder.Entity<AnimalMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AnimalMe__3214EC0792B51C40");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.MediaType).HasMaxLength(20);
            entity.Property(e => e.MediaUrl).HasMaxLength(255);
            entity.Property(e => e.UploadedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Animal).WithMany(p => p.AnimalMedia)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalMedia_Animal");
        });

        modelBuilder.Entity<AnimalTreatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC074F3D5BCD");

            entity.ToTable("AnimalTreatment");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TreatmentType).HasMaxLength(100);

            entity.HasOne(d => d.Animal).WithMany(p => p.AnimalTreatments)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AnimalTreatment_Animal");
        });

        modelBuilder.Entity<Shelter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shelter__3214EC076F25ED26");

            entity.ToTable("Shelter");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<ShelterZone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShelterZ__3214EC07EFF20C3D");

            entity.ToTable("ShelterZone");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Shelter).WithMany(p => p.ShelterZones)
                .HasForeignKey(d => d.ShelterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShelterZone_Shelter");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC0729C110B9");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07406753A9");

            entity.ToTable("Volunteer");

            entity.HasIndex(e => e.NormalizedUserName, "UQ__tmp_ms_x__54E8BE221103BA58").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__tmp_ms_x__A9D10534DF32E7F2").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__tmp_ms_x__C9F28456E40C050F").IsUnique();

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Volunteers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Volunteer_Role");
        });

        modelBuilder.Entity<VolunteerShift>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voluntee__3214EC0755695692");

            entity.ToTable("VolunteerShift");

            entity.Property(e => e.ShiftDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ShiftType).HasMaxLength(20);
            entity.Property(e => e.ZoneStatus).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.VolunteerShifts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerShift_CreatedBy");

            entity.HasOne(d => d.ShelterZone).WithMany(p => p.VolunteerShifts)
                .HasForeignKey(d => d.ShelterZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerShift_ShelterZone");
        });

        modelBuilder.Entity<VolunteerShiftAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voluntee__3214EC07C70573FB");

            entity.ToTable("VolunteerShiftAssignment");

            entity.HasOne(d => d.Volunteer).WithMany(p => p.VolunteerShiftAssignments)
                .HasForeignKey(d => d.VolunteerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerShiftAssignment_Volunteer");

            entity.HasOne(d => d.VolunteerShift).WithMany(p => p.VolunteerShiftAssignments)
                .HasForeignKey(d => d.VolunteerShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VolunteerShiftAssignment_Shift");
        });
#pragma warning disable S3251 // Implementations should be provided for "partial" methods
        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
#pragma warning restore S3251 // Implementations should be provided for "partial" methods
}

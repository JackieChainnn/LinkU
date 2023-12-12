using LinkU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Areas.Identity.Data;

// because multiple user-groups inherit from ApplicationUser, 
// By default, Entity Framework will create the one table for ApplicationUser and add a Discriminator column
// to distinguish between the different types of users. 
public partial class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
{

    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<JobOpening> JobOpenings { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SupportRequest> SupportRequests { get; set; }

    public virtual DbSet<SupportResponse> SupportResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(ApplicantStatus.Active);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hr).WithMany(p => p.Applicants);

            entity.HasMany(d => d.Skills).WithMany(p => p.Applicants)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicantSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne<Applicant>().WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("ApplicantId", "SkillId");
                            j.ToTable("ApplicantSkill");
                            j.IndexerProperty<string>("ApplicantId").HasColumnName("ApplicantID");
                            j.IndexerProperty<string>("SkillId").HasColumnName("SkillID");
                        });
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.JobOpening).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees);
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.Property(e => e.TimeStamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Owner).WithMany(p => p.Feedbacks).IsRequired();
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.Property(e => e.Status).HasDefaultValue(InterviewStatus.Pending);

            entity.HasOne(d => d.Application).WithMany(p => p.Interviews)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Feedback).WithMany(p => p.Interviews);

            entity.HasOne(d => d.Interviewer).WithMany(p => p.Interviews)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<JobOpening>(entity =>
        {
            entity.Property(e => e.OpenDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(JobOpeningStatus.Open);

            entity.HasOne(d => d.Department).WithMany(p => p.JobOpenings);

            entity.HasOne(d => d.Owner).WithMany(p => p.JobOpenings);

            entity.HasMany(d => d.Skills).WithMany(p => p.JobOpenings)
                .UsingEntity<Dictionary<string, object>>(
                    "JobOpeningSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<JobOpening>().WithMany()
                        .HasForeignKey("JobOpeningId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("JobOpeningId", "SkillId");
                        j.ToTable("JobOpeningSkill");
                        j.IndexerProperty<string>("JobOpeningId").HasColumnName("JobOpeningID");
                        j.IndexerProperty<string>("SkillId").HasColumnName("SkillID");
                    });
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasOne(d => d.Department).WithMany(p => p.Skills);
        });

        modelBuilder.Entity<SupportRequest>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(SupportRequestStatus.Open);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.SupportRequests)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SupportResponse>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.HasOne(d => d.Agent).WithMany(p => p.SupportResponses)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Request).WithMany(p => p.SupportResponses)
                .IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

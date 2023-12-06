using LinkU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkU.Areas.Identity.Data;

// Change the primary key type to Guid instead of NVARCHAR
public partial class AppIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{

    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<Applicant> Applicants { get; set; } = null!;

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
        // modelBuilder.HasDefaultSchema("nodbo");
        modelBuilder.Entity<Applicant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applican__3214EC07575BE777");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(ApplicantStatus.Active);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hr).WithMany(p => p.Applicants).HasConstraintName("FK_AspNetUsers (dbo) Applicant.HRId");

            entity.HasMany(d => d.Skills).WithMany(p => p.Applicants)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicantSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ApplicantSkill.SkillID"),
                    l => l.HasOne<Applicant>().WithMany()
                        .HasForeignKey("ApplicantId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ApplicantSkill.ApplicantID"),
                    j =>
                    {
                        j.HasKey("ApplicantId", "SkillId").HasName("PK__Applican__54549856C82C0234");
                        j.ToTable("ApplicantSkill");
                        j.IndexerProperty<string>("ApplicantId").HasColumnName("ApplicantID");
                        j.IndexerProperty<string>("SkillId").HasColumnName("SkillID");
                    });
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC27E522E407");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Applicant).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application.ApplicantID");

            entity.HasOne(d => d.JobOpening).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application.JobOpeningID");
        });




        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC27746E2DEE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC079FF5FCDD");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasConstraintName("FK_AspNetUsers (dbo) Employee.DepartmentId");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC2708FEDEEE");

            entity.Property(e => e.TimeStamp).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Owner).WithMany(p => p.Feedbacks).HasConstraintName("FK_Feedback.OwnerID");
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Intervie__3214EC27D1AE8828");

            entity.Property(e => e.Status).HasDefaultValue(InterviewStatus.Pending);

            entity.HasOne(d => d.Application).WithMany(p => p.Interviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interview.ApplicationID");

            entity.HasOne(d => d.Feedback).WithMany(p => p.Interviews).HasConstraintName("FK_Interview.FeedbackID");

            entity.HasOne(d => d.Interviewer).WithMany(p => p.Interviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Interview.InterviewerID");
        });

        modelBuilder.Entity<JobOpening>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobOpeni__3214EC27FAE7C983");

            entity.Property(e => e.OpenDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(JobOpeningStatus.Open);

            entity.HasOne(d => d.Department).WithMany(p => p.JobOpenings).HasConstraintName("FK_JobOpening.DepartmentID");

            entity.HasOne(d => d.Owner).WithMany(p => p.JobOpenings).HasConstraintName("FK_JobOpening.OwnerID");

            entity.HasMany(d => d.Skills).WithMany(p => p.JobOpenings)
                .UsingEntity<Dictionary<string, object>>(
                    "Requirement",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Requirement.SkillID"),
                    l => l.HasOne<JobOpening>().WithMany()
                        .HasForeignKey("JobOpeningId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Requirement.JobOpeningID"),
                    j =>
                    {
                        j.HasKey("JobOpeningId", "SkillId").HasName("PK__Requirem__93449146BB56B78F");
                        j.ToTable("Requirement");
                        j.IndexerProperty<string>("JobOpeningId").HasColumnName("JobOpeningID");
                        j.IndexerProperty<string>("SkillId").HasColumnName("SkillID");
                    });
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skill__3214EC277DB490BB");

            entity.HasOne(d => d.Department).WithMany(p => p.Skills).HasConstraintName("FK_Skill.DepartmentID");
        });

        modelBuilder.Entity<SupportRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SupportR__3214EC27A3211057");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue(SupportRequestStatus.Open);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.SupportRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupportRequest.CustomerID");
        });

        modelBuilder.Entity<SupportResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SupportR__3214EC27486A54DD");

            entity.HasOne(d => d.Agent).WithMany(p => p.SupportResponses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupportResponse.AgentID");

            entity.HasOne(d => d.Request).WithMany(p => p.SupportResponses).HasConstraintName("FK_SupportResponse.RequestID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

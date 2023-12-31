﻿// <auto-generated />
using System;
using LinkU.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LinkU.Data.Migrations
{
    [DbContext(typeof(AppIdentityDbContext))]
    [Migration("20231209065317_AddResponseAuto")]
    partial class AddResponseAuto
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicantSkill", b =>
                {
                    b.Property<string>("ApplicantId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ApplicantID");

                    b.Property<string>("SkillId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("SkillID");

                    b.HasKey("ApplicantId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("ApplicantSkill", (string)null);
                });

            modelBuilder.Entity("JobOpeningSkill", b =>
                {
                    b.Property<string>("JobOpeningId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SkillId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("JobOpeningId", "SkillId");

                    b.ToTable("JobOpeningSkill");
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateOnly?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("LinkU.Models.Application", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("ApplicantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ApplicantID");

                    b.Property<DateOnly?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("FeedbackId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("FeedbackID");

                    b.Property<string>("JobOpeningId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("JobOpeningID");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobOpeningId");

                    b.HasIndex(new[] { "ApplicantId", "JobOpeningId" }, "unique_application")
                        .IsUnique();

                    b.ToTable("Application");
                });

            modelBuilder.Entity("LinkU.Models.Department", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<DateOnly?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ__Departme__737584F6CAD530FA")
                        .IsUnique();

                    b.ToTable("Department");
                });

            modelBuilder.Entity("LinkU.Models.Feedback", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("FeedbackContent")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Feedback");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OwnerID");

                    b.Property<DateOnly?>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("LinkU.Models.Interview", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("ApplicationId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ApplicationID");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("FeedbackId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("FeedbackID");

                    b.Property<string>("InterviewerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("InterviewerID");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("InterviewerId");

                    b.ToTable("Interview");
                });

            modelBuilder.Entity("LinkU.Models.JobOpening", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<DateOnly?>("CloseDate")
                        .HasColumnType("date");

                    b.Property<string>("DepartmentId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("DepartmentID");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("OpenDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("OwnerId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("OwnerID");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Titile")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TotalOpening")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("OwnerId");

                    b.ToTable("JobOpening");
                });

            modelBuilder.Entity("LinkU.Models.Skill", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("DepartmentId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("DepartmentID");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("LinkU.Models.SupportRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<DateOnly?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("SupportRequest");
                });

            modelBuilder.Entity("LinkU.Models.SupportResponse", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ID");

                    b.Property<string>("AgentId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("AgentID");

                    b.Property<DateOnly?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RequestId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("RequestID");

                    b.Property<string>("Title")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("RequestId");

                    b.ToTable("SupportResponse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Requirement", b =>
                {
                    b.Property<string>("JobOpeningId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("JobOpeningID");

                    b.Property<string>("SkillId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("SkillID");

                    b.HasKey("JobOpeningId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("Requirement", (string)null);
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.Applicant", b =>
                {
                    b.HasBaseType("LinkU.Areas.Identity.Data.ApplicationUser");

                    b.Property<DateOnly?>("Birthday")
                        .HasColumnType("date");

                    b.Property<int?>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<string>("Hrid")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("HRId");

                    b.Property<string>("Resume")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasIndex("Hrid");

                    b.HasDiscriminator().HasValue("Applicant");
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.Employee", b =>
                {
                    b.HasBaseType("LinkU.Areas.Identity.Data.ApplicationUser");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("DepartmentId");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("ApplicantSkill", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.Applicant", null)
                        .WithMany()
                        .HasForeignKey("ApplicantId")
                        .IsRequired();

                    b.HasOne("LinkU.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .IsRequired();
                });

            modelBuilder.Entity("LinkU.Models.Application", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.Applicant", "Applicant")
                        .WithMany("Applications")
                        .HasForeignKey("ApplicantId")
                        .IsRequired();

                    b.HasOne("LinkU.Models.JobOpening", "JobOpening")
                        .WithMany("Applications")
                        .HasForeignKey("JobOpeningId")
                        .IsRequired();

                    b.Navigation("Applicant");

                    b.Navigation("JobOpening");
                });

            modelBuilder.Entity("LinkU.Models.Feedback", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.Employee", "Owner")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("LinkU.Models.Interview", b =>
                {
                    b.HasOne("LinkU.Models.Application", "Application")
                        .WithMany("Interviews")
                        .HasForeignKey("ApplicationId")
                        .IsRequired();

                    b.HasOne("LinkU.Models.Feedback", "Feedback")
                        .WithMany("Interviews")
                        .HasForeignKey("FeedbackId");

                    b.HasOne("LinkU.Areas.Identity.Data.Employee", "Interviewer")
                        .WithMany("Interviews")
                        .HasForeignKey("InterviewerId")
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("Feedback");

                    b.Navigation("Interviewer");
                });

            modelBuilder.Entity("LinkU.Models.JobOpening", b =>
                {
                    b.HasOne("LinkU.Models.Department", "Department")
                        .WithMany("JobOpenings")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("LinkU.Areas.Identity.Data.Employee", "Owner")
                        .WithMany("JobOpenings")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Department");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("LinkU.Models.Skill", b =>
                {
                    b.HasOne("LinkU.Models.Department", "Department")
                        .WithMany("Skills")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("LinkU.Models.SupportRequest", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.Applicant", "Customer")
                        .WithMany("SupportRequests")
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LinkU.Models.SupportResponse", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.Employee", "Agent")
                        .WithMany("SupportResponses")
                        .HasForeignKey("AgentId")
                        .IsRequired();

                    b.HasOne("LinkU.Models.SupportRequest", "Request")
                        .WithMany("SupportResponses")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinkU.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Requirement", b =>
                {
                    b.HasOne("LinkU.Models.JobOpening", null)
                        .WithMany()
                        .HasForeignKey("JobOpeningId")
                        .IsRequired();

                    b.HasOne("LinkU.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .IsRequired();
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.Applicant", b =>
                {
                    b.HasOne("LinkU.Areas.Identity.Data.Employee", "Hr")
                        .WithMany("Applicants")
                        .HasForeignKey("Hrid");

                    b.Navigation("Hr");
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.Employee", b =>
                {
                    b.HasOne("LinkU.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("LinkU.Models.Application", b =>
                {
                    b.Navigation("Interviews");
                });

            modelBuilder.Entity("LinkU.Models.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("JobOpenings");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("LinkU.Models.Feedback", b =>
                {
                    b.Navigation("Interviews");
                });

            modelBuilder.Entity("LinkU.Models.JobOpening", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("LinkU.Models.SupportRequest", b =>
                {
                    b.Navigation("SupportResponses");
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.Applicant", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("SupportRequests");
                });

            modelBuilder.Entity("LinkU.Areas.Identity.Data.Employee", b =>
                {
                    b.Navigation("Applicants");

                    b.Navigation("Feedbacks");

                    b.Navigation("Interviews");

                    b.Navigation("JobOpenings");

                    b.Navigation("SupportResponses");
                });
#pragma warning restore 612, 618
        }
    }
}

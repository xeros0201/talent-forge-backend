﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFBackend.Data;

#nullable disable

namespace TFBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TFBackend.Models.BBProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Active")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TFBackend.Models.CalendarProjectStaff", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayStatus")
                        .HasColumnType("int");

                    b.Property<bool>("IsHoliday")
                        .HasColumnType("bit");

                    b.HasKey("ProjectId", "StaffId", "Date");

                    b.HasIndex("StaffId");

                    b.ToTable("CalendarProjectStaff");
                });

            modelBuilder.Entity("TFBackend.Models.Cert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CertCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CertCategoryId");

                    b.ToTable("Certs");
                });

            modelBuilder.Entity("TFBackend.Models.CertCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CertCategories");
                });

            modelBuilder.Entity("TFBackend.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Active")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientSince")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastUpdated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StreetNo")
                        .HasColumnType("int");

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("TFBackend.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TFBackend.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TFBackend.Models.ProjectSkill", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("ProjectSkills");
                });

            modelBuilder.Entity("TFBackend.Models.ProjectStaff", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("ProjectStaff");
                });

            modelBuilder.Entity("TFBackend.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 2002,
                            Name = "Staff"
                        },
                        new
                        {
                            Id = 1001,
                            Name = "Manager"
                        });
                });

            modelBuilder.Entity("TFBackend.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("TFBackend.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Available")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvailableDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("TFBackend.Models.StaffCert", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("CertId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AcquiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CertLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InterNationalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssuingOrganisation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RenewalDate")
                        .HasColumnType("datetime2");

                    b.HasKey("StaffId", "CertId");

                    b.HasIndex("CertId");

                    b.ToTable("StaffCerts");
                });

            modelBuilder.Entity("TFBackend.Models.StaffSkills", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("StaffId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("StaffSkills");
                });

            modelBuilder.Entity("TFBackend.Models.BBProject", b =>
                {
                    b.HasOne("TFBackend.Models.Client", "client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientId");

                    b.HasOne("TFBackend.Models.Department", "Department")
                        .WithMany("Projects")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("TFBackend.Models.Location", "Location")
                        .WithMany("Projects")
                        .HasForeignKey("LocationId");

                    b.HasOne("TFBackend.Models.Staff", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Department");

                    b.Navigation("Location");

                    b.Navigation("Manager");

                    b.Navigation("client");
                });

            modelBuilder.Entity("TFBackend.Models.CalendarProjectStaff", b =>
                {
                    b.HasOne("TFBackend.Models.BBProject", "Project")
                        .WithMany("CalendarProjectStaff")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFBackend.Models.Staff", "Staff")
                        .WithMany("CalendarProjectStaff")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TFBackend.Models.Cert", b =>
                {
                    b.HasOne("TFBackend.Models.CertCategory", "CertCategory")
                        .WithMany("Certs")
                        .HasForeignKey("CertCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CertCategory");
                });

            modelBuilder.Entity("TFBackend.Models.ProjectSkill", b =>
                {
                    b.HasOne("TFBackend.Models.BBProject", "Project")
                        .WithMany("ProjectSkill")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFBackend.Models.Skill", "Skill")
                        .WithMany("ProjectSkill")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("TFBackend.Models.ProjectStaff", b =>
                {
                    b.HasOne("TFBackend.Models.BBProject", "Project")
                        .WithMany("ProjectStaff")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFBackend.Models.Staff", "Staff")
                        .WithMany("ProjectStaff")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TFBackend.Models.Staff", b =>
                {
                    b.HasOne("TFBackend.Models.Role", "Role")
                        .WithMany("Staffs")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TFBackend.Models.StaffCert", b =>
                {
                    b.HasOne("TFBackend.Models.Cert", "Cert")
                        .WithMany("Staffs")
                        .HasForeignKey("CertId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFBackend.Models.Staff", "Staff")
                        .WithMany("StaffCerts")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cert");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TFBackend.Models.StaffSkills", b =>
                {
                    b.HasOne("TFBackend.Models.Skill", "Skill")
                        .WithMany("StaffSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFBackend.Models.Staff", "Staff")
                        .WithMany("StaffSkills")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TFBackend.Models.BBProject", b =>
                {
                    b.Navigation("CalendarProjectStaff");

                    b.Navigation("ProjectSkill");

                    b.Navigation("ProjectStaff");
                });

            modelBuilder.Entity("TFBackend.Models.Cert", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("TFBackend.Models.CertCategory", b =>
                {
                    b.Navigation("Certs");
                });

            modelBuilder.Entity("TFBackend.Models.Client", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("TFBackend.Models.Department", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("TFBackend.Models.Location", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("TFBackend.Models.Role", b =>
                {
                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("TFBackend.Models.Skill", b =>
                {
                    b.Navigation("ProjectSkill");

                    b.Navigation("StaffSkills");
                });

            modelBuilder.Entity("TFBackend.Models.Staff", b =>
                {
                    b.Navigation("CalendarProjectStaff");

                    b.Navigation("ProjectStaff");

                    b.Navigation("Projects");

                    b.Navigation("StaffCerts");

                    b.Navigation("StaffSkills");
                });
#pragma warning restore 612, 618
        }
    }
}

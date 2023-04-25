﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFBackend.Data;

#nullable disable

namespace TFBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230424042419_Add-username")]
    partial class Addusername
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Projects");
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

                    b.Property<string>("LastUpdated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalProjects")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
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

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Staff");
                });

            modelBuilder.Entity("TFBackend.Models.StaffClient", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.HasKey("StaffId", "ClientId");

                    b.HasIndex("ClientId");

                    b.ToTable("StaffClients");
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

                    b.Navigation("Department");

                    b.Navigation("Location");

                    b.Navigation("client");
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

            modelBuilder.Entity("TFBackend.Models.StaffClient", b =>
                {
                    b.HasOne("TFBackend.Models.Client", "Client")
                        .WithMany("StaffClients")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TFBackend.Models.Staff", "Staff")
                        .WithMany("StaffClients")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

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
                    b.Navigation("ProjectSkill");

                    b.Navigation("ProjectStaff");
                });

            modelBuilder.Entity("TFBackend.Models.Client", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("StaffClients");
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
                    b.Navigation("ProjectStaff");

                    b.Navigation("StaffClients");

                    b.Navigation("StaffSkills");
                });
#pragma warning restore 612, 618
        }
    }
}

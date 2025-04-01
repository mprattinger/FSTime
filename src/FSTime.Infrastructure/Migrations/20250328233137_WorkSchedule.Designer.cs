﻿// <auto-generated />
using System;
using FSTime.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    [DbContext(typeof(FSTimeDbContext))]
    [Migration("20250328233137_WorkSchedule")]
    partial class WorkSchedule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FSTime.Domain.CompanyAggregate.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("FSTime.Domain.EmployeeAggregate.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmployeeCode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsHead")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<Guid?>("SupervisorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WorkplanId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FSTime.Domain.TenantAggregate.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsLicensed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("FSTime.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean");

                    b.Property<string>("VerifyToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("VerifyTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FSTime.Domain.WorkScheduleAggregate.WorkSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<double?>("Friday")
                        .HasColumnType("double precision");

                    b.Property<double?>("Monday")
                        .HasColumnType("double precision");

                    b.Property<double?>("Saturday")
                        .HasColumnType("double precision");

                    b.Property<double?>("Sunday")
                        .HasColumnType("double precision");

                    b.Property<double?>("Thursday")
                        .HasColumnType("double precision");

                    b.Property<double?>("Tuesday")
                        .HasColumnType("double precision");

                    b.Property<double?>("Wednesday")
                        .HasColumnType("double precision");

                    b.Property<int?>("WeekWorkDays")
                        .HasColumnType("integer");

                    b.Property<double?>("WeekWorkTime")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("WorkSchedules");
                });

            modelBuilder.Entity("FSTime.Domain.CompanyAggregate.Company", b =>
                {
                    b.HasOne("FSTime.Domain.TenantAggregate.Tenant", "Tenant")
                        .WithMany("Companies")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("FSTime.Domain.EmployeeAggregate.Employee", b =>
                {
                    b.HasOne("FSTime.Domain.CompanyAggregate.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FSTime.Domain.EmployeeAggregate.Employee", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupervisorId");

                    b.Navigation("Company");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("FSTime.Domain.TenantAggregate.Tenant", b =>
                {
                    b.OwnsMany("FSTime.Domain.Common.ValueObjects.TenantRole", "Users", b1 =>
                        {
                            b1.Property<Guid>("TenantId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("RoleName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("TenantId", "UserId");

                            b1.ToTable("TenantRoles", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TenantId");
                        });

                    b.Navigation("Users");
                });

            modelBuilder.Entity("FSTime.Domain.UserAggregate.User", b =>
                {
                    b.HasOne("FSTime.Domain.EmployeeAggregate.Employee", "Employee")
                        .WithOne("User")
                        .HasForeignKey("FSTime.Domain.UserAggregate.User", "EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FSTime.Domain.WorkScheduleAggregate.WorkSchedule", b =>
                {
                    b.HasOne("FSTime.Domain.CompanyAggregate.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("FSTime.Domain.EmployeeAggregate.Employee", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("FSTime.Domain.TenantAggregate.Tenant", b =>
                {
                    b.Navigation("Companies");
                });
#pragma warning restore 612, 618
        }
    }
}

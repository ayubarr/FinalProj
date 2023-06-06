﻿// <auto-generated />
using System;
using FinalProj.DAL.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinalProj.DAL.Migrations.PgDb
{
    [DbContext(typeof(PgDbContext))]
    partial class PgDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Logs.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("EventId")
                        .HasColumnType("integer");

                    b.Property<string>("EventName")
                        .HasColumnType("text");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("text");

                    b.Property<string>("ExceptionSource")
                        .HasColumnType("text");

                    b.Property<string>("ExceptionStackTrace")
                        .HasColumnType("text");

                    b.Property<string>("LogLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<int?>("ThreadId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.WorkTeams.TechnicalTeamWorker", b =>
                {
                    b.Property<string>("TechnicalTeamId")
                        .HasColumnType("text");

                    b.Property<Guid?>("WorkerId")
                        .HasColumnType("uuid");

                    b.HasKey("TechnicalTeamId", "WorkerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("TechnicalTeamWorker");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.WorkTeams.Worker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("HireTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<string>("Salary")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TechTeamId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.EcoBox", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("LocationId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TemplateId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("WearDegree")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TemplateId");

                    b.ToTable("EcoBoxes");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.EcoBoxTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Capacity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaterialType")
                        .HasColumnType("integer");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SupplierId")
                        .HasColumnType("uuid");

                    b.Property<int>("TrashType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("EcoBoxTemplates");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.SupplierCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApartmentNumber")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaterialPrice")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SuppliersCompanies");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApartmentNumber")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.RecyclingPlant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApartmentNumber")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Income")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RecyclingPlants");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BoxQuantity")
                        .HasColumnType("integer");

                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CompletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("OperatorId")
                        .HasColumnType("text");

                    b.Property<Guid?>("PlantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RequestCreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("integer");

                    b.Property<int>("RequestType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ReviewId")
                        .HasColumnType("uuid");

                    b.Property<bool>("StatusClientInfo")
                        .HasColumnType("boolean");

                    b.Property<bool>("StatusTeamInfo")
                        .HasColumnType("boolean");

                    b.Property<string>("TechTeamId")
                        .HasColumnType("text");

                    b.Property<int>("WorkType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.HasIndex("OperatorId");

                    b.HasIndex("PlantId");

                    b.HasIndex("ReviewId")
                        .IsUnique();

                    b.HasIndex("TechTeamId")
                        .IsUnique();

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.RequestStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("NewStatus")
                        .HasColumnType("integer");

                    b.Property<int>("PreviousStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("RequestId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestStatusHistories");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Evaluation")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RequestCreatedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReviewText")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.Users.Client", b =>
                {
                    b.HasBaseType("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.Users.SupportOperator", b =>
                {
                    b.HasBaseType("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.ToTable("AspNetUsers", t =>
                        {
                            t.Property("UserType")
                                .HasColumnName("SupportOperator_UserType");
                        });

                    b.HasDiscriminator().HasValue("SupportOperator");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.Users.TechTeam", b =>
                {
                    b.HasBaseType("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser");

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("WorkerId")
                        .HasColumnType("uuid");

                    b.ToTable("AspNetUsers", t =>
                        {
                            t.Property("UserType")
                                .HasColumnName("TechTeam_UserType");
                        });

                    b.HasDiscriminator().HasValue("TechTeam");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.WorkTeams.TechnicalTeamWorker", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Entities.Persons.Users.TechTeam", "TechnicalTeam")
                        .WithMany("Workers")
                        .HasForeignKey("TechnicalTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProj.Domain.Models.Entities.Persons.WorkTeams.Worker", "Worker")
                        .WithMany("TechnicalTeams")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TechnicalTeam");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.EcoBox", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Location", "Location")
                        .WithMany("EcoBoxes")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.EcoBoxTemplate", "Template")
                        .WithMany("EcoBoxes")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.EcoBoxTemplate", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.SupplierCompany", "SupplierCompany")
                        .WithMany("EcoBoxTemplates")
                        .HasForeignKey("SupplierId");

                    b.Navigation("SupplierCompany");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Entities.Persons.Users.Client", "Client")
                        .WithMany("Requests")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Location", "Location")
                        .WithOne("Request")
                        .HasForeignKey("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", "LocationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FinalProj.Domain.Models.Entities.Persons.Users.SupportOperator", "SupportOperator")
                        .WithMany("Requests")
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.RecyclingPlant", "RecyclingPlant")
                        .WithMany("Requests")
                        .HasForeignKey("PlantId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Review", "Review")
                        .WithOne("Request")
                        .HasForeignKey("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", "ReviewId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FinalProj.Domain.Models.Entities.Persons.Users.TechTeam", "TechnicalTeam")
                        .WithOne("Request")
                        .HasForeignKey("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", "TechTeamId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Client");

                    b.Navigation("Location");

                    b.Navigation("RecyclingPlant");

                    b.Navigation("Review");

                    b.Navigation("SupportOperator");

                    b.Navigation("TechnicalTeam");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.RequestStatusHistory", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", "Request")
                        .WithMany("StatusHistory")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                    b.HasOne("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser", null)
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

                    b.HasOne("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FinalProj.Domain.Models.Abstractions.BaseUsers.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.WorkTeams.Worker", b =>
                {
                    b.Navigation("TechnicalTeams");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.EcoBoxTemplate", b =>
                {
                    b.Navigation("EcoBoxes");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo.SupplierCompany", b =>
                {
                    b.Navigation("EcoBoxTemplates");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Location", b =>
                {
                    b.Navigation("EcoBoxes");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.RecyclingPlant", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Request", b =>
                {
                    b.Navigation("StatusHistory");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Requests.RequestsInfo.Review", b =>
                {
                    b.Navigation("Request");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.Users.Client", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.Users.SupportOperator", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("FinalProj.Domain.Models.Entities.Persons.Users.TechTeam", b =>
                {
                    b.Navigation("Request");

                    b.Navigation("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using DAL_B3.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL_B3.Migrations
{
    [DbContext(typeof(BaiTap2_DbContext))]
    [Migration("20230314082420_133")]
    partial class _133
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL_B3.DomainClass.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("CreatedPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("GroupCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GroupId");

                    b.HasIndex("GroupCode")
                        .IsUnique();

                    b.ToTable("Group");
                });

            modelBuilder.Entity("DAL_B3.DomainClass.Role", b =>
                {
                    b.Property<Guid?>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("RoleId");

                    b.HasIndex("RoleCode")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DAL_B3.DomainClass.User", b =>
                {
                    b.Property<Guid?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newid())");

                    b.Property<string>("CreatedPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("DAL_B3.DomainClass.User", b =>
                {
                    b.HasOne("DAL_B3.DomainClass.Role", "RoleId_Navigation")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("RoleId_Navigation");
                });

            modelBuilder.Entity("DAL_B3.DomainClass.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
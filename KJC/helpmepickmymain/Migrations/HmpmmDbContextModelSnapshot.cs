﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using helpmepickmymain.Database;

#nullable disable

namespace helpmepickmymain.Migrations
{
    [DbContext(typeof(HmpmmDbContext))]
    partial class HmpmmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RaceWowClass", b =>
                {
                    b.Property<Guid>("RacesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WowClassesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RacesId", "WowClassesId");

                    b.HasIndex("WowClassesId");

                    b.ToTable("RaceWowClass");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Faction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Factions");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Race", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FactionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FactionID");

                    b.ToTable("Races");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("WowClassId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WowClassId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Spec", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WowClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WowheadLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("WowClassId");

                    b.ToTable("Specs");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.WowClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WowClasses");
                });

            modelBuilder.Entity("RaceWowClass", b =>
                {
                    b.HasOne("helpmepickmymain.Models.Domain.Race", null)
                        .WithMany()
                        .HasForeignKey("RacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("helpmepickmymain.Models.Domain.WowClass", null)
                        .WithMany()
                        .HasForeignKey("WowClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Race", b =>
                {
                    b.HasOne("helpmepickmymain.Models.Domain.Faction", "Faction")
                        .WithMany("Races")
                        .HasForeignKey("FactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faction");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Role", b =>
                {
                    b.HasOne("helpmepickmymain.Models.Domain.WowClass", null)
                        .WithMany("Roles")
                        .HasForeignKey("WowClassId");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Spec", b =>
                {
                    b.HasOne("helpmepickmymain.Models.Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("helpmepickmymain.Models.Domain.WowClass", "WowClass")
                        .WithMany("Specs")
                        .HasForeignKey("WowClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("WowClass");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.Faction", b =>
                {
                    b.Navigation("Races");
                });

            modelBuilder.Entity("helpmepickmymain.Models.Domain.WowClass", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Specs");
                });
#pragma warning restore 612, 618
        }
    }
}

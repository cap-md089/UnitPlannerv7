﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnitPlanner.Apis.Main.Data;

#nullable disable

namespace UnitPlanner.Apis.Main.Migrations
{
    [DbContext(typeof(UnitPlannerDbContext))]
    partial class UnitPlannerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("UnitPlanner.Apis.Main.Models.CalendarEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UnitId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("UnitPlanner.Apis.Main.Models.Unit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Units", (string)null);
                });

            modelBuilder.Entity("UnitPlanner.Apis.Main.Models.CalendarEvent", b =>
                {
                    b.HasOne("UnitPlanner.Apis.Main.Models.Unit", "Unit")
                        .WithMany("Events")
                        .HasForeignKey("UnitId");

                    b.OwnsOne("UnitPlanner.Apis.Main.Models.MemberReference", "Author", b1 =>
                        {
                            b1.Property<int>("CalendarEventId")
                                .HasColumnType("int");

                            b1.Property<int>("MemberId")
                                .HasColumnType("int");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("CalendarEventId");

                            b1.ToTable("Events");

                            b1.WithOwner()
                                .HasForeignKey("CalendarEventId");
                        });

                    b.Navigation("Author")
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("UnitPlanner.Apis.Main.Models.Unit", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
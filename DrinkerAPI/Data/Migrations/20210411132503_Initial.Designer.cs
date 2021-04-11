﻿// <auto-generated />
using DrinkerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DrinkerAPI.Data.Migrations
{
    [DbContext(typeof(CoctailContext))]
    [Migration("20210411132503_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DrinkerAPI.Models.Coctail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Alcoholic")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Glass")
                        .HasColumnType("TEXT");

                    b.Property<string>("Instructions")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coctails");
                });

            modelBuilder.Entity("DrinkerAPI.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CoctailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Measure")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CoctailId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DrinkerAPI.Models.Ingredient", b =>
                {
                    b.HasOne("DrinkerAPI.Models.Coctail", "Coctail")
                        .WithMany("Ingradients")
                        .HasForeignKey("CoctailId");

                    b.Navigation("Coctail");
                });

            modelBuilder.Entity("DrinkerAPI.Models.Coctail", b =>
                {
                    b.Navigation("Ingradients");
                });
#pragma warning restore 612, 618
        }
    }
}

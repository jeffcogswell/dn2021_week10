﻿// <auto-generated />
using FavoritesDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FavoritesDemo.Migrations
{
    [DbContext(typeof(ArtContext))]
    [Migration("20220204200913_ArtDB6")]
    partial class ArtDB6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FavoritesDemo.Models.ArtMiniDetails", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("artist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("thumbnail_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("ArtMiniDetails");
                });

            modelBuilder.Entity("FavoritesDemo.Models.Favorite", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("artwork_id")
                        .HasColumnType("int");

                    b.Property<string>("mynotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("UserFavorites");
                });
#pragma warning restore 612, 618
        }
    }
}

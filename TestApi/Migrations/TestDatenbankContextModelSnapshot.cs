﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TestApi.ORMapper.Models;

namespace TestApi.Migrations
{
    [DbContext(typeof(TestDatenbankContext))]
    partial class TestDatenbankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestApi.ORMapper.Models.Book", b =>
                {
                    b.Property<string>("ArticleNumber")
                        .HasMaxLength(50);

                    b.Property<bool>("IsLoaned");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ArticleNumber");

                    b.ToTable("Books");
                });
#pragma warning restore 612, 618
        }
    }
}

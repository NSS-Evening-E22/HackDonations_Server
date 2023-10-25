﻿// <auto-generated />
using HackDonations_Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HackDonations_Server.Migrations
{
    [DbContext(typeof(HackDonationsDbContext))]
    partial class HackDonationsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HackDonations_Server.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "UNICEF works in over 190 countries and territories to save children's lives, to defend their rights, and to help them fulfill their potential, from early childhood through adolescence.",
                            ImageUrl = "https://th.bing.com/th/id/R.7b5717ff47e176e9ac2a5580df80d6db?rik=N4afUMyWSF696Q&pid=ImgRaw&r=0",
                            Title = "UNICEF",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "Doctors Without Borders provides medical care to those affected by conflict, epidemics, disasters, or exclusion from healthcare. They are an international medical humanitarian organization that delivers emergency aid to people in need.",
                            ImageUrl = "https://4.bp.blogspot.com/-IJAS3gWYhCE/V8SZeGbKD8I/AAAAAAAAQFI/SyJeu-SzCpkh8R1NzePSysZ3Kni5oDtbQCLcB/s1600/msf_dual_english_cmyk_0.jpg",
                            Title = "Doctors Without Borders",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("HackDonations_Server.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Health and Medical"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Environmental and Conservation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Child Welfare and Development"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Social Welfare and Poverty Alleviation"
                        });
                });

            modelBuilder.Entity("HackDonations_Server.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Uid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "The greatest person you'll ever meet.",
                            Email = "riley@email.com",
                            ImageUrl = "https://th.bing.com/th/id/R.f08431063da214d8c07452cca215447f?rik=7gKQvCXgiLVQXw&pid=ImgRaw&r=0",
                            Name = "Riley Tullis",
                            PhoneNumber = "123-456-7890",
                            Uid = ""
                        },
                        new
                        {
                            Id = 2,
                            Bio = "Eh, he's ok.",
                            Email = "jovanni@email.com",
                            ImageUrl = "https://th.bing.com/th/id/R.e733fb390ae9a3c28ca2389bd2466be7?rik=tGXnQ7Yf6T1kBQ&pid=ImgRaw&r=0",
                            Name = "Jovanni Feliz",
                            PhoneNumber = "098-765-4321",
                            Uid = ""
                        });
                });

            modelBuilder.Entity("OrganizationTag", b =>
                {
                    b.Property<int>("OrganizationListId")
                        .HasColumnType("integer");

                    b.Property<int>("TagListId")
                        .HasColumnType("integer");

                    b.HasKey("OrganizationListId", "TagListId");

                    b.HasIndex("TagListId");

                    b.ToTable("OrganizationTag");
                });

            modelBuilder.Entity("OrganizationTag", b =>
                {
                    b.HasOne("HackDonations_Server.Models.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackDonations_Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
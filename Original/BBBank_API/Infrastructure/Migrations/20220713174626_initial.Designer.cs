﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(BBBankContext))]
    [Migration("20220713174626_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountStatus")
                        .HasColumnType("int");

                    b.Property<string>("AccountTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            AccountNumber = "0001-1001",
                            AccountStatus = 1,
                            AccountTitle = "Raas Masood",
                            CurrentBalance = 3500m,
                            UserId = "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3"
                        });
                });

            modelBuilder.Entity("Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = "d9a5c15d-89ec-4f30-967a-964af3485651",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 3000m,
                            TransactionDate = new DateTime(2022, 7, 12, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8160),
                            TransactionType = 0
                        },
                        new
                        {
                            Id = "6a56bd7c-7104-4992-98e3-8bc66cc405d3",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = -500m,
                            TransactionDate = new DateTime(2021, 7, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8187),
                            TransactionType = 1
                        },
                        new
                        {
                            Id = "1bda4dae-ca2d-44f2-8a82-a35b4bbd51da",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 1000m,
                            TransactionDate = new DateTime(2020, 7, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8192),
                            TransactionType = 0
                        },
                        new
                        {
                            Id = "54a25060-8dfa-48be-adad-e18fbae51b52",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 500m,
                            TransactionDate = new DateTime(2022, 4, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8206),
                            TransactionType = 0
                        },
                        new
                        {
                            Id = "0fc256f8-193e-4a9d-a9de-a99b2fe8985c",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = -200m,
                            TransactionDate = new DateTime(2022, 3, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8211),
                            TransactionType = 1
                        },
                        new
                        {
                            Id = "ec2295e6-2d8c-4d75-8cd4-87c5a4cb464a",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 500m,
                            TransactionDate = new DateTime(2022, 2, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8215),
                            TransactionType = 0
                        },
                        new
                        {
                            Id = "30a44bf9-2b64-47d3-941c-f5fb0673255b",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 200m,
                            TransactionDate = new DateTime(2022, 1, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8219),
                            TransactionType = 0
                        },
                        new
                        {
                            Id = "b4596834-cedd-43c6-82fa-c90906d4779c",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = -300m,
                            TransactionDate = new DateTime(2021, 12, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8222),
                            TransactionType = 1
                        },
                        new
                        {
                            Id = "89e82f7c-c947-473a-a938-0e166ece430a",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = -100m,
                            TransactionDate = new DateTime(2021, 11, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8226),
                            TransactionType = 1
                        },
                        new
                        {
                            Id = "3de866d0-a520-4a3c-a8b8-cae109b956be",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 200m,
                            TransactionDate = new DateTime(2021, 10, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8230),
                            TransactionType = 0
                        },
                        new
                        {
                            Id = "d0ccf4c1-94da-4652-b537-2adb46886ed3",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = -500m,
                            TransactionDate = new DateTime(2021, 9, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8234),
                            TransactionType = 1
                        },
                        new
                        {
                            Id = "c94bc558-4c28-43da-8174-05cfbeee028a",
                            AccountId = "37846734-172e-4149-8cec-6f43d1eb3f60",
                            TransactionAmount = 900m,
                            TransactionDate = new DateTime(2021, 8, 13, 22, 46, 25, 906, DateTimeKind.Local).AddTicks(8241),
                            TransactionType = 0
                        });
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3",
                            Email = "rassmasood@hotmail.com",
                            FirstName = "Raas",
                            LastName = "Masood",
                            ProfilePicUrl = "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg"
                        });
                });

            modelBuilder.Entity("Entities.Account", b =>
                {
                    b.HasOne("Entities.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("Entities.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Transaction", b =>
                {
                    b.HasOne("Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Navigation("Account")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

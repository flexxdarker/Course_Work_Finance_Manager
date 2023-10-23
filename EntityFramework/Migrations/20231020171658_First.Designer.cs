﻿// <auto-generated />
using EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(FinanceManagerDbContext))]
    [Migration("20231020171658_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityFramework.Entities.Acount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Profit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Acounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "qwerty",
                            Password = "qwerty-1",
                            Profit = 0m
                        });
                });

            modelBuilder.Entity("EntityFramework.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AcountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Summ")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AcountId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AcountId = 1,
                            Name = "Food",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 2,
                            AcountId = 1,
                            Name = "Cafe",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 3,
                            AcountId = 1,
                            Name = "Entertainment",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 4,
                            AcountId = 1,
                            Name = "Transport",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 5,
                            AcountId = 1,
                            Name = "Health",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 6,
                            AcountId = 1,
                            Name = "Pet",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 7,
                            AcountId = 1,
                            Name = "Family",
                            Summ = 0m
                        },
                        new
                        {
                            Id = 8,
                            AcountId = 1,
                            Name = "Clothes",
                            Summ = 0m
                        });
                });

            modelBuilder.Entity("EntityFramework.Entities.Cost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Costs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "Cheese",
                            Price = 85m
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 8,
                            Name = "Jacket",
                            Price = 2000m
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 5,
                            Name = "Vitamins",
                            Price = 500m
                        });
                });

            modelBuilder.Entity("EntityFramework.Entities.Incoum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AcountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AcountId");

                    b.ToTable("Incoums");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AcountId = 1,
                            Name = "Salary",
                            Price = 10200m
                        },
                        new
                        {
                            Id = 2,
                            AcountId = 1,
                            Name = "Hospital",
                            Price = 3000m
                        });
                });

            modelBuilder.Entity("EntityFramework.Entities.Category", b =>
                {
                    b.HasOne("EntityFramework.Entities.Acount", "Acount")
                        .WithMany("Categories")
                        .HasForeignKey("AcountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acount");
                });

            modelBuilder.Entity("EntityFramework.Entities.Cost", b =>
                {
                    b.HasOne("EntityFramework.Entities.Category", "Category")
                        .WithMany("Costs")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EntityFramework.Entities.Incoum", b =>
                {
                    b.HasOne("EntityFramework.Entities.Acount", "Acount")
                        .WithMany("Incoums")
                        .HasForeignKey("AcountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acount");
                });

            modelBuilder.Entity("EntityFramework.Entities.Acount", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Incoums");
                });

            modelBuilder.Entity("EntityFramework.Entities.Category", b =>
                {
                    b.Navigation("Costs");
                });
#pragma warning restore 612, 618
        }
    }
}

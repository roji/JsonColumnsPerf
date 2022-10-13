﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Migrations
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.2.22472.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("_id"));

                    b.Property<string>("Business")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(450)")
                        .HasComputedColumnSql("JSON_VALUE(Contact,'$.Address.Country')");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InformalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JoinedCountryId")
                        .HasColumnType("int");

                    b.HasKey("_id");

                    b.HasIndex("Country");

                    b.HasIndex("JoinedCountryId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.HasOne("Country", "JoinedCountry")
                        .WithMany("Customers")
                        .HasForeignKey("JoinedCountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("Customer_id")
                                .HasColumnType("int");

                            b1.Property<bool>("IsActive")
                                .HasColumnType("bit");

                            b1.HasKey("Customer_id");

                            b1.ToTable("Customers");

                            b1.ToJson("Contact");

                            b1.WithOwner()
                                .HasForeignKey("Customer_id");

                            b1.OwnsOne("Address", "Address", b2 =>
                                {
                                    b2.Property<int>("ContactCustomer_id")
                                        .HasColumnType("int");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Country")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Postcode")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("ContactCustomer_id");

                                    b2.ToTable("Customers");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactCustomer_id");
                                });

                            b1.OwnsMany("PhoneNumber", "PhoneNumbers", b2 =>
                                {
                                    b2.Property<int>("ContactCustomer_id")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    b2.Property<int>("CountryCode")
                                        .HasColumnType("int");

                                    b2.Property<string>("Number")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<string>("Type")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.HasKey("ContactCustomer_id", "Id");

                                    b2.ToTable("Customers");

                                    b2.WithOwner()
                                        .HasForeignKey("ContactCustomer_id");
                                });

                            b1.Navigation("Address")
                                .IsRequired();

                            b1.Navigation("PhoneNumbers");
                        });

                    b.Navigation("Contact")
                        .IsRequired();

                    b.Navigation("JoinedCountry");
                });

            modelBuilder.Entity("Country", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
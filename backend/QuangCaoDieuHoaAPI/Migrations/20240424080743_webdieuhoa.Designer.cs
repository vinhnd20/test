﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuangCaoDieuHoaAPI.Database;

#nullable disable

namespace QuangCaoDieuHoaAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240424080743_webdieuhoa")]
    partial class webdieuhoa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("QuangCaoDieuHoaAPI.Models.DonHang", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("diaChiGiaoHang")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ghiChuThem")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("hoTenKH")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ngayDatHang")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("soDienThoaiKH")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("soLuongSanPham")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("tblDonHangs");
                });

            modelBuilder.Entity("QuangCaoDieuHoaAPI.Models.FormKhachHang", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("diaChi")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("hoTen")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.Property<string>("soDienThoai")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("tblQuanLies");
                });

            modelBuilder.Entity("QuangCaoDieuHoaAPI.Models.Hang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("moTa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("tenHang")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("tenHang")
                        .IsUnique();

                    b.ToTable("tblHangs");
                });

            modelBuilder.Entity("QuangCaoDieuHoaAPI.Models.SanPham", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HangID")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("congSuat")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("giaTien")
                        .HasColumnType("float");

                    b.Property<string>("kieuDang")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("moTa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("model")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("soChieu")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("soLuong")
                        .HasColumnType("int");

                    b.Property<string>("ten")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("tblSanPhams");
                });

            modelBuilder.Entity("QuangCaoDieuHoaAPI.Models.TrangThai", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("trangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("trangThai")
                        .IsUnique();

                    b.ToTable("trangThais");
                });

            modelBuilder.Entity("QuangCaoDieuHoaAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OTP")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "",
                            Avatar = "",
                            Birthday = new DateTime(2001, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreatedDate = new DateTime(2024, 4, 24, 8, 7, 43, 615, DateTimeKind.Utc).AddTicks(1611),
                            Email = "Test@gmail.com",
                            FirstName = "Test",
                            LastName = "Test",
                            OTP = "",
                            Password = "0CBC6611F5540BD0809A388DC95A615B",
                            Position = 0,
                            RoleName = "Admin",
                            Status = 1,
                            UserName = "Test"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

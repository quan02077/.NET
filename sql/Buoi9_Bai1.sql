create database Buoi9_Bai1
use Buoi9_Bai1

create table Khoa
(
	MaKhoa char(10) primary key,
	TenKhoa nvarchar(20)
)

create table Lop
(
	MaLop char(10) primary key,
	MaKhoa char(10),
	Constraint FK_Lop_Khoa foreign key(MaKhoa) references Khoa(MaKhoa)
)

create table SinhVien
(
	MaSinhVien Char(10) primary key,
	HoTen nvarchar(30),
	GioiTinh nvarchar(3),
	NgaySinh date,
	MaLop char(10),
	constraint FK_SinhVien foreign key(MaLop) references Lop(MaLop)
)

create table MonHoc
(
	MaMH char(10) primary key,
	TenMH nvarchar(30),
	SoTC tinyint,
	TinhChat nvarchar(20),
)

create table KetQua
(
	MaSinhVien char(10),
	MaMH char(10),
	NamHoc varchar(20),
	HocKy char(3),
	Diem float,
	constraint PK_KetQua primary key(MaSinhVien, MaMH, NamHoc, HocKy),
	constraint FK_KetQua_SinhVien foreign key(MaSinhVien) references SinhVien(MaSinhVien),
	constraint FK_KetQua_MonHoc foreign key(MaMH) references MonHoc(MaMH),
)

insert into Khoa
values(
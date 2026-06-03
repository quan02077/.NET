CREATE DATABASE QL_Karaoke_KT2;
GO

USE QL_Karaoke_KT2;
GO

CREATE TABLE LOAIPHONG (
    MaNhom VARCHAR(10) PRIMARY KEY,
    TenNhom NVARCHAR(100) NOT NULL
);

CREATE TABLE PHONG (
    MaPhong VARCHAR(10) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    SucChua INT,
    GiaPhong FLOAT,
    KieuPhong INT, 
    MaNhom VARCHAR(10),
    CONSTRAINT FK_Phong_LoaiPhong FOREIGN KEY (MaNhom) REFERENCES LOAIPHONG(MaNhom)
);

CREATE TABLE KHACHHANG (
    MaKhachHang VARCHAR(10) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    SoDT VARCHAR(15)
);

CREATE TABLE PHUTHU (
    MaPhuThu VARCHAR(10) PRIMARY KEY,
    TenPhuThu NVARCHAR(100) NOT NULL,
    GiaPT FLOAT
);

CREATE TABLE DATPHONG (
    MaDatPhong INT IDENTITY(1,1) PRIMARY KEY, 
    MaPh VARCHAR(10),
    MaKH VARCHAR(10),
    NgayDat DATETIME,
    NgayTra DATETIME,
    CONSTRAINT FK_DatPhong_Phong FOREIGN KEY (MaPh) REFERENCES PHONG(MaPhong),
    CONSTRAINT FK_DatPhong_KhachHang FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKhachHang)
);

CREATE TABLE CHITIETDATPHONG (
    MaCT VARCHAR(10) PRIMARY KEY,
    MaDP INT, 
    MaPT VARCHAR(10),
    SL INT,
    CONSTRAINT FK_CTDP_DatPhong FOREIGN KEY (MaDP) REFERENCES DATPHONG(MaDatPhong),
    CONSTRAINT FK_CTDP_PhuThu FOREIGN KEY (MaPT) REFERENCES PHUTHU(MaPhuThu)
);
GO

INSERT INTO LOAIPHONG (MaNhom, TenNhom) VALUES
('T1', N'Tầng 1'),
('T2', N'Tầng 2'),
('T3', N'Tầng 3');

INSERT INTO PHONG (MaPhong, TenPhong, SucChua, GiaPhong, KieuPhong, MaNhom) VALUES
('P01', N'Phòng VIP 1', 10, 150000, 2, 'T1'),
('P02', N'Phòng Thường 1', 8, 100000, 1, 'T1'),
('P03', N'Phòng VIP 2', 20, 300000, 2, 'T1'),
('P04', N'Phòng Thường 2', 20, 200000, 1, 'T1'),

('P05', N'Phòng VIP 3', 12, 180000, 2, 'T2'),
('P06', N'Phòng Thường 3', 10, 120000, 1, 'T2'),
('P07', N'Phòng Gia Đình', 15, 220000, 2, 'T2'),

('P08', N'Phòng VIP 4', 25, 350000, 2, 'T3'),
('P09', N'Phòng Thường 4', 12, 130000, 1, 'T3'),
('P10', N'Phòng Nhóm Bạn', 18, 250000, 2, 'T3');

INSERT INTO KHACHHANG (MaKhachHang, TenKH, SoDT) VALUES
('KH01', N'Nguyễn Văn A', '0901234567'),
('KH02', N'Trần Thị B', '0912345678'),
('KH03', N'Lê Văn C', '0923456789'),
('KH04', N'Phạm Thị D', '0934567890'),
('KH05', N'Hoàng Minh E', '0945678901');

INSERT INTO PHUTHU (MaPhuThu, TenPhuThu, GiaPT) VALUES
('PT01', N'Bia Tiger', 20000),
('PT02', N'Nước ngọt', 15000),
('PT03', N'Trái cây dĩa', 150000),
('PT04', N'Khăn lạnh', 5000),
('PT05', N'Nước suối', 10000),
('PT06', N'Bim bim', 12000),
('PT07', N'Đậu phộng', 25000);

INSERT INTO DATPHONG (MaPh, MaKH, NgayDat, NgayTra) VALUES
('P01', 'KH01', '2026-05-26 13:00:00', '2026-05-26 15:00:00'),
('P02', 'KH02', '2026-05-26 19:00:00', '2026-05-26 22:00:00'),
('P03', 'KH03', '2026-05-27 18:00:00', '2026-05-27 21:00:00'),
('P05', 'KH04', '2026-05-28 14:00:00', '2026-05-28 17:00:00'),
('P08', 'KH05', '2026-05-29 20:00:00', '2026-05-29 23:00:00');

INSERT INTO CHITIETDATPHONG (MaCT, MaDP, MaPT, SL) VALUES
('CT01', 1, 'PT01', 5),
('CT02', 1, 'PT02', 2),
('CT03', 2, 'PT03', 1),
('CT04', 2, 'PT04', 4),
('CT05', 3, 'PT01', 8),
('CT06', 3, 'PT05', 6),
('CT07', 4, 'PT06', 3),
('CT08', 4, 'PT02', 5),
('CT09', 5, 'PT03', 2),
('CT10', 5, 'PT07', 2);
GO
CREATE TABLE NguoiDung (
    MaNguoiDung NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    QueQuan NVARCHAR(100),
    NgaySinh DATE,
    Email NVARCHAR(100) UNIQUE,
    SoDienThoai NVARCHAR(20),
    CCCD NVARCHAR(20) UNIQUE,
    DiaChiThuongTru NVARCHAR(200),
    DiaChiTamTru NVARCHAR(200)
);

CREATE TABLE Nganh (
    MaNganh NVARCHAR(10) PRIMARY KEY,
    TenNganh NVARCHAR(100) NOT NULL
);

CREATE TABLE Khoa (
    MaKhoa NVARCHAR(10) PRIMARY KEY,
    TenKhoa NVARCHAR(100) NOT NULL
);

CREATE TABLE ChuongTrinhDaoTao (
    MaCTDT NVARCHAR(10) PRIMARY KEY,
    TenCTDT NVARCHAR(100),
    MaNganh NVARCHAR(10) UNIQUE,
    FOREIGN KEY (MaNganh) REFERENCES Nganh(MaNganh)
);

CREATE TABLE HocPhi (
    MaHocPhi NVARCHAR(10) PRIMARY KEY,
    GiaTheoTin DECIMAL(10,2)
);

CREATE TABLE MonHoc (
    MaMonHoc NVARCHAR(10) PRIMARY KEY,
    TenMonHoc NVARCHAR(100),
    SoTinChi INT,
    LoaiMon NVARCHAR(50),
    MaHocPhi NVARCHAR(10) NOT NULL,
    FOREIGN KEY (MaHocPhi) REFERENCES HocPhi(MaHocPhi)
);

CREATE TABLE SinhVien (
    MaNguoiDung NVARCHAR(10) PRIMARY KEY,
    MaNganh NVARCHAR(10) NOT NULL,
    NienKhoa NVARCHAR(20),
    TinhTrangHoc NVARCHAR(50),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaNganh) REFERENCES Nganh(MaNganh)
);

CREATE TABLE GiangVien (
    MaNguoiDung NVARCHAR(10) PRIMARY KEY,
    ChuyenMon NVARCHAR(100),
    MaKhoa NVARCHAR(10) NOT NULL,
    TinhTrangCongTac NVARCHAR(50),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung),
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);

CREATE TABLE CanBo (
    MaNguoiDung NVARCHAR(10) PRIMARY KEY,
    TinhTrangCongTac NVARCHAR(50),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);

CREATE TABLE TaiKhoan (
    MaNguoiDung NVARCHAR(10) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(200) NOT NULL,
    TrangThai NVARCHAR(20),
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);

CREATE TABLE Khoa_Nganh (
    MaKhoa NVARCHAR(10),
    MaNganh NVARCHAR(10),
    PRIMARY KEY (MaKhoa, MaNganh),
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa),
    FOREIGN KEY (MaNganh) REFERENCES Nganh(MaNganh)
);

CREATE TABLE HocKy (
    MaHocKy NVARCHAR(10) PRIMARY KEY,
    NamHoc NVARCHAR(20) NOT NULL,     
    HocKySo INT NOT NULL,             
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    CONSTRAINT UQ_HocKy UNIQUE (NamHoc, HocKySo)
);

CREATE TABLE PhongHoc (
    MaPhong NVARCHAR(10) PRIMARY KEY,   
    Tang INT NOT NULL,                  
    Khu NVARCHAR(50) NOT NULL,          
    LoaiPhong NVARCHAR(50) NOT NULL     
);


CREATE TABLE LopHocPhan (
    MaLopHocPhan NVARCHAR(10) PRIMARY KEY,
    MaMonHoc NVARCHAR(10) NOT NULL,
    MaGiangVien NVARCHAR(10) NOT NULL,
    MaHocKy NVARCHAR(10) NOT NULL,
    NgayHoc DATE,
    GioBatDau TIME,
    GioKetThuc TIME,
    PhongHoc NVARCHAR(10),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaGiangVien) REFERENCES GiangVien(MaNguoiDung),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy),
	FOREIGN KEY (PhongHoc) REFERENCES PhongHoc(MaPhong)
);

CREATE TABLE DangKyHocPhan (
    MaSinhVien NVARCHAR(10),
    MaLopHocPhan NVARCHAR(10),
    TrangThai NVARCHAR(20),
    PRIMARY KEY (MaSinhVien, MaLopHocPhan),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung),
    FOREIGN KEY (MaLopHocPhan) REFERENCES LopHocPhan(MaLopHocPhan)
);

CREATE TABLE DiemRenLuyen (
    MaSinhVien NVARCHAR(10),
    MaHocKy NVARCHAR(10),
    Diem INT,
    XepLoai NVARCHAR(20),
    GhiChu NVARCHAR(200),
    PRIMARY KEY (MaSinhVien, MaHocKy),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE HoatDongCTXH (
    MaHoatDong NVARCHAR(10) PRIMARY KEY,
    TenHoatDong NVARCHAR(200),
    Diem INT,                 -- Có thể âm hoặc dương
    NgayToChuc DATE,
    GhiChu NVARCHAR(200)
);

CREATE TABLE SinhVien_HoatDongCTXH (
    MaSinhVien NVARCHAR(10),
    MaHoatDong NVARCHAR(10),
    NgayThamGia DATE,
    DiemThucTe INT NULL,      -- Nếu NULL → dùng điểm mặc định của hoạt động
    GhiChu NVARCHAR(200),
    PRIMARY KEY (MaSinhVien, MaHoatDong),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung),
    FOREIGN KEY (MaHoatDong) REFERENCES HoatDongCTXH(MaHoatDong)
);

CREATE TABLE DiemCongTacXaHoi (
    MaSinhVien NVARCHAR(10) PRIMARY KEY,
    TongDiem INT,
    GhiChu NVARCHAR(200),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung)
);

CREATE VIEW v_TongDiemCTXH AS
SELECT 
    sv.MaNguoiDung AS MaSinhVien,
    SUM(ISNULL(svhd.DiemThucTe, hd.Diem)) AS TongDiem
FROM SinhVien_HoatDongCTXH svhd
JOIN HoatDongCTXH hd ON svhd.MaHoatDong = hd.MaHoatDong
JOIN SinhVien sv ON sv.MaNguoiDung = svhd.MaSinhVien
GROUP BY sv.MaNguoiDung;
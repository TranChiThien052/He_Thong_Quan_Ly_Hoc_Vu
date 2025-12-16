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

CREATE TABLE KhoaNganh (
    MaKhoa NVARCHAR(10),
    MaNganh NVARCHAR(10),
    PRIMARY KEY (MaKhoa, MaNganh),
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa),
    FOREIGN KEY (MaNganh) REFERENCES Nganh(MaNganh)
);

CREATE TABLE HocKy (
    MaHocKy NVARCHAR(20) PRIMARY KEY,
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
    SoPhong INT,
    LoaiPhong NVARCHAR(50) NOT NULL     
);


CREATE TABLE LopHocPhan (
    MaLopHocPhan NVARCHAR(10) PRIMARY KEY,
    MaMonHoc NVARCHAR(10) NOT NULL,
    MaGiangVien NVARCHAR(10) NOT NULL,
    MaHocKy NVARCHAR(20) NOT NULL,
    CaHoc INT,
    NgayHoc NVARCHAR(10), 
    PhongHoc NVARCHAR(10),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc),
    FOREIGN KEY (MaGiangVien) REFERENCES GiangVien(MaNguoiDung),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy),
	FOREIGN KEY (PhongHoc) REFERENCES PhongHoc(MaPhong)
);

CREATE TABLE DangKyHocPhan (
    MaSinhVien NVARCHAR(10),
    MaLopHocPhan NVARCHAR(10),
    PRIMARY KEY (MaSinhVien, MaLopHocPhan),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung),
    FOREIGN KEY (MaLopHocPhan) REFERENCES LopHocPhan(MaLopHocPhan)
);

CREATE TABLE DiemRenLuyen (
    MaSinhVien NVARCHAR(10),
    MaHocKy NVARCHAR(20),
    Diem INT,
    PRIMARY KEY (MaSinhVien, MaHocKy),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung),
    FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

CREATE TABLE DiemCongTacXaHoi (
    MaSinhVien NVARCHAR(10) PRIMARY KEY,
    TongDiem INT,
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung)
);

CREATE TABLE DiemHocPhan (
    MaSinhVien NVARCHAR(10),
    MaLopHocPhan NVARCHAR(10),
    DiemChuyenCan DECIMAL(5,2) NULL,
    DiemGiuaKy DECIMAL(5,2) NULL,
    DiemCuoiKy DECIMAL(5,2) NULL,
    PRIMARY KEY (MaSinhVien, MaLopHocPhan),
    FOREIGN KEY (MaSinhVien) REFERENCES SinhVien(MaNguoiDung),
    FOREIGN KEY (MaLopHocPhan) REFERENCES LopHocPhan(MaLopHocPhan)
);

CREATE TABLE ChiTietChuongTrinh (
    MaCTDT NVARCHAR(10),
    MaMonHoc NVARCHAR(10),
    HocKy INT,
    PRIMARY KEY (MaCTDT, MaMonHoc),
    FOREIGN KEY (MaCTDT) REFERENCES ChuongTrinhDaoTao(MaCTDT),
    FOREIGN KEY (MaMonHoc) REFERENCES MonHoc(MaMonHoc)
)
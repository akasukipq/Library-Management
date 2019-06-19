﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;
using System.Data;

namespace QuanLyThuVien.BLL
{
    class NhanVienBLL
    {
        private static NhanVienBLL instance;

        public static NhanVienBLL Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhanVienBLL();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public string TaiKhoan;
        private NhanVienBLL() { }

        public List<NhanVienDTO> ShowNhanVien()
        {
            List<NhanVienDTO> data = NhanVienDAL.Instance.LoadNhanVien();

            return data;
        }

        public DataTable ShowChucVuToCombobox()
        {
            DataTable data = NhanVienDAL.Instance.LoadChucVuToComboBox();
            return data;
        }

        public string SaveNhanVien(string MaNV, string TenNV, string ChucVu, string TaiKhoan, string MatKhau)
        {
            //kiểm tra điều kiện
            if (MaNV == "" || TenNV == "" || ChucVu == "" || TaiKhoan == "" || MatKhau == "")
                return "Thêm thất bại! Các trường không được bỏ trống!";
            // lưu xuống CSDL
            int Chucvu = 0;
            switch(ChucVu)
            {
                case "Nhân viên":
                    Chucvu = 0;
                    break;
                case "Quản lý":
                    Chucvu = 1;
                    break;
                default:
                    break;
            }
            if (NhanVienDAL.Instance.SaveNhanVien(MaNV, TenNV, Chucvu, TaiKhoan, MatKhau))
                return "Thêm thành công!";
            else
                return "Thêm thất bại! Có lỗi xảy ra.";

        }

        public string UpdateNhanVien(string MaNV, string TenNV, string ChucVu, string TaiKhoan, string MatKhau)
        {
            //kiểm tra điều kiện
            if (MaNV == "" || TenNV == "" || ChucVu == "" || TaiKhoan == "" || MatKhau == "")
                return "Sửa thất bại! Các trường không được bỏ trống!";
            // lưu xuống CSDL
            int Chucvu = 0;
            switch (ChucVu)
            {
                case "Nhân viên":
                    Chucvu = 0;
                    break;
                case "Quản lý":
                    Chucvu = 1;
                    break;
                default:
                    break;
            }
            if (NhanVienDAL.Instance.UpdateNhanVien(MaNV, TenNV, Chucvu, TaiKhoan, MatKhau))
                return "Sửa thành công!";
            else
                return "Sửa thất bại! Có lỗi xảy ra.";

        }

        public string DeleteNhanVien(string MaNV)
        {
            if (NhanVienDAL.Instance.DeleteNhanVien(MaNV))
                return "Xóa thành công!";
            else
                return "Xóa thất bại! Có lỗi xảy ra.";
        }

        public string CheckLogin(string taikhoan, string matkhau)
        {
            if (taikhoan == "" || matkhau == "")
                return "Tên đăng nhập và mật khẩu không được để trống!";
            return NhanVienDAL.Instance.CheckLogin(taikhoan, matkhau);
        }

        public void SaveTaiKhoan(string taikhoan)
        {
            TaiKhoan = taikhoan;
        }

        public NhanVienDTO ShowCurrentNV()
        {
            if (TaiKhoan != null)
                return NhanVienDAL.Instance.LoadNhanVienByTaikhoan(TaiKhoan);
            else
                return null;
        }

        public string UpdateMatKhau(string manv, string matkhau,string matkhaucu, string matkhaumoi, string rematkhaumoi)
        {
            //check điều kiện đơn giản
            if (matkhaucu == "" || matkhaumoi == "" || rematkhaumoi == "")
                return "Các trường không được để trống!";
            else if (matkhau != matkhaucu)
                return "Mật khẩu không đúng!";
            else if (matkhaumoi != rematkhaumoi)
                return "Mật khẩu nhập lại không khớp!";

            //lưu xuống DB
            if (NhanVienDAL.Instance.UpdateMatKhau(manv, matkhaumoi))
                return "Đã đổi mật khẩu!";
            else
                return "Có lỗi xảy ra, vui lòng thử lại!";
        }
    }
}

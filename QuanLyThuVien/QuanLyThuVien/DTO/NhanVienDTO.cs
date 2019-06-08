using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyThuVien.DTO
{
    class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string MatKhau { get; set; }
        public string TaiKhoan { get; set; }
        public string ChucVu { get; set; }
        public NhanVienDTO(DataRow row)
        {
            MaNV = (string)row["MaNV"];
            TenNV = (string)row["TenNV"];
            MatKhau = (string)row["MatKhau"];
            TaiKhoan = (string)row["TaiKhoan"];
            if(Convert.ToInt32(row["ChucVu"]) == 0) //0: member , 1: admin
                ChucVu = "Nhân viên";
            else if(Convert.ToInt32(row["ChucVu"]) == 1)
                ChucVu = "Quản lý";

        }
    }
}

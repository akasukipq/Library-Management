using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;
using System.Data;

namespace QuanLyThuVien.BLL
{
    class TheLoaiBLL
    {
        private static TheLoaiBLL instance;

        public static TheLoaiBLL Instance
        {
            get
            {
                if (instance == null)
                    instance = new TheLoaiBLL();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private TheLoaiBLL() { }

        public List<TheLoaiDTO> ShowTheLoai()
        {
            List<TheLoaiDTO> data = TheLoaiDAL.Instance.LoadTheLoai();

            return data;
        }

        public DataTable ShowTheLoaiToCombobox()
        {
            DataTable data = TheLoaiDAL.Instance.LoadTheLoaiToComboBox();
            return data;
        }
        public string SaveTheLoai(string MaTL, string TenTL)
        {
            //kiểm tra điều kiện
            if (MaTL == "" || TenTL == "")
                return "Thêm thất bại! Các trường không được bỏ trống!";
            // lưu xuống CSDL

            if (TheLoaiDAL.Instance.SaveTheLoai(MaTL, TenTL))
                return "Thêm thành công!";
            else
                return "Thêm thất bại! Có lỗi xảy ra.";

        }
        public string UpdateTheLoai(string MaTL, string TenTL)
        {
            //kiểm tra điều kiện
            if (MaTL == "" || TenTL == "")
                return "Sửa thất bại! Các trường không được bỏ trống!";
            // lưu xuống CSDL

            if (TheLoaiDAL.Instance.UpdateTheLoai(MaTL, TenTL))
                return "Sửa thành công!";
            else
                return "Sửa thất bại! Có lỗi xảy ra.";

        }

        public string DeleteTheLoai(string MaTL)
        {
            if (TheLoaiDAL.Instance.DeleteTheLoai(MaTL))
                return "Xóa thành công!";
            else
                return "Xóa thất bại! Có lỗi xảy ra.";
        }

        public int CountTL()
        {
            return TheLoaiDAL.Instance.CountTL();
        }
    }
}

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
    class TacGiaBLL
    {
        private static TacGiaBLL instance;

        public static TacGiaBLL Instance
        {
            get
            {
                if (instance == null)
                    instance = new TacGiaBLL();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private TacGiaBLL() { }

        public List<TacGiaDTO> ShowTacGia()
        {
            List<TacGiaDTO> data = TacGiaDAL.Instance.LoadTacGia();

            return data;
        }

        public DataTable ShowTacGiaToCombobox()
        {
            DataTable data = TacGiaDAL.Instance.LoadTacGiaToComboBox();
            return data;
        }
        public string SaveTacGia(string MaTG, string TenTG)
        {
            //kiểm tra điều kiện
            if (MaTG == "" || TenTG == "")
                return "Thêm thất bại! Các trường không được bỏ trống!";
            // lưu xuống CSDL

            if (TacGiaDAL.Instance.SaveTacGia(MaTG, TenTG))
                return "Thêm thành công!";
            else
                return "Thêm thất bại! Có lỗi xảy ra.";

        }
        public string UpdateTacGia(string MaTG, string TenTG)
        {
            //kiểm tra điều kiện
            if (MaTG == "" || TenTG == "")
                return "Sửa thất bại! Các trường không được bỏ trống!";
            // lưu xuống CSDL

            if (TacGiaDAL.Instance.UpdateTacGia(MaTG, TenTG))
                return "Sửa thành công!";
            else
                return "Sửa thất bại! Có lỗi xảy ra.";

        }

        public string DeleteTacGia(string MaTG)
        {
            if (TacGiaDAL.Instance.DeleteTacGia(MaTG))
                return "Xóa thành công!";
            else
                return "Xóa thất bại! Có lỗi xảy ra.";
        }
    }
}

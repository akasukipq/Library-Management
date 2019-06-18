using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.DTO;
using System.Data;

namespace QuanLyThuVien.DAL
{
    class NhanVienDAL : DatabaseAccess
    {
        private static NhanVienDAL instance;

        public static NhanVienDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhanVienDAL();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private NhanVienDAL() { }

        #region Methods
        public List<NhanVienDTO> LoadNhanVien()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();

            DataTable data = DatabaseAcess.Instance.ExcuteQuery("USP_LOADNHANVIEN");

            // lọc qua DTO
            foreach (DataRow row in data.Rows)
            {
                NhanVienDTO nhanvien = new NhanVienDTO(row);
                list.Add(nhanvien);
            }
            return list;
        }

        //Dung lay chuc vu nhan vien len combobox
        public DataTable LoadChucVuToComboBox()
        {
            DataTable data = DatabaseAcess.Instance.ExcuteQuery("USP_LOADCHUCVUNHANVIEN");

            DataTable dt = new DataTable();
            dt.Columns.Add("Chức vụ");
            foreach (DataRow row in data.Rows)
            {
                switch(Convert.ToInt32(row["ChucVu"]))
                {
                    case 0:
                        dt.Rows.Add("Nhân viên");
                        break;
                    case 1:
                        dt.Rows.Add("Quản lý");
                        break;
                    default:
                    break;
                }
            }
            return dt;

        }
        public bool SaveNhanVien(string MaNV, string TenNV, int ChucVu, string TaiKhoan, string MatKhau)
        {
            string query = "USP_SAVENHANVIEN @MaNV , @TenNV , @ChucVu , @TaiKhoan , @MatKhau";

            int ret = DatabaseAcess.Instance.ExecuteNonQuery(query, new object[] { MaNV, TenNV, ChucVu, TaiKhoan, MatKhau });
            if (ret > 0)
                return true;
            else
                return false;
        }
        public bool UpdateNhanVien(string MaNV, string TenNV, int ChucVu, string TaiKhoan, string MatKhau)
        {
            string query = "USP_UPDATENHANVIEN @MaNV , @TenNV , @ChucVu , @TaiKhoan , @MatKhau";
            int ret = DatabaseAcess.Instance.ExecuteNonQuery(query, new object[] { MaNV, TenNV, ChucVu, TaiKhoan, MatKhau });
            if (ret > 0)
                return true;
            else
                return false;
        }

        public bool DeleteNhanVien(string MaNV)
        {
            string query = "delete from NHANVIEN where MaNV ='" + MaNV + "'";
            int ret = DatabaseAcess.Instance.ExecuteNonQuery(query);

            if (ret > 0)
                return true;
            else
                return false;
        }

        public string CheckLogin(string taikhoan,string matkhau)
        {
            int ret = Convert.ToInt32(DatabaseAcess.Instance.ExecuteScalar("USP_CHECKLOGIN @TaiKhoan , @MatKhau", new object[] { taikhoan, matkhau }));
            if (ret > 0)
                return "Đăng nhập thành công!";
            else
                return "Tên tài khoản hoặc mật khẩu không đúng!";

        }

        #endregion
    }
}

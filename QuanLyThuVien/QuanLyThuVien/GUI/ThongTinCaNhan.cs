using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QuanLyThuVien.DTO;
using QuanLyThuVien.BLL;
namespace QuanLyThuVien.GUI
{
    public partial class ThongTinCaNhan : DevExpress.XtraEditors.XtraUserControl
    {
        public ThongTinCaNhan()
        {
            InitializeComponent();
            ShowThongTinCaNhan();
        }

        private void ShowThongTinCaNhan()
        {
            NhanVienDTO nhanvien = NhanVienBLL.Instance.ShowCurrentNV();
            txtMaNV.Text = nhanvien.MaNV;
            txtTenNV.Text = nhanvien.TenNV;
            txtTaiKhoan.Text = nhanvien.TaiKhoan;
            txtMatKhau.Text = nhanvien.MatKhau;
            txtChucVu.Text = nhanvien.ChucVu;

            txtMaNV.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTaiKhoan.ReadOnly = true;
            txtMatKhau.ReadOnly = true;
            txtChucVu.ReadOnly = true;
            txtMatKhau.PasswordChar = '*';

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void txtMatKhauCu_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();
            txtReMatKhauMoi.Clear();
            lbThongBao.Text = "";
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ret = NhanVienBLL.Instance.UpdateMatKhau(txtMaNV.Text, txtMatKhau.Text, txtMatKhauCu.Text, txtMatKhauMoi.Text, txtReMatKhauMoi.Text);

            if (ret == "Đã đổi mật khẩu!")
                MessageBox.Show("Đã đổi mật khẩu!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                lbThongBao.Text = ret;

            txtMatKhauCu.Clear();
            txtMatKhauMoi.Clear();
            txtReMatKhauMoi.Clear();
            lbThongBao.Text = "";
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            ShowThongTinCaNhan();
        }
    }
}

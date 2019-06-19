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
using System.Data.SqlClient;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
using DevExpress;

namespace QuanLyThuVien.GUI
{
    public partial class QLNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        int flag = 0;
        public QLNhanVien()
        {
            InitializeComponent();
            ShowNhanVien();
            cbChucVu.DataSource = NhanVienBLL.Instance.ShowChucVuToCombobox();///_________Note
            cbChucVu.ValueMember = "Chức vụ";
            Lock(true);
            NhanVienDTO nv = NhanVienBLL.Instance.ShowCurrentNV();
            if (nv.ChucVu == "admin")
                btnThem.Enabled = true;
            else
                btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

        }
        public void Lock(bool b)
        {
            if (b)
            {
                btnLuu.Enabled = false;
                txtMaNV.ReadOnly = true;
                txtTenNV.ReadOnly = true;
                cbChucVu.Enabled = false;
                txtTaiKhoan.ReadOnly = true;
                txtMatKhau.ReadOnly = true;
            }
            else
            {
                btnLuu.Enabled = true;
                txtMaNV.ReadOnly = false;
                txtTenNV.ReadOnly = false;
                txtTaiKhoan.ReadOnly = false;
                txtMatKhau.ReadOnly = false;
                cbChucVu.Enabled = true;

            }
        }

        public void ShowNhanVien()
        {
            List<NhanVienDTO> listBooks = NhanVienBLL.Instance.ShowNhanVien();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã nhân viên");
            dt.Columns.Add("Tên nhân viên");
            dt.Columns.Add("Chức vụ");
            dt.Columns.Add("Tài khoản");
            dt.Columns.Add("Mật khẩu");
            foreach (NhanVienDTO book in listBooks)
            {
                dt.Rows.Add(book.MaNV, book.TenNV, book.ChucVu, book.TaiKhoan, book.MatKhau);
            }
            gridNhanVien.DataSource = dt;
            grvNhanVien.ClearSelection();
            BookDetailBinding();
        }

        public void BookDetailBinding()
        {
            if (txtMaNV.DataBindings.Count > 0)
                txtMaNV.DataBindings.RemoveAt(0);
            if (txtTenNV.DataBindings.Count > 0)
                txtTenNV.DataBindings.RemoveAt(0);
            if (txtMatKhau.DataBindings.Count > 0)
                txtMatKhau.DataBindings.RemoveAt(0);
            if (txtTaiKhoan.DataBindings.Count > 0)
                txtTaiKhoan.DataBindings.RemoveAt(0);

            txtMaNV.DataBindings.Add(new Binding("Text", gridNhanVien.DataSource, "Mã nhân viên")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTenNV.DataBindings.Add(new Binding("Text", gridNhanVien.DataSource, "Tên nhân viên")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            txtTaiKhoan.DataBindings.Add(new Binding("Text", gridNhanVien.DataSource, "tài khoản")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtMatKhau.DataBindings.Add(new Binding("Text", gridNhanVien.DataSource, "mật khẩu")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            Lock(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            cbChucVu.Text = "Chọn chức vụ";
            //Lấy mã sách mới nhất

            txtMaNV.Text = Utilities.Instance.NextID("NV", grvNhanVien.GetRowCellValue(grvNhanVien.RowCount - 1, grvNhanVien.Columns[0]).ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                string ret = NhanVienBLL.Instance.SaveNhanVien(txtMaNV.Text, txtTenNV.Text, cbChucVu.SelectedValue.ToString()
                    , txtTaiKhoan.Text, txtMatKhau.Text);
                MessageBox.Show(ret);
                if (ret == "Thêm thành công!")
                    Lock(true);
            }
            else if (flag == 2)
            {
                string ret = NhanVienBLL.Instance.UpdateNhanVien(txtMaNV.Text, txtTenNV.Text, cbChucVu.SelectedValue.ToString()
                    , txtTaiKhoan.Text, txtMatKhau.Text);
                MessageBox.Show(ret);
                if (ret == "Sửa thành công!")
                    Lock(true);
            }
            ShowNhanVien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            Lock(false);
            txtMaNV.ReadOnly = true;
            cbChucVu.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên này?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string ret = NhanVienBLL.Instance.DeleteNhanVien(txtMaNV.Text);
                MessageBox.Show(ret);
                ShowNhanVien();
            }
        }

        private void gridNhanVien_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Text = "Xoá";
            btnXoa.Enabled = true;
            Lock(true);

            //xử lí chức vụ
            string chucvu = grvNhanVien.GetRowCellValue(grvNhanVien.FocusedRowHandle, grvNhanVien.Columns[2]).ToString();
            cbChucVu.SelectedValue = chucvu;
        }
    }
}


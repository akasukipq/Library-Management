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

namespace QuanLyThuVien
{
    public partial class QLTacGia : DevExpress.XtraEditors.XtraUserControl
    {
        private static QLTacGia _instance;

        public static QLTacGia Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QLTacGia();
                return _instance;
            }
        }
        int flag = 0;
        public QLTacGia()
        {
            InitializeComponent();
            ShowTacGia();
            Lock(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

        }
        public void Lock(bool b)
        {
            if (b)
            {
                btnLuu.Enabled = false;
                txtMaTG.ReadOnly = true;
                txtTenTG.ReadOnly = true;

            }
            else
            {
                btnLuu.Enabled = true;
                txtMaTG.ReadOnly = false;
                txtTenTG.ReadOnly = false;

            }
        }
        public void ShowTacGia()
        {
            List<TacGiaDTO> listBooks = TacGiaBLL.Instance.ShowTacGia();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã tác giả");
            dt.Columns.Add("Tên tác giả");
            foreach (TacGiaDTO book in listBooks)
            {
                dt.Rows.Add(book.MaTG, book.TenTG);
            }
            gridTacGia.DataSource = dt;
            grvTacGia.ClearSelection();
            BookDetailBinding();
        }
        public void BookDetailBinding()
        {
            if (txtMaTG.DataBindings.Count > 0)
                txtMaTG.DataBindings.RemoveAt(0);
            if (txtTenTG.DataBindings.Count > 0)
                txtTenTG.DataBindings.RemoveAt(0);
            txtMaTG.DataBindings.Add(new Binding("Text", gridTacGia.DataSource, "Mã tác giả")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTenTG.DataBindings.Add(new Binding("Text", gridTacGia.DataSource, "Tên tác giả")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            Lock(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            btnXoa.Text = "Hủy";
            txtMaTG.Text = "";
            txtTenTG.Text = "";
            //Lấy mã sách mới nhất
            txtMaTG.Text = Utilities.Instance.NextID("TL", grvTacGia.GetRowCellValue(grvTacGia.RowCount - 1, grvTacGia.Columns[0]).ToString());
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                string ret = TacGiaBLL.Instance.SaveTacGia(txtMaTG.Text, txtTenTG.Text);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ret == "Thêm thành công!")
                    Lock(true);
            }
            else if (flag == 2)
            {
                string ret = TacGiaBLL.Instance.UpdateTacGia(txtMaTG.Text, txtTenTG.Text);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ret == "Sửa thành công!")
                    Lock(true);
            }
            ShowTacGia();
            btnXoa.Text = "Xóa";

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            Lock(false);
            txtMaTG.ReadOnly = true;
            btnXoa.Text = "Hủy";
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (btnXoa.Text == "Hủy")
            {
                if (MessageBox.Show("Bạn có muốn huỷ không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnXoa.Text = "Xoá";
                    Lock(false);
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    btnSua.Enabled = false;
                }

            }
            else if (btnXoa.Text == "Xoá")
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa tác giả này?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ret = TacGiaBLL.Instance.DeleteTacGia(txtMaTG.Text);
                    MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowTacGia();
                }
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
            }
        }

        private void gridTacGia_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Text = "Xoá";
            btnXoa.Enabled = true;
            Lock(true);
        }
    }
}

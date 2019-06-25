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
    public partial class QLTheLoai : DevExpress.XtraEditors.XtraUserControl
    {
        private static QLTheLoai _instance;

        public static QLTheLoai Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QLTheLoai();
                return _instance;
            }
        }

        int flag = 0;
        public QLTheLoai()
        {
            InitializeComponent();
            ShowTheLoai();
            Lock(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }


        public void Lock(bool b)
        {
            if (b)
            {
                btnLuu.Enabled = false;
                txtMaTL.ReadOnly = true;
                txtTenTL.ReadOnly = true;

            }
            else
            {
                btnLuu.Enabled = true;
                txtMaTL.ReadOnly = false;
                txtTenTL.ReadOnly = false;

            }
        }
        public void ShowTheLoai()
        {
            List<TheLoaiDTO> listBooks = TheLoaiBLL.Instance.ShowTheLoai();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã thể loại");
            dt.Columns.Add("Tên thể loại");
            foreach (TheLoaiDTO book in listBooks)
            {
                dt.Rows.Add(book.MaTL, book.TenTL);
            }
            gridTheLoai.DataSource = dt;
            grvTheLoai.ClearSelection();
            BookDetailBinding();
        }
        public void BookDetailBinding()
        {
            if (txtMaTL.DataBindings.Count > 0)
                txtMaTL.DataBindings.RemoveAt(0);
            if (txtTenTL.DataBindings.Count > 0)
                txtTenTL.DataBindings.RemoveAt(0);
            txtMaTL.DataBindings.Add(new Binding("Text", gridTheLoai.DataSource, "Mã thể loại")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTenTL.DataBindings.Add(new Binding("Text", gridTheLoai.DataSource, "Tên thể loại")
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
            txtMaTL.Text = "";
            txtTenTL.Text = "";
            //Lấy mã sách mới nhất
            txtMaTL.Text = Utilities.Instance.NextID("TL", grvTheLoai.GetRowCellValue(grvTheLoai.RowCount - 1, grvTheLoai.Columns[0]).ToString());
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                string ret = TheLoaiBLL.Instance.SaveTheLoai(txtMaTL.Text, txtTenTL.Text);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (flag == 2)
            {
                string ret = TheLoaiBLL.Instance.UpdateTheLoai(txtMaTL.Text, txtTenTL.Text);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ShowTheLoai();
            btnXoa.Text = "Xóa";
            Lock(true);
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            Lock(false);
            txtMaTL.ReadOnly = true;
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
                if (MessageBox.Show("Bạn chắc chắn muốn xóa thể loại này?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ret = TheLoaiBLL.Instance.DeleteTheLoai(txtMaTL.Text);
                    MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowTheLoai();
                }
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
            }
        }

        private void gridTheLoai_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Text = "Xoá";
            btnXoa.Enabled = true;
            Lock(true);
        }
    }
}

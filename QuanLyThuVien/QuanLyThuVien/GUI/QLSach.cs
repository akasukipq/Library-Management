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
using System.Drawing.Imaging;

namespace QuanLyThuVien
{
    public partial class QLSach : DevExpress.XtraEditors.XtraUserControl
    {
        private static QLSach _instance;

        public static QLSach Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QLSach();
                return _instance;
            }
        }
        const string PATH = @"bookcover\example.png";

        int flag = 0;
        public QLSach()
        {
            InitializeComponent();
            ShowBooks();
            cbTheLoai.DataSource = TheLoaiBLL.Instance.ShowTheLoaiToCombobox();
            cbTheLoai.DisplayMember = "Tên TL";
            cbTheLoai.ValueMember = "Mã TL";
            cbTacGia.DataSource = TacGiaBLL.Instance.ShowTacGiaToCombobox();
            cbTacGia.DisplayMember = "Tên TG";
            cbTacGia.ValueMember = "Mã TG";
            Lock(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        public void Lock(bool b)
        {
            if(b)
            {
                btnImage.Enabled = false;
                btnLuu.Enabled = false;
                txtMaSach.ReadOnly = true;
                txtTenSach.ReadOnly = true;
                txtNamXB.ReadOnly = true;
                dtNgayNhap.Enabled = false;
                txtNXB.ReadOnly = true;
                cbTacGia.Enabled = false;
                txtTriGia.ReadOnly = true;
                cbTheLoai.Enabled = false;
                rdbTrong.Enabled = false;
                rdbBorrowed.Enabled = false;
            }
            else
            {
                btnImage.Enabled = true;
                btnLuu.Enabled = true;
                txtMaSach.ReadOnly = false;
                txtTenSach.ReadOnly = false;
                txtNamXB.ReadOnly = false;
                dtNgayNhap.Enabled = true;
                txtNXB.ReadOnly = false;
                cbTacGia.Enabled = true;
                txtTriGia.ReadOnly = false;
                cbTheLoai.Enabled = true;
                rdbTrong.Enabled = true;
                rdbBorrowed.Enabled = true;
            }
        }

        public void ShowBooks()
        {
            List<SachDTO> listBooks = SachBLL.Instance.ShowBook();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã sách");
            dt.Columns.Add("Tên sách");
            dt.Columns.Add("Thể loại");
            dt.Columns.Add("Tác giả");
            dt.Columns.Add("Năm XB");
            dt.Columns.Add("Nhà xuất bản");
            dt.Columns.Add("Ngày nhập");
            dt.Columns.Add("Trị giá");
            dt.Columns.Add("Tình trạng");
            dt.Columns.Add("Ảnh bìa");
            foreach (SachDTO book in listBooks)
            {
                dt.Rows.Add(book.MaSach, book.TenSach, book.MaTL.ToString(), book.MaTG, book.NamXB.ToString(), book.NXB, book.NgayNhap.ToString(), book.TriGia.ToString(), book.TinhTrang.ToString(), book.AnhBia);
            }
            gridSach.DataSource = dt;
            grvSach.ClearSelection();
            BookDetailBinding();
        }

        public void BookDetailBinding()
        {
            if (txtMaSach.DataBindings.Count > 0)
                txtMaSach.DataBindings.RemoveAt(0);
            if (txtTenSach.DataBindings.Count > 0)
                txtTenSach.DataBindings.RemoveAt(0);
            if (txtNXB.DataBindings.Count > 0)
                txtNXB.DataBindings.RemoveAt(0);
            if (txtNamXB.DataBindings.Count > 0)
                txtNamXB.DataBindings.RemoveAt(0);
            if (dtNgayNhap.DataBindings.Count > 0)
                dtNgayNhap.DataBindings.RemoveAt(0);
            if (txtTriGia.DataBindings.Count > 0)
                txtTriGia.DataBindings.RemoveAt(0);

            txtMaSach.DataBindings.Add(new Binding("Text", gridSach.DataSource, "Mã sách")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTenSach.DataBindings.Add(new Binding("Text", gridSach.DataSource, "Tên sách")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
          
            txtNXB.DataBindings.Add(new Binding("Text", gridSach.DataSource, "Nhà xuất bản")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            txtNamXB.DataBindings.Add(new Binding("Text", gridSach.DataSource, "Năm XB")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            dtNgayNhap.DataBindings.Add(new Binding("Text", gridSach.DataSource, "Ngày nhập")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTriGia.DataBindings.Add(new Binding("Text", gridSach.DataSource, "Trị giá")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

        }

        private void gridSach_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Text = "Xóa";
            btnXoa.Enabled = true;
            string ret = grvSach.GetRowCellValue(grvSach.FocusedRowHandle, grvSach.Columns[8]).ToString();
            //xử lí cho tình trạng sách
            if (ret == "True")
                rdbTrong.Checked = true;
            else
                rdbBorrowed.Checked = true;

            //xử lí thể loại sách
            string theloai = grvSach.GetRowCellValue(grvSach.FocusedRowHandle, grvSach.Columns[2]).ToString();
            cbTheLoai.SelectedValue = theloai;
            //Xử lý tác giả
            string tacgia = grvSach.GetRowCellValue(grvSach.FocusedRowHandle, grvSach.Columns[3]).ToString();
            cbTacGia.SelectedValue = tacgia;
            //xử lý ảnh bìa
            string path = grvSach.GetRowCellValue(grvSach.FocusedRowHandle, grvSach.Columns[9]).ToString();
            if (path == "Không có ảnh")
                path = PATH;
            ptbAnhBia.Image = new Bitmap(path);
            Lock(true);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            Lock(false);
            btnXoa.Text = "Hủy";
            btnXoa.Enabled = true;
            dtNgayNhap.Value = DateTime.Now;
            btnSua.Enabled = false;
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtNamXB.Text = "";
            txtNXB.Text = "";
            cbTacGia.Text = "Chọn tác giả";
            txtTriGia.Text = "";
            cbTheLoai.Text = "Chọn thể loại";
            rdbTrong.Checked = false;
            rdbBorrowed.Checked = false;

            //Lấy mã sách mới nhất


            txtMaSach.Text = Utilities.Instance.NextID("S",grvSach.GetRowCellValue(grvSach.RowCount - 1, grvSach.Columns[0]).ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                string TinhTrang;
                if (rdbBorrowed.Checked == true)
                    TinhTrang = "False";
                else
                    TinhTrang = "True";
                //lưu file ảnh bìa 
                string path;
                if (ptbAnhBia.Image != null)
                {
                    path = @"bookcover\" + txtMaSach.Text;
                    ptbAnhBia.Image.Save(path, ImageFormat.Png);
                }
                else // không có ảnh bìa
                    path = "";
                string ret = SachBLL.Instance.SaveBook(txtMaSach.Text, txtTenSach.Text, cbTheLoai.SelectedValue.ToString(), cbTacGia.SelectedValue.ToString(), txtNamXB.Text
                    , txtNXB.Text, dtNgayNhap.Value, txtTriGia.Text, TinhTrang, path);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ret == "Thêm thành công!")
                    Lock(true);
            }
            else if(flag == 2)
            {
                string TinhTrang;
                if (rdbBorrowed.Checked == true)
                    TinhTrang = "False";
                else
                    TinhTrang = "True";
                //lưu file ảnh bìa 
                string path;
                if (ptbAnhBia.Image != null)
                {
                    path = @"bookcover\" + txtMaSach.Text;
                    ptbAnhBia.Image.Save(path, ImageFormat.Png);
                }
                else // không có ảnh bìa
                    path = "";
                string ret = SachBLL.Instance.UpdateBook(txtMaSach.Text, txtTenSach.Text, cbTheLoai.SelectedValue.ToString(), cbTacGia.SelectedValue.ToString(), txtNamXB.Text
                    , txtNXB.Text, dtNgayNhap.Value.ToString(), txtTriGia.Text, TinhTrang, path);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (ret == "Sửa thành công!")
                    Lock(true);
            }
            ShowBooks();
            btnXoa.Text = "Xóa";

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            Lock(false);
            btnXoa.Text = "Hủy";
            btnXoa.Enabled = true;
            txtMaSach.ReadOnly = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (btnXoa.Text == "Hủy")
            {
                if (MessageBox.Show("Bạn có muốn huỷ không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Lock(false);
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    btnSua.Enabled = false;
                }

            }
            else if (btnXoa.Text == "Xóa")
            {
                if (rdbBorrowed.Checked == true)
                    MessageBox.Show("Sách đang được mượn. Không thể xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (MessageBox.Show("Bạn chắc chắn muốn xóa quyển sách này?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string ret = SachBLL.Instance.DeleteBook(txtMaSach.Text);
                        MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowBooks();
                    }
                }
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
            }
         
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();

            img.Title = "Vui lòng chọn ảnh bìa sách";
            img.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

            if (img.ShowDialog() == DialogResult.OK)
            {
                ptbAnhBia.Image = new Bitmap(img.FileName);
            }
        }
    }
}

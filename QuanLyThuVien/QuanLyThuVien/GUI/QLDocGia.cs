﻿using System;
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
    public partial class QLDocGia : DevExpress.XtraEditors.XtraUserControl
    {
        private static QLDocGia _instance;

        public static QLDocGia Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QLDocGia();
                return _instance;
            }
        }
        int flag = 0;
        public QLDocGia()
        {
            InitializeComponent();
            ShowDocGia();
            dtNgayHetHan.Enabled = false;
            cbLoaiDG.DataSource = LoaiDocGiaBLL.Instance.ShowLoaiDocGiaToCombobox();///_________Note
            cbLoaiDG.DisplayMember = "Mã LDG";
            cbLoaiDG.ValueMember = "Tên LDG";
            Lock(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public void Lock(bool b)
        {
            if (b)
            {
                btnLuu.Enabled = false;
                txtMaDG.ReadOnly = true;
                txtTenDG.ReadOnly = true;
                txtTongNo.ReadOnly = true;
                dtNgaySinh.Enabled = false;
                txtDiaChi.ReadOnly = true;
                dtNgayLapThe.Enabled = false;
                txtEmail.ReadOnly = true;
                cbLoaiDG.Enabled = false;
                rbHetHan.Enabled = false;
                rbHoatDong.Enabled = false;
            }
            else
            {
                btnLuu.Enabled = true;
                txtMaDG.ReadOnly = false;
                txtTenDG.ReadOnly = false;
                txtTongNo.ReadOnly = false;
                dtNgaySinh.Enabled = true;
                txtDiaChi.ReadOnly = false;
                dtNgayLapThe.Enabled = true;
                txtEmail.ReadOnly = false;
                cbLoaiDG.Enabled = true;
                rbHetHan.Enabled = true;
                rbHoatDong.Enabled = true;
            }
        }
        public void ShowDocGia()
        {
            List<DocGiaDTO> listDocGia = DocGiaBLL.Instance.ShowDocGia();  
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã đọc giả");
            dt.Columns.Add("Tên đọc giả");
            dt.Columns.Add("Loại đọc giả");
            dt.Columns.Add("Ngày sinh");
            dt.Columns.Add("Địa chỉ");
            dt.Columns.Add("Email");
            dt.Columns.Add("Ngày lập thẻ");
            dt.Columns.Add("Ngày hết hạn");
            dt.Columns.Add("Tình trạng thẻ");
            dt.Columns.Add("Tổng nợ");
            foreach (DocGiaDTO docgia in listDocGia)
            {
                if(docgia.TinhTrangThe == true)
                    dt.Rows.Add(docgia.MaDG, docgia.HoTen, docgia.MaLDG.ToString(), docgia.NgaySinh.ToString(), docgia.DiaChi, docgia.Email, docgia.NgayLapThe.ToString(), docgia.NgayHetHan.ToString(), "Hoạt động", docgia.TongNo);
                else
                    dt.Rows.Add(docgia.MaDG, docgia.HoTen, docgia.MaLDG.ToString(), docgia.NgaySinh.ToString(), docgia.DiaChi, docgia.Email, docgia.NgayLapThe.ToString(), docgia.NgayHetHan.ToString(), "Hết hạn", docgia.TongNo);

            }
            gridDocGia.DataSource = dt;
            grvDocGia.ClearSelection();
            DocGiaDetailBinding();
        }

        public void DocGiaDetailBinding()
        {
            if (txtMaDG.DataBindings.Count > 0)
                txtMaDG.DataBindings.RemoveAt(0);
            if (txtTenDG.DataBindings.Count > 0)
                txtTenDG.DataBindings.RemoveAt(0);
            if (txtDiaChi.DataBindings.Count > 0)
                txtDiaChi.DataBindings.RemoveAt(0);
            if (txtEmail.DataBindings.Count > 0)
                txtEmail.DataBindings.RemoveAt(0);
            if (dtNgaySinh.DataBindings.Count > 0)
                dtNgaySinh.DataBindings.RemoveAt(0);
            if (dtNgayLapThe.DataBindings.Count > 0)
                dtNgayLapThe.DataBindings.RemoveAt(0);
            if (txtTongNo.DataBindings.Count > 0)
                txtTongNo.DataBindings.RemoveAt(0);
            if (dtNgayHetHan.DataBindings.Count > 0)
                dtNgayHetHan.DataBindings.RemoveAt(0);
           
            txtMaDG.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Mã đọc giả")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTenDG.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Tên đọc giả")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            dtNgaySinh.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Ngày sinh")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            txtDiaChi.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Địa chỉ")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            txtEmail.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Email")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            dtNgayLapThe.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Ngày lập thẻ")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            txtTongNo.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Tổng nợ")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
            dtNgayHetHan.DataBindings.Add(new Binding("Text", gridDocGia.DataSource, "Ngày hết hạn")
            {
                DataSourceUpdateMode = DataSourceUpdateMode.Never,
                ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });
           
        }

        private void gridDocGia_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Text = "Xoá";
            btnXoa.Enabled = true;
            string ret = grvDocGia.GetRowCellValue(grvDocGia.FocusedRowHandle, grvDocGia.Columns[8]).ToString();
            //xử lí cho tình trạng the
            if (ret == "Hoạt động")
                rbHoatDong.Checked = true;
            else
                rbHetHan.Checked = true;

            //xử lí thể loại sách
            string loaiDG = grvDocGia.GetRowCellValue(grvDocGia.FocusedRowHandle, grvDocGia.Columns[2]).ToString();
            cbLoaiDG.SelectedValue = loaiDG;
            
            
            Lock(true);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            Lock(false);
            dtNgayLapThe.Value = DateTime.Now;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtTenDG.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtTongNo.Text = "0";
            cbLoaiDG.Text = "Chọn loại đọc giả";
            rbHoatDong.Checked = true;
            rbHetHan.Enabled = false;
            btnXoa.Text = "Huỷ";
            dtNgayHetHan.Enabled = false;
            txtMaDG.ReadOnly = true;

            int x = ThamSoBLL.Instance.ThoiHanThe();
            
            dtNgayHetHan.Value = dtNgayLapThe.Value.AddMonths(x);
            
            if(grvDocGia.GetRowCellValue(grvDocGia.RowCount - 1, grvDocGia.Columns[0]) != null)
                txtMaDG.Text = Utilities.Instance.NextID("DG", grvDocGia.GetRowCellValue(grvDocGia.RowCount - 1, grvDocGia.Columns[0]).ToString());
            else
                txtMaDG.Text = Utilities.Instance.NextID("DG", "DG000");

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            Lock(false);
            txtMaDG.ReadOnly = true;
            cbLoaiDG.Enabled = true;
            btnXoa.Text = "Huỷ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (btnXoa.Text == "Huỷ")
            {
                if (MessageBox.Show("Bạn có muốn huỷ không!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    btnXoa.Text = "Xoá";
                    txtTenDG.Text = "";
                    txtDiaChi.Text = "";
                    txtEmail.Text = "";
                    txtTongNo.Text = "0";
                    cbLoaiDG.Text = "Chọn loại đọc giả";
                    Lock(false);
                    btnXoa.Enabled = false;
                    btnLuu.Enabled = false;
                    btnSua.Enabled = false;
                }

            }
            else if (btnXoa.Text == "Xoá")
            {
                //if(rbHoatDong.Checked==true)
                //{
                //    MessageBox.Show("Thẻ đọc giả đang hoạt động không được xoá!", "Thông báo", MessageBoxButtons.YesNo);
                //}

                //else 
                if (MessageBox.Show("Bạn chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ret = DocGiaBLL.Instance.DeleteDocGia(txtMaDG.Text);
                    MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowDocGia();
                }
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                string TinhTrang="True";
               
                string ret = DocGiaBLL.Instance.SaveDocGia(txtMaDG.Text, txtTenDG.Text, cbLoaiDG.Text, dtNgaySinh.Value, txtDiaChi.Text, txtEmail.Text, dtNgayLapThe.Value, dtNgayHetHan.Value, TinhTrang, txtTongNo.Text);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else if (flag == 2)
            {
                string TinhTrang;
                if (rbHoatDong.Checked == true)
                    TinhTrang = "True";
                else
                    TinhTrang = "False";

                string ret = DocGiaBLL.Instance.UpdateDocGia(txtMaDG.Text, txtTenDG.Text, cbLoaiDG.Text, dtNgaySinh.Value, txtDiaChi.Text, txtEmail.Text, dtNgayLapThe.Value, dtNgayHetHan.Value, TinhTrang, txtTongNo.Text);
                MessageBox.Show(ret, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ShowDocGia();
            btnXoa.Text = "Xoá";
            Lock(true);

        }

        private void dtNgayLapThe_ValueChanged(object sender, EventArgs e)
        {
            int x = ThamSoBLL.Instance.ThoiHanThe();

           // dtNgayHetHan.Value = dtNgayLapThe.Value.AddDays(x);
            dtNgayHetHan.Value = dtNgayLapThe.Value.AddMonths(x);

        }


        private void txtTongNo_Click(object sender, EventArgs e)
        {
           
            if (flag==1)
            txtTongNo.Text = "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.BLL;
namespace QuanLyThuVien.LOGIN
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string ret = NhanVienBLL.Instance.CheckLogin(txtTaiKhoan.Text, txtMatKhau.Text);
            if (ret == "Đăng nhập thành công!")
            {
                lbThongBao.Text = "";
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();
                //truyền tài khoản vào NhanVienBLL
                NhanVienBLL.Instance.SaveTaiKhoan(txtTaiKhoan.Text);

                //gọi trang chủ
                frmMain main = new frmMain();
                this.Hide();
                main.LookAndFeel.SkinName = DevExpress.LookAndFeel.SkinStyle.DevExpress;
                main.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                main.LookAndFeel.UseDefaultLookAndFeel = true;
                main.ShowDialog();
                this.Show();
            }
            else
                lbThongBao.Text = "* " + ret;  // trả lỗi
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

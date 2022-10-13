using QuanLyBanCafe.LogicTier;
using QuanLyBanCafe.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanCafe.PresentationTier
{
    public partial class FrmDangNhap : Form
    {
        private NhanVienBUS nhanVienBUS;
        public FrmDangNhap()
        {
            InitializeComponent();
            this.Load += FrmDangNhap_Load;
            nhanVienBUS = new NhanVienBUS();
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.TenDangNhap != string.Empty)
            {
                txtTenDangNhap.Text = Properties.Settings.Default.TenDangNhap;
                txtMatKhau.Text = Properties.Settings.Default.MatKhau;
                chkLuuMatKhau.Checked = true;
            }
        }

        // KIỂM TRA TEXT BOX RỖNG
        private bool KiemTraMatKhauRong()
        {

            if (txtTenDangNhap.Text == "" || txtMatKhau.Text == "")
            {
                return false;
            }
            return true;
        }

        private void TxtInputTextChange(object sender, EventArgs e)
        {

            btnDangNhap.Enabled = KiemTraMatKhauRong();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.PasswordChar = (chkHienMatKhau.Checked) ? '\0' : '*';
        }

        private void chkLuuMatKhau_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (chkLuuMatKhau.Checked)
            {
                Properties.Settings.Default.TenDangNhap = txtTenDangNhap.Text;
                Properties.Settings.Default.MatKhau = txtMatKhau.Text;
            }
            else
            {
                Properties.Settings.Default.TenDangNhap = "";
                Properties.Settings.Default.MatKhau = "";
            }
            Properties.Settings.Default.Save();

            NhanVienViewModel nv;

            if (nhanVienBUS.KiemTraNhanVien(txtTenDangNhap.Text, txtMatKhau.Text, out nv))
            {
                MessageBox.Show("dang nhap thanh cong");
                FrmBanHang frm = new FrmBanHang(nv);
                frm.Show();
                this.Hide();
                frm.FormClosed += Frm_FormClosed;
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}

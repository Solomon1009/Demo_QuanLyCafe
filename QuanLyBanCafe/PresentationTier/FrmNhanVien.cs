using QuanLyBanCafe.DataTier.Models;
using QuanLyBanCafe.LogicTier;
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
    public partial class FrmNhanVien : Form
    {
        private readonly NhanVienBUS nhanVienBUS;
        private int maNhanVien = -1;
        public FrmNhanVien()
        {
            InitializeComponent();
            btnLuu.Enabled = false;
            this.Load += FrmNhanVien_Load;
            nhanVienBUS = new NhanVienBUS();
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
        }

        private void LoadDanhSachNhanVien()
        {
            dgvDanhSach.DataSource = nhanVienBUS.GetNhanViens();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.Ten = txtTenThat.Text;
            nv.TenDangNhap = txtTenDangNhap.Text;
            nv.MatKhau = txtMatKhau.Text;
            if (maNhanVien != -1)
                nv.MaNhanVien = maNhanVien;

            try
            {
                nhanVienBUS.LuuNhanVien(nv);
                txtTenThat.Text = txtTenDangNhap.Text = txtMatKhau.Text = txtXacNhanMatKhau.Text = "";
                LoadDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtThongTin_TextChanged(object sender, EventArgs e)
        {
            if (maNhanVien == -1)
            {
                btnLuu.Enabled =
                    !string.IsNullOrWhiteSpace(txtTenDangNhap.Text) &&
                    !string.IsNullOrWhiteSpace(txtTenThat.Text) &&
                    !string.IsNullOrWhiteSpace(txtMatKhau.Text) &&
                    txtMatKhau.Text == txtXacNhanMatKhau.Text;
            }
            else
            {
                btnLuu.Enabled =
                    !string.IsNullOrWhiteSpace(txtTenThat.Text) &&
                    !string.IsNullOrWhiteSpace(txtMatKhau.Text) &&
                    txtMatKhau.Text == txtXacNhanMatKhau.Text;
            }
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtTenDangNhap.Enabled = false;

            maNhanVien = Convert.ToInt32(dgvDanhSach.Rows[rowIndex].Cells[0].Value);
            txtTenThat.Text = dgvDanhSach.Rows[rowIndex].Cells[1].Value.ToString();

            txtTenDangNhap.Text = dgvDanhSach.Rows[rowIndex].Cells[2].Value.ToString();

            txtMatKhau.Text = txtXacNhanMatKhau.Text = "";
            btnLuu.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            maNhanVien = -1;
            txtTenDangNhap.Text = txtTenThat.Text = txtMatKhau.Text = txtXacNhanMatKhau.Text = "";
            txtTenDangNhap.Enabled = txtMatKhau.Enabled = txtXacNhanMatKhau.Enabled = true;
        }

        private void dgvDanhSach_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvDanhSach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                btnXoa.PerformClick();
            }
        }
    }
}

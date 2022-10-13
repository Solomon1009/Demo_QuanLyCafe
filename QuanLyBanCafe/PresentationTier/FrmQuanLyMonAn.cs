using QuanLyBanCafe.DataTier.Models;
using QuanLyBanCafe.LogicTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyBanCafe.PresentationTier
{
    public partial class FrmQuanLyMonAn : Form
    {
        private readonly DanhMucBUS danhMucBUS;
        private readonly MonBUS monBUS;
        public FrmQuanLyMonAn()
        {
            InitializeComponent();
            danhMucBUS = new DanhMucBUS();
            monBUS = new MonBUS();
            this.Load += FrmQuanLyMonAn_Load;
        }

        private void FrmQuanLyMonAn_Load(object sender, EventArgs e)
        {
            CaiDatDieuKhien();
            LoadDanhMuc();
            LoadDanhSachMon();
        }

        private void CaiDatDieuKhien()
        {
            cbxDanhMuc.DisplayMember = "Ten";
            cbxDanhMuc.ValueMember = "MaDanhMuc";
        }

        private void LoadDanhMuc()
        {
            cbxDanhMuc.DataSource = danhMucBUS.GetDanhMucs();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "JPG",
                Filter = "Image files (*.jpg)|*.jpg",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Mon mon = new Mon();
            mon.Ten = txtTenMon.Text;
            mon.GiaTien = Convert.ToDouble(txtGiaTien.Text);
            mon.MaDanhMuc = Convert.ToInt32(cbxDanhMuc.SelectedValue);

            int pos = txtFilePath.Text.LastIndexOf("\\") + 1;
            string tenHinh = txtFilePath.Text.Substring(pos, txtFilePath.Text.Length - pos);

            try
            {
                LuuHinhAnh(txtFilePath.Text, tenHinh);
                mon.Hinh = "//Hinh//" + tenHinh;
                monBUS.ThemMon(mon);
                LoadDanhSachMon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thong bao loi luu hinh");
            }

        }

        private void LoadDanhSachMon()
        {
            int rowAdd;
            foreach (var item in monBUS.LayTatCaMon())
            {
                rowAdd = dgvDanhSachMon.Rows.Add();

                dgvDanhSachMon.Rows[rowAdd].Cells[0].Value = item.MaMon;
                dgvDanhSachMon.Rows[rowAdd].Cells[1].Value = item.Ten;
                dgvDanhSachMon.Rows[rowAdd].Cells[2].Value = item.GiaTien;
                string duongDanHinh = @"../.." + item.Hinh;
                Image img;
                if (File.Exists(duongDanHinh))
                    img = Image.FromFile(duongDanHinh);
                else
                    img = Image.FromFile(@"../../Hinh/default.jpg");
                dgvDanhSachMon.Rows[rowAdd].Cells[3].Value = img;
            }
        }

        private bool LuuHinhAnh(string duongDanHinh, string tenHinh)
        {

            try
            {
                Bitmap b = new Bitmap(duongDanHinh);
                if(File.Exists(@"../../Hinh/" + tenHinh))
                {
                    var randIndex = new Random();

                    tenHinh = randIndex.Next(0, 1000) + tenHinh;
                }
                    
                   
                b.Save(@"../../Hinh/" + tenHinh);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

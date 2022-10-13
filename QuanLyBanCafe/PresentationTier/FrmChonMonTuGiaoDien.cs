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

namespace QuanLyBanCafe.PresentationTier
{
    public delegate void UyQuyenChonMon(Mon mon, int maSoBan, int SoLuong);

    public partial class FrmChonMonTuGiaoDien : Form
    {
        public event UyQuyenChonMon UyQuyenChonMon;

        private readonly DanhMucBUS danhMucBUS;
        private readonly MonBUS monBUS;
        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");

        private int maSoBanChon;

        public FrmChonMonTuGiaoDien(int maSoBan)
        {
            InitializeComponent();

            this.maSoBanChon = maSoBan;
            
            this.StartPosition = FormStartPosition.CenterScreen;
            danhMucBUS = new DanhMucBUS();
            monBUS = new MonBUS();
            this.Load += FrmChonMonTuGiaoDien_Load;

        }

        private void FrmChonMonTuGiaoDien_Load(object sender, EventArgs e)
        {
            CaiDatDieuKhien();
            LoadDanhMuc();
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

        private void cbxDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMonAn.Controls.Clear();
            int maDanhMuc = Convert.ToInt32(cbxDanhMuc.SelectedValue);
            LoadMonAn(maDanhMuc);
        }

        private void LoadMonAn(int maDanhMuc)
        {
            int x = 26;
            int y = 29;
            int count = 0;
            foreach (var mon in monBUS.GetMonsTheoDanhMuc(maDanhMuc))
            {
                TaoBan(x, y, mon);
                count++;
                if (count % 5 == 0)
                {
                    y += 110;
                    x = 26;
                    continue;
                }
                x += 110;
            }
        }
        private void TaoBan(int x, int y, Mon mon)
        {
            Button btn = new Button();
            btn.Location = new Point(x, y);
            btn.Size = new Size(100, 80);
            btn.Text = mon.Ten + "\n" + String.Format(fVND, "{0:C0}", mon.GiaTien);
            btn.TextAlign = ContentAlignment.BottomCenter;

            string duongDanHinh = "../.." + mon.Hinh;

            if(File.Exists(duongDanHinh))
                btn.Image = Image.FromFile(duongDanHinh);
            else
                btn.Image = Image.FromFile("../../Hinh/default.jpg");
            btn.ImageAlign = ContentAlignment.TopCenter;

            btn.Tag = mon;

            btn.BackColor = Color.White;

            btn.Click += Btn_Click;

            pnlMonAn.Controls.Add(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Mon m = button.Tag as Mon;
            int soLuong = Convert.ToInt32(nupSoLuong.Value);
            UyQuyenChonMon(m, maSoBanChon, soLuong);
        }
    }
}

using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using QuanLyBanCafe.DataTier.Models;
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
    public partial class FrmBanHang : Form
    {
        //chieu rong cua ban
        private const int W = 105;
        //chieu cao cua ban
        private const int H = 81;
        //khoang cach x
        private const int _X = 144;
        //khoang cach y
        private const int _Y = 110;
        //SO LUONG BAN TREN 1 DONG
        private const int COT = 5;
        private Button btnBanChon = null;

        private double tongTien = 0;
        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");

        private BanCafeBUS banCafeBUS;
        private DanhMucBUS danhMucBUS;
        private MonBUS monBUS;
        private HoaDonBUS hoaDonBUS;

        private NhanVienViewModel nhanVienBanHang;
        // DANH SÁCH THÔNG TIN ĐẶT MÓN
        private List<ThongTinDatBan> DanhSachDatBan =
            new List<ThongTinDatBan>();

        public FrmBanHang(NhanVienViewModel nv)
        {
            InitializeComponent();

            nhanVienBanHang = nv;

            nupGiamGia.TextChanged += NupGiamGia_TextChanged;

            this.Text = "Xin chao: " + nv.Ten + ": MSNV: " + nv.Ma;

            CaiDatDieuKhien();

            banCafeBUS = new BanCafeBUS();
            danhMucBUS = new DanhMucBUS();
            monBUS = new MonBUS();
            hoaDonBUS = new HoaDonBUS();
            // Hiển thị danh sách bàn combobox đổi bàn

        }

        private void NupGiamGia_TextChanged(object sender, EventArgs e)
        {
            double tienGiam = tongTien * (double)nupGiamGia.Value / 100;

            txtThanhTien.Text = String.Format(fVND, "{0:C0}", tongTien - tienGiam);
        }

        private void CaiDatDieuKhien()
        {
            cbxDanhMuc.DisplayMember = "Ten";
            cbxDanhMuc.ValueMember = "MaDanhMuc";

            cbxMon.DisplayMember = "Ten";
            cbxMon.ValueMember = "MaMon";
        }

        private void TaoBan(int x, int y, BanCafe ban)
        {
            Button btn = new Button();
            btn.Location = new Point(x, y);
            btn.Size = new Size(W, H);
            btn.Text = ban.TenBan;
            btn.Tag = ban.MaSoBan;
            btn.BackColor = Color.White;
            btn.Click += Btn_Click;
            pnlBan.Controls.Add(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btnBanChon == null)
            {
                btn.BackColor = Color.LightGreen;
            }
            else if (btnBanChon != btn)
            {
                dgvDanhSachMon.Rows.Clear();
                btnBanChon.BackColor = Color.White;
                btn.BackColor = Color.LightGreen;
                ThongTinDatBan thongTinDatBan =
                    DanhSachDatBan.Where(x => x.MaBan == (int)btn.Tag).FirstOrDefault();
                if (thongTinDatBan != null)
                {
                    HienThiDanhSachMon(thongTinDatBan.DanhSachMon);
                    tongTien = thongTinDatBan.DanhSachMon.Sum(x => x.ThanhTien);
                    txtThanhTien.Text = String.Format(fVND, "{0:C0}", tongTien);
                }
                else
                {
                    tongTien = 0;
                    txtThanhTien.Clear();
                }
            }
            else
            {
                btn.BackColor = Color.White;
                btnBanChon = null;
                tongTien = 0;
                txtThanhTien.Clear();
                return;
            }
            btnBanChon = btn;
        }

        private void KhoiTaoSoBan()
        {
            int x = 26;
            int y = 29;
            int count = 0;
            foreach (BanCafe ban in banCafeBUS.GetBanCafes())
            {
                TaoBan(x, y, ban);
                count++;
                if (count % COT == 0)
                {
                    y += _Y;
                    x = 26;
                    continue;
                }
                x += _X;
            }
        }
        private void FrmBanHang_Load(object sender, EventArgs e)
        {
            KhoiTaoSoBan();
            LoadDanhMuc();
            LoadBan();
            WindowState = FormWindowState.Maximized;
        }
        private void LoadDanhMuc()
        {
            cbxDanhMuc.DataSource = danhMucBUS.GetDanhMucs();
        }

        // HIỂN THỊ BÀN TRÊN COMBOBOX
        private void LoadBan()
        {
            cbxBanDoi.ValueMember = "MaSoBan";
            cbxBanDoi.DisplayMember = "TenBan";
            cbxBanDoi.DataSource = banCafeBUS.GetBanCafes();
        }

        private void cbxDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            int maDanhMuc = int.Parse(cbx.SelectedValue.ToString());
            cbxMon.DataSource = monBUS.GetMonsTheoDanhMuc(maDanhMuc);
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            // kiểm tra chọn bàn 
            if (btnBanChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi đặt món");
                return;
            }
            // Kiểm tra số lượng món
            if (nupSoLuong.Value <= 0)
            {
                MessageBox.Show("Phải chọn số lượng >= 0!");
                return;
            }
            int maBan = int.Parse(btnBanChon.Tag.ToString());

            ThongTinDatBan thongTinDatBan = DanhSachDatBan
                .Where(x => x.MaBan == maBan)
                .FirstOrDefault();

            // TRƯỜNG HỢP BÀN CHƯA ĐẶT
            if (thongTinDatBan == null)
            {
                thongTinDatBan = new ThongTinDatBan();
                thongTinDatBan.MaBan = maBan;
                DanhSachDatBan.Add(thongTinDatBan);
                // thay hình button 
                btnBanChon.Image =
                                Image.FromFile("../../Resources/download.png");
            }
            // Thêm món vào bàn
            Mon monChon = (Mon)cbxMon.SelectedItem;
            MonDat monMoi = new MonDat()
            {
                MaMon = monChon.MaMon,
                TenMon = monChon.Ten,
                DonGia = (double)monChon.GiaTien,
                SoLuong = (int)nupSoLuong.Value
            };
            thongTinDatBan.CapNhatMon(monMoi);

            tongTien = thongTinDatBan.DanhSachMon.Sum(x => x.ThanhTien);
            txtThanhTien.Text = String.Format(fVND, "{0:C0}", tongTien);

            HienThiDanhSachMon(thongTinDatBan.DanhSachMon);
        }

        // Hiển thị thong Tin Dat BBanCafean ra datagirdView
        private void HienThiDanhSachMon(List<MonDat> monDats)
        {
            dgvDanhSachMon.Rows.Clear();

            foreach (MonDat mon in monDats)
            {
                int index = dgvDanhSachMon.Rows.Add();
                dgvDanhSachMon.Rows[index].Cells[0].Value
                   = mon.TenMon;
                dgvDanhSachMon.Rows[index].Cells[1].Value
                   = mon.SoLuong;
                dgvDanhSachMon.Rows[index].Cells[2].Value
                   = mon.DonGia;
                dgvDanhSachMon.Rows[index].Cells[3].Value
                   = mon.ThanhTien;
            }
        }
        // Chức năng đổi bàn - gộp bàn
        private void btnDoiBan_Click(object sender, EventArgs e)
        {
            // Lấy thống tinbàn muốn đến 
            int maBanMuonDen = int.Parse(cbxBanDoi.SelectedValue.ToString());
            int maBanHienTai = int.Parse(btnBanChon.Tag.ToString());

            if (maBanMuonDen == maBanHienTai)
            {
                MessageBox.Show("Không được chuyển 2 bàn giống nhau");
                return;
            }
            if (btnBanChon == null)
            {
                MessageBox.Show("Vui long chọn 1 bàn trước khi chuyển!");
                return;
            }

            ThongTinDatBan thongTinBanDen =
                DanhSachDatBan.Where(x => x.MaBan == maBanMuonDen).FirstOrDefault();

            ThongTinDatBan thongTinBanHienTai =
                DanhSachDatBan.Where(x => x.MaBan == maBanHienTai).FirstOrDefault();
            if (thongTinBanHienTai == null)
            {
                MessageBox.Show("Bàn hiện tại trống! Không thể chuyển");
                return;
            }
            dgvDanhSachMon.Rows.Clear();
            // Nếu bàn muốn đến là trống -> 
            if (thongTinBanDen == null)
            {
                thongTinBanHienTai.MaBan = maBanMuonDen;
                HienThiDanhSachMon(thongTinBanHienTai.DanhSachMon);
            }
            else
            {
                foreach (MonDat mon in thongTinBanHienTai.DanhSachMon)
                {
                    thongTinBanDen.CapNhatMon(mon);
                }

                HienThiDanhSachMon(thongTinBanDen.DanhSachMon);
            }

            btnBanChon.Image = null;
            btnBanChon.BackColor = Color.White;

            Button BanChon = pnlBan.Controls.OfType<Button>()
                .Where(x => x.Tag.ToString() == maBanMuonDen.ToString())
                .FirstOrDefault();
            BanChon.Image = Image.FromFile("../../Resources/download.png");
            BanChon.BackColor = Color.LightGreen;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (btnBanChon == null)
            {
                MessageBox.Show("vui long chon ban");
                return;
            }
            int maSoBanChon = Convert.ToInt32(btnBanChon.Tag);
            ThongTinDatBan thongTin = DanhSachDatBan.Where(x => x.MaBan == maSoBanChon).FirstOrDefault();

            if (thongTin == null)
            {
                MessageBox.Show("Ban trong khong the thanh toan!");
                return;
            }

            DialogResult result = MessageBox.Show("ban co muon thanh toan khong!", "thong bao", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            HoaDon hoaDon = new HoaDon();
            hoaDon.MaSoBan = maSoBanChon;
            hoaDon.Ngay = DateTime.Now;
            hoaDon.TongTien = tongTien;
            hoaDon.GiamGia = (double)nupGiamGia.Value;

            ChiTietHoaDon chiTiet;
            foreach (var item in thongTin.DanhSachMon)
            {
                chiTiet = new ChiTietHoaDon();
                chiTiet.MaMon = item.MaMon;
                chiTiet.SoLuong = item.SoLuong;
                hoaDon.ChiTietHoaDons.Add(chiTiet);
            }
            try
            {
                hoaDonBUS.LuuHoaDon(hoaDon);
                btnBanChon.BackColor = Color.Blue;
                btnBanChon.Image = null;
                dgvDanhSachMon.Rows.Clear();
                nupGiamGia.ResetText();
                tongTien = 0;
                txtThanhTien.Clear();
                DanhSachDatBan.Remove(thongTin);
            }
            catch (Exception ex)
            {
                MessageBox.Show("LOI " + ex.Message);
            }
            //ban co muon in hoa don khong

            DialogResult thongBaoInHoaDon = MessageBox.Show("ban co muon in hoa don khong?", "thong bao", MessageBoxButtons.YesNo);

            if (thongBaoInHoaDon == DialogResult.OK)
            {
                //in hoa don
            }
        }

        private void XuatFilePDF(HoaDon hoaDon)
        {
            //PdfDocument doc = new PdfDocument(@"D:\test\pdf\1.pdf");

            //PdfTextField[] result = null;
            //foreach (var word in dictionary)
            //{
            //    foreach (PdfPageBase page in documents.Pages)
            //    {
            //        result = page.FindText(word.Key, TextFindParameter.WholeWord).Finds;
            //        foreach (PdfTextFind find in result)
            //        {
            //            find.ApplyRecoverString(word.Value, System.Drawing.Color.Red, true);
            //        }
            //    }
            //}

            //doc.Save(@"D:\test\pdf\1.pdf");
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNhanVien frm = new FrmNhanVien();
            frm.ShowDialog();
        }

        private void btnThemMonGiaoDien_Click(object sender, EventArgs e)
        {
            //mo man hinh de them mon an tu giao dien

            if (btnBanChon == null)
            {
                MessageBox.Show("vui long chon ban");
                return;
            }
            int maSoBan = Convert.ToInt32(btnBanChon.Tag);
            FrmChonMonTuGiaoDien frm = new FrmChonMonTuGiaoDien(maSoBan);

            frm.UyQuyenChonMon += Frm_UyQuyenChonMon;

            frm.ShowDialog();
        }

        private void Frm_UyQuyenChonMon(Mon mon, int maSoban, int soLuong)
        {
            //mon 
            ThongTinDatBan thongTinDatBan = DanhSachDatBan
                .Where(x => x.MaBan == maSoban)
                .FirstOrDefault();

            // TRƯỜNG HỢP BÀN CHƯA ĐẶT
            if (thongTinDatBan == null)
            {
                thongTinDatBan = new ThongTinDatBan();
                thongTinDatBan.MaBan = maSoban;
                DanhSachDatBan.Add(thongTinDatBan);
                // thay hình button 
                btnBanChon.Image =
                                Image.FromFile("../../Resources/download.png");
            }
            // Thêm món vào bàn
            Mon monChon = mon;
            MonDat monMoi = new MonDat()
            {
                MaMon = monChon.MaMon,
                TenMon = monChon.Ten,
                DonGia = (double)monChon.GiaTien,
                SoLuong = soLuong
            };
            thongTinDatBan.CapNhatMon(monMoi);

            tongTien = thongTinDatBan.DanhSachMon.Sum(x => x.ThanhTien);
            txtThanhTien.Text = String.Format(fVND, "{0:C0}", tongTien);

            HienThiDanhSachMon(thongTinDatBan.DanhSachMon);
        }

        private void menuQLMonAn_Click(object sender, EventArgs e)
        {
            FrmQuanLyMonAn frm = new FrmQuanLyMonAn();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }
    }
}

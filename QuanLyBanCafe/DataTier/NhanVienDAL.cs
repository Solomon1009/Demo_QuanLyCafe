using QuanLyBanCafe.DataTier.Models;
using QuanLyBanCafe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.DataTier
{
    internal class NhanVienDAL
    {
        private QuanLyCafeModel quanLyCafeModel;

        public NhanVienDAL()
        {
            quanLyCafeModel = new QuanLyCafeModel();
        }
        //lay tat ca nhan vien
        public IEnumerable<NhanVienViewModel> GetNhanViens()
        {
            var danhSachNhanVien = quanLyCafeModel.NhanViens
                                   .Select(x => new NhanVienViewModel
                                   {
                                       Ma = x.MaNhanVien,
                                       Ten = x.Ten,
                                       TenDangNhap = x.TenDangNhap
                                   }).ToList();

            return danhSachNhanVien;
        }
        //luu nhan vien (them + sua)
        public bool LuuNhanVien(NhanVien nhanVien)
        {
            try
            {
                NhanVien nv = quanLyCafeModel.NhanViens.Where(x => x.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();



                if (nv != null)
                {
                    nv.Ten = nhanVien.Ten;
                    nv.TenDangNhap = nhanVien.TenDangNhap;
                    nv.MatKhau = nhanVien.MatKhau;
                }
                else
                {
                    if (quanLyCafeModel.NhanViens.Any(x => x.TenDangNhap == nhanVien.TenDangNhap))
                        throw new Exception("Ma nhan vien khong duoc trung");


                    quanLyCafeModel.NhanViens.Add(nhanVien);
                }
                quanLyCafeModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //xoa nhan vien
        public bool XoaNhanVien(int maNhanVien)
        {
            try
            {
                var nv = quanLyCafeModel.NhanViens.Where(x => x.MaNhanVien == maNhanVien).FirstOrDefault();

                if (nv != null)
                {
                    quanLyCafeModel.NhanViens.Remove(nv);
                    quanLyCafeModel.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //lay 1 nhan vien
        public NhanVien GetNhanVien(int maNhanVien)
        {
            return quanLyCafeModel.NhanViens.Where(x => x.MaNhanVien == maNhanVien).FirstOrDefault();
        }
        //kiem tra dang nhap
        public bool KiemTraDangNhap(string tenDangNhap, string matKhau, out NhanVienViewModel nv)
        {
            var nhanVien = quanLyCafeModel.NhanViens
                .Where(x => x.TenDangNhap == tenDangNhap)
                .FirstOrDefault();

            nv = new NhanVienViewModel();

            if (nhanVien == null)
                return false;

            if (BCrypt.Net.BCrypt.Verify(matKhau, nhanVien.MatKhau))
            {
                nv.Ten = nhanVien.Ten;
                nv.Ma = nhanVien.MaNhanVien;
                nv.TenDangNhap = nhanVien.TenDangNhap;
                return true;
            }
            return false;
        }

        internal IEnumerable<NhanVienViewModel> TimKiemTuDb(string timKiem)
        {
            return quanLyCafeModel.NhanViens
                .Where(x => x.Ten.ToLower().Contains(timKiem))
                .Select(x => new NhanVienViewModel() { 
                    Ma = x.MaNhanVien,
                    Ten = x.Ten,
                    TenDangNhap = x.TenDangNhap
                }).ToList();
        }
    }
}

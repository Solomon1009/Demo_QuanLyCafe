using QuanLyBanCafe.DataTier;
using QuanLyBanCafe.DataTier.Models;
using QuanLyBanCafe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.LogicTier
{
    internal class NhanVienBUS
    {
        private NhanVienDAL nhanVienDAL;

        public NhanVienBUS()
        {
            nhanVienDAL = new NhanVienDAL();
        }
        public IEnumerable<NhanVienViewModel> GetNhanViens()
        {
            return nhanVienDAL.GetNhanViens();
        }
        public bool LuuNhanVien(NhanVien nhanVien)
        {
            try
            {
                //ma hoa mat khau
                nhanVien.MatKhau = BCrypt.Net.BCrypt.HashPassword(nhanVien.MatKhau);
                return nhanVienDAL.LuuNhanVien(nhanVien);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool KiemTraNhanVien(string tenDangNhap, string matKhau, out NhanVienViewModel nv)
        {
            return nhanVienDAL.KiemTraDangNhap(tenDangNhap, matKhau, out nv);
        }

        internal IEnumerable<NhanVienViewModel> TimKiemTuDb(string timKiem)
        {
            return nhanVienDAL.TimKiemTuDb(timKiem);
        }
    }
}

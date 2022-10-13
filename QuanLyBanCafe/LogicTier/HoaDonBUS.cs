using QuanLyBanCafe.DataTier;
using QuanLyBanCafe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.LogicTier
{
    internal class HoaDonBUS
    {
        private HoaDonDAL hoaDonDAL;

        public HoaDonBUS()
        {
            hoaDonDAL = new HoaDonDAL();
        }
        public bool LuuHoaDon(HoaDon hoaDon)
        {
            try
            {
                return hoaDonDAL.LuuHoaDon(hoaDon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

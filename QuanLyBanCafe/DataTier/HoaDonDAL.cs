using QuanLyBanCafe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.DataTier
{
    //Data Access Layer: DAL
    //DAO: Data Access Object
    //UI - Presentation, User Interface
    internal class HoaDonDAL
    {
        private QuanLyCafeModel quanLyCafeModel;

        public HoaDonDAL()
        {
            quanLyCafeModel = new QuanLyCafeModel();
        }
        public bool LuuHoaDon(HoaDon hoaDon)
        {
            try
            {
                quanLyCafeModel.HoaDons.Add(hoaDon);
                quanLyCafeModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

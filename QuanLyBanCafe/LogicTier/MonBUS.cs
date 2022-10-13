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
    internal class MonBUS
    {
        private MonDAL monDAL;

        public MonBUS()
        {
            monDAL = new MonDAL();
        }
        public IEnumerable<Mon> GetMonsTheoDanhMuc(int maDanhMuc)
        {
            return monDAL.GetMonTheoMaDanhMuc(maDanhMuc);
        }

        public bool ThemMon(Mon mon)
        {
            try
            {
                return monDAL.ThemMon(mon);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<MonViewModel> LayTatCaMon()
        {
            return monDAL.LayTatCaMon();
        }
    }
}

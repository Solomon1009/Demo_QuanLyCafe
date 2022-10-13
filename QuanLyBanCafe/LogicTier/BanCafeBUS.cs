using QuanLyBanCafe.DataTier;
using QuanLyBanCafe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.LogicTier
{
    internal class BanCafeBUS
    {
        private BanCafeDAL banCafeDAL;

        public BanCafeBUS()
        {
            banCafeDAL = new BanCafeDAL();
        }
        public IEnumerable<BanCafe> GetBanCafes()
        {
            return banCafeDAL.GetBanCafes();
        }
    }
}

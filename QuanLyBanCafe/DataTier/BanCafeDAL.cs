using QuanLyBanCafe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.DataTier
{
    internal class BanCafeDAL
    {
        private QuanLyCafeModel quanLyCafeModel;

        public BanCafeDAL()
        {
            quanLyCafeModel = new QuanLyCafeModel();
        }
        public IEnumerable<BanCafe> GetBanCafes()
        {
            return quanLyCafeModel.BanCafes.ToList();
        }
    }
}

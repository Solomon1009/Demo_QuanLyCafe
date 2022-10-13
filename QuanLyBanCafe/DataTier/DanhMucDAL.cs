using QuanLyBanCafe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.DataTier
{
    internal class DanhMucDAL
    {
        private QuanLyCafeModel quanLyCafeModel;

        public DanhMucDAL()
        {
            quanLyCafeModel = new QuanLyCafeModel();
        }
        public IEnumerable<DanhMuc> GetDanhMucs()
        {
            return quanLyCafeModel.DanhMucs.ToList();
        }
    }
}

using QuanLyBanCafe.DataTier.Models;
using QuanLyBanCafe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanCafe.DataTier
{
    internal class MonDAL
    {
        private QuanLyCafeModel quanLyCafeModel;

        public MonDAL()
        {
            quanLyCafeModel = new QuanLyCafeModel();
        }
        public IEnumerable<Mon> GetMonTheoMaDanhMuc(int maDanhMuc)
        {
            return quanLyCafeModel.Mons.Where(x => x.MaDanhMuc == maDanhMuc).ToList();
        }

        public bool ThemMon(Mon mon)
        {
            try
            {
                Mon m = quanLyCafeModel.Mons.FirstOrDefault(x => x.Ten == mon.Ten);
                if (m != null)
                {
                    m.MaDanhMuc = mon.MaDanhMuc;
                    m.GiaTien = mon.GiaTien;
                    m.Hinh = mon.Hinh;
                }
                else
                {
                    quanLyCafeModel.Mons.Add(mon);
                }
                quanLyCafeModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<MonViewModel> LayTatCaMon()
        {
            return quanLyCafeModel.Mons
                .Select(x => new MonViewModel() { 
                    Ten = x.Ten,
                    MaMon = x.MaMon,
                    Hinh = x.Hinh,
                    GiaTien = (double)x.GiaTien
                }).ToList();
        }
    }
}

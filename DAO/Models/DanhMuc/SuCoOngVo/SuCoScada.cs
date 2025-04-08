using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.SuCoOngVo
{

    public class SuCoScadaViewModel
    {
        public int Id { get; set; }
        public int IdDHK { get; set; }
        public string TenDHK { get; set; }

        public string TenTQL { get; set; }
        public string ThongTinSuCo { get; set; }
        public DateTime? NgayXayRa { get; set; }
        public DateTime? NgayKiemTra { get; set; }
        public string XuLy_QLDiaBan { get; set; }
        public string XuLy_CNTT { get; set; }
        public string XuLy_XNDH { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public string KetQua { get; set; }
        public string NguyenNhan { get; set; }
        public string ChiSo_DH { get; set; }
        public string ChiSo_HT { get; set; }
    }




}

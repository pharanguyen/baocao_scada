using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.CanhBao
{
    public class CanhBaoTieuThuNgay
    {
       
            public int IdTram { get; set; }
            public string MaTram { get; set; }
            public string TenTram { get; set; }

            public double MucCanhBao { get; set; } // set thủ công
            public double TyLeCanhBao { get; set; } // set thủ công %

            public double LuuLuongTuNgay { get; set; }
            public double LuuLuongDenNgay { get; set; }

            public double TieuThu => LuuLuongDenNgay - LuuLuongTuNgay;
       
    }
}

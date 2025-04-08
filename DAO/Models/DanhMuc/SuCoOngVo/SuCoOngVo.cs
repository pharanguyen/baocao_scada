using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.SuCoOngVo

{
    
        public class SuCo_OngVo
        {
            public int Id { get; set; }
            public DateTime NgayBaoCao { get; set; }
            public int IdChiNhanh { get; set; }
            public int IdDHK { get; set; }
        public string ten_dhk { get; set; }  // 🔥 dùng để hiển thị trên Grid


        public DateTime? NgayTimKiem { get; set; }
            public DateTime? NgaySuaChua { get; set; }

            public string SoDiemVo { get; set; }
            public string MoTa { get; set; }

            public double? TieuThu13_Truoc { get; set; }
            public double? TieuThu13_Sau { get; set; }
            public double? TieuThu07_Truoc { get; set; }
            public double? TieuThu07_Sau { get; set; }

            public string GhiChu { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.Now;
        }
    
}

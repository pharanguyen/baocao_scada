using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.CanhBao
{
    public class CanhBaoApLucViewModel
    {
        public int Id { get; set; }
        public int Id_Tram { get; set; }
        public string TenTram { get; set; }
        public float GiaTri { get; set; }

        public DateTime NgayThangNam { get; set; }
        public TimeSpan GioPhutGiay { get; set; }

        public DateTime ThoiGian => NgayThangNam.Add(GioPhutGiay); // ✅ Gộp để hiển thị

        public float? ApLucMin { get; set; } // Cấu hình cảnh báo
        public float? ApLucMax { get; set; }
    }
}

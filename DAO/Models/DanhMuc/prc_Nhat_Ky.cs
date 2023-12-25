using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class prc_Nhat_Ky
    {
        public int Id { get; set; }
        public int Id_ThongSo { get; set; }
        public string TenThongSo { get; set; }
        public int Id_Tram { get; set; }
        public string TenTram { get; set; }
        public string Gia_Tri { get; set; }
        public DateTime Thoi_Gian { get; set; }

    }
}

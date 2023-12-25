using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class Nhat_Ky
    {
        [Key]
        public int Id { get; set; }
        public int Id_ThongSo { get; set; }

        public int Id_Tram { get; set; }

        public string Gia_Tri { get; set; }
        public DateTime Thoi_Gian { get; set; }
        public List<Nhat_Ky_Nam> GiaTriThoiGian { get; set; }
    }
}

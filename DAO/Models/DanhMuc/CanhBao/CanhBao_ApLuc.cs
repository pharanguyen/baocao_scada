using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.CanhBao
{
    public class CanhBao_ApLuc
    {
        public int Id { get; set; }
        public int Id_Tram { get; set; }
        public float? ApLucMin { get; set; }
        public float? ApLucMax { get; set; }
        public DateTime NgayCapNhat { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.CanhBao
{
    public class CanhBao_Tram
    {
        
            public int Id { get; set; }
            public int IdTram { get; set; }
            public double? MucCanhBao { get; set; }
            public double? TyLeCanhBao { get; set; }
            public DateTime NgayCapNhat { get; set; }
        
    }
}

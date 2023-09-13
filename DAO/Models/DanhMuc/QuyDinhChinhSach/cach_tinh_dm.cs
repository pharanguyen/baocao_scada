using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.QuyDinhChinhSach
{
    public class cach_tinh_dm
    {
        [Key]
        public int ms_cach_tinh { get; set; }
        public string mo_ta { get; set; }
        public string cach_tinh { get; set; }
    }
}

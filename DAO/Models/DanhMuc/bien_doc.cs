using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class bien_doc
    {
        [Key]
        public int ms_bd { get; set; }
        public string ten_bd { get; set; }
        public string ghi_chu { get; set; }
        public string mat_khau { get; set; }

    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class to_quan_ly
    {
        public to_quan_ly() { }
        [Key]
        public int ms_tql{ get; set; }
        public string ten_tql{ get; set; }
        public string ghi_chu{ get; set; }
        public int? ms_chinhanh{ get; set; }
    }
}

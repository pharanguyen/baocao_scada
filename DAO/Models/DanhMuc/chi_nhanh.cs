using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class chi_nhanh
    {
         public chi_nhanh() { } 
        [Key]
        public int ms_chinhanh { get; set; }
        public string ten_chinhanh { get; set; }
        public string ghi_chu { get; set; }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class duong
    {
        [Key]
        public  int ms_duong { get; set; }
        public  string ten_duong { get; set; }
    }
}

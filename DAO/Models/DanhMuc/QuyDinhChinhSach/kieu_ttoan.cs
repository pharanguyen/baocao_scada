using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.QuyDinhChinhSach
{
    public class kieu_ttoan
    {
        [Key]
        public int ms_kieu_tt { get; set; }
        public string ten_kieu_tt { get; set; }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.KhachHang
{
    public class tt_k_hang
    {
        [Key]
        public int ms_tt_kh { get; set; }
        public string ten_tt_kh { get; set; }

    }
}

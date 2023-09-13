using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.PhanLoaiDoiTuong
{
    public class loai_hdon
    {
        [Key]
        public int ms_loai_hd { get; set; }
        public string mo_ta_loai_hd { get; set; }
    }
}

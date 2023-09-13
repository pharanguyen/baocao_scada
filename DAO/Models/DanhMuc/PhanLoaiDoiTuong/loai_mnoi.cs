using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.PhanLoaiDoiTuong
{
    public class loai_mnoi
    {
        [Key]
        public int ms_loai_mn { get; set; } 
        public string ten_loai_mn { get; set; } 
    }
}

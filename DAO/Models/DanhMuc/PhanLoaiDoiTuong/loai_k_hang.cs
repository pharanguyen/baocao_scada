using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.PhanLoaiDoiTuong
{
    public class loai_k_hang
    {
        [Key]
        public int ms_loai_kh { get; set; }
        public string ten_loai_kh { get; set; }


    }
}

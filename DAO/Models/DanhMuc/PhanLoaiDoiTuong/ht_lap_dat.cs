using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.PhanLoaiDoiTuong
{
    public class ht_lap_dat
    {
       
        [Key]
        public int ms_ht_lap_dat { get; set; }
        public string ten_ht_lap_dat { get; set; }


    }
}

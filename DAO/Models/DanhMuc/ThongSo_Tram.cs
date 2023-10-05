﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class ThongSo_Tram
    {
        [Key]

        public int Id { get; set; }
        public int Id_Tram { get; set; }
        public int Id_ThongSo { get; set; }

        public int Id_Tql{ get; set; }
        public int Id_ChiNhanh { get; set; }
        public int ms_dhk { get; set; }
    }
}

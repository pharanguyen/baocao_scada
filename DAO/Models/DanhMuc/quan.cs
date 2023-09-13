using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class quan
    {
        [Key]
        public int ms_quan { get; set; }

        public string ten_quan { get; set; }

        public string vi_tri { get; set; }

        public int ds_quan { get; set; }

    }
}

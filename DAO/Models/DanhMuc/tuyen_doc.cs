using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class tuyen_doc
    {
        //public long Id { get; set; }
        [Key]
        public int ms_tuyen { get; set; }
        public int? ms_tql { get; set; }
        public int? ms_cky { get; set; }
        public string mo_ta_tuyen { get; set; }
        public int ms_tn { get; set; }
        public int? ms_bd { get; set; }
    }
}

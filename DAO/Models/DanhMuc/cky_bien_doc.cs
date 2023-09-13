using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class cky_bien_doc
    {

        public cky_bien_doc() { }
        [Key]
        public int? ms_cky { get; set; }

        public string mo_ta_cky { get; set; }

        public DateTime? ngay_thuc_te { get; set; }

    }
}

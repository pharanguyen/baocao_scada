using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.PhanQuyen
{
    public partial class Quyen
    {
        public Quyen() { }
        public string dsquyen_id { get; set; }

        public string dsquyen_name { get; set; }

        public string menu_name { get; set; }

        public string nhomquyen_list { get; set; }

        public string tenform_list { get; set; }

        public byte? trangthai { get; set; }

        public bool col1 { get; set; }
        public bool col2 { get; set; }
        public bool col3 { get; set; }
        public bool col4 { get; set; }
        public bool col5 { get; set; }
        public bool col6 { get; set; }
        public bool col8 { get; set; }
        public bool col11 { get; set; }
        public bool col12{ get; set; }


    }
}

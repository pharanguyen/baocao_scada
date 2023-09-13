using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.PhanQuyen
{
    public class ds_quyen
    {
        public ds_quyen() { }
        [Key]
        public string dsquyen_id { get; set; }

        public string dsquyen_name { get; set; }

        public string menu_name { get; set; }

        public string nhomquyen_list { get; set; }

        public string tenform_list { get; set; }
        public string link { get; set; }

        public byte? trangthai { get; set; }

        public int childcount { get; set; }

        public string css_icon { get; set;}
    }
}

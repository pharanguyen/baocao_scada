using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.PhanQuyen
{
    public class ds_phanquyen
    {
        public ds_phanquyen() { }
        [Key]
        public int phanquyen_id { get; set; }
        public int thanhvien_id { get; set; }

        public string dsquyen_id { get; set; }

        public bool? them { get; set; }
        public bool? sua { get; set; }
        public bool? xoa { get; set; }
        

    }
}

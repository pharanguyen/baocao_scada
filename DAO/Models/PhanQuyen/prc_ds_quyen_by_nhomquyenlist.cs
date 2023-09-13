using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.PhanQuyen
{
    public class prc_ds_quyen_by_nhomquyenlist
    {
        public string dsquyen_id { get; set; }
        public string dsquyen_name { get; set; }
        public int phanquyen_id { get; set; }   
        public bool? Xem { get; set; }
        public bool? them { get; set; }
        public bool? sua { get; set; }
        public bool? xoa { get; set; }

    }
}

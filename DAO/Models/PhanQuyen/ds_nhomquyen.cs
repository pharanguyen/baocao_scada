using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.PhanQuyen
{
    public partial class ds_nhomquyen
    {
  
        public ds_nhomquyen() { }
        [Key]
        public int nhomquyen_id { get; set; }

        public string nhomquyen_name { get; set; }

        public string chuthich { get; set; }
    }
}

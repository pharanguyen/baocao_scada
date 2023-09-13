using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.PhanQuyen
{
    public class ds_thanhvien
    {
        public ds_thanhvien() { }
        
        [Key]
        public int thanhvien_id { get; set; }
        public string thanhvien_name { get; set; }
        public string donvi { get; set; }
        public int? nhomquyen_id { get; set; }
        public int? ms_chinhanh { get; set; }
        public int? ms_tql { get; set; }
        public string chuthich { get; set; }
        public string tendangnhap { get; set; }
        public string matkhau { get; set; }
        public bool phutrachchinh { get; set; }
    }
}

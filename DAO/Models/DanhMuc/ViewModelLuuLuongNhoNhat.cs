using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class ViewModelLuuLuongNhoNhat
    {

        public int Id_dongho { get; set; }
        public string Tendongho { get; set; }
        public string sonhonhat { get; set; }
        public string aplucthoidiemnhonhat { get; set; }
        public DateTime? Ngaythangnam_nhonhat { get; set; }
        public DateTime? Ngaythangnam_lonnhat { get; set; }

        public TimeSpan? giophutgiaynhonhat { get; set; }
        public string solonnhat { get; set; }
        public string aplucthoidiemlonnhat { get; set; }
        
        public TimeSpan? giophutgiaylonnhat { get; set; }
        public string co_dongho { get; set; }
        public int? soluongkhachhang { get; set; }
        public string Nguongcanhbao { get; set; }
        public string Tenchinhanh { get; set; }
        public float? trangthaicanhbao { get; set; }


    }
}

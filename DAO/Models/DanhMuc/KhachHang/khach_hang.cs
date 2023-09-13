using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.KhachHang
{
    public class khach_hang
    {
        [Key]
        public int ms_kh { get; set; }

        public int? ms_duong { get; set; }

        public int ms_tt_kh { get; set; }

        public int ms_loai_kh { get; set; }

        public string ten_kh { get; set; }

        public string dia_chi_kh { get; set; }

        public string dien_thoai { get; set; }

        public DateTime ngay_nhap_kh { get; set; }

        public string ms_thue { get; set; }

        public string so_tai_khoan { get; set; }

        public string mo_tai { get; set; }
    }
}

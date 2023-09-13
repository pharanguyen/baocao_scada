using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.DongHo
{
    public class dh_khoi
    {
        public int ms_dhk { get; set; }

        public int? ms_dh { get; set; }

        public int? ms_tdhk { get; set; }

        public int ms_tk { get; set; }

        public string ten_dhk { get; set; }

        public int? so_thu_tu { get; set; }

        public int? so_tthu_cu1 { get; set; }

        public int? chi_so_cu1 { get; set; }

        public DateTime? ngay_doc_cu1 { get; set; }

        public DateTime? ngay_doc_moi1 { get; set; }

        public int? chi_so_moi1 { get; set; }

        public int? s_tieu_thu1 { get; set; }

        public int? so_tthu_cu2 { get; set; }

        public int? chi_so_cu2 { get; set; }

        public DateTime? ngay_doc_cu2 { get; set; }

        public int? chi_so_moi2 { get; set; }

        public DateTime? ngay_doc_moi2 { get; set; }

        public int? s_tieu_thu2 { get; set; }

        public decimal? toa_do_bac { get; set; }

        public decimal? toa_do_dong { get; set; }

        public int? ms_nhom { get; set; }

        public int ms_tt_dh { get; set; }

        public bool co_chi_so { get; set; }

        public int? ms_bd { get; set; }

        public int? ms_phuong { get; set; }

        public string ghi_chu { get; set; }

        public string url_image { get; set; }
    }
}

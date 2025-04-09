using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class DM_Dhk
    {
        
            public int ms_dhk { get; set; } // [ms_dhk]
            public decimal? ms_dh { get; set; } // [ms_dh]
            public decimal? ms_tdhk { get; set; } // [ms_tdhk]
            public decimal ms_tk { get; set; } // [ms_tk]
            public string ten_dhk { get; set; } // [ten_dhk]
            public int? so_thu_tu { get; set; } // [so_thu_tu]
            public double? so_tthu_cu1 { get; set; } // [so_tthu_cu1]
            public double? chi_so_cu1 { get; set; } // [chi_so_cu1]
            public DateTime? ngay_doc_cu1 { get; set; } // [ngay_doc_cu1]
            public DateTime? ngay_doc_moi1 { get; set; } // [ngay_doc_moi1]
            public double? chi_so_moi1 { get; set; } // [chi_so_moi1]
            public double? s_tieu_thu1 { get; set; } // [s_tieu_thu1]
            public double? so_tthu_cu2 { get; set; } // [so_tthu_cu2]
            public double? chi_so_cu2 { get; set; } // [chi_so_cu2]
            public DateTime? ngay_doc_cu2 { get; set; } // [ngay_doc_cu2]
            public double? chi_so_moi2 { get; set; } // [chi_so_moi2]
            public DateTime? ngay_doc_moi2 { get; set; } // [ngay_doc_moi2]
            public double? s_tieu_thu2 { get; set; } // [s_tieu_thu2]
            public decimal? toa_do_bac { get; set; } // [toa_do_bac]
            public decimal? toa_do_dong { get; set; } // [toa_do_dong]
            public decimal? ms_nhom { get; set; } // [ms_nhom]
            public decimal ms_tt_dh { get; set; } // [ms_tt_dh]
            public bool? co_chi_so { get; set; } = true; // [co_chi_so], Default Value: 1
            public decimal? ms_bd { get; set; } // [ms_bd]
            public decimal? ms_phuong { get; set; } // [ms_phuong]
            public string ghi_chu { get; set; } // [ghi_chu]
            public string url_image { get; set; } // [url_image]
        
    }
}

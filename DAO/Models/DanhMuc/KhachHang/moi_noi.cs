using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.KhachHang
{
    public class moi_noi
    {
        [Key]
        public int ms_mnoi { get; set; }

        public int? ms_cs { get; set; }

        public int? ms_duong { get; set; }

        public int? ms_bduong { get; set; }

        public int ms_loai_mn { get; set; }

        public int ms_phuong { get; set; }

        public int? ms_k_hoach { get; set; }

        public int ms_tuyen { get; set; }

        public int ms_kh { get; set; }

        public int ms_tt_dd { get; set; }

        public int ms_ttrang { get; set; }

        public int? ms_cach_tinh { get; set; }

        public int? ms_dhk { get; set; }

        public int? ms_dh_mn { get; set; }

        public string nguoi_thue { get; set; }

        public int? so_ho { get; set; }

        public int? so_nguoi_o { get; set; }

        public string dia_chi_mnoi { get; set; }

        public int stt_lo_trinh { get; set; }

        public float? dinh_muc { get; set; }

        public bool tinh_ptn { get; set; }

        public int? tien_dho { get; set; }

        public int? tien_dho_tra { get; set; }

        public int? toa_do_bac { get; set; }

        public int? toa_do_dong { get; set; }

        public float? so_tthu_cu { get; set; }

        public int? ms_han_muc { get; set; }

        public bool tinh_luy_ke { get; set; }

        public byte tieu_thu_tt { get; set; }

        public int? ms_cong_thuc_moi { get; set; }

        public int? ms_cong_thuc_cu { get; set; }

        public int? ms_ht_ld { get; set; }

        public int? ms_nm_thoat_nuoc { get; set; }

        public bool? in_the_KH { get; set; }

        public bool? dk_tu_doc { get; set; }

        public DateTime? ngay_dk_tu_doc { get; set; }
    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;

namespace DAO.ViewModel
{
    public class BaoCaoNgayViewModel
    {
        public int TT { get; set; }
        public DateTime ThoiGian { get; set; }
    }
    public class DataBaoCaoNgayViewModel
    {
        public int Total { get; set; }
        public ResultModel<List<prc_Nhat_Ky_Ngay>> NhatKy { get; set; }
    }
    public class ChiTietViewModel
    {
        public string TenThongSo { get; set; }
        public decimal TongGiaTriThongSo { get; set; } = 0;
        public decimal MinGiaTriThongSo { get; set; } = 0;
        public decimal AvrGiaTriThongSo { get; set; } = 0;
        public decimal CountGiaTriThongSo { get; set; } = 1;
        public decimal MaxGiaTriThongSo { get; set; } = 0;
        public decimal MinGiaTriTieuThu { get; set; } = 0;
        public decimal AvrGiaTriTieuThu { get; set; } = 0;
        public decimal CountGiaTriTieuThu { get; set; } = 1;
        public decimal MaxGiaTriTieuThu { get; set; } = 0;
        public string TenTieuThu { get; set; }
        public decimal TongGiaTriTieuThu { get; set; } = 0;

    }
}

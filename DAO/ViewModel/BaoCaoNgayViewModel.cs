using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;

namespace ERPProject.ViewModel
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
}

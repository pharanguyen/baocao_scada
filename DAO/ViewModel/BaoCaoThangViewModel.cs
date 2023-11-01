using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;


namespace DAO.ViewModel
{
    public class BaoCaoThangViewModel
    {
       
            public int TT { get; set; }
            public DateTime ThoiGian { get; set; }
        }
        public class DataBaoCaoThangViewModel
        {
            public int Total { get; set; }
            public ResultModel<List<prc_Nhat_Ky_Thang>> NhatKy { get; set; }
        }
    }


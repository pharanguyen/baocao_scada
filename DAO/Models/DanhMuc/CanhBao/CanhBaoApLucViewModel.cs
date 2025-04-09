using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc.CanhBao
{
    public class CanhBaoApLucViewModel
    {
        public int Id { get; set; }
        public DateTime NgayThangNam { get; set; }
        public TimeSpan GioPhutGiay { get; set; }
        public int Id_Tram { get; set; }
        public string TenTram { get; set; }

        // ✅ Đổi từ kiểu double sang string để tránh lỗi parse khi dữ liệu là "MTH"
        public string GiaTri { get; set; }

        public double? ApLucMin { get; set; }
        public double? ApLucMax { get; set; }

        // 👉 Nếu bạn vẫn cần dùng giá trị số để cảnh báo
        public double? GiaTriSo
        {
            get
            {
                return double.TryParse(GiaTri, out var val) ? val : null;
            }
        }
    }
}

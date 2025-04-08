using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class LuuLuongNhoNhatService
    {
        public static async Task<ResultModel<List<ViewModelLuuLuongNhoNhat>>> GetLuuLuongNhoNhat(DateTime fromDate, DateTime toDate, string chiNhanhIds, string tenDongHo)
        {
            StringBuilder sb = new();

            sb.AppendLine("WITH A2 AS (");
            sb.AppendLine("    SELECT ");
            sb.AppendLine("        Giatriluuluongtonghop.*, ");
            sb.AppendLine("        Dm_dongho.Tendongho, ");
            sb.AppendLine("        Dm_dongho.co_dongho, ");
            sb.AppendLine("        Dm_dongho.soluongkhachhang, ");
            sb.AppendLine("        Dm_dongho.Nguongcanhbao, ");
            sb.AppendLine("        Dm_dongho.Id_tram ");
            sb.AppendLine("    FROM Giatriluuluongtonghop ");
            sb.AppendLine("    LEFT JOIN Dm_dongho ON Giatriluuluongtonghop.ID_dongho = Dm_dongho.Id_dongho ");
            sb.AppendLine(")");

            sb.AppendLine("SELECT ");
            sb.AppendLine("    A2.ID_dongho, ");
            sb.AppendLine("    A2.Tendongho, ");
            sb.AppendLine("    A2.sonhonhat, ");
            sb.AppendLine("    A2.aplucthoidiemnhonhat, ");
            sb.AppendLine("    A2.Ngaythangnam_nhonhat, ");
            sb.AppendLine("    A2.giophutgiaynhonhat, ");
            sb.AppendLine("    A2.solonnhat, ");
            sb.AppendLine("    A2.aplucthoidiemlonnhat, ");
            sb.AppendLine("    A2.Ngaythangnam_lonnhat, ");
            sb.AppendLine("    A2.giophutgiaylonnhat, ");
            sb.AppendLine("    A2.co_dongho, ");
            sb.AppendLine("    TRY_CONVERT(int, A2.soluongkhachhang) AS soluongkhachhang, ");
            sb.AppendLine("    A2.Nguongcanhbao, ");
            sb.AppendLine("    Chinhanh.Tenchinhanh AS TenChiNhanh, ");
            sb.AppendLine("    ROUND(TRY_CAST(A2.sonhonhat AS FLOAT) - TRY_CAST(A2.Nguongcanhbao AS FLOAT), 2) AS trangthaicanhbao ");

            sb.AppendLine("FROM A2 ");
            sb.AppendLine("LEFT JOIN DMtram ON A2.Id_tram = DMtram.Id_tram ");
            sb.AppendLine("LEFT JOIN Chinhanh ON DMtram.Id_chinhanh = Chinhanh.Id_chinhanh ");

            sb.AppendLine("WHERE ");
            sb.AppendLine("    A2.Ngaythangnam_ghidl >= @FromDate ");
            sb.AppendLine("    AND A2.Ngaythangnam_ghidl <= @ToDate ");
            sb.AppendLine("    AND Chinhanh.Id_chinhanh IN (SELECT Value FROM dbo.SplitStringToTable(@ChiNhanhIds, ',')) ");
            sb.AppendLine("    AND A2.Tendongho LIKE @TenDongHo ");

            sb.AppendLine("ORDER BY ");
            sb.AppendLine("    A2.Ngaythangnam_ghidl DESC, ");
            sb.AppendLine("    A2.giophutgiay_ghidl DESC");

            try
            {
                var db = new SqlHelper();
                var parameters = new
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    ChiNhanhIds = chiNhanhIds,
                    TenDongHo = $"%{tenDongHo}%"
                };

                var data = db.ExecQueryData<ViewModelLuuLuongNhoNhat>(sb.ToString(), parameters, 3)?.ToList();

                return new ResultModel<List<ViewModelLuuLuongNhoNhat>>
                {
                    isThanhCong = true,
                    Data = data ?? new()
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<ViewModelLuuLuongNhoNhat>>
                {
                    isThanhCong = false,
                    ThongBao = ex.Message
                };
            }
        }
    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.CanhBao;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.CanhBao
{
    public class canh_bao_tieu_thuService
    {
        public static ResultModel<List<CanhBaoTieuThuNgay>> GetCanhBaoTieuThuNgay(
      DateTime tuNgay, DateTime denNgay, List<int> idTram)
        {
            try
            {
                if (idTram == null || !idTram.Any())
                {
                    return new ResultModel<List<CanhBaoTieuThuNgay>>()
                    {
                        isThanhCong = false,
                        ThongBao = "Chưa chọn trạm để truy vấn."
                    };
                }

                var _db = new SqlHelper();

                DateTime tuNgay7h = tuNgay.Date.AddHours(7);
                DateTime denNgay7h = denNgay.Date.AddHours(7);

                string sql = @"
            SELECT 
                dt.Id AS IdTram,
                dt.Ma AS MaTram,
                dt.Ten AS TenTram,
                ISNULL((
                    SELECT SUM(CAST(nk.Gia_Tri AS FLOAT))
                    FROM Nhat_Ky nk
                    WHERE nk.Id_Tram = dt.Id 
                        AND nk.Thoi_Gian = @TuNgay7h
                        AND nk.Id_ThongSo IN (13,14,15,16,17)
                ), 0) AS LuuLuongTuNgay,
                ISNULL((
                    SELECT SUM(CAST(nk.Gia_Tri AS FLOAT))
                    FROM Nhat_Ky nk
                    WHERE nk.Id_Tram = dt.Id 
                        AND nk.Thoi_Gian = @DenNgay7h
                        AND nk.Id_ThongSo IN (13,14,15,16,17)
                ), 0) AS LuuLuongDenNgay
            FROM Dm_Tram dt
            WHERE dt.Id IN @IdTram
            ORDER BY dt.Ten ASC
        ";

                var parametter = new DynamicParameters();
                parametter.Add("@TuNgay7h", tuNgay7h);
                parametter.Add("@DenNgay7h", denNgay7h);
                parametter.Add("@IdTram", idTram);


                var data = _db.ExecQueryData<CanhBaoTieuThuNgay>(sql, parametter, 4)?.ToList() ?? new();

                return new ResultModel<List<CanhBaoTieuThuNgay>>()
                {
                    isThanhCong = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<CanhBaoTieuThuNgay>>()
                {
                    isThanhCong = false,
                    ThongBao = ex.Message
                };
            }
        }


    }
}

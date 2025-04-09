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
    public class DmDhkService
    {
        public static async Task<ResultModel<List<DM_Dhk>>> GetDhkAsync()
        {
            string sql = @"SELECT dhk.ms_dhk ,dhk.ten_dhk FROM dh_khoi dhk LEFT JOIN ThongSo_Tram tst ON dhk.ms_dhk = tst.ms_dhk";

            try
            {
                var _db = new SqlHelper();

                var data = _db.ExecQueryData<DM_Dhk>(sql, null, 1).ToList();

                if (data == null || data.Count == 0)
                {
                    throw new Exception("No data found.");
                }

                return new ResultModel<List<DM_Dhk>>()
                {
                    isThanhCong = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<DM_Dhk>>()
                {
                    isThanhCong = false,
                    ThongBao = ex.Message
                };
            }
        }
        public static ResultModel<List<DM_Dhk>> GetDhkTheoChiNhanh(int idChiNhanh)
        {
            string sql = @"
        SELECT DISTINCT 
            t.ms_dhk, 
            d.ten_dhk
        FROM baocao_scada_db.dbo.ThongSo_Tram t
        INNER JOIN baocao_scada_db.dbo.dh_khoi d ON t.ms_dhk = d.ms_dhk
        WHERE t.Id_ChiNhanh = @IdChiNhanh
        ORDER BY d.ten_dhk";

            try
            {
                var db = new SqlHelper();
                var parameters = new DynamicParameters();
                parameters.Add("@IdChiNhanh", idChiNhanh);

                var data = db.ExecQueryData<DM_Dhk>(sql, parameters,  1).ToList();

                return new ResultModel<List<DM_Dhk>>
                {
                    isThanhCong = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<DM_Dhk>>
                {
                    isThanhCong = false,
                    ThongBao = $"Lỗi khi lấy DHK theo chi nhánh: {ex.Message}"
                };
            }
        }

        public static async Task<ResultModel<List<DM_Dhk>>> GetDhk()
        {
            string sql = @"
        SELECT DISTINCT 
            t.ms_dhk,
            d.ten_dhk
        FROM [baocao_scada_db].[dbo].[ThongSo_Tram] t
        INNER JOIN [baocao_scada_db].[dbo].[dh_khoi] d ON t.ms_dhk = d.ms_dhk
        WHERE t.ms_dhk IS NOT NULL
        ORDER BY d.ten_dhk";

            try
            {
                var _db = new SqlHelper();
                var data = _db.ExecQueryData<DM_Dhk>(sql, null, 1).ToList();

                if (data == null || data.Count == 0)
                    throw new Exception("Không tìm thấy dữ liệu đồng hồ khối.");

                return new ResultModel<List<DM_Dhk>>()
                {
                    isThanhCong = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<DM_Dhk>>()
                {
                    isThanhCong = false,
                    ThongBao = "Lỗi khi lấy danh sách DHK: " + ex.Message
                };
            }
        }


    }
}

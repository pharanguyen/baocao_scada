using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
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
    }
}

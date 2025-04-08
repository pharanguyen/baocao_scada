using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class Nhom_dhkService
    {
        public static ResultModel<List<Nhom_dh_khoi>> GetNhomDhk()
        {
            string sql = @"SELECT  [ms_nhom], [mo_ta] FROM [baocao_scada_db].[dbo].[nhom_dh_khoi]";

            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhom_dh_khoi>>() { Data = _db.ExecQueryData<Nhom_dh_khoi>(sql, null, 2).ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhom_dh_khoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static async Task<ResultModel<List<Nhom_dh_khoi>>> GetNhomDhkAsync()
        {
            string sql = @"SELECT  [ms_nhom], [mo_ta] FROM [baocao_scada_db].[dbo].[nhom_dh_khoi]";

            try
            {
                var _db = new SqlHelper();

                var data = _db.ExecQueryData<Nhom_dh_khoi>(sql, null, 1).ToList();

                if (data == null || data.Count == 0)
                {
                    throw new Exception("No data found.");
                }

                return new ResultModel<List<Nhom_dh_khoi>>()
                {
                    isThanhCong = true,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<List<Nhom_dh_khoi>>()
                {
                    isThanhCong = false,
                    ThongBao = ex.Message
                };
            }
        }
    }
}
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
    public class cky_bien_docService
    {
        public static ResultModel<List<cky_bien_doc>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<cky_bien_doc>>() { Data = _db.GetAll<cky_bien_doc>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<cky_bien_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<cky_bien_doc>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<cky_bien_doc>("prc_cky_bien_doc_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<cky_bien_doc>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<cky_bien_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

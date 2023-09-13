using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class tuyen_docService
    {
        public static ResultModel<List<tuyen_doc>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<tuyen_doc>>() { Data = _db.GetAllByWhereCondition<tuyen_doc>("where ms_tuyen >= 5000 ").ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<tuyen_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<tuyen_doc>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<tuyen_doc>>() { Data = _db.GetAll<tuyen_doc>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<tuyen_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<tuyen_doc> GetById(int ms_tuyendoc)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<tuyen_doc>(ms_tuyendoc);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<tuyen_doc>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<tuyen_doc>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<prc_tuyen_doc> GetById_prc_tuyen_doc(int ms_tuyen)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ms_tuyen", ms_tuyen);
                var _obj = _db.QueryProc<prc_tuyen_doc>("prc_GetById_prc_tuyen_doc",p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_tuyen_doc>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<prc_tuyen_doc>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(tuyen_doc entity)
        {
            try
            {
                var _db = new SqlHelper(); 
                string sql =  "Insert into tuyen_doc(ms_tuyen, ms_tql, ms_cky, mo_ta_tuyen, ms_bd) " +
                                "values ("+entity.ms_tuyen+","+entity.ms_tql+", "+entity.ms_cky+", '"+entity.mo_ta_tuyen+"', "+entity.ms_bd +")";
                DataTable dt1 = _db.ExecuteSQLDataTable(sql);     
                if (dt1 == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(tuyen_doc entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objId = _db.UpdateEntity(entity);
                if (objId == null) { throw new Exception(_db.LoiNgoaiLe); }
                return new ResultModel<int?>() { Data = _db.UpdateEntity(entity) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> DeleteById(int id)
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<tuyen_doc>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_tuyen_doc>> GetAll_prc_tuyen_doc()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_tuyen_doc>("prc_tuyen_doc").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_tuyen_doc>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_tuyen_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<tuyen_doc>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<tuyen_doc>("prc_tuyen_doc_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<tuyen_doc>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<tuyen_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class bien_docService
    {
        public static ResultModel<List<bien_doc>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<bien_doc>>() { Data = _db.GetAll<bien_doc>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<bien_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<bien_doc>> GetByKeyword(string keyWord )
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<bien_doc>("prc_bien_doc_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<bien_doc>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<bien_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<bien_doc>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<bien_doc>>() { Data = _db.GetAll<bien_doc>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<bien_doc>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<bien_doc> GetById(int ms_bd)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<bien_doc>(ms_bd);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<bien_doc>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<bien_doc>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(bien_doc entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_bd = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(bien_doc entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<bien_doc>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

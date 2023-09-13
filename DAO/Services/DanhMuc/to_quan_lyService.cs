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
    public class to_quan_lyService
    {
        public static ResultModel<List<to_quan_ly>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<to_quan_ly>>() { Data = _db.GetAll<to_quan_ly>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<to_quan_ly>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<to_quan_ly>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<to_quan_ly>>() { Data = _db.GetAll<to_quan_ly>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<to_quan_ly>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<to_quan_ly> GetById(int ms_tql)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<to_quan_ly>(ms_tql);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<to_quan_ly>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<to_quan_ly>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(to_quan_ly entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_tql = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(to_quan_ly entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<to_quan_ly>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_to_quan_ly>> GetAll_prc_to_quan_ly()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_to_quan_ly>("prc_to_quan_ly").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_to_quan_ly>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_to_quan_ly>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<to_quan_ly>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<to_quan_ly>("prc_to_quan_ly_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<to_quan_ly>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<to_quan_ly>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.DongHo;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.DongHo
{
    public class dh_khoiService
    {
        public static ResultModel<List<dh_khoi>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<dh_khoi>>() { Data = _db.GetAll<dh_khoi>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<dh_khoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<dh_khoi>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<dh_khoi>>() { Data = _db.GetAll<dh_khoi>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<dh_khoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<dh_khoi> GetById(int ms_dhk)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<dh_khoi>(ms_dhk);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<dh_khoi>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<dh_khoi>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(dh_khoi entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_dhk = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(dh_khoi entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<dh_khoi>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        //public static ResultModel<List<prc_to_quan_ly>> GetAll_prc_dh_khoi()
        //{
        //    try
        //    {
        //        var _db = new SqlHelper();
        //        var _obj = _db.QueryProc<prc_to_quan_ly>("prc_to_quan_ly").ToList();
        //        if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
        //        return new ResultModel<List<prc_to_quan_ly>>() { Data = _obj };
        //    }
        //    catch (Exception ex) { return new ResultModel<List<prc_to_quan_ly>>() { isThanhCong = false, ThongBao = ex.Message }; }
        //}

        public static ResultModel<List<dh_khoi>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<dh_khoi>("prc_dh_khoi_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<dh_khoi>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<dh_khoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

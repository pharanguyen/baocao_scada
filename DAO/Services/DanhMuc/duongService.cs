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
    public class duongService
    {
        public static ResultModel<List<duong>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<duong>>() { Data = _db.GetAll<duong>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<duong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<duong>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<duong>>() { Data = _db.GetAll<duong>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<duong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<duong> GetById(int ms_duong)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<duong>(ms_duong);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<duong>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<duong>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(duong entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_duong = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(duong entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<duong>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }


        public static ResultModel<List<duong>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<duong>("prc_duong_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<duong>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<duong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

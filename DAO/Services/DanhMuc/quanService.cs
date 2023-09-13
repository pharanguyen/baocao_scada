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
    public class quanService
    {
        public static ResultModel<List<quan>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<quan>>() { Data = _db.GetAll<quan>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<quan>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<quan>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<quan>>() { Data = _db.GetAll<quan>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<quan>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<quan> GetById(int ms_quan)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<quan>(ms_quan);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<quan>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<quan>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(quan entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_quan = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(quan entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<quan>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<quan>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<quan>("prc_quan_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<quan>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<quan>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

    }
}

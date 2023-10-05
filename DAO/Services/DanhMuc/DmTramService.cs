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
    public class DmTramService
    {
        public static ResultModel<List<Dm_Tram>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Dm_Tram>>() { Data = _db.GetAll<Dm_Tram>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Dm_Tram>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Dm_Tram>>() { Data = _db.GetAll<Dm_Tram>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<Dm_Tram> GetById(int Id)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<Dm_Tram>(Id);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<Dm_Tram>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<Dm_Tram>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(Dm_Tram entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.Id = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(Dm_Tram entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<Dm_Tram>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_Tram>> GetAll_prc_Tram()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_Tram>("prc_Tram").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Tram>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Dm_Tram>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<Dm_Tram>("Dm_Tram_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<Dm_Tram>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}


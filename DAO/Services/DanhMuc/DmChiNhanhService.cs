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
    public class DmChiNhanhService
    {
        public static ResultModel<List<Dm_ChiNhanh>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Dm_ChiNhanh>>() { Data = _db.GetAll<Dm_ChiNhanh>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_ChiNhanh>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Dm_ChiNhanh>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Dm_ChiNhanh>>() { Data = _db.GetAll<Dm_ChiNhanh>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_ChiNhanh>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<Dm_ChiNhanh> GetById(int ms_bd)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<Dm_ChiNhanh>(ms_bd);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<Dm_ChiNhanh>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<Dm_ChiNhanh>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(Dm_ChiNhanh entity)
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

        public static ResultModel<int?> Update(Dm_ChiNhanh entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<Dm_ChiNhanh>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

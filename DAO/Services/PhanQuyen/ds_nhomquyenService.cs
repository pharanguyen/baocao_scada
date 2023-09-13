using DAO.Models.CommonModels;
using DAO.Models.PhanQuyen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.PhanQuyen
{
    public static class ds_nhomquyenService
    {
        public static ResultModel<List<ds_nhomquyen>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ds_nhomquyen>>() { Data = _db.GetAll<ds_nhomquyen>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ds_nhomquyen>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(ds_nhomquyen entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.nhomquyen_id = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(ds_nhomquyen entity)
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
        public static ResultModel<ds_nhomquyen> GetById(int nhomquyen_id)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<ds_nhomquyen>(nhomquyen_id);
                if (_obj == null) throw new Exception($"Không tìm thấy nhóm quyền id = {nhomquyen_id} !");
                return new ResultModel<ds_nhomquyen>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<ds_nhomquyen>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> DeleteById(int id)
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<ds_nhomquyen>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

    }
}

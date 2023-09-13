using DAO.Models;
using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public  class chi_nhanhService
    {
        public static ResultModel<List<chi_nhanh>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<chi_nhanh>>() { Data = _db.GetAll<chi_nhanh>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<chi_nhanh>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<chi_nhanh>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<chi_nhanh>>() { Data = _db.GetAll<chi_nhanh>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<chi_nhanh>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<chi_nhanh> GetById(int ms_chinhanh)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<chi_nhanh>(ms_chinhanh);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<chi_nhanh>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<chi_nhanh>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(chi_nhanh entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_chinhanh = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(chi_nhanh entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<chi_nhanh>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

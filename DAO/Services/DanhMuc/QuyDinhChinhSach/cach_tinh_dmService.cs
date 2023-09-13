using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.QuyDinhChinhSach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.QuyDinhChinhSach
{
    public class cach_tinh_dmService
    {
        public static ResultModel<List<cach_tinh_dm>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<cach_tinh_dm>>() { Data = _db.GetAll<cach_tinh_dm>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<cach_tinh_dm>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<cach_tinh_dm>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<cach_tinh_dm>>() { Data = _db.GetAll<cach_tinh_dm>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<cach_tinh_dm>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<cach_tinh_dm> GetById(int ms_cach_tinh)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<cach_tinh_dm>(ms_cach_tinh);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<cach_tinh_dm>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<cach_tinh_dm>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(cach_tinh_dm entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_cach_tinh = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(cach_tinh_dm entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<cach_tinh_dm>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

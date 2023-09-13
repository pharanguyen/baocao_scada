using DAO.Models.CommonModels;
using DAO.Models.DanhMuc.PhanLoaiDoiTuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.PhanLoaiDoiTuong
{
    public class ttrang_dh_ddService
    {
        public static ResultModel<List<ttrang_dh_dd>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ttrang_dh_dd>>() { Data = _db.GetAll<ttrang_dh_dd>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ttrang_dh_dd>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<ttrang_dh_dd>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ttrang_dh_dd>>() { Data = _db.GetAll<ttrang_dh_dd>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ttrang_dh_dd>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<ttrang_dh_dd> GetById(int ms_ttrang)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<ttrang_dh_dd>(ms_ttrang);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<ttrang_dh_dd>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<ttrang_dh_dd>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(ttrang_dh_dd entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_ttrang = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };


            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(ttrang_dh_dd entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<ttrang_dh_dd>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

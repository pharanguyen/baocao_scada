using DAO.Models.CommonModels;
using DAO.Models.DanhMuc.PhanLoaiDoiTuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.PhanLoaiDoiTuong
{
    public class loai_mnoiService
    {
        public static ResultModel<List<loai_mnoi>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<loai_mnoi>>() { Data = _db.GetAll<loai_mnoi>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<loai_mnoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<loai_mnoi>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<loai_mnoi>>() { Data = _db.GetAll<loai_mnoi>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<loai_mnoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<loai_mnoi> GetById(int ms_loai_mn)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<loai_mnoi>(ms_loai_mn);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<loai_mnoi>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<loai_mnoi>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(loai_mnoi entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_loai_mn = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };


            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(loai_mnoi entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<loai_mnoi>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

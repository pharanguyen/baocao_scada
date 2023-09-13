using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.PhanLoaiDoiTuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.PhanLoaiDoiTuong
{
    public class loai_k_hangService
    {
        public static ResultModel<List<loai_k_hang>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<loai_k_hang>>() { Data = _db.GetAll<loai_k_hang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<loai_k_hang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<loai_k_hang>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<loai_k_hang>>() { Data = _db.GetAll<loai_k_hang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<loai_k_hang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<loai_k_hang> GetById(int ms_loai_kh)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<loai_k_hang>(ms_loai_kh);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<loai_k_hang>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<loai_k_hang>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(loai_k_hang entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_loai_kh = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(loai_k_hang entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<loai_k_hang>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

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
    public class loai_hdonService
    {
        public static ResultModel<List<loai_hdon>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<loai_hdon>>() { Data = _db.GetAll<loai_hdon>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<loai_hdon>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<loai_hdon>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<loai_hdon>>() { Data = _db.GetAll<loai_hdon>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<loai_hdon>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<loai_hdon> GetById(int ms_loai_hd)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<loai_hdon>(ms_loai_hd);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<loai_hdon>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<loai_hdon>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(loai_hdon entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_loai_hd = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(loai_hdon entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<loai_hdon>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

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
    public class kieu_ttoanService
    {
        public static ResultModel<List<kieu_ttoan>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<kieu_ttoan>>() { Data = _db.GetAll<kieu_ttoan>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<kieu_ttoan>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<kieu_ttoan>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<kieu_ttoan>>() { Data = _db.GetAll<kieu_ttoan>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<kieu_ttoan>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<kieu_ttoan> GetById(int ms_kieu_tt)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<kieu_ttoan>(ms_kieu_tt);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<kieu_ttoan>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<kieu_ttoan>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(kieu_ttoan entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_kieu_tt = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(kieu_ttoan entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<kieu_ttoan>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

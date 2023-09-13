using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.KhachHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.KhachHang
{
    public class tt_k_hangService
    {
        public static ResultModel<List<tt_k_hang>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<tt_k_hang>>() { Data = _db.GetAll<tt_k_hang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<tt_k_hang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<tt_k_hang>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<tt_k_hang>>() { Data = _db.GetAll<tt_k_hang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<tt_k_hang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<tt_k_hang> GetById(int ms_tt_kh)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<tt_k_hang>(ms_tt_kh);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<tt_k_hang>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<tt_k_hang>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(tt_k_hang entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_tt_kh = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(tt_k_hang entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<tt_k_hang>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

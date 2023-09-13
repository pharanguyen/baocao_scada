using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.KhachHang;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.KhachHang
{
    public class khach_hangService
    {
        public static ResultModel<List<khach_hang>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<khach_hang>>() { Data = _db.GetAll<khach_hang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<khach_hang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<khach_hang> GetById(int ms_kh)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<khach_hang>(ms_kh);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<khach_hang>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<khach_hang>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(khach_hang entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_kh = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = objID };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(khach_hang entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<khach_hang>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_khach_hang>> GetAll_prc_khach_hang()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_khach_hang>("prc_khach_hang").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_khach_hang>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_khach_hang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<prc_khach_hang> GetById_prc_khach_hang(int ms_kh)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ms_kh", ms_kh);
                var _obj = _db.QueryProc<prc_khach_hang>("prc_khach_hang_byid", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_khach_hang>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<prc_khach_hang>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

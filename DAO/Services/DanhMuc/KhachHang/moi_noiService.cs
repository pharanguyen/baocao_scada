using DAO.Models.CommonModels;
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
    public class moi_noiService
    {
        public static ResultModel<List<moi_noi>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<moi_noi>>() { Data = _db.GetAll<moi_noi>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<moi_noi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<moi_noi> GetById(int ms_mnoi)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<moi_noi>(ms_mnoi);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<moi_noi>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<moi_noi>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(moi_noi entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_mnoi = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = objID };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(moi_noi entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<moi_noi>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_khachhang_moinoi>> GetAll_prc_khach_hang_moinoi()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_khachhang_moinoi>("prc_khach_hang_moinoi").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_khachhang_moinoi>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_khachhang_moinoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_khachhang_moinoi>> GetAll_prc_khach_hang_moinoi_by_ms_tuyen(int ms_tuyen)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("ms_tuyen", ms_tuyen);
                var _obj = _db.QueryProc<prc_khachhang_moinoi>("prc_khach_hang_moinoi_by_ms_tuyen", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_khachhang_moinoi>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_khachhang_moinoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }


    }
}

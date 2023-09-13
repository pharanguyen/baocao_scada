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
    public class khachhang_moinoiService
    {
        public static ResultModel<List<prc_khachhang_moinoi>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<prc_khachhang_moinoi>>() { Data = _db.GetAll<prc_khachhang_moinoi>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_khachhang_moinoi>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<prc_khachhang_moinoi> GetById(int ms_mnoi)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<prc_khachhang_moinoi>(ms_mnoi);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_khachhang_moinoi>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<prc_khachhang_moinoi>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(prc_khachhang_moinoi entity)
        {
            try
            {
                //var _db = new SqlHelper();
                //string sql = "Insert into tuyen_doc(ms_tuyen, ms_tql, ms_cky, mo_ta_tuyen, ms_bd) " +
                //                "values (" + entity.ms_kh + "," + entity.dia_chi_kh + ", " + entity.dien_thoai + ", '" + entity.ms_thue + "', " + entity.so_tai_khoan + ")";
                //DataTable dt1 = _db.ExecuteSQLDataTable(sql);
                //if (dt1 == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(prc_khachhang_moinoi entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<prc_khachhang_moinoi>(id) };
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
        public static ResultModel<prc_khachhang_moinoi> GetById_prc_khachhang_moinoi_byms_mnoi(int ms_mnoi)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ms_mnoi", ms_mnoi);
                var _obj = _db.QueryProc<prc_khachhang_moinoi>("prc_khachhang_moinoi_byms_mnoi", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_khachhang_moinoi>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<prc_khachhang_moinoi>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> GetMaxSTTLoTrinh(int ms_tuyen)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ms_tuyen", ms_tuyen);
                //var _obj = _db.ExcuteProc()
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }


    }
}

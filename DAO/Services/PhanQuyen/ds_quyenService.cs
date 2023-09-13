using DAO.Models.CommonModels;
using DAO.Models.PhanQuyen;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.PhanQuyen
{
    public static class ds_quyenService
    {

        public static ResultModel<List<ds_quyen>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ds_quyen>>() { Data = _db.GetAll<ds_quyen>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ds_quyen>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<Quyen>> GetAllDsQuyenToList()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<Quyen>("prc_DsQuyen").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<Quyen>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<Quyen>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(ds_quyen entity)
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
        public static void DeleteAll()
        {
            try
            {
                var _db = new SqlHelper();

                _db.ExcuteProc("prc_DeleteQuyenNotInNhomQuyenList");
            }
            catch (Exception ex)
            {

            }
        }
        public static ResultModel<List<prc_ds_quyen_by_nhomquyenlist>> GetAllDsQuyenTheoNhom(int thanhvien_id, int nhomquyen_id)
        {
            try
            {


                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_id", thanhvien_id);
                p.Add("@nhomquyen_id", nhomquyen_id);
                var _obj = _db.QueryProc<prc_ds_quyen_by_nhomquyenlist>("prc_ds_quyen_by_nhomquyenlist", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_ds_quyen_by_nhomquyenlist>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_ds_quyen_by_nhomquyenlist>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<ds_quyen>> prc_ds_quyen_theothanhvienId(int thanhvien_id)
        {
            try
            {


                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_id", thanhvien_id);
                var _obj = _db.QueryProc<ds_quyen>("prc_ds_quyen_theothanhvienId", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<ds_quyen>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ds_quyen>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

       
    }
}

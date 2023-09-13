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
    public class ds_phanquyenService
    {
        public static ResultModel<int?> Add(ds_phanquyen entity)
        {
            try
            {
                var _db = new SqlHelper();
                var obj = _db.InsertEntity(entity);
                if (obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = obj };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> DeleteByThanhVienId(int thanhvien_id)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_id", thanhvien_id);
                var _obj = _db.QueryProc<prc_ThanhVien_DanhSach>("prc_delete_phanquyen_bythanhvienid", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(ds_phanquyen entity)
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<int?>() { Data = _db.UpdateEntity(entity) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<ds_phanquyen> GetQuyenThanhVien(int thanhvien_id, string menu_name)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_id", thanhvien_id);
                p.Add("@menu_name", menu_name);
                //var _obj = _db.GetAllByWhereCondition<ds_phanquyen>("where thanhvien_id =@thanhvien_id and menu_name = '@menu_name'", p).ToList().FirstOrDefault();
                var _obj = _db.QueryProc<ds_phanquyen>("prc_ds_quyen_theothanhvienId_va_menu_name", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<ds_phanquyen>() { Data = _obj };

            }
            catch (Exception ex) { return new ResultModel<ds_phanquyen>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

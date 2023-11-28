using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.PhanQuyen
{
    public class ds_thanhvienService
    {
        public static ResultModel<List<ds_thanhvien>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ds_thanhvien>>() { Data = _db.GetAll<ds_thanhvien>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ds_thanhvien>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_ThanhVien_DanhSach>> GetAllPlus()
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                return new ResultModel<List<prc_ThanhVien_DanhSach>>() { Data = _db.QueryProc<prc_ThanhVien_DanhSach>("prc_ThanhVien_DanhSach", p).ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_ThanhVien_DanhSach>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        

        public static ResultModel<prc_ThanhVien_DanhSach> GetById(int thanhvien_id)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_id", thanhvien_id);
                var _obj = _db.QueryProc<prc_ThanhVien_DanhSach>("prc_ThanhVien_DanhSach_ByThanhVienId", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_ThanhVien_DanhSach>() { Data = _obj };

            }
            catch (Exception ex) { return new ResultModel<prc_ThanhVien_DanhSach>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_ThanhVien_DanhSach>> GetThanhVienByTenDangNhap(string thanhvien_name)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_name", thanhvien_name);
                var _obj = _db.QueryProc<prc_ThanhVien_DanhSach>("prc_ds_thanhvien_by_thanhvien_name", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_ThanhVien_DanhSach>>() { Data = _obj };

            }
            catch (Exception ex) { return new ResultModel<List<prc_ThanhVien_DanhSach>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Add(ds_thanhvien entity)
        {
            try
            {
                var _db = new SqlHelper();
                // thêmthành viên
                var objId = _db.InsertEntity(entity);
                entity.thanhvien_id = (int)objId;
                if (objId == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = objId };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }

        }
        public static ResultModel<int?> Update(ds_thanhvien entity)
        {
            try
            {
                var _db = new SqlHelper();
                //update 1 bản ghi objId =1
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<ds_thanhvien>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<ds_thanhvien> GetByUserName(string TenDangNhap, string MatKhau)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@tendangnhap", TenDangNhap);
                p.Add("@matkhau", MatKhau);
                return new ResultModel<ds_thanhvien>() { Data = _db.GetAllByWhereCondition<ds_thanhvien>("where tendangnhap=@tendangnhap AND matkhau=@matkhau", p).FirstOrDefault() };
            }
            catch (Exception ex) { return new ResultModel<ds_thanhvien>() { isThanhCong = false, ThongBao = ex.Message }; }

        }
        public static ResultModel<ds_thanhvien> ChangePassword(int thanhvien_id, string MatKhau)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@thanhvien_id", thanhvien_id);
                p.Add("@new_password", MatKhau);
                var _obj = _db.QueryProc<ds_thanhvien>("ChangePassword", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<ds_thanhvien>() { Data = _obj };
                
            }
            catch (Exception ex) { return new ResultModel<ds_thanhvien>() { isThanhCong = false, ThongBao = ex.Message }; }

        }
    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.KhachHang;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class ThongSoTramService
    {
         public static ResultModel<List<ThongSo_Tram>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ThongSo_Tram>>() { Data = _db.GetAll<ThongSo_Tram>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ThongSo_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<ThongSo_Tram> GetById(int ms_mnoi)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<ThongSo_Tram>(ms_mnoi);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<ThongSo_Tram>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<ThongSo_Tram>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(ThongSo_Tram entity)
        {
            try
            {
                //var _db = new SqlHelper();
                //string sql = "Insert into tuyen_doc(Id_ChiNhanh, ms_tql, ms_cky, mo_ta_tuyen, ms_bd) " +
                //                "values (" + entity.ms_kh + "," + entity.dia_chi_kh + ", " + entity.dien_thoai + ", '" + entity.ms_thue + "', " + entity.so_tai_khoan + ")";
                //DataTable dt1 = _db.ExecuteSQLDataTable(sql);
                //if (dt1 == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(ThongSo_Tram entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<ThongSo_Tram>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_ThongSo_Tram>> GetAll_prc_ThongSo_Tram()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_ThongSo_Tram>("prc_ThongSo_Tram").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_ThongSo_Tram>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_ThongSo_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<ThongSo_Tram> GetById_ThongSo_Tram_byms_mnoi(int ms_mnoi)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ms_mnoi", ms_mnoi);
                var _obj = _db.QueryProc<ThongSo_Tram>("ThongSo_Tram_byms_mnoi", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<ThongSo_Tram>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<ThongSo_Tram>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> GetMaxSTTLoTrinh(int Id_ChiNhanh)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@Id_ChiNhanh", Id_ChiNhanh);
                //var _obj = _db.ExcuteProc()
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
       
        public static ResultModel<List<prc_ThongSo_Tram>> GetAll_prc_ThongSo_Tram_by_Id_Tram(int Id_ChiNhanh , int Id_Tram)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("Id_ChiNhanh", Id_ChiNhanh);
                p.Add("Id_Tram", Id_Tram);
                var _obj = _db.QueryProc<prc_ThongSo_Tram>("prc_ThongSo_Tram_by_Id_Tram", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_ThongSo_Tram>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_ThongSo_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_ThongSo_Tram>> GetAll_prc_ThongSo_Tram_by_Id_Tram(int Id_Tram)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("Id_Tram", Id_Tram);
                var _obj = _db.QueryProc<prc_ThongSo_Tram>("prc_ThongSo_Tram_by_Id_Tram", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_ThongSo_Tram>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_ThongSo_Tram>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

    }
    
}

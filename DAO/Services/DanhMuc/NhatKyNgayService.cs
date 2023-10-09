using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class NhatKyNgayService
    {
        public static ResultModel<List<prc_Nhat_Ky_Ngay>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { Data = _db.GetAll<prc_Nhat_Ky_Ngay>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_Nhat_Ky_Ngay>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { Data = _db.GetAll<prc_Nhat_Ky_Ngay>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<prc_Nhat_Ky_Ngay> GetById(int Id)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<prc_Nhat_Ky_Ngay>(Id);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_Nhat_Ky_Ngay>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<prc_Nhat_Ky_Ngay>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        
        

        public static ResultModel<List<prc_Nhat_Ky_Ngay>> GetAll_prc_Nhat_Ky_Ngay()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_Nhat_Ky_Ngay>("prc_Nhat_Ky_Ngay").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_Nhat_Ky_Ngay>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<prc_Nhat_Ky_Ngay>("prc_Nhat_Ky_Ngay_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Ngay>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}


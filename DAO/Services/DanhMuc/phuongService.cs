using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc
{
    public class phuongService
    {
        public static ResultModel<List<phuong>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<phuong>>() { Data = _db.GetAll<phuong>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<phuong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        //public static ResultModel<List<prc_phuong>> GetToComBoPrcPhuong()
        //{
        //    try
        //    {
        //        var _db = new SqlHelper();
        //        return new ResultModel<List<prc_phuong>>() { Data = _db.ExcuteProc("prc_phuong"). };
        //    }
        //    catch (Exception ex) { return new ResultModel<List<prc_phuong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        //}
        public static ResultModel<List<phuong>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<phuong>>() { Data = _db.GetAll<phuong>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<phuong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<phuong> GetById(int ms_phuong)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<phuong>(ms_phuong);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<phuong>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<phuong>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<prc_phuong> GetById_prc_phuong(int ms_phuong)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@ms_phuong", ms_phuong);
                var _obj = _db.QueryProc<prc_phuong>("prc_GetById_prc_phuong", p).ToList().FirstOrDefault();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<prc_phuong>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<prc_phuong>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(phuong entity)
        {
            try
            {
                var _db = new SqlHelper();
                string sql = "Insert into phuong( ms_quan, ten_phuong, vi_tri) " +
                                "values (" + entity.ms_quan + ", '" + entity.ten_phuong + "' , '" + entity.vi_tri + "')";
                DataTable dt1 = _db.ExecuteSQLDataTable(sql);
                if (dt1 == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(phuong entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<phuong>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_phuong>> GetAll_prc_phuong()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_phuong>("prc_phuong").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_phuong>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_phuong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<quan>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<quan>("prc_quan_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<quan>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<quan>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_phuong>> GetPrcPhuongByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<prc_phuong>("prc_phuong_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_phuong>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<prc_phuong>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

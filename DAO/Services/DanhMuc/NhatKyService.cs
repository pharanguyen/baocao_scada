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
    public class NhatKyService
    {
        public static ResultModel<List<Nhat_Ky>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhat_Ky>>() { Data = _db.GetAll<Nhat_Ky>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Nhat_Ky>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhat_Ky>>() { Data = _db.GetAll<Nhat_Ky>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<Nhat_Ky> GetById(int Id)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<Nhat_Ky>(Id);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<Nhat_Ky>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<Nhat_Ky>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(Nhat_Ky entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.Id = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Update(Nhat_Ky entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<Nhat_Ky>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_Nhat_Ky>> Get_prc_Nhat_Ky(string Id_ChiNhanh, string Id_Tram, string Id_ThongSo, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("IdChiNhanh", Id_ChiNhanh);
                p.Add("IdTram", Id_Tram);
                p.Add("IdThongSo", Id_ThongSo);
                p.Add("StartDate", StartDate);
                p.Add("EndDate", EndDate);
                var _obj = _db.QueryProc<prc_Nhat_Ky>("prc_Nhat_Ky", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }


        public static ResultModel<List<prc_Nhat_Ky>> GetAll_prc_Nhat_Ky()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_Nhat_Ky>("prc_Nhat_Ky").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Nhat_Ky>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<Nhat_Ky>("Nhat_Ky_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<Nhat_Ky>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}


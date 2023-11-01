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
    public class NhatKyNamService
    {
        public static ResultModel<List<Nhat_Ky_Nam>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhat_Ky_Nam>>() { Data = _db.GetAll<Nhat_Ky_Nam>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky_Nam>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Nhat_Ky_Nam>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhat_Ky_Nam>>() { Data = _db.GetAll<Nhat_Ky_Nam>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky_Nam>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<Nhat_Ky_Nam> GetById(int Id)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<Nhat_Ky_Nam>(Id);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<Nhat_Ky_Nam>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<Nhat_Ky_Nam>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(Nhat_Ky_Nam entity)
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
        public static ResultModel<int?> Update(Nhat_Ky_Nam entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<Nhat_Ky_Nam>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prc_Nhat_Ky_Nam>> Get_prc_Nhat_Ky_Nam(string Id_ChiNhanh, string Id_Tram, string Id_ThongSo)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("IdChiNhanh", Id_ChiNhanh);
                p.Add("IdTram", Id_Tram);
                p.Add("IdThongSo", Id_ThongSo);
                var _obj = _db.QueryProc<prc_Nhat_Ky_Nam>("prc_Nhat_Ky_Nam", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky_Nam>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Nam>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }


        public static ResultModel<List<prc_Nhat_Ky_Nam>> GetAll_prc_Nhat_Ky_Nam()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_Nhat_Ky_Nam>("prc_Nhat_Ky_Nam").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky_Nam>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Nam>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Nhat_Ky_Nam>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<Nhat_Ky_Nam>("Nhat_Ky_Nam_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<Nhat_Ky_Nam>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky_Nam>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}


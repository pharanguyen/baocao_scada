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
    public class NhatKyThangService
    {
        public static ResultModel<List<Nhat_Ky_Thang>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhat_Ky_Thang>>() { Data = _db.GetAll<Nhat_Ky_Thang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky_Thang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Nhat_Ky_Thang>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Nhat_Ky_Thang>>() { Data = _db.GetAll<Nhat_Ky_Thang>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky_Thang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<Nhat_Ky_Thang> GetById(int Id)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<Nhat_Ky_Thang>(Id);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<Nhat_Ky_Thang>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<Nhat_Ky_Thang>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(Nhat_Ky_Thang entity)
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
        public static ResultModel<int?> Update(Nhat_Ky_Thang entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<Nhat_Ky_Thang>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_Nhat_Ky_Thang>> GetAll_prc_Nhat_Ky_Thang()
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_Nhat_Ky_Thang>("prc_Nhat_Ky_Thang").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky_Thang>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Thang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<prc_Nhat_Ky_Thang>> Get_prc_Nhat_Ky_Thang(int page, int take)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.QueryProc<prc_Nhat_Ky_Thang>("prc_Nhat_Ky_Thang").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prc_Nhat_Ky_Thang>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prc_Nhat_Ky_Thang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Nhat_Ky_Thang>> GetByKeyword(string keyWord)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("@keyWord", keyWord);
                var _obj = _db.QueryProc<Nhat_Ky_Thang>("Nhat_Ky_Thang_bykeyWord", p);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<Nhat_Ky_Thang>>() { Data = _obj.ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Nhat_Ky_Thang>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}


using DAO.Models.CommonModels;
using DAO.Models.DanhMuc.PhanLoaiDoiTuong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.PhanLoaiDoiTuong
{
    public  class ht_lap_datService
    {
        public static ResultModel<List<ht_lap_dat>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ht_lap_dat>>() { Data = _db.GetAll<ht_lap_dat>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ht_lap_dat>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<ht_lap_dat>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<ht_lap_dat>>() { Data = _db.GetAll<ht_lap_dat>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<ht_lap_dat>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<ht_lap_dat> GetById(int ms_ht_lap_dat)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<ht_lap_dat>(ms_ht_lap_dat);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<ht_lap_dat>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<ht_lap_dat>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(ht_lap_dat entity)
        {
            try
            {
                var _db = new SqlHelper();
                var objID = _db.InsertEntity(entity);
                entity.ms_ht_lap_dat = (int)objID;
                if (objID == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<int?>() { Data = 1 };
            }
            catch (Exception ex)
            { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<int?> Update(ht_lap_dat entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<ht_lap_dat>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}

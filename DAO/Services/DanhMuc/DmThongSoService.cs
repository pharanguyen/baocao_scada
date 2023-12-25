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
  public class DmThongSoService
    {
        public static ResultModel<List<Dm_ThongSo>> GetToComBo()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Dm_ThongSo>>() { Data = _db.GetAll<Dm_ThongSo>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_ThongSo>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prcThongSo>> GetToComBobyIdThongSo(int? Id_ChiNhanh)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("IdChiNhanh", Id_ChiNhanh);

                var _obj = _db.QueryProc<prcThongSo>("[prc_GetComboThongSo]", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prcThongSo>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prcThongSo>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prcThongSo>> GetComboThongSoMutiTram(int[] Id_Tram)
        {
            try
            {
                var _db = new SqlHelper();
                DynamicParameters p = new DynamicParameters();
                p.Add("IdTram", string.Join(',', Id_Tram));

                var _obj = _db.QueryProc<prcThongSo>("prc_GetComboThongSoMutiTram", p).ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prcThongSo>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prcThongSo>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<List<prcThongSo>> GetToComBobyIdThongSoAll()
        {
            try
            {
                var _db = new SqlHelper();

                var _obj = _db.QueryProc<prcThongSo>("[prc_GetComboThongSoAll]").ToList();
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<List<prcThongSo>>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<List<prcThongSo>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<List<Dm_ThongSo>> GetAll()
        {
            try
            {
                var _db = new SqlHelper();
                return new ResultModel<List<Dm_ThongSo>>() { Data = _db.GetAll<Dm_ThongSo>().ToList() };
            }
            catch (Exception ex) { return new ResultModel<List<Dm_ThongSo>>() { isThanhCong = false, ThongBao = ex.Message }; }
        }

        public static ResultModel<Dm_ThongSo> GetById(int ms_bd)
        {
            try
            {
                var _db = new SqlHelper();
                var _obj = _db.GetSingleEntityById<Dm_ThongSo>(ms_bd);
                if (_obj == null) throw new Exception(_db.LoiNgoaiLe);
                return new ResultModel<Dm_ThongSo>() { Data = _obj };
            }
            catch (Exception ex) { return new ResultModel<Dm_ThongSo>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
        public static ResultModel<int?> Add(Dm_ThongSo entity)
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

        public static ResultModel<int?> Update(Dm_ThongSo entity)
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
                return new ResultModel<int?>() { Data = _db.DeleteEntityById<Dm_ThongSo>(id) };
            }
            catch (Exception ex) { return new ResultModel<int?>() { isThanhCong = false, ThongBao = ex.Message }; }
        }
    }
}


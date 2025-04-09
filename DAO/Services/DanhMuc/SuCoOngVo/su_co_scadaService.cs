using DAO.Models.DanhMuc.SuCoOngVo;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using static DAO.Models.DanhMuc.SuCoOngVo.SuCoScadaViewModel;

namespace DAO.Services.DanhMuc.SuCoOngVo
{
    public class su_co_scadaService
    {
        public static List<SuCoScadaViewModel> GetAll(DateTime tuNgay, DateTime denNgay, List<int> idChiNhanh, List<int> idDHK)
        {
            var db = new SqlHelper();

            string sql = @"
SELECT 
    s.Id,
    s.IdDHK,
    s.IdChiNhanh,
    d.ten_dhk,
    s.TenTQL,
    s.ThongTinSuCo,
    s.NgayXayRa,
    s.NgayKiemTra,
    s.XuLy_QLDiaBan,
    s.XuLy_CNTT,
    s.XuLy_XNDH,
    s.NgayHoanThanh,
    s.KetQua,
    s.NguyenNhan,
    s.ChiSo_DH,
    s.ChiSo_HT
FROM SuCoScada s
LEFT JOIN [baocao_scada_db].[dbo].[ThongSo_Tram] t ON s.IdChiNhanh = t.Id_ChiNhanh AND s.IdDHK = t.ms_dhk
LEFT JOIN [baocao_scada_db].[dbo].[dh_khoi] d ON t.ms_dhk = d.ms_dhk
WHERE 
    
    s.IdChiNhanh IN @IdChiNhanh
";

            if (idDHK != null && idDHK.Any())
            {
                sql += " AND s.IdDHK IN @IdDHK";
            }

            sql += " ORDER BY s.NgayXayRa DESC";

            var parameters = new DynamicParameters();
            parameters.Add("@TuNgay", tuNgay);
            parameters.Add("@DenNgay", denNgay);
            parameters.Add("@IdChiNhanh", idChiNhanh);

            if (idDHK != null && idDHK.Any())
            {
                parameters.Add("@IdDHK", idDHK);
            }

            return db.ExecQueryData<SuCoScadaViewModel>(sql, parameters, 1)?.ToList() ?? new();
        }


        public static bool Insert(SuCoScadaViewModel item)
        {
            var db = new SqlHelper();
            string sql = @"
        INSERT INTO SuCoScada (
            IdChiNhanh, IdDHK, TenTQL, ThongTinSuCo,
            NgayXayRa, NgayKiemTra,
            XuLy_QLDiaBan, XuLy_CNTT, XuLy_XNDH,
            NgayHoanThanh, KetQua, NguyenNhan,
            ChiSo_DH, ChiSo_HT
        )
        VALUES (
            @IdChiNhanh, @IdDHK, @TenTQL, @ThongTinSuCo,
            @NgayXayRa, @NgayKiemTra,
            @XuLy_QLDiaBan, @XuLy_CNTT, @XuLy_XNDH,
            @NgayHoanThanh, @KetQua, @NguyenNhan,
            @ChiSo_DH, @ChiSo_HT
        )";

            return db.ExecQueryNonData(sql, item, 1) > 0;
        }


        public static bool Update(SuCoScadaViewModel item)
        {
            var db = new SqlHelper();
            string sql = @"
        UPDATE SuCoScada SET
            IdChiNhanh = @IdChiNhanh,
            IdDHK = @IdDHK,
            TenTQL = @TenTQL,
            ThongTinSuCo = @ThongTinSuCo,
            NgayXayRa = @NgayXayRa,
            NgayKiemTra = @NgayKiemTra,
            XuLy_QLDiaBan = @XuLy_QLDiaBan,
            XuLy_CNTT = @XuLy_CNTT,
            XuLy_XNDH = @XuLy_XNDH,
            NgayHoanThanh = @NgayHoanThanh,
            KetQua = @KetQua,
            NguyenNhan = @NguyenNhan,
            ChiSo_DH = @ChiSo_DH,
            ChiSo_HT = @ChiSo_HT
        WHERE Id = @Id";

            return db.ExecQueryNonData(sql, item, 1) > 0;
        }

        public static bool Delete(int id)
        {
            var db = new SqlHelper();
            string sql = "DELETE FROM SuCoScada WHERE Id = @Id";
            return db.ExecQueryNonData(sql, new { Id = id }, 1) > 0;
        }


        public static SuCoScadaViewModel GetById(int id)
        {
            var db = new SqlHelper();
            string sql = "SELECT * FROM SuCoScada WHERE Id = @Id";
            return db.ExecQueryData<SuCoScadaViewModel>(sql, new { Id = id }, 1)?.FirstOrDefault();
        }


    }
}

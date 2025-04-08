
using DAO.Models.DanhMuc.SuCoOngVo;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.BaoCao
{
    public class SuCoOngVoService
    {
        public static List<SuCo_OngVo> GetBaoCaoOngVo(DateTime tuNgay, DateTime denNgay, List<int> idChiNhanh, List<int> idDHK )
        {
            var db = new SqlHelper();

            string sql = @"
SELECT 
    sc.Id,
    sc.NgayBaoCao,
    sc.IdChiNhanh,
    sc.IdDHK,
    sc.NgayTimKiem,
    sc.NgaySuaChua,
    sc.SoDiemVo,
    sc.MoTa,
    sc.TieuThu13_Truoc,
    sc.TieuThu13_Sau,
    sc.TieuThu07_Truoc,
    sc.TieuThu07_Sau,
    sc.GhiChu,
    sc.CreatedAt,
    dhk.Tendongho AS ten_dhk  -- 👈 JOIN để lấy tên DHK
FROM SuCoOngVo sc
LEFT JOIN [dbo].[Dm_dhk] dhk ON sc.IdDHK = dhk.Id_dongho
WHERE sc.NgayBaoCao BETWEEN @TuNgay AND @DenNgay
    AND sc.IdChiNhanh IN @IdChiNhanh";

            if (idDHK != null && idDHK.Any())
            {
                sql += " AND sc.IdDHK IN @IdDHK";
            }

            sql += " ORDER BY sc.NgayBaoCao DESC";

            var parameters = new DynamicParameters();
            parameters.Add("@TuNgay", tuNgay.Date);
            parameters.Add("@DenNgay", denNgay.Date);
            parameters.Add("@IdChiNhanh", idChiNhanh);

            if (idDHK != null && idDHK.Any())
            {
                parameters.Add("@IdDHK", idDHK);
            }

            return db.ExecQueryData<SuCo_OngVo>(sql, parameters, 2)?.ToList() ?? new();
        }


        public static bool Insert(SuCo_OngVo item)
        {
            var db = new SqlHelper();

            string sql = @"
INSERT INTO SuCoOngVo (
    NgayBaoCao, IdChiNhanh, IdDHK,
    NgayTimKiem, NgaySuaChua,
    SoDiemVo, MoTa,
    TieuThu13_Truoc, TieuThu13_Sau,
    TieuThu07_Truoc, TieuThu07_Sau,
    GhiChu, CreatedAt
)
VALUES (
    @NgayBaoCao, @IdChiNhanh, @IdDHK,
    @NgayTimKiem, @NgaySuaChua,
    @SoDiemVo, @MoTa,
    @TieuThu13_Truoc, @TieuThu13_Sau,
    @TieuThu07_Truoc, @TieuThu07_Sau,
    @GhiChu, GETDATE()
)";
            return db.ExecQueryNonData(sql, item, 2) > 0;
        }

        public static bool Update(SuCo_OngVo item)
        {
            var db = new SqlHelper();

            string sql = @"
UPDATE SuCoOngVo SET
    NgayBaoCao = @NgayBaoCao,
    IdChiNhanh = @IdChiNhanh,
    IdDHK = @IdDHK,
    NgayTimKiem = @NgayTimKiem,
    NgaySuaChua = @NgaySuaChua,
    SoDiemVo = @SoDiemVo,
    MoTa = @MoTa,
    TieuThu13_Truoc = @TieuThu13_Truoc,
    TieuThu13_Sau = @TieuThu13_Sau,
    TieuThu07_Truoc = @TieuThu07_Truoc,
    TieuThu07_Sau = @TieuThu07_Sau,
    GhiChu = @GhiChu
WHERE Id = @Id
";
            return db.ExecQueryNonData(sql, item, 2) > 0;
        }

        public static bool Delete(int id)
        {
            var db = new SqlHelper();

            string sql = "DELETE FROM SuCoOngVo WHERE Id = @Id";

            return db.ExecQueryNonData(sql, new { Id = id }, 2) > 0;
        }
    }
}

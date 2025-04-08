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
        public static List<SuCoScadaViewModel> GetAll(DateTime tuNgay, DateTime denNgay)
        {
            var db = new SqlHelper();
            string sql = @"
       SELECT 
    s.Id,
    s.IdDHK,
    d.Tendongho AS TenDHK,  -- Đặt alias để dùng trong ViewModel
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
LEFT JOIN Dm_dhk d ON s.IdDHK = d.Id_dongho
WHERE s.NgayXayRa BETWEEN @TuNgay AND @DenNgay
ORDER BY s.NgayXayRa DESC
";

            var parameters = new DynamicParameters();
            parameters.Add("@TuNgay", tuNgay.Date);
            parameters.Add("@DenNgay", denNgay.Date);

            return db.ExecQueryData<SuCoScadaViewModel>(sql, parameters, 2)?.ToList() ?? new();
        }


        public static bool Insert(SuCoScadaViewModel item)
        {
            var db = new SqlHelper();
            string sql = @"
                INSERT INTO SuCoScada (
                    IdDHK, TenTQL, ThongTinSuCo,
                    NgayXayRa, NgayKiemTra,
                    XuLy_QLDiaBan, XuLy_CNTT, XuLy_XNDH,
                    NgayHoanThanh, KetQua, NguyenNhan,
                    ChiSo_DH, ChiSo_HT
                )
                VALUES (
                    @IdDHK, @TenTQL, @ThongTinSuCo,
                    @NgayXayRa, @NgayKiemTra,
                    @XuLy_QLDiaBan, @XuLy_CNTT, @XuLy_XNDH,
                    @NgayHoanThanh, @KetQua, @NguyenNhan,
                    @ChiSo_DH, @ChiSo_HT
                )";

            return db.ExecQueryNonData(sql, item, 2) > 0;
        }

        public static bool Update(SuCoScadaViewModel item)
        {
            var db = new SqlHelper();
            string sql = @"
                UPDATE SuCoScada SET
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
                WHERE Id = @Id"; // ⚠️ Sử dụng Id mới là khóa chính

            return db.ExecQueryNonData(sql, item, 2) > 0;
        }

        public static bool Delete(int id)
        {
            var db = new SqlHelper();
            string sql = "DELETE FROM SuCoScada WHERE Id = @Id";
            return db.ExecQueryNonData(sql, new { Id = id }, 2) > 0;
        }

        public static SuCoScadaViewModel GetById(int id)
        {
            var db = new SqlHelper();
            string sql = "SELECT * FROM SuCoScada WHERE Id = @Id";
            return db.ExecQueryData<SuCoScadaViewModel>(sql, new { Id = id }, 2)?.FirstOrDefault();
        }
    }
}

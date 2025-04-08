using DAO.Models.DanhMuc.CanhBao;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.CanhBao
{
    public class canh_bao_ap_luc_tramService
    {
        public static List<CanhBaoApLucViewModel> GetCanhBaoApLuc(DateTime tuNgay, DateTime denNgay, List<int> idTram)
        {
            var db = new SqlHelper();

            // ✅ Sử dụng trực tiếp danh sách truyền vào bằng cú pháp Dapper hỗ trợ
            string sql = @"
        SELECT 
            nt.id AS Id,
            nt.Ngaythangnam AS NgayThangNam,
            nt.giophutgiay AS GioPhutGiay,
            nt.Id_tram AS Id_Tram,
            t.Tentram AS TenTram,
            nt.Giatri AS GiaTri,
            cb.ApLucMin,
            cb.ApLucMax
        FROM data_nhapso.dbo.nhatkyapluc nt
        INNER JOIN data_nhapso.dbo.DMtram t ON nt.Id_tram = t.Id_tram
        LEFT JOIN data_nhapso.dbo.CanhBao_ApLuc cb ON cb.Id_Tram = nt.Id_tram
        WHERE nt.Ngaythangnam BETWEEN @TuNgay AND @DenNgay
            AND nt.Id_tram IN @IdTram
        ORDER BY nt.Ngaythangnam DESC, nt.giophutgiay DESC";

            var parameters = new DynamicParameters();
            parameters.Add("@TuNgay", tuNgay.Date);
            parameters.Add("@DenNgay", denNgay.Date);

            // ✅ Quan trọng: Dapper hỗ trợ danh sách IN với IEnumerable
            parameters.Add("@IdTram", idTram);

            return db.ExecQueryData<CanhBaoApLucViewModel>(sql, parameters, 2)?.ToList() ?? new();
        }

    }
}

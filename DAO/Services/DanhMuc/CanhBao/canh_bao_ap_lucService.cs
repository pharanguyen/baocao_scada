using DAO.Models.DanhMuc.CanhBao;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.CanhBao
{
    public class canh_bao_ap_lucService
    {
        public static List<CanhBao_ApLuc> GetAll()
        {
            var db = new SqlHelper();
            string sql = "SELECT * FROM CanhBao_ApLuc";
            return db.ExecQueryData<CanhBao_ApLuc>(sql, null, 2)?.ToList() ?? new();
        }

        public static void Save(CanhBao_ApLuc model)
        {
            var db = new SqlHelper();
            string sql = @"
IF EXISTS (SELECT 1 FROM CanhBao_ApLuc WHERE Id_Tram = @Id_Tram)
    UPDATE CanhBao_ApLuc
    SET ApLucMin = @ApLucMin, ApLucMax = @ApLucMax, NgayCapNhat = GETDATE()
    WHERE Id_Tram = @Id_Tram
ELSE
    INSERT INTO CanhBao_ApLuc (Id_Tram, ApLucMin, ApLucMax)
    VALUES (@Id_Tram, @ApLucMin, @ApLucMax)";

            var p = new DynamicParameters(model);
            db.ExecCommand(sql, p, 2);
        }
    }
}

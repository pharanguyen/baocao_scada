using DAO.Models.DanhMuc.CanhBao;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Services.DanhMuc.CanhBao
{
    public class canh_bao_tramService
    {
        public static List<CanhBao_Tram> GetAll()
        {
            var db = new SqlHelper();
            string sql = "SELECT * FROM CanhBao_Tram";
            return db.ExecQueryData<CanhBao_Tram>(sql, null, 4)?.ToList() ?? new();
        }

        public static void Save(CanhBao_Tram model)
        {
            var db = new SqlHelper();
            string sql = @"
IF EXISTS (SELECT 1 FROM CanhBao_Tram WHERE IdTram = @IdTram)
    UPDATE CanhBao_Tram
    SET MucCanhBao = @MucCanhBao, TyLeCanhBao = @TyLeCanhBao, NgayCapNhat = GETDATE()
    WHERE IdTram = @IdTram
ELSE
    INSERT INTO CanhBao_Tram (IdTram, MucCanhBao, TyLeCanhBao)
    VALUES (@IdTram, @MucCanhBao, @TyLeCanhBao)";

            var p = new DynamicParameters(model);
            db.ExecCommand(sql, p, 4); // ✅ sửa chỗ này
        }

    }

}

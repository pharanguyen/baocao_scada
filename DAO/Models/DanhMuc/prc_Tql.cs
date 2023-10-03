
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class prc_Tql
    {
      
        public prc_Tql() { }
        public int Id { get; set; }
        public string TenTql { get; set; }
        public string Ma { get; set; }
        public string Dia_Chi { get; set; }
        public int Id_ChiNhanh { get; set; }
        public string TenChiNhanh { get; set; }
        public int Stt { get; set; }
    }
}

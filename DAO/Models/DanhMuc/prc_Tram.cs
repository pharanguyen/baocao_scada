using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
   public class prc_Tram
    {
        public prc_Tram() { }
        public int Id { get; set; }
        public string TenTram { get; set; }
        public string Ma { get; set; }
        public string Dia_Chi { get; set; }
        public int Id_Tql { get; set; }
        public string TenTql { get; set; }
        public bool NhaMay_DuongOng { get; set; }
        public string Toa_Do { get; set; }
        public int Stt { get; set; }
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class Dm_ChiNhanh
    {
        [Key]
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public string Dia_Chi { get; set; }
        public int Stt { get; set; }
    }
}

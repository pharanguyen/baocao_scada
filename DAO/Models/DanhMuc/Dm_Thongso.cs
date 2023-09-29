
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.DanhMuc
{
    public class Dm_ThongSo
    {
        [Key]
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public string Don_Vi { get; set; }
        public bool Cong_Tong { get; set; }
        public int Stt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models.CommonModels
{
    public class Paging
    {
        public int SoBanGhi { get; set; }
        private int _sotrang = 1;
        public int SoTrang { get { return _sotrang == 0 ? 1 : _sotrang; } set { _sotrang = value; } }
        public int TrangHienTai { get; set; }
    }
}

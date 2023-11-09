using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class ListThongSoViewModel
    {
        public int id_Tram { get; set; }
        public List<ThongSoTram> listThongSo { get; set; }
    }
public class ThongSoTram
{
    public string TenThongSo { get; set; }
    }
}

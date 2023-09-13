using System.Collections;
using System.Web;
using System.Drawing;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Data;
using DAO;
using DAO.Models.PhanQuyen;
using Microsoft.AspNetCore.Components;
using DAO.Models.CommonModels;
using DAO.Services.PhanQuyen;

namespace ERPProject.Libs
{
    public class clsGlobal
    {
       
        public clsGlobal() { }
        /*'Lay cap cua mot node
        Manode: Ma cua node can tim cap: 001002000
        Tree_Struct: Chuoi chua cau truc Node: 333
        Vidu: Manode="001002000"
        Tree_Struct="333" cau truc cay
        g_CapNode=2' cay cap 2*/
        public int g_CapNode(string MaNode, string Tree_Struct)
        {
            int LenSubKey;
            int Sum = 0;
            int kq = 0;
            if (MaNode == "")
            {
                return -1;
            }
            for (int i = Tree_Struct.Length; i >= 1; i += -1)
            {
                LenSubKey = Convert.ToInt32(Tree_Struct.Substring(Tree_Struct.Length - i, 1));
                Sum += LenSubKey;
                if (MaNode.Substring(MaNode.Length - Sum, Sum) != PublicVariable.g_String_0.Substring(0, Sum))
                {
                    kq = i;
                    break;
                }
            }
            return kq;
        }

        //lay thong tin ve quyen truy cap
        //dau vao la ten menu
        //dau ra g_LayQuyenTruycap="xem,them,sua,xoa"
        //vi du:g_LayQuyenTruycap="1,0,1,1"
        //1:ok;0:not ok
        public string g_LayQuyenTruycap(string sMenuName)
        {
            var _db = new SqlHelper();
            string kq = "";
            if (sMenuName == "")
            {
                return kq;
            }
            string sSQL = "select pq.them, pq.sua,pq.xoa from ds_phanquyen pq inner join ds_quyen ds " +
                " on pq.dsquyen_id=ds.dsquyen_id " +
                " where (pq.thanhvien_id='" + PublicVariable.g_THANHVIEN_ID + "') and (ds.menu_name='" + sMenuName + "') ";
            DataSet ds = _db.ExecuteSQLDataSet(sSQL);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    kq = "1," + ((ds.Tables[0].Rows[0]["them"].ToString() == "False") ? "0," : "1,") + ((ds.Tables[0].Rows[0]["sua"].ToString() == "False") ? "0," : "1,") + ((ds.Tables[0].Rows[0]["xoa"].ToString() == "False") ? "0" : "1");
                }
            }
            return kq;
        }
        public string g_GetDatatoValue(object Database_Data, string sDefault)
        {
            string kq;
            if (Database_Data == null)
            {
                kq = sDefault;
            }
            else
            {
                if (Database_Data.ToString() == "" || Database_Data.ToString() == "{}")
                {
                    kq = sDefault;
                }
                else
                {
                    kq = Database_Data.ToString();
                }
            }
            return kq;
        }

        

    }
}

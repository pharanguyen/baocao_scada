using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAO.Models
{
    public class DM_Menu
    {
        [Key]
        [IgnoreInsert]
        [IgnoreUpdate]

        public int ID { get; set; }
        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string Link { get; set; }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public string AreaName { get; set; }

        public string ActiveLink { get; set; }

        public string ParentCode { get; set; }

        public float? AZ { get; set; }

        public string MenuIcon { get; set; }

        public string GroupName { set; get; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPProject.Services
{
    public class AppDataScoped
    {

 

        public string LayoutTitle { get; set; }
        public string LayoutSubTitle { get; set; }
 

        public AppDataScoped()
        {
            LayoutTitle = "...";
            LayoutSubTitle = "";
        }
        //2. Khai bao bien loading trong AppDataScoped
        public Shared.LoadingPanel loadingPanel;

        public void AddTieuDe(string title,string subTitle = "")
        {
            LayoutTitle = title;
            LayoutSubTitle = subTitle;
            NotifyDataChanged();
        }

        public event Action OnChange;
        private void NotifyDataChanged() => OnChange?.Invoke();

     
    }

  
}

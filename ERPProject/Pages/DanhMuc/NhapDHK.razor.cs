using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAO.Models.DanhMuc;
using DAO.Services.DanhMuc;
using ERPProject.Shared.Combobox;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc
{
    public class NhomDHKComponentBase : ComponentBase
    {
       // [Inject] protected NhomDhkService NhomDhkService { get; set; }

        // Properties
        protected List<Nhom_dh_khoi> ListNhomDHK { get; set; } = new();
        protected string SelectedNhomDHK { get; set; }
        protected string Thang { get; set; } = DateTime.Now.Month.ToString();
        protected string Nam { get; set; } = DateTime.Now.Year.ToString();
        protected List<string> Gios { get; set; } = new() { "07:00", "08:00", "09:00", "10:00" };
        protected string SelectedGio { get; set; } = "07:00";
        protected string SortOption { get; set; } = "asc";
        protected List<DHKModel> DhkData { get; set; } = new();

        protected CbNhomDHK CbNhomDhk;
        protected CbMultiChiNhanh CbNhomDhk1;
        protected CbMultiDHK CbMultiDHK;

        protected override async Task OnInitializedAsync()
        {
            await LoadNhomDHKDataAsync();
        }

        public async Task LoadNhomDHKDataAsync()
        {
           // ListNhomDHK = await NhomDhkService.GetAllNhomDHKAsync();
        }

        protected void ValueChangeHandler(int[] args)
        {
            if (args != null)
            {
                SelectedNhomDHK = string.Join(",", args.Select(arg => arg.ToString()));
            }
        }

        protected async Task LoadData()
        {
            // Mock implementation for data
            DhkData = Enumerable.Range(1, 10).Select(i => new DHKModel
            {
                STT = i,
                TenDHK = $"Đồng hồ {i}",
                NgayDoc = DateTime.Now.ToShortDateString(),
                NhomDHK = "Nhóm 1",
                LoaiDHK = "Loại A",
                ChiSoCu1 = (i * 100).ToString(),
                ChiSoMoi1 = (i * 110).ToString(),
                SoTieuThu1 = (10 * i).ToString(),
                NgayDocCu2 = DateTime.Now.AddDays(-1).ToShortDateString(),
                NgayDocMoi2 = DateTime.Now.ToShortDateString(),
                MSDHK = $"MSDHK{i}"
            }).ToList();

            await Task.CompletedTask;
        }

        protected void SaveToBAR()
        {
            Console.WriteLine("Saved to BAR.");
        }

        protected void ExportToExcel()
        {
            Console.WriteLine("Exported to Excel.");
        }

        protected void CloseForm()
        {
            Console.WriteLine("Form closed.");
        }
    }
}

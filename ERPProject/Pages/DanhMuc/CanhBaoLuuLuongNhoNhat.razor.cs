using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Buttons;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Calendars;
using DAO.Models.DanhMuc;
using DAO.Services.DanhMuc;
using ERPProject.Services;
using ERPProject.Shared;
using DocumentFormat.OpenXml.EMMA;
using DAO.Services.PhanQuyen;
using DAO.Models.PhanQuyen;

namespace ERPProject.Pages.DanhMuc
{
    public class CanhBaoLuuLuongNhoNhatBase : ComponentBase
    {
        public List<ViewModelLuuLuongNhoNhat> listThongSoTram { get; set; } = new List<ViewModelLuuLuongNhoNhat>();
        protected SfGrid<ViewModelLuuLuongNhoNhat> gdv;
        protected ViewModelLuuLuongNhoNhat SelectedThongSo { get; set; } = new ViewModelLuuLuongNhoNhat();
        public DateTime FromDate { get; set; } = DateTime.Today.Date;
        ds_phanquyen _QSD = new ds_phanquyen();
        
        public DateTime ToDate { get; set; } = DateTime.Today.Date.AddDays(1).AddTicks(-1);
       
        public List<int> SelectedNodeIndex { get; set; } = new List<int>();

        [Inject]
        protected ToastService toastService { get; set; }
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        //protected override async Task OnInitializedAsync()
        //{
        //    await LoadDefaultData();
        //}
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var _tokenE = await localStorage.GetItemAsync<string>("Token");
                var _token = MsUtils.Encryption.DecryptString(_tokenE);
                var strInfo = _token.Split("@MS@");

                if (strInfo.Length == 5)
                {
                    var rsModel = await Task.Run(() =>
                        ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "btnBC_LLDNN"));

                    _QSD = rsModel.Data;
                }

                await LoadDefaultData(); // hoặc LoadDefaultData nếu bạn rename lại tên
            }
            catch (Exception ex)
            {
                toastService.ShowDanger($"Lỗi khởi tạo: {ex.Message}");
            }
        }


        private async Task LoadDefaultData()
        {
            try
            {
                // Fetch data for chiNhanhIds from 1 to 13 for the current date
                string chiNhanhIds = string.Join(",", Enumerable.Range(1, 13));
                var rsModel = await LuuLuongNhoNhatService.GetLuuLuongNhoNhat(FromDate, ToDate, chiNhanhIds, "");

                if (rsModel.isThanhCong)
                {
                    listThongSoTram = rsModel.Data;
                }
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                }
            }
            catch (Exception ex)
            {
                toastService.ShowDanger($"An error occurred: {ex.Message}");
            }
        }
        public void OnRowDataBound(RowDataBoundEventArgs<ViewModelLuuLuongNhoNhat> args)
        {
            // Check conditions for styling rows
            if (args.Data.trangthaicanhbao > 0)
            {
                args.Row.AddClass(new string[] { "row-red" }); // Adds red-orange class
            }
            else
            {
                args.Row.AddClass(new string[] { "row-white" }); // Adds white class
            }
        }

       

        protected async Task ReloadData()
        {
            try
            {
                // Fetch data based on current date range
                string chiNhanhIds = string.Join(",", Enumerable.Range(1, 13));
                var rsModel = await LuuLuongNhoNhatService.GetLuuLuongNhoNhat(FromDate, ToDate, chiNhanhIds, "");

                if (rsModel.isThanhCong)
                {
                    listThongSoTram = rsModel.Data;
                }
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                }
            }
            catch (Exception ex)
            {
                toastService.ShowDanger($"An error occurred: {ex.Message}");
            }
        }

        protected void OnRowSelected(RowSelectEventArgs<ViewModelLuuLuongNhoNhat> args)
        {
            SelectedThongSo = args.Data ?? new ViewModelLuuLuongNhoNhat();
            StateHasChanged(); // Ensure UI updates immediately
        }

        public async Task ExportToExcel()
        {
            try
            {
                await gdv.ExportToExcelAsync(new Syncfusion.Blazor.Grids.ExcelExportProperties
                {
                    FileName = "ThongSoTram.xlsx"
                });
            }
            catch (Exception ex)
            {
                toastService.ShowDanger($"Export failed: {ex.Message}");
            }
        }

        public void onDoc()
        {
            // Implement logic for "Đọc" button
        }

        public void onDung()
        {
            // Implement logic for "Dừng" button
        }

        public void onSua()
        {
            // Implement logic for "Sửa" button
        }

        public void onGhiNhan()
        {
            // Implement logic for "Ghi nhận" button
        }
    }
}

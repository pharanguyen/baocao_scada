﻿using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using DAO.Services.DanhMuc;
using DAO.Services.PhanQuyen;
using ERPProject.Services;
using ERPProject.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;

namespace ERPProject.Pages.DanhMuc
{
    public  class BcNamBase : ComponentBase
    {
        public List<prc_Nhat_Ky_Nam> ListNhatKyNam { get; set; }
        //public List<to_quan_ly> ListNhatKyNam{ get; set; }
        protected Dm_Tram_CapNhat fCapNhat;
        ds_phanquyen _QSD = new ds_phanquyen();
        [Inject]
        AppDataScoped AppData { get; set; }
        protected FormXacNhan frmXacNhan;
        [Inject]
        protected ToastService toastService { get; set; }
        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        protected SfGrid<prc_Nhat_Ky_Nam> gdv;

        protected override void OnInitialized()
        {
            Task.Run(() =>
            {
                InvokeAsync(async () =>
                {
                    var _tokenE = await localStorage.GetItemAsync<string>("Token");
                    var _token = MsUtils.Encryption.DecryptString(_tokenE);
                    var strInfo = _token.Split("@MS@");
                    if (strInfo.Length == 5)
                    {
                        var rsModel = new ResultModel<ds_phanquyen>();
                        await Task.Run(() => { rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "btnBC_Nam"); });
                        _QSD = rsModel.Data;



                    }
                    onTaiLai();


                });
            });
            base.OnInitialized();
        }

        protected async void onTaiLai()
        {

            AppData.loadingPanel.show();
            var rsModel = new ResultModel<List<prc_Nhat_Ky_Nam>>();
            await Task.Run(() => { rsModel = NhatKyNamService.GetAll_prc_Nhat_Ky_Nam(); });
            if (rsModel.isThanhCong)
            {
                ListNhatKyNam = rsModel.Data;

                AppData.loadingPanel.hide();
            }
            else
            {
                AppData.loadingPanel.hide();
                toastService.ShowDanger(rsModel.ThongBao);
                return;
            }
            StateHasChanged();
        }

        protected void onThemMoi()
        {
             if (_QSD.them == false)
             {
                 toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                 return;
             }
            fCapNhat.isThemMoi = true;
            fCapNhat.TieuDe = "Thêm Trạm";
            fCapNhat.Show(0);
            onTaiLai();
        }
        protected void onCapNhat(int _ID)
        {
             if (_QSD.sua == false)
             {
                 toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                 return;
             }
            fCapNhat.TieuDe = "Cập nhật thông tin chi trạm";
            fCapNhat.isThemMoi = false;
            fCapNhat.Show(_ID);
            onTaiLai();
        }

        protected void XacNhanLuu(bool isLuuThanhCong)
        {
            if (isLuuThanhCong)
            {
                onTaiLai();
            }

            // StateHasChanged();
            //   gdv.Refresh();
        }
        public async Task onXuatExcel()
        {
            ExcelExportProperties excelExportProperties = new ExcelExportProperties();
            excelExportProperties.FileName = "Danh sach diem dung.xlsx";
            var selectedRecord = await gdv.GetSelectedRecordsAsync();
            if (selectedRecord.Count() > 0)
            {
                excelExportProperties.DataSource = selectedRecord;
            }
            else
            {
                excelExportProperties.DataSource = ListNhatKyNam;
            }
            await gdv.ExportToExcelAsync(excelExportProperties);
        }


        protected void onXoa(int _ID)
        {
            //check quyen xoa
              if (_QSD.xoa == false || _QSD.xoa == null)
              {
                  toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                  return;
              }
            frmXacNhan.Show("Xóa dòng được chọn ?", "300px", new System.Action(async () =>
            {
                AppData.loadingPanel.show();
                var rsModel = new ResultModel<int?>();
                await Task.Run(() => { rsModel = NhatKyNamService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }
    }
}

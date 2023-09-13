using DAO.Models.CommonModels;
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
    public class DuongBase : ComponentBase
    {
        public List<duong> listDuong { get; set; }
        protected Duong_CapNhat fCapNhat;
        ds_phanquyen _QSD = new ds_phanquyen();
        [Inject]
        AppDataScoped AppData { get; set; }
        protected FormXacNhan frmXacNhan;
        [Inject]
        protected ToastService toastService { get; set; }
        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        protected SfGrid<duong> gdv;
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
                        await Task.Run(() => { rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "mnuDM_Duong"); });
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
            var rsModel = new ResultModel<List<duong>>();
            await Task.Run(() => { rsModel = duongService.GetAll(); });
            if (rsModel.isThanhCong)
            {
                listDuong = rsModel.Data;
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
            fCapNhat.TieuDe = "Thêm Chi Nhánh";
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
            fCapNhat.TieuDe = "Cập nhật thông tin đường";
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
                await Task.Run(() => { rsModel = duongService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }
        public async Task onXuatExcel()
        {
            //if (!_QSD.XuatExcel)
            //{
            //    toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
            //    return;
            //}
            ExcelExportProperties excelExportProperties = new ExcelExportProperties();
            excelExportProperties.FileName = "Danh sach duong.xlsx";
            var selectedRecord = await gdv.GetSelectedRecordsAsync();
            if (selectedRecord.Count() > 0)
            {
                excelExportProperties.DataSource = selectedRecord;
            }
            else
            {
                excelExportProperties.DataSource = listDuong;
            }
            await gdv.ExportToExcelAsync(excelExportProperties);
        }
    }
}

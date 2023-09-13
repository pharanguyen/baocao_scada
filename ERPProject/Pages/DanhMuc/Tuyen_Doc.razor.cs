using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using DAO.Services.DanhMuc;
using DAO.Services.PhanQuyen;
using ERPProject.Services;
using ERPProject.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor;

namespace ERPProject.Pages.DanhMuc
{
    public class Tuyen_DocBase : ComponentBase
    {
        public List<prc_tuyen_doc> listPrcTuyenDoc { get; set; }

        protected Tuyen_Doc_CapNhat fCapNhat;
        ds_phanquyen _QSD = new ds_phanquyen();
        [Inject]
        AppDataScoped AppData { get; set; }
        protected FormXacNhan frmXacNhan;
        [Inject]
        protected ToastService toastService { get; set; }
        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        protected SfGrid<prc_tuyen_doc> gdv;

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
                        await Task.Run(() => { rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "mnuDM_Tuyendoc"); });
                        _QSD = rsModel.Data;


                    }
                    onTaiLai();


                });
            });
            base.OnInitialized();
        }
        protected void onThemMoi()
        {
            if (_QSD.them == false)
            {
                toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                return;
            }
            fCapNhat.isThemMoi = true;
            fCapNhat.TieuDe = "Thêm Tuyến Đọc";
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
            fCapNhat.TieuDe = "Cập nhật thông tin tuyến đọc";
            fCapNhat.isThemMoi = false;
            fCapNhat.Show(_ID);
            onTaiLai();
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
                await Task.Run(() => { rsModel = tuyen_docService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }
        protected async void onTaiLai()
        {

            AppData.loadingPanel.show();
            var rsModel = new ResultModel<List<prc_tuyen_doc>>();
            await Task.Run(() => { rsModel = tuyen_docService.GetAll_prc_tuyen_doc(); });
            if (rsModel.isThanhCong)
            {
                listPrcTuyenDoc = rsModel.Data;
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
        protected void XacNhanLuu(bool isLuuThanhCong)
        {
            if (isLuuThanhCong)
            {
                onTaiLai();
            }

             StateHasChanged();
              // gdv.Refresh();
        }
        //xuat ra các dòng được chọn
        public async Task onXuatExcel()
        {
            //if (!_QSD.XuatExcel)
            //{
            //    toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
            //    return;
            //}
            ExcelExportProperties excelExportProperties = new ExcelExportProperties();
            excelExportProperties.FileName = "Danh sach tuyen doc.xlsx";
            var selectedRecord = await gdv.GetSelectedRecordsAsync();
            if (selectedRecord.Count() > 0)
            {
                excelExportProperties.DataSource = selectedRecord;
            }
            else
            {
                excelExportProperties.DataSource = listPrcTuyenDoc;
            }
            await gdv.ExportToExcelAsync(excelExportProperties);
        }

      
    }
}

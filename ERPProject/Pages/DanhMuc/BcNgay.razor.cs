﻿using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using DAO.Services.DanhMuc;
using DAO.Services.PhanQuyen;
using DAO.ViewModel;
using ERPProject.Services;
using ERPProject.Shared;
using ERPProject.Shared.Combobox;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;

namespace ERPProject.Pages.DanhMuc
{
    public class BcNgayBase : ComponentBase
    {


        public List<prc_Nhat_Ky_Ngay> ListNhatKyNgay { get; set; }
        public List<BaoCaoNgayViewModel> data { get; set; }

        public int take = 20;
        public int totalPages = 0;      
        public int curPage = 1;

        //public List<to_quan_ly> ListNhatKyNgay{ get; set; }
        protected Dm_Tram_CapNhat fCapNhat;
        ds_phanquyen _QSD = new ds_phanquyen();
        [Inject]
        AppDataScoped AppData { get; set; }
        protected FormXacNhan frmXacNhan;
        protected CbMultiChiNhanh CbChiNhanh;
        protected CbMultiTram CbTram;
        protected CbMultiThongSo CbThongSo;
        protected CbTime CbThoiGian;
        [Inject]
        protected ToastService toastService { get; set; }
        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
        protected SfGrid<prc_Nhat_Ky_Ngay> gdv;

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
                        await Task.Run(() => { rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "btnBC_Ngay"); });
                        _QSD = rsModel.Data;



                    }
                    //onTaiLai();


                });
            });
            base.OnInitialized();
        }

        protected async void onTaiLai()
        {


            AppData.loadingPanel.show();
            int[] Id_ChiNhanh = CbChiNhanh.Value ?? new int[0];
            int[] Id_Tram = CbTram.Value ?? new int[0];
            int[] Id_ThongSo = CbThongSo.Value ?? new int[0]; 

            var rsModel = new ResultModel<List<prc_Nhat_Ky_Ngay>>();
            await Task.Run(() => { rsModel = NhatKyNgayService.Get_prc_Nhat_Ky_Ngay(string.Join(',', Id_ChiNhanh) , string.Join(',', Id_Tram), string.Join(',', Id_ThongSo)); });
            if (rsModel.isThanhCong)
            {
                ListNhatKyNgay = rsModel.Data;
                if (ListNhatKyNgay != null)
                {
                    var listNK = ListNhatKyNgay.GroupBy(x => x.Thoi_Gian).ToList();
                    data = new List<BaoCaoNgayViewModel>();
                    var i = 0;
                    foreach(var item in listNK)
                    {
                        i++;
                        var thongsotong = new BaoCaoNgayViewModel();
                        thongsotong.TT = i;
                        thongsotong.ThoiGian = item.Key;
                        data.Add(thongsotong);
                    }
                }
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

        public void PaginationData(PagerItemClickEventArgs args)
        {
            curPage = args.CurrentPage;
            onTaiLai();
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
                excelExportProperties.DataSource = ListNhatKyNgay;
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
                await Task.Run(() => { rsModel = NhatKyNgayService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }
    }


    //xuat ra các dòng được chọn
   

    }




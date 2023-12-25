using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using DAO.Services.DanhMuc;
using DAO.Services.PhanQuyen;
using DAO.Services.XuatExcel;
using DAO.ViewModel;
using DocumentFormat.OpenXml.Spreadsheet;
using ERPProject.Services;
using ERPProject.Shared;
using ERPProject.Shared.Combobox;
using Microsoft.AspNetCore.Components;
using MsUtils;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System.Net;


namespace ERPProject.Pages.DanhMuc
{
    public class BcNgayBase : ComponentBase
    {
        public List<prcTram> prcTrams = new List<prcTram>();
        public List<prcThongSo> prcThongSos = new List<prcThongSo>();

        public List<prc_Nhat_Ky_Ngay> ListNhatKyNgay { get; set; }
        public List<BaoCaoNgayViewModel> data { get; set; }
        public string urlFile { get; set; }
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
        public DateTime ToDay { get; set; } = DateTime.Now;
        public int Index { get; set; } = 1;
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
                   

                });
            });
            base.OnInitialized();
        }

        public void ValueChangeHandler(int[] args)
        {
            if (args != null)
            {
                prcTrams = DmTramService.GetComboTramMutiCN(args).Data;                
            }
            else
            {
                prcTrams = DmTramService.GetToComBobyIdTramAll().Data;
            }
        }
        public void ValueChangeHandler1(int[] args)
        {
            if (args != null)
            {
                prcThongSos = DmThongSoService.GetComboThongSoMutiTram(args).Data;
            }
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
                    if(CbThoiGian.Value == 1)
                    {
                        var listNK = ListNhatKyNgay.GroupBy(x => x.Thoi_Gian).OrderBy(group => group.Key).ToList();
                        data = new List<BaoCaoNgayViewModel>();
                        var i = 0;
                        foreach (var item in listNK)
                        {
                            i++;
                            var thongsotong = new BaoCaoNgayViewModel();
                            thongsotong.TT = i;
                            thongsotong.ThoiGian = item.Key;
                            data.Add(thongsotong);
                        }
                    }
                    if(CbThoiGian.Value == 2)
                    {
                        var listNK = ListNhatKyNgay.Where(x => x.Thoi_Gian.Minute == 0).GroupBy(x => x.Thoi_Gian).OrderByDescending(group => group.Key).ToList();
                        data = new List<BaoCaoNgayViewModel>();
                        var i = 0;
                        foreach (var item in listNK)
                        {
                            i++;
                            var thongsotong = new BaoCaoNgayViewModel();
                            thongsotong.TT = i;
                            thongsotong.ThoiGian = item.Key;
                            data.Add(thongsotong);
                        }
                    }
                    if (CbThoiGian.Value == 3)
                    {
                        var listNK = ListNhatKyNgay
                            .Where(x => x.Thoi_Gian.Minute == 0)
                            .GroupBy(x => x.Thoi_Gian)
                            .OrderByDescending(group => group.Key)
                            .ToList();

                        data = new List<BaoCaoNgayViewModel>();
                        var i = 0;

                        // Sắp xếp danh sách listNK theo thời gian giảm dần và chọn phần tử đầu tiên
                        var latestThoiGian = listNK.OrderByDescending(item => item.Key).FirstOrDefault();

                        if (latestThoiGian != null)
                        {
                            var thongsotong = new BaoCaoNgayViewModel();
                            thongsotong.TT = i + 1;
                            thongsotong.ThoiGian = latestThoiGian.Key;
                            data.Add(thongsotong);
                        }
                    }

                }
                if (ListNhatKyNgay.Count == 0)
                {
                    toastService.ShowWarning("Không có dữ liệu phù hợp");
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




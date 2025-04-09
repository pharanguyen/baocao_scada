using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.PhanQuyen;
using DAO.Services.DanhMuc;
using DAO.Services.PhanQuyen;
using DAO.Services.XuatExcel;
using DAO.ViewModel;
using ERPProject.Services;
using ERPProject.Shared;
using ERPProject.Shared.Combobox;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using DAO.Models.DanhMuc.CanhBao;
using DAO.Services.DanhMuc.CanhBao;
using Microsoft.JSInterop;

namespace ERPProject.Pages.DanhMuc.CanhBao
{
    public  class CanhBaoTieuThuBase:ComponentBase
    {
        public List<prcTram> prcTrams = new List<prcTram>();
        public List<prcThongSo> prcThongSos = new List<prcThongSo>();
        public List<ChiTietViewModel> ListSum = new List<ChiTietViewModel>();
        public List<prc_Nhat_Ky> ListNhatKy { get; set; }
        public List<CanhBaoTieuThuNgay> data { get; set; } = new();
        
        public string urlFile { get; set; }

        public int take = 20;
        public int totalPages = 0;
        public int curPage = 1;
        public int[] Id_ThongSo { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        

        //public List<to_quan_ly> ListNhatKy{ get; set; }
        protected Dm_Tram_CapNhat fCapNhat;
        ds_phanquyen _QSD = new ds_phanquyen();
        [Inject]
       public AppDataScoped AppData { get; set; }
        public LoadingPanel loadingPanel;



        protected SfGrid<CanhBaoTieuThuNgay> gdv;
        protected FormXacNhan frmXacNhan;
        protected CbMultiChiNhanh CbChiNhanh;
        protected CbMultiTram CbTram;
        protected CbMultiThongSo CbThongSo;
        protected CbTime CbThoiGian;
        
        public List<string> Toolbar = new() { "Edit", "Update", "Cancel" };
       public GridEditSettings editSettings = new() { AllowEditing = true, AllowAdding = false, AllowDeleting = false, Mode = EditMode.Normal };
        public DateTime StartDate { get; set; } = DateTime.Now.Date.AddDays(-1); // hôm qua
        public DateTime EndDate { get; set; } = DateTime.Now.Date;              // hôm nay

        public int Index { get; set; } = 1;
        [Inject]
        protected ToastService toastService { get; set; }
        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }
       

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
                        await Task.Run(() => { rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "btnBC_Thang"); });
                        _QSD = rsModel.Data;



                    }
                    //onTaiLai();


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
        }


        protected void OnActionBegin(ActionEventArgs<CanhBaoTieuThuNgay> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                var updated = args.Data;

                var model = new CanhBao_Tram
                {
                    IdTram = updated.IdTram,
                    MucCanhBao = updated.MucCanhBao,
                    TyLeCanhBao = updated.TyLeCanhBao
                };

                canh_bao_tramService.Save(model);
            }
        }
        //protected RenderFragment GetCanhBaoIcon(CanhBaoTieuThuNgay item)
        //{
        //    return builder =>
        //    {
        //        bool vuotMuc = item.TieuThu > item.MucCanhBao && item.MucCanhBao > 0;
        //        bool vuotTyLe = item.MucCanhBao > 0 && item.TyLeCanhBao > 0 &&
        //                        (item.TieuThu / item.MucCanhBao * 100 > item.TyLeCanhBao);

        //        if (vuotMuc || vuotTyLe)
        //        {
        //            builder.OpenElement(0, "span");
        //            builder.AddAttribute(1, "class", "warning-icon");
        //            builder.AddContent(2, "\u26A0"); // ⚠
        //            builder.CloseElement();
        //        }
        //    };
        //}


        //protected void OnRowDataBound(RowDataBoundEventArgs<CanhBaoTieuThuNgay> args)
        //{
        //    var item = args.Data;
        //    if (item != null)
        //    {
        //        // Điều kiện cảnh báo 1: vượt mức
        //        bool vuotMuc = item.TieuThu > item.MucCanhBao && item.MucCanhBao > 0;

        //        // Điều kiện cảnh báo 2: vượt tỷ lệ %
        //        bool vuotTyLe = item.MucCanhBao > 0 && item.TyLeCanhBao > 0 &&
        //                        (item.TieuThu / item.MucCanhBao * 100 > item.TyLeCanhBao);

        //        if (vuotMuc || vuotTyLe)
        //        {
        //            // Gán class nhấp nháy
        //            args.Row.AddClass(new[] { "blink-tieuthu" });
        //        }
        //    }
        //}
        protected RenderFragment GetCanhBaoIcon(CanhBaoTieuThuNgay item)
        {
            return builder =>
            {
                if (item.MucCanhBao > 0 && item.TyLeCanhBao > 0)
                {
                    double tileThucTe = (item.TieuThu - item.MucCanhBao) / item.MucCanhBao * 100;
                    if (tileThucTe > item.TyLeCanhBao)
                    {
                        builder.OpenElement(0, "span");
                        builder.AddAttribute(1, "class", "warning-icon");
                        builder.AddContent(2, "\u26A0"); // ⚠
                        builder.CloseElement();
                    }
                }
            };
        }
        protected void OnRowDataBound(RowDataBoundEventArgs<CanhBaoTieuThuNgay> args)
        {
            var item = args.Data;
            if (item != null && item.MucCanhBao > 0 && item.TyLeCanhBao > 0)
            {
                double tileThucTe = (item.TieuThu - item.MucCanhBao) / item.MucCanhBao * 100;
                if (tileThucTe > item.TyLeCanhBao)
                {
                    args.Row.AddClass(new[] { "blink-tieuthu" });
                }
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
            AppData.loadingPanel?.show();

            try
            {
                if (StartDate == DateTime.MinValue)
                    StartDate = DateTime.Now.Date.AddDays(-1);
                if (EndDate == DateTime.MinValue)
                    EndDate = DateTime.Now.Date;

                // ✅ Nếu không chọn thì lấy toàn bộ Id trạm
                var selectedTramIds = CbTram?.Value?.ToList();
                if (selectedTramIds == null || selectedTramIds.Count == 0)
                {
                    selectedTramIds = prcTrams.Select(x => x.Id).ToList(); // lấy tất cả
                }

                var result = canh_bao_tieu_thuService.GetCanhBaoTieuThuNgay(StartDate, EndDate, selectedTramIds);

                if (result.isThanhCong)
                {
                    var duLieu = result.Data;

                    if (duLieu == null || duLieu.Count == 0)
                    {
                        toastService.ShowWarning("Không có dữ liệu.");
                    }

                    var canhBaos = canh_bao_tramService.GetAll();

                    foreach (var tram in duLieu)
                    {
                        var cb = canhBaos.FirstOrDefault(x => x.IdTram == tram.IdTram);
                        if (cb != null)
                        {
                            tram.MucCanhBao = cb.MucCanhBao ?? 0;
                            tram.TyLeCanhBao = cb.TyLeCanhBao ?? 0;
                        }
                    }

                    data = duLieu;
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", result.ThongBao);
                }
            }
            finally
            {
                AppData.loadingPanel?.hide();
            }
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
        protected async Task onXuatExcel()
        {
            try
            {
                AppData?.loadingPanel?.show();

                var exportProps = new ExcelExportProperties
                {
                    FileName = $"CanhBaoTieuThu_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
                    ExportType = ExportType.AllPages,
                    IncludeHiddenColumn = true,
                    IncludeTemplateColumn = true
                };

                if (gdv != null)
                {
                    await gdv.ExportToExcelAsync(exportProps);
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Grid chưa sẵn sàng để xuất Excel.");
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi xuất Excel: {ex.Message}");
            }
            finally
            {
                AppData?.loadingPanel?.hide();
            }
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
                await Task.Run(() => { rsModel = NhatKyService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }
    }
}
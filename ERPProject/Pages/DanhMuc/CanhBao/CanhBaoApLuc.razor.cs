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
    public class CanhBaoApLucBase : ComponentBase
    {
        public List<prcTram> prcTrams = new List<prcTram>();
        public List<prcThongSo> prcThongSos = new List<prcThongSo>();
        public List<ChiTietViewModel> ListSum = new List<ChiTietViewModel>();
        public List<prc_Nhat_Ky> ListNhatKy { get; set; }
        public List<CanhBaoApLucViewModel> data { get; set; } = new();
        private System.Timers.Timer autoReloadTimer;

        public string urlFile { get; set; }
        public int take = 20;
        public int totalPages = 0;
        public int curPage = 1;
        public int[] Id_ThongSo { get; set; }

        [Inject] IJSRuntime JSRuntime { get; set; }
        protected Dm_Tram_CapNhat fCapNhat;
        ds_phanquyen _QSD = new ds_phanquyen();
        [Inject] public AppDataScoped AppData { get; set; }
        public LoadingPanel loadingPanel;

        protected SfGrid<CanhBaoApLucViewModel> gdv;
        protected FormXacNhan frmXacNhan;
        protected CbMultiChiNhanh CbChiNhanh;
        protected CbMultiTram CbTram;
        protected CbMultiThongSo CbThongSo;
        protected CbTime CbThoiGian;

        public List<string> Toolbar = new() { "Edit", "Update", "Cancel" };
        public GridEditSettings editSettings = new() { AllowEditing = true, AllowAdding = false, AllowDeleting = false, Mode = EditMode.Normal };
        public DateTime StartDate { get; set; } = DateTime.Now.Date.AddDays(-1);
        public DateTime EndDate { get; set; } = DateTime.Now.Date;

        public int Index { get; set; } = 1;
        [Inject] protected ToastService toastService { get; set; }
        [Inject] protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        protected override void OnInitialized()
        {
            Task.Run(async () =>
            {
                await InvokeAsync(async () =>
                {
                    var _tokenE = await localStorage.GetItemAsync<string>("Token");
                    var _token = MsUtils.Encryption.DecryptString(_tokenE);
                    var strInfo = _token.Split("@MS@");
                    if (strInfo.Length == 5)
                    {
                        var rsModel = new ResultModel<ds_phanquyen>();
                        await Task.Run(() =>
                        {
                            rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "btnBC_Thang");
                        });
                        _QSD = rsModel.Data;
                    }

                    onTaiLai();
                    StartAutoReload();
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

        protected void OnActionBegin(ActionEventArgs<CanhBaoApLucViewModel> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                var updated = args.Data;

                var model = new CanhBao_ApLuc
                {
                    Id_Tram = updated.Id_Tram,
                    ApLucMin = updated.ApLucMin,
                    ApLucMax = updated.ApLucMax
                };

                canh_bao_ap_lucService.Save(model);
                toastService.ShowSuccess("Đã lưu cấu hình cảnh báo áp lực.");
            }
        }

        //protected RenderFragment GetCanhBaoIcon(CanhBaoApLucViewModel item)
        //{
        //    return builder =>
        //    {
        //        bool isCanhBao = item.GiaTri < item.ApLucMin || item.GiaTri > item.ApLucMax;
        //        if (isCanhBao)
        //        {
        //            builder.OpenElement(0, "span");
        //            builder.AddAttribute(1, "class", "warning-icon");
        //            builder.AddContent(2, "\u26A0");
        //            builder.CloseElement();
        //        }
        //    };
        //}
        protected RenderFragment GetCanhBaoIcon(CanhBaoApLucViewModel item)
        {
            return builder =>
            {
                bool isCanhBao = item.GiaTriSo.HasValue &&
                                 ((item.ApLucMin.HasValue && item.GiaTriSo.Value < item.ApLucMin.Value) ||
                                  (item.ApLucMax.HasValue && item.GiaTriSo.Value > item.ApLucMax.Value));

                if (isCanhBao)
                {
                    builder.OpenElement(0, "span");
                    builder.AddAttribute(1, "class", "warning-icon");
                    builder.AddContent(2, "\u26A0"); // Cảnh báo
                    builder.CloseElement();
                }
            };
        }

        protected string GetGiaTriStyle(CanhBaoApLucViewModel item)
        {
            return (item.GiaTriSo.HasValue &&
                    ((item.ApLucMin.HasValue && item.GiaTriSo.Value < item.ApLucMin.Value) ||
                     (item.ApLucMax.HasValue && item.GiaTriSo.Value > item.ApLucMax.Value)))
                ? "red"
                : "black";
        }

        protected void OnRowDataBound(RowDataBoundEventArgs<CanhBaoApLucViewModel> args)
        {
            var item = args.Data;
            if (item == null || !item.GiaTriSo.HasValue) return;

            bool isMin = item.ApLucMin.HasValue && item.GiaTriSo.Value < item.ApLucMin.Value;
            bool isMax = item.ApLucMax.HasValue && item.GiaTriSo.Value > item.ApLucMax.Value;

            if (isMin)
                args.Row.AddClass(new[] { "blink-min" });

            if (isMax)
                args.Row.AddClass(new[] { "blink-max" });
        }









        public void ValueChangeHandler1(int[] args)
        {
            if (args != null)
            {
                prcThongSos = DmThongSoService.GetComboThongSoMutiTram(args).Data;
            }
        }

        private void StartAutoReload()
        {
            autoReloadTimer = new System.Timers.Timer(30000);
            autoReloadTimer.Elapsed += async (sender, args) =>
            {
                await InvokeAsync(() => onTaiLai());
            };
            autoReloadTimer.AutoReset = true;
            autoReloadTimer.Start();
        }

        public void Dispose()
        {
            autoReloadTimer?.Dispose();
        }

        protected async void onTaiLai()
        {
            AppData.loadingPanel?.show();

            try
            {
                var selectedTramIds = new List<int>
        {
            336, 380, 382, 383, 455, 484,
            2, 3, 4, 5, 6, 18,
            387, 38, 215, 416, 716, 463, 570, 178, 337, 360, 385, 357,
            439, 474, 106, 104, 388, 418, 468, 151, 96, 231, 399, 82,
            467, 252, 404, 436, 393, 245, 343, 427, 443, 479, 274, 406,
            424, 31, 348, 401, 112, 210, 455, 68, 211, 227, 472, 175,
            390, 392, 473, 191, 241, 375, 407, 408, 442, 47, 181, 281,
            364, 449, 44, 262, 371, 403, 405, 446, 471, 423, 425, 441,
            475, 134, 152, 243, 329, 379
        };

                var duLieu = canh_bao_ap_luc_tramService.GetCanhBaoApLuc(StartDate, EndDate, selectedTramIds);

                data = duLieu
                    .Where(x => !string.IsNullOrWhiteSpace(x.GiaTri)) // ✅ Tránh lỗi nếu null hoặc chuỗi trống
                    .GroupBy(x => x.Id_Tram)
                    .Select(g => g.OrderByDescending(x => x.NgayThangNam).ThenByDescending(x => x.GioPhutGiay).First())
                    .ToList();

                StateHasChanged();
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi: {ex.Message}");
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
        }
        protected async Task onXuatExcel()
        {
            try
            {
                AppData?.loadingPanel?.show();

                var exportProps = new ExcelExportProperties
                {
                    FileName = $"CanhBaoApLuc_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
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
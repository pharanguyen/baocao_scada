using Microsoft.AspNetCore.Components;
using ERPProject.Shared.Combobox;
using DAO.Models.DanhMuc.SuCoOngVo;
using DAO.Services.BaoCao;
using DAO.Services.DanhMuc;
using DAO.Models.DanhMuc;
using DAO.Models.CommonModels;
using Syncfusion.Blazor.Grids;
using ERPProject.Services;

namespace ERPProject.Pages.DanhMuc
{
    public class Sua_chuaBase : ComponentBase
    {
        protected CbChiNhanh CbChiNhanhNhap;
        protected CbChiNhanh CbChiNhanhLoc;
        protected DhkCombo CbDhk;
        protected CbMultiDHK CbDhkMulti;
        protected SfGrid<SuCo_OngVo> gdv;

        [Inject] public ToastService ToastService { get; set; }

        public List<DM_Dhk> listDhkTheoChiNhanhNhap { get; set; } = new();
        public List<DM_Dhk> listDhkTheoChiNhanhLoc { get; set; } = new();
        public List<SuCo_OngVo> listThongSoTramLoc { get; set; } = new();
        public int[] SelectedDhkList { get; set; } = Array.Empty<int>();

        public DateTime? NgayTimKiem { get; set; }
        public DateTime? NgaySuaChua { get; set; }
        public string SoDiemVo { get; set; }
        public string MoTa { get; set; }
        public string GhiChu { get; set; }
        public string TieuThu13_Truoc { get; set; }
        public string TieuThu13_Sau { get; set; }
        public string TieuThu07_Truoc { get; set; }
        public string TieuThu07_Sau { get; set; }

        public DateTime TuNgay { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DenNgay { get; set; } = DateTime.Now;

        private int IdEditing = 0;
        public decimal? SelectedDhk { get; set; }

        protected void OnChiNhanhNhapChanged(int idChiNhanh)
        {
            var result = DmDhkService.GetDhkTheoChiNhanh(idChiNhanh);
            if (result.isThanhCong)
                listDhkTheoChiNhanhNhap = result.Data;
        }

        protected void OnChiNhanhLocChanged(int idChiNhanh)
        {
            var result = DmDhkService.GetDhkTheoChiNhanh(idChiNhanh);
            if (result.isThanhCong)
                listDhkTheoChiNhanhLoc = result.Data;

            InvokeAsync(StateHasChanged);
        }

        protected async Task ValueChangeHandlerDhk(int[] value)
        {
            SelectedDhkList = value;
            await LoadData();
        }

        protected async Task LoadData()
        {
            var idChiNhanh = CbChiNhanhLoc?.Value ?? 0;

            if (idChiNhanh == 0)
            {
                listThongSoTramLoc = new();
                await InvokeAsync(StateHasChanged);
                return;
            }

            var listChiNhanh = new List<int> { idChiNhanh };
            var listDhk = listDhkTheoChiNhanhLoc.Select(x => (int)x.ms_dhk).ToList();

            listThongSoTramLoc = SuCoOngVoService.GetBaoCaoOngVo(TuNgay, DenNgay, listChiNhanh, listDhk);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task OnSave()
        {
            double.TryParse(TieuThu13_Truoc, out var tt13Truoc);
            double.TryParse(TieuThu13_Sau, out var tt13Sau);
            double.TryParse(TieuThu07_Truoc, out var tt07Truoc);
            double.TryParse(TieuThu07_Sau, out var tt07Sau);

            var idChiNhanh = CbChiNhanhNhap?.Value ?? 0;

            if (idChiNhanh == 0 || !SelectedDhk.HasValue)
            {
                ToastService.ShowWarning("Vui lòng chọn đầy đủ thông tin.");
                return;
            }

            var item = new SuCo_OngVo
            {
                Id = IdEditing,
                NgayBaoCao = DateTime.Now,
                IdChiNhanh = idChiNhanh,
                IdDHK = (int)SelectedDhk.Value,
                NgayTimKiem = NgayTimKiem,
                NgaySuaChua = NgaySuaChua,
                SoDiemVo = SoDiemVo,
                MoTa = MoTa,
                TieuThu13_Truoc = tt13Truoc,
                TieuThu13_Sau = tt13Sau,
                TieuThu07_Truoc = tt07Truoc,
                TieuThu07_Sau = tt07Sau,
                GhiChu = GhiChu
            };

            bool result = IdEditing == 0
                ? SuCoOngVoService.Insert(item)
                : SuCoOngVoService.Update(item);

            if (result)
            {
                ToastService.ShowSuccess("✅ Đã lưu thành công.");

                // Gán tên đồng hồ (nếu có) để hiển thị lên grid
                var tenDhk = listDhkTheoChiNhanhNhap.FirstOrDefault(x => x.ms_dhk == item.IdDHK)?.ten_dhk;
                item.ten_dhk = tenDhk;

                if (IdEditing == 0)
                {
                    // Chỉ thêm vào list khi chi nhánh nhập = chi nhánh lọc
                    if ((CbChiNhanhLoc?.Value ?? 0) == item.IdChiNhanh)
                        listThongSoTramLoc.Insert(0, item);
                }
                else
                {
                    var existing = listThongSoTramLoc.FirstOrDefault(x => x.Id == item.Id);
                    if (existing != null)
                    {
                        // Cập nhật lại trực tiếp trên list
                        existing.NgayBaoCao = item.NgayBaoCao;
                        existing.IdChiNhanh = item.IdChiNhanh;
                        existing.IdDHK = item.IdDHK;
                        existing.NgayTimKiem = item.NgayTimKiem;
                        existing.NgaySuaChua = item.NgaySuaChua;
                        existing.SoDiemVo = item.SoDiemVo;
                        existing.MoTa = item.MoTa;
                        existing.TieuThu13_Truoc = item.TieuThu13_Truoc;
                        existing.TieuThu13_Sau = item.TieuThu13_Sau;
                        existing.TieuThu07_Truoc = item.TieuThu07_Truoc;
                        existing.TieuThu07_Sau = item.TieuThu07_Sau;
                        existing.GhiChu = item.GhiChu;
                        existing.ten_dhk = item.ten_dhk;
                    }
                }

                ClearFields();
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                ToastService.ShowWarning("❌ Lưu thất bại.");
            }
        }


        protected void onCapNhat(int id)
        {
            var item = listThongSoTramLoc.FirstOrDefault(x => x.Id == id);
            if (item == null) return;

            IdEditing = item.Id;

            if (CbChiNhanhNhap != null)
                CbChiNhanhNhap.Value = item.IdChiNhanh;

            var result = DmDhkService.GetDhkTheoChiNhanh(item.IdChiNhanh);
            if (result.isThanhCong)
                listDhkTheoChiNhanhNhap = result.Data;

            SelectedDhk = item.IdDHK;
            SelectedDhkList = new int[] { item.IdDHK };

            if (CbDhk != null)
                CbDhk.Value = item.IdDHK;

            NgayTimKiem = item.NgayTimKiem;
            NgaySuaChua = item.NgaySuaChua;
            SoDiemVo = item.SoDiemVo;
            MoTa = item.MoTa;
            TieuThu13_Truoc = item.TieuThu13_Truoc?.ToString();
            TieuThu13_Sau = item.TieuThu13_Sau?.ToString();
            TieuThu07_Truoc = item.TieuThu07_Truoc?.ToString();
            TieuThu07_Sau = item.TieuThu07_Sau?.ToString();
            GhiChu = item.GhiChu;
        }

        protected void onXoa(int id)
        {
            if (SuCoOngVoService.Delete(id))
            {
                ToastService.ShowSuccess("🗑️ Đã xóa thành công.");
                _ = LoadData();
            }
        }

        protected async Task onXuatExcel()
        {
            var exportProps = new ExcelExportProperties
            {
                FileName = $"SuCoOngVo_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
                ExportType = ExportType.AllPages,
                IncludeHiddenColumn = true,
                IncludeTemplateColumn = true
            };

            var selectedData = await gdv.GetSelectedRecordsAsync();
            exportProps.DataSource = (selectedData != null && selectedData.Count > 0)
                ? selectedData
                : listThongSoTramLoc;

            await gdv.ExportToExcelAsync(exportProps);
        }

        protected void ClearFields()
        {
            IdEditing = 0;
            SelectedDhk = null;

            NgayTimKiem = null;
            NgaySuaChua = null;
            SoDiemVo = MoTa = GhiChu = "";
            TieuThu13_Truoc = TieuThu13_Sau = TieuThu07_Truoc = TieuThu07_Sau = "";
        }
    }
}
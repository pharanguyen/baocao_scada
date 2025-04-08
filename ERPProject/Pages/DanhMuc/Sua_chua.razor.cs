using Microsoft.AspNetCore.Components;
using ERPProject.Shared.Combobox;
using DAO.Models.DanhMuc.SuCoOngVo;
using DAO.Services.BaoCao;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Grids;


namespace ERPProject.Pages.DanhMuc
{
    public class Sua_chuaBase : ComponentBase
    {
        protected CbChiNhanh CbChiNhanh;
        protected CbTram CbTram;
        protected CbMultiDHK CbDhk;
        protected SfGrid<SuCo_OngVo> gdv;

        public List<SuCo_OngVo> listThongSoTram { get; set; } = new();

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
        public int SelectedDhkId;

        protected override void OnInitialized() { }

        protected async Task ValueChangeHandlerDhk(int[] value)
        {
            if (value != null && value.Length > 0)
                SelectedDhkId = value[0];
        }

        protected async Task LoadData()
        {
            var listChiNhanh = new List<int> { CbChiNhanh?.Value ?? 0 };
            var listDhk = CbDhk?.Value?.ToList() ?? new();

            listThongSoTram = SuCoOngVoService.GetBaoCaoOngVo(TuNgay, DenNgay, listChiNhanh, listDhk);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task onXuatExcel()
        {
            var exportProps = new ExcelExportProperties
            {
                FileName = $"SuCoOngVo_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
                ExportType = ExportType.AllPages,
                IncludeHiddenColumn = true,
                IncludeTemplateColumn=true// Không dùng IgnoreStackedColumns nếu chưa hỗ trợ
            };

            var selectedData = await gdv.GetSelectedRecordsAsync();
            exportProps.DataSource = (selectedData != null && selectedData.Count > 0)
                ? selectedData
                : listThongSoTram;

            await gdv.ExportToExcelAsync(exportProps);
        }






        protected void OnSave()
        {
            double.TryParse(TieuThu13_Truoc, out var tt13Truoc);
            double.TryParse(TieuThu13_Sau, out var tt13Sau);
            double.TryParse(TieuThu07_Truoc, out var tt07Truoc);
            double.TryParse(TieuThu07_Sau, out var tt07Sau);

            var item = new SuCo_OngVo
            {
                Id = IdEditing,
                NgayBaoCao = DateTime.Now,
                IdChiNhanh = CbChiNhanh?.Value ?? 0,
                IdDHK = SelectedDhkId,
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

            var result = IdEditing == 0 ? SuCoOngVoService.Insert(item) : SuCoOngVoService.Update(item);
            if (result)
            {
                ClearFields();
                _ = LoadData();
            }
        }

        protected void onCapNhat(int id)
        {
            var item = listThongSoTram.FirstOrDefault(x => x.Id == id);
            if (item == null) return;

            IdEditing = item.Id;
            SelectedDhkId = item.IdDHK;
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
                _ = LoadData();
            }
        }

        protected void ClearFields()
        {
            IdEditing = 0;
            NgayTimKiem = null;
            NgaySuaChua = null;
            SoDiemVo = MoTa = GhiChu = "";
            TieuThu13_Truoc = TieuThu13_Sau = TieuThu07_Truoc = TieuThu07_Sau = "";
            SelectedDhkId = 0;
        }
    }
}

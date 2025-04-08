using Microsoft.AspNetCore.Components;
using ERPProject.Shared.Combobox;
using DAO.Models.DanhMuc.SuCoOngVo;
using DAO.Services.DanhMuc.SuCoOngVo;
using Syncfusion.Blazor.Grids;
using static DAO.Models.DanhMuc.SuCoOngVo.SuCoScadaViewModel;

namespace ERPProject.Pages.DanhMuc
{
    public class Tong_hop_nguyen_nhanBase : ComponentBase
    {
        // protected SfGrid<SuCoScada> gdv;
        protected CbMultiDHK CbDhk;


        // public List<SuCoScada> listSuCo { get; set; } = new();
        protected SfGrid<SuCoScadaViewModel> gdv;

        public List<SuCoScadaViewModel> listSuCo { get; set; } = new();


        public int IdEditing = 0;
        public int SelectedDhkId;

        // Input fields
        public string TenTQL { get; set; }
        public string ThongTinSuCo { get; set; }
        public DateTime? NgayXayRa { get; set; }
        public DateTime? NgayKiemTra { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public string XuLyQLDiaBan { get; set; }
        public string XuLyCNTT { get; set; }
        public string XuLyXNDH { get; set; }
        public string KetQua { get; set; }
        public string NguyenNhan { get; set; }
        public string ChiSo_DH { get; set; }
        public string ChiSo_HT { get; set; }

        public DateTime TuNgay { get; set; } = DateTime.Now.AddMonths(-1);
        public DateTime DenNgay { get; set; } = DateTime.Now;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            listSuCo = su_co_scadaService.GetAll(TuNgay, DenNgay);
            await InvokeAsync(StateHasChanged);
        }

        protected async Task ValueChangeHandlerDhk(int[] value)
        {
            if (value != null && value.Length > 0)
                SelectedDhkId = value[0]; // hoặc gán danh sách nếu multi
        }


        protected void OnSave()
        {
            var item = new SuCoScadaViewModel
            {
                Id = IdEditing,
                IdDHK = SelectedDhkId,
                TenTQL = TenTQL,
                ThongTinSuCo = ThongTinSuCo,
                NgayXayRa = NgayXayRa,
                NgayKiemTra = NgayKiemTra,
                NgayHoanThanh = NgayHoanThanh,
                XuLy_QLDiaBan = XuLyQLDiaBan,
                XuLy_CNTT = XuLyCNTT,
                XuLy_XNDH = XuLyXNDH,
                KetQua = KetQua,
                NguyenNhan = NguyenNhan,
                ChiSo_DH = ChiSo_DH,
                ChiSo_HT = ChiSo_HT
            };

            bool result = IdEditing == 0
                ? su_co_scadaService.Insert(item)
                : su_co_scadaService.Update(item);

            if (result)
            {
                ClearFields();
                _ = LoadData();
            }
        }

        protected void onCapNhat(int id)
        {
            var item = listSuCo.FirstOrDefault(x => x.Id == id);
            if (item == null) return;

            IdEditing = item.Id;
            SelectedDhkId = item.IdDHK;
            TenTQL = item.TenTQL;
            ThongTinSuCo = item.ThongTinSuCo;
            NgayXayRa = item.NgayXayRa;
            NgayKiemTra = item.NgayKiemTra;
            NgayHoanThanh = item.NgayHoanThanh;
            XuLyQLDiaBan = item.XuLy_QLDiaBan;
            XuLyCNTT = item.XuLy_CNTT;
            XuLyXNDH = item.XuLy_XNDH;
            KetQua = item.KetQua;
            NguyenNhan = item.NguyenNhan;
            ChiSo_DH = item.ChiSo_DH;
            ChiSo_HT = item.ChiSo_HT;
        }

        protected void onXoa(int id)
        {
            if (su_co_scadaService.Delete(id))
            {
                _ = LoadData();
            }
        }

        protected void ClearFields()
        {
            IdEditing = 0;
            SelectedDhkId = 0;
            TenTQL = ThongTinSuCo = XuLyQLDiaBan = XuLyCNTT = XuLyXNDH = KetQua = NguyenNhan = ChiSo_DH = ChiSo_HT = "";
            NgayXayRa = NgayKiemTra = NgayHoanThanh = null;
        }
        protected async Task onXuatExcel()
        {
            var exportProps = new ExcelExportProperties
            {
                FileName = $"SuCoScada_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
                ExportType = ExportType.AllPages,
                IncludeHiddenColumn = true,
                IncludeTemplateColumn = true// Không dùng IgnoreStackedColumns nếu chưa hỗ trợ
            };

            var selectedData = await gdv.GetSelectedRecordsAsync();
            exportProps.DataSource = (selectedData != null && selectedData.Count > 0)
                ? selectedData
                : listSuCo;

            await gdv.ExportToExcelAsync(exportProps);
        }

        //public async Task onXuatExcel()
        //{
        //    var exportProps = new ExcelExportProperties
        //    {
        //        FileName = $"SuCoScada_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
        //        ExportType = ExportType.AllPages,
        //        DataSource = listSuCo
        //    };

        //    await gdv.ExportToExcelAsync(exportProps);
        //}
    }
}

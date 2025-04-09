using Microsoft.AspNetCore.Components;
using ERPProject.Shared.Combobox;
using DAO.Models.DanhMuc.SuCoOngVo;
using DAO.Services.DanhMuc.SuCoOngVo;
using DAO.Services.DanhMuc;
using DAO.Models.DanhMuc;
using ERPProject.Services;
using Syncfusion.Blazor.Grids;
using DAO.Services.BaoCao;

namespace ERPProject.Pages.DanhMuc
{
    public class Tong_hop_nguyen_nhanBase : ComponentBase
    {
        protected CbChiNhanh CbChiNhanhNhap;
        protected CbChiNhanh CbChiNhanhLoc;
        protected DhkCombo CbDhk;
        protected CbMultiDHK CbDhkMulti;
        protected SfGrid<SuCoScadaViewModel> gdv;

        [Inject] public ToastService ToastService { get; set; }
        public List<SuCoScadaViewModel> listThongSoTramLoc { get; set; } = new();

        public List<DM_Dhk> listDhkTheoChiNhanhNhap { get; set; } = new();
        public List<DM_Dhk> listDhkTheoChiNhanhLoc { get; set; } = new();
        public List<SuCoScadaViewModel> listSuCo { get; set; } = new();
        public int[] SelectedDhkList { get; set; } = Array.Empty<int>();

        private int IdEditing = 0;
        public int SelectedDhkId;

        public decimal? SelectedDhk { get; set; }

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
            var listDhk = SelectedDhkList?.ToList();
            if (listDhk == null || listDhk.Count == 0)
            {
                listDhk = listDhkTheoChiNhanhLoc.Select(x => (int)x.ms_dhk).ToList(); // lấy tất cả DHK của chi nhánh lọc
            }
            // ✅ Dùng danh sách người dùng chọn

            listThongSoTramLoc = su_co_scadaService.GetAll(TuNgay, DenNgay, listChiNhanh, listDhk);
            await InvokeAsync(StateHasChanged);
        }



        protected async Task OnSave()
        {
            var idChiNhanh = CbChiNhanhNhap?.Value ?? 0;

            if (idChiNhanh == 0 || !SelectedDhk.HasValue)
            {
                ToastService.ShowWarning("Vui lòng chọn chi nhánh và đồng hồ khối.");
                return;
            }

            var item = new SuCoScadaViewModel
            {
                Id = IdEditing,
                IdChiNhanh = idChiNhanh,
                IdDHK = (int)SelectedDhk.Value,
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
                ChiSo_HT = ChiSo_HT,
                CreatedAt = DateTime.Now
            };

            bool result = IdEditing == 0
                ? su_co_scadaService.Insert(item)
                : su_co_scadaService.Update(item);

            if (result)
            {
                ToastService.ShowSuccess("✅ Đã lưu thành công.");

                item.ten_dhk = listDhkTheoChiNhanhNhap.FirstOrDefault(x => x.ms_dhk == item.IdDHK)?.ten_dhk;

                var idLoc = CbChiNhanhLoc?.Value ?? 0;

                if (IdEditing == 0)
                {
                    if (idLoc == item.IdChiNhanh &&
                        (SelectedDhkList.Length == 0 || SelectedDhkList.Contains(item.IdDHK)))
                    {
                        listThongSoTramLoc.Insert(0, item);
                    }
                }
                else
                {
                    var existing = listThongSoTramLoc.FirstOrDefault(x => x.Id == item.Id);
                    if (existing != null)
                    {
                        existing.IdChiNhanh = item.IdChiNhanh;
                        existing.IdDHK = item.IdDHK;
                        existing.TenTQL = item.TenTQL;
                        existing.ThongTinSuCo = item.ThongTinSuCo;
                        existing.NgayXayRa = item.NgayXayRa;
                        existing.NgayKiemTra = item.NgayKiemTra;
                        existing.NgayHoanThanh = item.NgayHoanThanh;
                        existing.XuLy_QLDiaBan = item.XuLy_QLDiaBan;
                        existing.XuLy_CNTT = item.XuLy_CNTT;
                        existing.XuLy_XNDH = item.XuLy_XNDH;
                        existing.KetQua = item.KetQua;
                        existing.NguyenNhan = item.NguyenNhan;
                        existing.ChiSo_DH = item.ChiSo_DH;
                        existing.ChiSo_HT = item.ChiSo_HT;
                        existing.ten_dhk = item.ten_dhk;
                    }
                    else if (idLoc == item.IdChiNhanh &&
                             (SelectedDhkList.Length == 0 || SelectedDhkList.Contains(item.IdDHK)))
                    {
                        listSuCo.Insert(0, item);
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
            SelectedDhkId = item.IdDHK;

            if (CbChiNhanhNhap != null)
                CbChiNhanhNhap.Value = item.IdChiNhanh;

            var result = DmDhkService.GetDhkTheoChiNhanh(item.IdChiNhanh);
            if (result.isThanhCong)
                listDhkTheoChiNhanhNhap = result.Data;

            SelectedDhk = item.IdDHK;

            if (CbDhk != null)
                CbDhk.Value = item.IdDHK;

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

        protected async void onXoa(int id)
        {
            if (su_co_scadaService.Delete(id))
            {
                ToastService.ShowSuccess("🗑️ Đã xóa thành công.");
                listThongSoTramLoc.RemoveAll(x => x.Id == id);
                await InvokeAsync(StateHasChanged);
            }
        }


        protected void ClearFields()
        {
            IdEditing = 0;
            SelectedDhk = null;
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
                IncludeTemplateColumn = true
            };

            var selectedData = await gdv.GetSelectedRecordsAsync();
            exportProps.DataSource = (selectedData != null && selectedData.Count > 0)
                ? selectedData
                : listSuCo;

            await gdv.ExportToExcelAsync(exportProps);
        }
    }
}
using DAO.Models.CommonModels;
using DAO.Models.PhanQuyen;
using DAO.Services.PhanQuyen;
using ERPProject.Libs;
using ERPProject.Services;
using ERPProject.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Buttons;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Security.Cryptography;

namespace ERPProject.Pages.PhanQuyen
{
    public class Ds_ThanhVienPageBase : ComponentBase
    {
        [Inject]
        protected ToastService toastService { get; set; }

        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
        AppDataScoped AppData { get; set; }

        public List<prc_ThanhVien_DanhSach> GridData { get; set; }
        protected SfGrid<prc_ThanhVien_DanhSach> gdv;

        protected Ds_ThanhVienPage_CapNhat fCapNhat;
        protected Ds_ThanhVien_PhanQuyenChiTiet fPhanQuyen;
        protected FormXacNhan frmXacNhan;

        ds_phanquyen _QSD = new ds_phanquyen();
        //protected var strInfo = null;
        public Boolean disableBtnThemtv;
        public Boolean disableBtnSuatv;
        public Boolean disableBtnXoatv;
        public Boolean disableBtnCapQuyentv;
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
                        await Task.Run(() => { rsModel = ds_phanquyenService.GetQuyenThanhVien(Convert.ToInt32(strInfo[0]), "btnDsThanhvien"); });
                        _QSD = rsModel.Data;
                       // if (_QSD.them == false) disableBtnThemtv = true;
                       // if (_QSD.sua == false) disableBtnSuatv = true;
                       // if (_QSD.xoa == false) disableBtnXoatv = true;

                        
                    }
                    onTaiLai();


                });
            });
            base.OnInitialized();
        }
        protected async void onTaiLai()
        {


            AppData.loadingPanel.show();
            var rsModel = new ResultModel<List<prc_ThanhVien_DanhSach>>();
            await Task.Run(() => { rsModel = ds_thanhvienService.GetAllPlus(); });
            if (rsModel.isThanhCong)
            {
                //binding data to grid
                GridData = rsModel.Data;
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
        protected void onCapNhat(int _ID)
        {
            if (_QSD.sua == false)
            {
                toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                return;
            }
            fCapNhat.TieuDe = "Cập nhật thông tin nhân viên";
            fCapNhat.isThemMoi = false;
            fCapNhat.Show(_ID);
        }

        protected void onThemMoi()
        {
            if (_QSD.them == false)
            {
                toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                return;
            }
            fCapNhat.isThemMoi = true;
            fCapNhat.TieuDe = "Thêm nhân viên";
            fCapNhat.Show(0);
        }
        protected void onXoa(int _ID)
        {
            //check quyen xoa
            if (_QSD.xoa == false)
            {
                toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                return;
            }

            frmXacNhan.Show("Xóa dòng được chọn ?", "300px", new System.Action(async () =>
            {
                AppData.loadingPanel.show();
                var rsModel = new ResultModel<int?>();
                await Task.Run(() => { rsModel = ds_thanhvienService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }
        protected void onCapQuyen(int _ID, int nhomquyen_id)
        {
            if (_QSD.sua == false)
            {
                toastService.ShowWarning("Bạn không có quyền sử dụng tính năng này !");
                return;
            }

            fPhanQuyen.TieuDe = "Phân Quyền Chi Tiết";
            fPhanQuyen.Show(_ID, nhomquyen_id);
        }

    }
}

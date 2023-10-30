using DAO.Models.CommonModels;
using DAO.Models.PhanQuyen;
using DAO.Services.PhanQuyen;
using ERPProject.Pages.PhanQuyen;
using ERPProject.Services;
using ERPProject.Shared;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Calendars;
using Syncfusion.Blazor.Inputs;

namespace ERPProject.Pages.PhanQuyen
{
    public class ds_nhomquyen_pageBase : ComponentBase
    {
        public List<ds_nhomquyen> list_ds_nhomquyen { get; set; }
        protected Ds_NhomQuyen_CapNhat fCapNhat;
        [Inject]
        AppDataScoped AppData { get; set; }
        protected FormXacNhan frmXacNhan;
        [Inject]
        protected ToastService toastService { get; set; }


        protected override void OnInitialized()
        {
            var rsModel = new ResultModel<List<ds_nhomquyen>>();
            rsModel = ds_nhomquyenService.GetAll();
            list_ds_nhomquyen = rsModel.Data;
            base.OnInitialized();
        }

        protected async void onTaiLai()
        {

            AppData.loadingPanel.show();
            var rsModel = new ResultModel<List<ds_nhomquyen>>();
            await Task.Run(() => { rsModel = ds_nhomquyenService.GetAll(); });
            if (rsModel.isThanhCong)
            {
                list_ds_nhomquyen = rsModel.Data;
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

        protected void onThemMoi()
        {
            //check quyen them moi

            //hiển thị form
            fCapNhat.isThemMoi = true;
            fCapNhat.TieuDe = "Thêm Nhóm Quyền";
            fCapNhat.Show(0);
            onTaiLai();
        }
        protected void onCapNhat(int _ID)
        {
            //check quyen sua
            
            fCapNhat.TieuDe = "Cập nhật nhóm quyền";
            fCapNhat.isThemMoi = false;
            fCapNhat.Show(_ID);

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

            frmXacNhan.Show("Xóa dòng được chọn ?", "300px", new System.Action(async () =>
            {
                AppData.loadingPanel.show();
                var rsModel = new ResultModel<int?>();
                await Task.Run(() => { rsModel = ds_nhomquyenService.DeleteById(_ID); });
                AppData.loadingPanel.hide();
                if (rsModel.isThanhCong) onTaiLai();
                else toastService.ShowDanger(rsModel.ThongBao);
            }));
        }

    }
}

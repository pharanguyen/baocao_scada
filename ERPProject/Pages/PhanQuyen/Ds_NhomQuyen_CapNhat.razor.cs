using DAO.Models.PhanQuyen;
using Microsoft.AspNetCore.Components;
using ERPProject.Services;
using DAO.Models.CommonModels;
using System.Runtime.Serialization;
using DAO.Services.PhanQuyen;

namespace ERPProject.Pages.PhanQuyen
{
    public class Ds_NhomQuyen_CapNhatBase: ComponentBase
    {
        public bool isShow = false;
        public bool isThemMoi = false;
        public string TieuDe = "";
        public bool isDaCapNhatDuLieu = false;
        [Parameter]
        public Action<bool> OnLuu { get; set; }
        public int objID { get; set; }
        //khai báo hiển thị thông báo
        [Inject]
        protected ToastService toastService { get; set; }

        public ds_nhomquyen objModel = new ds_nhomquyen();
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = ds_nhomquyenService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else {
                objModel = new ds_nhomquyen();
            }
            isShow = true;
            StateHasChanged();
        }
        protected void onClose()
        {
            isShow = false;
            StateHasChanged();
        }
        protected async void onSave()
        {
            if (objModel.nhomquyen_name == null)
            {
                toastService.ShowWarning("Chưa nhập tên nhóm quyền !");
                // tbHoTen.FocusIn();
                return;
            }
            if (objModel.chuthich == null)
            {
                toastService.ShowWarning("Chưa nhập chú thích !");
                // tbHoTen.FocusIn();
                return;
            }
            try
            {
                if (objModel.nhomquyen_id == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = ds_nhomquyenService.Add(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm nhóm quyền!");
                    isShow = false;
                }
                else
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = ds_nhomquyenService.Update(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã cập nhật!");
                    isDaCapNhatDuLieu = true;
                    isShow = false;
                }
            }
            catch (Exception ex)
            {
                toastService.ShowDanger(ex.Message);
            }
            StateHasChanged();
        }

    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc.PhanLoaiDoiTuong;
using DAO.Services.DanhMuc.PhanLoaiDoiTuong;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc.PhanLoaiDoiTuong
{
    public class Loai_HDon_CapNhatBase : ComponentBase
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

        public loai_hdon objModel = new loai_hdon();
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = loai_hdonService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new loai_hdon();
            }
            isShow = true;
            StateHasChanged();
        }
        protected async void onSave()
        {
            if (objModel.mo_ta_loai_hd == null)
            {
                toastService.ShowWarning("Chưa nhập tên loại hóa đơn!");
                // tbHoTen.FocusIn();
                return;
            }
            try
            {
                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = loai_hdonService.Add(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm hóa đơn!");
                    isShow = false;
                }
                else
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = loai_hdonService.Update(objModel); });
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

        public void onClose()
        {
            isShow = false;
            StateHasChanged();
        }

        public void XacNhanLuu()
        {

        }
    }
}

using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.PhanLoaiDoiTuong;
using DAO.Services.DanhMuc;
using DAO.Services.DanhMuc.PhanLoaiDoiTuong;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc.PhanLoaiDoiTuong
{
    public class Loai_K_Hang_CapNhatBase : ComponentBase
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

        public loai_k_hang objModel = new loai_k_hang();
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = loai_k_hangService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new loai_k_hang();
            }
            isShow = true;
            StateHasChanged();
        }
        protected async void onSave()
        {
            if (objModel.ten_loai_kh == null)
            {
                toastService.ShowWarning("Chưa nhập tên loại khách hàng !");
                // tbHoTen.FocusIn();
                return;
            }
            try
            {
                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = loai_k_hangService.Add(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm chi nhánh!");
                    isShow = false;
                }
                else
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = loai_k_hangService.Update(objModel); });
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

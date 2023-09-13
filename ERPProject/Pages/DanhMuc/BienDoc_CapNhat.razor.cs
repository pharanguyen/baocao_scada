using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Services.DanhMuc;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc
{
    public class BienDoc_CapNhatBase : ComponentBase
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

        public bien_doc objModel = new bien_doc();
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = bien_docService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new bien_doc();
            }
            isShow = true;
            StateHasChanged();
        }
        protected async void onSave()
        {
            if (objModel.ten_bd == null)
            {
                toastService.ShowWarning("Chưa nhập tên chi nhánh !");
                // tbHoTen.FocusIn();
                return;
            }
            try
            {
                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = bien_docService.Add(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm biên đọc thành công!");
                    isShow = false;
                }
                else
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = bien_docService.Update(objModel); });
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

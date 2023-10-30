using Microsoft.AspNetCore.Components;
using ERPProject.Services;
using DAO.Models.CommonModels;
using DAO.Services.DanhMuc;

namespace ERPProject.Pages.DanhMuc
{
    public partial class Dm_ThongSo_CapNhatBase: ComponentBase
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

        public DAO.Models.DanhMuc.Dm_ThongSo objModel = new DAO.Models.DanhMuc.Dm_ThongSo();
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = DmThongSoService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new DAO.Models.DanhMuc.Dm_ThongSo();
            }
            isShow = true;
            isDaCapNhatDuLieu = true;
            StateHasChanged();
        }
        protected async void onSave()
        {
            if (objModel.Ten == null)
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
                    await Task.Run(() => { rsModel = DmThongSoService.Add(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm chi nhánh thành công!");
                    isDaCapNhatDuLieu = true;
                    isShow = false;
                }
                else
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = DmThongSoService.Update(objModel); });
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


    }
}

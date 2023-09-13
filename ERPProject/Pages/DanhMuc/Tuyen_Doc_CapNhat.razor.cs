using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Services.DanhMuc;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc
{
    public class Tuyen_Doc_CapNhatBase : ComponentBase
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

        public prc_tuyen_doc objModel = new prc_tuyen_doc();

        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = tuyen_docService.GetById_prc_tuyen_doc(_id);

                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new prc_tuyen_doc();
            }
            isShow = true;
            StateHasChanged();
        }

        protected async void onSave()
        {
            if (objModel.ms_tuyen == null || objModel.ms_tuyen.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập mã tuyến !");
                return;
            }
            if (objModel.mo_ta_tuyen == null|| objModel.mo_ta_tuyen.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập tên tuyến !");
                return;
            }
            if (objModel.ms_tql == null || objModel.ms_tql.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập tổ quản lý !");
                return;
            }
            if (objModel.ms_cky == null || objModel.ms_tql.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập số chu kỳ !");
                return;
            }
            try
            {
                tuyen_doc td = new tuyen_doc();
                td.ms_tuyen = objModel.ms_tuyen;
                td.ms_tql = objModel.ms_tql;
                td.ms_cky = objModel.ms_cky;
                td.ms_bd = objModel.ms_bd;
                td.mo_ta_tuyen = objModel.mo_ta_tuyen;
                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    //kiểm tra tuyến đọc đã tồn tại chưa
                    tuyen_doc temp = new tuyen_doc();
                    temp = tuyen_docService.GetById(objModel.ms_tuyen).Data;
                    if(temp != null)
                    {
                        toastService.ShowWarning("Đã tồn tại tuyến đọc !");
                        return;
                    }

                 
                    await Task.Run(() => { rsModel = tuyen_docService.Add(td); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm tuyến ["+ td.ms_tuyen + "]!");
                    isShow = false;
                }
                else
                {
                    int ms_tuyen = objID;
                    if (ms_tuyen != objModel.ms_tuyen)
                    {
                        toastService.ShowWarning("Không được thay đổi mã tuyến !");
                        return;
                    }
                    else {
                        var rsModel = new ResultModel<int?>();
                        await Task.Run(() => { rsModel = tuyen_docService.Update(td); });
                        if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                        toastService.ShowSuccess("Đã cập nhật!");
                        isDaCapNhatDuLieu = true;
                        isShow = false;
                    }
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

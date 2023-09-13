using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Services.DanhMuc;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc
{
    public class Phuong_CapNhatBase : ComponentBase
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

        public prc_phuong objModel = new prc_phuong();

        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = phuongService.GetById_prc_phuong(_id);

                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new prc_phuong();
            }
            isShow = true;
            StateHasChanged();
        }

        protected async void onSave()
        {
            
          
            if (objModel.ms_quan == null || objModel.ms_quan.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập thông tin quận!");
                return;
            }
            if (objModel.ten_phuong == null || objModel.ten_phuong.Equals(""))
            {
                toastService.ShowWarning("Chưa tên phường !");
                return;
            }
            try
            {
                phuong td = new phuong();
                td.ms_phuong = objModel.ms_phuong;
                td.ms_quan = objModel.ms_quan;
                td.ten_phuong = objModel.ten_phuong;
                td.vi_tri = objModel.vi_tri;
                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    //kiểm tra tuyến đọc đã tồn tại chưa
                    //phuong temp = new phuong();
                    //temp = phuongService.GetById(objModel.ms_phuong).Data;
                    //if (temp != null)
                    //{
                    //    toastService.ShowWarning("Đã tồn tại tuyến đọc !");
                    //    return;
                    //}


                    await Task.Run(() => { rsModel = phuongService.Add(td); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm tuyến [" + td.ten_phuong + "]!");
                    isShow = false;
                }
                else
                {
                    
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = phuongService.Update(td); });
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

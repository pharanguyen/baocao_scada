using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Services.DanhMuc;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc
{
    public class Dm_Tql_CapNhatBase : ComponentBase
    {
        /* public bool isShow = false;
         public bool isThemMoi = false;
         public string TieuDe = "";
         public bool isDaCapNhatDuLieu = false;
         [Parameter]
         public Action<bool> OnLuu { get; set; }
         public int objID { get; set; }
         //khai báo hiển thị thông báo
         [Inject]
         protected ToastService toastService { get; set; }

         public prc_Tql objModel = new prc_Tql();

         public void Show(int _id)
         {
             objID = _id;
             if (!isThemMoi && objID != 0)
             {
                 var rsModel = DmTqlService.GetById(_id);

                 if (rsModel.isThanhCong) objModel = rsModel.Data;
                 else
                 {
                     toastService.ShowDanger(rsModel.ThongBao);
                     return;
                 }
             }
             else
             {
                 objModel = new prc_Tql();
             }
             isShow = true;
             StateHasChanged();
         }

         protected async void onSave()
         {


             if (objModel.TenTql == null || objModel.TenTql.Equals(""))
             {
                 toastService.ShowWarning("Chưa nhập thông tin Tổ quản lí");
                 return;
             }
             if (objModel.TenChiNhanh == null || objModel.TenChiNhanh.Equals(""))
             {
                 toastService.ShowWarning("Chưa nhập chi nhánh !");
                 return;
             }
             if (objModel.Ma == null || objModel.Ma.Equals(""))
             {
                 toastService.ShowWarning("Chưa nhập Mã chi nhánh");
                 return;
             }
             if (objModel.Dia_Chi == null || objModel.Dia_Chi.Equals(""))
             {
                 toastService.ShowWarning("Chưa nhập thông tin địa chỉ");
                 return;
             }
             if (objModel.Stt == null || objModel.Stt.Equals(""))
             {
                 toastService.ShowWarning("Chưa nhập thông tin thứ tự");
                 return;
             }
             try
             {
                prc_Tql td = new prc_Tql();
                // td.Id = objModel.Id;
                 td.TenTql = objModel.TenTql;
                 td.TenChiNhanh = objModel.TenChiNhanh;
                 td.Ma = objModel.Ma;
                 td.Dia_Chi = objModel.Dia_Chi;
                 td.Stt = objModel.Stt;
                 if (objID == 0)
                 {
                     var rsModel = new ResultModel<int?>();
                     //kiểm tra tuyến đọc đã tồn tại chưa
                     //Dm_Tql temp = new Dm_Tql();
                     //temp = DmTqlService.GetById(objModel.ms_Dm_Tql).Data;
                     //if (temp != null)
                     //{
                     //    toastService.ShowWarning("Đã tồn tại tuyến đọc !");
                     //    return;
                     //}


                     await Task.Run(() => { rsModel = DmTqlService.Add(td); });
                     if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                     toastService.ShowSuccess("Đã thêm tuyến [" + td.TenTql + "]!");
                     isShow = false;
                 }
                 else
                 {

                     var rsModel = new ResultModel<int?>();
                     await Task.Run(() => { rsModel = DmTqlService.Update(td); });
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
     }*/
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

        public DAO.Models.DanhMuc.prc_Tql objModel = new DAO.Models.DanhMuc.prc_Tql();
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = DmTqlService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new DAO.Models.DanhMuc.prc_Tql();
            }
            isShow = true;
            StateHasChanged();
        }
        protected async void onSave()
        {
            if (objModel.TenTql == null)
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
                    await Task.Run(() => { rsModel = DmTqlService.Add(objModel); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm chi nhánh thành công!");
                    isShow = false;
                }
                else
                {
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = DmTqlService.Update(objModel); });
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


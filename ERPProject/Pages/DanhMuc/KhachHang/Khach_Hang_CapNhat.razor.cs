using DAO.Models.CommonModels;
using DAO.Models.DanhMuc;
using DAO.Models.DanhMuc.KhachHang;
using DAO.Services.DanhMuc;
using DAO.Services.DanhMuc.KhachHang;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc.KhachHang
{
    public class Khach_Hang_CapNhatBase : ComponentBase
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

        public prc_khach_hang objModel = new prc_khach_hang();

        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = khach_hangService.GetById_prc_khach_hang(_id);

                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new prc_khach_hang();
            }
            isShow = true;
            StateHasChanged();
        }

        protected async void onSave()
        {
            if (objModel.ten_kh == null || objModel.ten_kh.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập tên khách hàng !");
                return;
            }
            if (objModel.dia_chi_kh == null || objModel.dia_chi_kh.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập địa chỉ khách hàng !");
                return;
            }
            if (objModel.dien_thoai == null || objModel.dien_thoai.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập tên khách hàng !");
                return;
            }
            if (objModel.ms_loai_kh == null || objModel.ms_loai_kh.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập tổ quản lý !");
                return;
            }
            try
            {
                khach_hang td = new khach_hang();
                td.ms_kh = objModel.ms_kh;
                td.ten_kh = objModel.ten_kh;
                td.dia_chi_kh = objModel.dia_chi_kh;
                td.ms_duong = objModel.ms_duong;
                td.dien_thoai = objModel.dien_thoai;
                td.ngay_nhap_kh = objModel.ngay_nhap_kh;
                td.ms_thue = objModel.ms_thue;
                td.so_tai_khoan = objModel.so_tai_khoan;
                td.mo_tai = objModel.so_tai_khoan;
                td.ms_loai_kh = objModel.ms_loai_kh;
                td.ms_tt_kh = objModel.ms_tt_kh;
                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    //kiểm tra tuyến đọc đã tồn tại chưa
                    khach_hang temp = new khach_hang();
                    temp = khach_hangService.GetById(objModel.ms_kh).Data;
                    if (temp != null)
                    {
                        toastService.ShowWarning("Đã tồn tại khách hàng !");
                        return;
                    }


                    await Task.Run(() => { rsModel = khach_hangService.Add(td); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    toastService.ShowSuccess("Đã thêm khách hàng [" + td.ten_kh + "]!");
                    isShow = false;
                }
                else
                {
                    int ms_kh = objID;                   
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = khach_hangService.Update(td); });
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

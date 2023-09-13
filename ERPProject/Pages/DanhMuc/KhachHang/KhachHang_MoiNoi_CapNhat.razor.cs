using DAO.Models.CommonModels;
using DAO.Models.DanhMuc.KhachHang;
using DAO.Services.DanhMuc.KhachHang;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;

namespace ERPProject.Pages.DanhMuc.KhachHang
{
    public class KhachHang_MoiNoi_CapNhatBase : ComponentBase
    {
        public bool isShow = false;
        public bool isThemMoi = false;
        public string TieuDe = "";
        public bool isDaCapNhatDuLieu = false;
        public int selectedTabIndex = 0;
        [Parameter]
        
        public Action<bool> OnLuu { get; set; }
        public int objID { get; set; }
        //khai báo hiển thị thông báo
        [Inject]
        protected ToastService toastService { get; set; }

        public prc_khachhang_moinoi objModel = new prc_khachhang_moinoi();

        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = khachhang_moinoiService.GetById_prc_khachhang_moinoi_byms_mnoi(_id);

                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                objModel = new prc_khachhang_moinoi();
            }
            isShow = true;
            StateHasChanged();
        }

        protected async void onSave()
        {
            if (objModel.nguoi_thue == null || objModel.ten_kh.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập tên khách hàng !");
                return;
            }
            if (objModel.dia_chi_mnoi == null || objModel.dia_chi_mnoi.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập địa chỉ điểm dùng !");
                return;
            }
            if (objModel.dien_thoai == null || objModel.dien_thoai.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập số điện thoại điểm dùng !");
                return;
            }
            if (objModel.ms_loai_mn == null || objModel.ms_loai_mn.Equals(""))
            {
                toastService.ShowWarning("Chưa nhập loại mối nối !");
                return;
            }
            try
            {
                moi_noi mn = new moi_noi();
                mn.ms_cs = objModel.ms_cs;
                mn.ms_duong = objModel.ms_duong;
                mn.ms_bduong = objModel.ms_duong;
                mn.ms_loai_mn = objModel.ms_loai_mn;
                mn.ms_phuong = objModel.ms_phuong;
                mn.ms_k_hoach = objModel.ms_k_hoach;
                mn.ms_tuyen = objModel.ms_tuyen;
                mn.ms_kh = objModel.ms_kh;
                mn.ms_tt_dd = objModel.ms_tt_dd;
                mn.ms_ttrang = objModel.ms_ttrang;
                mn.ms_cach_tinh = objModel.ms_cach_tinh;
                mn.ms_dhk = objModel.ms_dhk;
                mn.ms_dh_mn = objModel.ms_dh_mn;
                mn.nguoi_thue = objModel.nguoi_thue;
                mn.dia_chi_mnoi = objModel.dia_chi_mnoi;
                mn.stt_lo_trinh = objModel.stt_lo_trinh;
                mn.tinh_ptn = objModel.tinh_ptn;
                mn.tien_dho = objModel.tien_dho;
                mn.tien_dho_tra = objModel.tien_dho_tra;
                mn.toa_do_bac = objModel.toa_do_bac;
                mn.toa_do_dong = objModel.toa_do_dong;
                mn.ms_han_muc = objModel.ms_han_muc;
                mn.tinh_luy_ke = objModel.tinh_luy_ke;
                mn.ms_cong_thuc_moi = objModel.ms_cong_thuc_moi;
                mn.ms_ht_ld = objModel.ms_ht_ld;
                mn.in_the_KH = objModel.in_the_KH;
                mn.ngay_dk_tu_doc = objModel.ngay_dk_tu_doc;

                if (objID == 0)
                {
                    var rsModel = new ResultModel<int?>();
                    //kiểm tra tuyến đọc đã tồn tại chưa
                    moi_noi temp = new moi_noi();
                    temp = moi_noiService.GetById(objModel.ms_mnoi).Data;
                    if (temp != null)
                    {
                        toastService.ShowWarning("Đã tồn tại khách hàng !");
                        return;
                    }
                    //cập nhật thông tin khách hàng
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

                        var rsModelKH = new ResultModel<int?>();
                        //kiểm tra tuyến đọc đã tồn tại chưa
                        khach_hang tempkh = new khach_hang();
                        tempkh = khach_hangService.GetById(objModel.ms_kh).Data;
                        if (temp != null)
                        {
                            toastService.ShowWarning("Đã tồn tại khách hàng !");
                            return;
                        }


                        await Task.Run(() => { rsModelKH = khach_hangService.Add(td); });
                        if (!rsModelKH.isThanhCong)
                        {
                            throw new Exception(rsModelKH.ThongBao);

                        }
                        else
                        {
                            if(rsModelKH.Data != null) { 
                                mn.ms_kh = (int)rsModelKH.Data;
                                await Task.Run(() => { rsModel = moi_noiService.Add(mn); });
                                if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                                toastService.ShowSuccess("Đã thêm điểm dùng [" + mn.nguoi_thue + "]!");
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
                else
                {
                    int ms_kh = objID;
                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = moi_noiService.Update(mn); });
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

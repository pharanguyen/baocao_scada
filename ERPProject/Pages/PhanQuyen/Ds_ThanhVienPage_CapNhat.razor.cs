using DAO.Models.CommonModels;
using DAO.Models.PhanQuyen;
using DAO.Services.PhanQuyen;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System.Security.Cryptography;

namespace ERPProject.Pages.PhanQuyen
{
    public class Ds_ThanhVienPage_CapNhatBase : ComponentBase
    {
        [Inject]
        protected ToastService toastService { get; set; }

        [Inject]
        protected AppDataScoped AppData { get; set; }

        [Parameter]
        public Action<bool> OnLuu { get; set; }
        [Parameter]
        public int objID { get; set; }

        public string TieuDe = "";
        public bool isThemMoi = false;
        public prc_ThanhVien_DanhSach objModel = new prc_ThanhVien_DanhSach();
        public bool isShow = false;
        public bool isDaCapNhatDuLieu = false;

        protected async void onSave()
        {
            if (objID == 0)
            {
                //gán dữ liệu cập nhật bảng ds_thanhvien
                ds_thanhvien dstv = new ds_thanhvien();
                dstv.thanhvien_name = objModel.thanhvien_name;
                dstv.tendangnhap = objModel.tendangnhap;
                dstv.donvi = objModel.donvi;
                dstv.nhomquyen_id = objModel.nhomquyen_id;    
                dstv.ms_chinhanh = objModel.ms_chinhanh;
                dstv.ms_tql = objModel.ms_tql;
                dstv.chuthich = objModel.chuthich;
                dstv.tendangnhap= objModel.tendangnhap;
                dstv.matkhau = objModel.matkhau;
                dstv.phutrachchinh = objModel.phutrachchinh;
                var rsModel = new ResultModel<int?>();
                await Task.Run(() => { rsModel = ds_thanhvienService.Add(dstv); });
                if (rsModel.isThanhCong)
                {
                    int thanhvien_id = (int)rsModel.Data;
                    int nhomquyen_id = (int)dstv.nhomquyen_id;
                    //lấy toàn bộ quyền đc phân cho nhóm này ghi vào bảng phân quyền tương ứng với id của thành viên
                    var rsModel1 = new ResultModel<List<prc_ds_quyen_by_nhomquyenlist>>();
                    List<prc_ds_quyen_by_nhomquyenlist> listquyentheonhom = new List<prc_ds_quyen_by_nhomquyenlist>();
                    rsModel1 = ds_quyenService.GetAllDsQuyenTheoNhom(thanhvien_id, nhomquyen_id);
                    listquyentheonhom = rsModel1.Data;
                    ds_phanquyen dspq = new ds_phanquyen();
                    foreach (prc_ds_quyen_by_nhomquyenlist ds in listquyentheonhom)
                    {
                        dspq.thanhvien_id = thanhvien_id;
                        dspq.dsquyen_id = ds.dsquyen_id;
                        dspq.them = false;
                        dspq.sua = false;
                        dspq.xoa = false;

                        var rsModel2 = new ResultModel<int?>();
                        await Task.Run(() => { rsModel2 = ds_phanquyenService.Add(dspq); });
                        if (!rsModel2.isThanhCong) throw new Exception(rsModel.ThongBao);
                    }
             
                    toastService.ShowSuccess("Cập nhật thành viên mới thành công!");
                    //binding data to grid
                    //objModel = rsModel.Data;
                    AppData.loadingPanel.hide();
                }
                else
                {
                    AppData.loadingPanel.hide();
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
                StateHasChanged();
            }
            //trường hợp sửa
            if (objID != 0)
            {
                //lấy dữ liệu cũ của thành viên
                var rsModel = ds_thanhvienService.GetById(objID);
                prc_ThanhVien_DanhSach tv = rsModel.Data;
                prc_ThanhVien_DanhSach tvChanged = objModel;
                //kiem tra xem nhom quyen co thay doi khong
                if (tvChanged.nhomquyen_id != tv.nhomquyen_id)
                {
                    //xoa toan bo quyen của thanh vien nay trong bang ds_phanquyen
                    var rs = new ResultModel<int?>();
                    rs = ds_phanquyenService.DeleteByThanhVienId(objID);                    
                    if (rs.Data == 1) { 
                        //toastService.ShowSuccess("Ok!");
                    }else
                    {
                        toastService.ShowSuccess(rs.ThongBao);
                        return;
                    }
                    //lấy toàn bộ quyền đc phân cho nhóm này ghi vào bảng phân quyền tương ứng với id của thành viên
                    var rsModel1 = new ResultModel<List<prc_ds_quyen_by_nhomquyenlist>>();
                    List<prc_ds_quyen_by_nhomquyenlist> listquyentheonhom = new List<prc_ds_quyen_by_nhomquyenlist>();
                    rsModel1 = ds_quyenService.GetAllDsQuyenTheoNhom(objID, (int)tvChanged.nhomquyen_id);
                    listquyentheonhom = rsModel1.Data;
                    ds_phanquyen dspq = new ds_phanquyen();
                    foreach (prc_ds_quyen_by_nhomquyenlist ds in listquyentheonhom)
                    {
                        dspq.thanhvien_id = objID;
                        dspq.dsquyen_id = ds.dsquyen_id;
                        dspq.them = false;
                        dspq.sua = false;
                        dspq.xoa = false;

                        var rsModel2 = new ResultModel<int?>();
                        await Task.Run(() => { rsModel2 = ds_phanquyenService.Add(dspq); });
                        if (!rsModel2.isThanhCong) throw new Exception(rsModel.ThongBao);
                    }
                    //toastService.ShowSuccess("Đã cập nhật!");

                }
                //kiem tra xem da ton tai thanh vien khac co cung ten dang nhap
                string tenmoi = tvChanged.tendangnhap;
                //lay danh sach thanh vien
                List<prc_ThanhVien_DanhSach> listthanhviencungten = ds_thanhvienService.GetThanhVienByTenDangNhap(tenmoi).Data;
                if(listthanhviencungten.Count > 0) {
                    toastService.ShowSuccess("Đã tồn tại thành viên có tên đăng nhập này, vui lòng kiểm tra lại!");
                    return;
                }
                //khong trung ten
                else
                {
                                     
                    ds_thanhvien dstv = new ds_thanhvien();
                    dstv.thanhvien_id = objModel.thanhvien_id;
                    dstv.thanhvien_name = objModel.thanhvien_name;
                    dstv.tendangnhap = objModel.tendangnhap;
                    dstv.donvi = objModel.donvi;
                    dstv.nhomquyen_id = objModel.nhomquyen_id;
                    dstv.ms_chinhanh = objModel.ms_chinhanh;
                    dstv.ms_tql = objModel.ms_tql;
                    dstv.chuthich = objModel.chuthich;
                    dstv.tendangnhap = objModel.tendangnhap;
                    dstv.matkhau = objModel.matkhau;
                    dstv.phutrachchinh = objModel.phutrachchinh;
                    var rs = new ResultModel<int?>();
                    rs = ds_thanhvienService.Update(dstv);
                    if(rs.isThanhCong) {
                        toastService.ShowSuccess("Cập nhật thành công!");
                    }
                    else { return; }

                }






            }
        }
        protected void onClose()
        {
           

            isShow = false;
            StateHasChanged();
        }
        public void Show(int _id)
        {
            objID = _id;
            if (!isThemMoi && objID != 0)
            {
                var rsModel = ds_thanhvienService.GetById(_id);
                if (rsModel.isThanhCong) objModel = rsModel.Data;
                else
                {
                    toastService.ShowDanger(rsModel.ThongBao);
                    return;
                }
            }
            else
            {
                //Kiểm tra xem có quyền thêm mới không
                objModel = new prc_ThanhVien_DanhSach();
            }
               
            isShow = true;
            StateHasChanged();
        }

    }
}

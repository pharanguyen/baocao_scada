using DAO.Models.CommonModels;
using DAO.Models.PhanQuyen;
using DAO.Services.PhanQuyen;
using ERPProject.Libs;
using ERPProject.Shared;
using ERPProject.Services;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Schedule;
using System.Linq;
using ERPProject.Shared.Combobox;

namespace ERPProject.Pages.PhanQuyen
{
    public class Ds_ThanhVien_PhanQuyenChiTietBase : ComponentBase
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
        public List<prc_ds_quyen_by_nhomquyenlist> listdsquyen = new List<prc_ds_quyen_by_nhomquyenlist>();
        public bool isShow = false;
        public bool isDaCapNhatDuLieu = false;
        public clsGlobal glb = new clsGlobal();
        public bool chkXem = false;
        public bool chkThem = false;
        public bool chkSua = false;
        public bool chkXoa = false;

        public bool isDisableCheckbox = true;
        protected override void OnInitialized()
        {

        }
        public void Show(int _id, int nhomquyen_id)
        {
            objID = _id;

            //lấy ds quyền của thành viên theo thanhvien_id + nhomquyen_id
            var rsModel = ds_quyenService.GetAllDsQuyenTheoNhom(_id, nhomquyen_id);
            listdsquyen = rsModel.Data;
            //Hiển thị dialog
            isShow = true;
            StateHasChanged();
        }
        public void onSave()
        {
            try
            {
                //Xoa toan bo quyen cua thanh vien trong bang phanquyen
                var rsDelete = new ResultModel<int?>();
                rsDelete = ds_phanquyenService.DeleteByThanhVienId(objID);
                if (rsDelete.Data != 1)
                {
                    toastService.ShowDanger(rsDelete.ThongBao);
                    return;
                }
                else
                {
                    foreach (prc_ds_quyen_by_nhomquyenlist dspq in listdsquyen)
                    {
                        if (dspq.Xem == true)
                        {
                            ds_phanquyen ds = new ds_phanquyen();
                            ds.dsquyen_id = dspq.dsquyen_id;
                            ds.thanhvien_id = objID;
                            ds.them = dspq.them;
                            ds.sua = dspq.sua;
                            ds.xoa = dspq.xoa;
                            //ds.phanquyen_id = dspq.phanquyen_id;
                            ds_phanquyenService.Add(ds);

                        }

                    }
                    toastService.ShowSuccess("Cập nhật thành công!");
                }


            }
            catch (Exception ex)
            {
                toastService.ShowDanger(ex.Message);
            }

        }

        public void onClose()
        {
            isShow = false;
            StateHasChanged();
        }
       


        public void onChange(prc_ds_quyen_by_nhomquyenlist temp)
        {
            //get dsphanquyen_id
            //string kytudau2 = temp.dsquyen_id.Substring(0, 2).ToString();
            string dsphanquyen_id = temp.dsquyen_id;
            int i = glb.g_CapNode(dsphanquyen_id, "22222");
            if (temp.Xem == false)
            {
                temp.them = false;
                temp.sua = false;
                temp.xoa = false;

            }
            //    string sTmp = dsphanquyen_id.Substring(0, 2) + "00000000"; //0200000000
            foreach (prc_ds_quyen_by_nhomquyenlist dspq in listdsquyen)
            {
                if (i == 1) //0200000000
                {
                    string kytudau2 = temp.dsquyen_id.Substring(0, 2).ToString();
                    int node = glb.g_CapNode(dspq.dsquyen_id, "222222");
                    if (node > 1 && kytudau2.Equals(dspq.dsquyen_id.Substring(0, 2)))
                    {
                        if (temp.Xem == false)
                        {
                            dspq.Xem = false;
                            dspq.them = false;
                            dspq.sua = false;
                            dspq.xoa = false;
                        }
                    }
                }

                if (i == 2) //0201000000
                {
                    string kytudau4 = temp.dsquyen_id.Substring(0, 4).ToString();
                    int node = glb.g_CapNode(dspq.dsquyen_id, "22222");
                    if (node > 2 && kytudau4.Equals(dspq.dsquyen_id.Substring(0, 4)))
                    {
                        if (temp.Xem == false)
                        {
                            dspq.Xem = false;
                            dspq.them = false;
                            dspq.sua = false;
                            dspq.xoa = false;
                        }
                    }
                }

                if (i == 3) //0201000000
                {
                    string kytudau4 = temp.dsquyen_id.Substring(0, 4).ToString();
                    int node = glb.g_CapNode(dspq.dsquyen_id, "2222");
                    if (node > 3 && kytudau4.Equals(dspq.dsquyen_id.Substring(0, 6)))
                    {
                        if (temp.Xem == false)
                        {
                            dspq.Xem = false;
                            dspq.them = false;
                            dspq.sua = false;
                            dspq.xoa = false;
                        }
                    }
                }
                if (i == 4) //0201000000
                {
                    string kytudau4 = temp.dsquyen_id.Substring(0, 4).ToString();
                    int node = glb.g_CapNode(dspq.dsquyen_id, "222");
                    if (node > 4 && kytudau4.Equals(dspq.dsquyen_id.Substring(0, 8)))
                    {
                        if (temp.Xem == false)
                        {
                            dspq.Xem = false;
                            dspq.them = false;
                            dspq.sua = false;
                            dspq.xoa = false;
                        }
                    }
                }
            }

            //toastService.ShowSuccess(dsquyen_id);
        }
        public void ValueChanged(Microsoft.AspNetCore.Components.ChangeEventArgs args)

        {
            if (chkXem == false)
            {
                this.isDisableCheckbox = true;
                chkThem = false;
                chkSua = false;
                chkXoa = false;
            }
            else
            {
                this.isDisableCheckbox = false;
            }
        }

        protected CboDanhMuc cboDanhMuc;
        public void onThucHien(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            //lay dsquyen_id;
            string dsquyen_id = cboDanhMuc.Value;

            if(dsquyen_id == null)
            {
                return;
            }
            string sTmp = dsquyen_id.ToString().Substring(0, 2);
            foreach(prc_ds_quyen_by_nhomquyenlist prc in listdsquyen)
            {
                if (prc.dsquyen_id.ToString().Substring(0, 2).Equals(sTmp))
                {
                    prc.Xem = chkXem;
                    prc.them = chkThem;
                    prc.sua = chkSua;
                    prc.xoa = chkXoa;
                }
            }
            //List<ds_quyen> listss = cboDanhMuc.list;    

        }
    }
}

using ERPProject.Services;
using Microsoft.AspNetCore.Components;
using DAO.Models.PhanQuyen;
using Syncfusion.Blazor.Grids;
using ERPProject.Libs;
using DAO.Models.CommonModels;
using DAO.Services.PhanQuyen;

namespace ERPProject.Pages.PhanQuyen
{
    public class PhanQuyenNhomGridBase : ComponentBase
    {
        [Inject]
        AppDataScoped AppData { get; set; }
        [Inject]
        HttpClient Http { get; set; }
        [Inject]
        protected Blazored.LocalStorage.ILocalStorageService localStorage { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        protected ToastService toastService { get; set; }

        public List<Quyen> lstQuyen { get; set; }
        protected List<ds_nhomquyen> lstNhomQuyen { get; set; }
        protected string[]? arr_Nhom { get; set; } = new string[100];
        public SfGrid<Quyen> myGrid { get; set; }
        public clsGlobal glb = new clsGlobal();

        protected override void OnInitialized()
        {
            var rsModel = ds_quyenService.GetAllDsQuyenToList();
            lstQuyen = rsModel.Data;
            var rsModelDsQuyen = ds_nhomquyenService.GetAll();
            lstNhomQuyen = rsModelDsQuyen.Data;
            for (int i = 0; i <= lstNhomQuyen.Count()-1; i++)
            {
                arr_Nhom[i] = lstNhomQuyen.ElementAt(i).nhomquyen_id.ToString();
            }
        }
        protected async void onTaiLai()
        {
            try
            {
                AppData.loadingPanel.show();
                var rsModel = new ResultModel<List<Quyen>>();
                await Task.Run(() => { rsModel = ds_quyenService.GetAllDsQuyenToList(); });
                if (rsModel.isThanhCong)
                {
                    lstQuyen = rsModel.Data;
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
            catch (Exception ex)
            {
                toastService.ShowDanger(ex.Message);
            }

        }
     

        protected async void onLuuQuyen()
        {
            try
            {

                AppData.loadingPanel.show();
        
                AppData.loadingPanel.hide();
                List<Quyen> list = lstQuyen.ToList();
                foreach( Quyen quyen in lstQuyen)
                {
                    string str = "";
                    if (quyen.col1 == true)
                    {
                        str += arr_Nhom[0].ToString() + ",";
                    }
                    if (quyen.col2 == true)
                    {
                        str += arr_Nhom[1].ToString() + ",";
                    }
                    if (quyen.col3 == true)
                    {
                        str += arr_Nhom[2].ToString() + ",";
                    }
                    if (quyen.col4 == true)
                    {
                        str += arr_Nhom[3].ToString() + ",";
                    }
                    if (quyen.col5 == true)
                    {
                        str += arr_Nhom[4].ToString() + ",";
                    }
                    if (quyen.col6 == true)
                    {
                        str += arr_Nhom[5].ToString() + ",";
                    }
                    if (quyen.col8 == true)
                    {
                        str += arr_Nhom[6].ToString() + ",";
                    }
                    if (quyen.col11 == true)
                    {
                        str += arr_Nhom[7].ToString() + ",";
                    }
                    if (quyen.col12 == true)
                    {
                        str += arr_Nhom[8].ToString() + ",";
                    }
                    ds_quyen dsquyen = new ds_quyen();
                    dsquyen.dsquyen_id = quyen.dsquyen_id;
                    dsquyen.dsquyen_name = quyen.dsquyen_name;
                    dsquyen.menu_name = quyen.menu_name;
                    dsquyen.nhomquyen_list =str;
                    dsquyen.tenform_list = quyen.tenform_list;
                    dsquyen.trangthai = quyen.trangthai;

                    var rsModel = new ResultModel<int?>();
                    await Task.Run(() => { rsModel = ds_quyenService.Update(dsquyen); });
                    if (!rsModel.isThanhCong) throw new Exception(rsModel.ThongBao);
                    //toastService.ShowSuccess("Đã cập nhật!");

                }
                //xoa quyen dc cap cho tat ca thanh vien vua huy
                ds_quyenService.DeleteAll();
                toastService.ShowSuccess("Đã cập nhật!");

            }
            catch (Exception ex)
            {
                toastService.ShowDanger(ex.Message);
            }

        }

    }
}

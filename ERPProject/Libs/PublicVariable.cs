using System;
using System.Data.SqlClient;

namespace ERPProject.Libs
{
    public class PublicVariable
    {
        clsGlobal glb = new clsGlobal();
        public static string g_mau_file_bao_cao = "mau_file_bao_cao.xlsx";
        public static long g_MaKhachHang = 0;
        public static long g_MaDiemDung = 0;
        public static byte g_CheDoKhoiTao = 0;
        //Thông tin liên quan đến chi nhánh cửa người đăng nhập
        public static long g_MaChiNhanh = 0;
        public static string g_TenChiNhanh = "";
        //public static string g_FolderBarToPDA = @"C:\PDA_Data_Output";
        public static string g_FolderPDA = @"C:\PDA";
        public static string g_FolderBarToPDA = @"C:\PDA\BarToPDA";
        public static string g_FolderPDAToBar = @"C:\PDA\PDAToBar";
        //Biến này sử dụng người quản lý tại các chi nhánh
        //Được phép sửa lại thông tin nhập số và tính tiền
        public static bool g_PhuTrachChinh = false;
        public static string g_IPWebAPI = @"http://113.160.100.217:8080";
        public static long g_MaToQuanLy = 0;
        public static long g_MaSoTuyen = 0;
        public static long g_MaThoiKy = 130;
        public static bool g_PrintProgess = false;
        public static long g_MaSoDH = 0;
        public static long g_GioiHanSoBanGhi = 10000;
        public static long g_GioiHanTieuThu = 10000;
        public static string g_Tendangnhap = "";
        public static string g_Matkhaudangnhap = "";
        public static string g_USERLOGIN = "";
        public static string g_PASSLOGIN = "";
        public static string g_DatabaseName = "BARSDATA_HP";
        public static string g_DatabaseChuanName = "BARSDATA_HP";
        public static string g_DatabasePastName = "BARSDATA_PAST";
        public static string g_ServerName = "";
        public static string g_TenCongTy = "Công ty cổ phần Cấp nước Hải Phòng";
        public static string ID = "";
        public static string ID_Update = "";
        public static string skey = "conghoaxahoichunghiavietnamdoclaptudohanhphuc";
        public static string sMaBaoCao = "";
        //-----------------------------------------------------------
        public static SqlConnection ConnectSQL;
        public static string strConnectSQL;
        public static string g_MessBox = "QUAN LY HOA DON NUOC";
        //--------------------------------
        public static string g_String_0 = "00000000000000000000";
        //--------------------------------
        //bi?n l?u tên máy ch?
        public static string g_ComputerName = "VISL";
        //bi?n l?u tên Server
        public static string g_SQLServerName = "VISL";
        //-----------------------------------------------
        public static int g_THANHVIEN_ID = 0;
        public static int g_THANHVIEN_QUYEN = -1;
        //----------------------------------------------------
        public static int g_ms_gia_GD = 132;
        public static int g_ms_gia_KD = 142;
        //----------------------------------------------------
        //gia tri g_Licenses duoc gan =0 doi voi phien ban cai dat dau tien
        //sau nay doi voi mỗi 1 lan sua chu y thay doi gia trị g_Licenses<>0(dành cho file M_BARMAN.exe)
        public static int g_Licenses = 1;
        //giá trị g_LicensesUpdate được gán =0 đối với phiên bản cài đặt đầu tiên
        //sau này đối với mỗi 1 lần sửa chú ý thay đổi giá trị g_LicensesUpdate (dành cho file UpdateVersion)
        public static int g_LicensesUpdate = 1;
        //sonnt edit 27/07/2009
        public static Boolean bKtraDangNhap = false;
        public static DateTime g_TuNgay = Convert.ToDateTime("01/07/2009");
        public static DateTime g_DenNgay = Convert.ToDateTime("02/07/2009");
        public static DateTime g_ExpriteWarrantyDate = new DateTime(2019, 12, 31);//Convert.ToDateTime("10/31/2014");
        //Phungnd Bo sung biến phục vụ ngắt trang
        public static bool g_NgatTrangIn = true;
        //Phungnd Bổ sung cho báo cáo in hóa đơn nước
        public static bool g_PrintStart = false;
    }
}

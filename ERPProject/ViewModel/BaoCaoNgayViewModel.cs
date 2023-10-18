namespace ERPProject.ViewModel
{
    public class BaoCaoNgayViewModel
    {
        public string? ThoiGian { get; set; }
        public List<ThongSoTram>? dataTram { get; set; }
    }
    public class ThongSoTram
    {
        public string? TenTram { get; set; }
        public string? LuuLuongTucThoi { get; set; }
        public string? TongLuuLuong { get; set; }
    }
}

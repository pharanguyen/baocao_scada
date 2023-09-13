namespace DAO.Models.CommonModels
{
    public class ResultModel<T>
    {
        public bool isThanhCong { get; set; } = true;

        public string ThongBao { get; set; }

        public T Data { get; set; }

        public Paging Page { get; set; }
    }
}
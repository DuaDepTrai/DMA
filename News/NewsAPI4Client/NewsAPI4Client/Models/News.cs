namespace NewsAPI4Client.Models
{
    public class News
    {
        public int ID { get; set; }
        public string tieu_de { get; set; }
        public string tom_tat { get; set; }
        public string noi_dung { get; set; }
        public System.DateTime? time_tao { get; set; }
        public int? numberRead { get; set; }
        public string image { get; set; }
        public int? CategoryID { get; set; }
        public int? UsersID { get; set; }
    }
}

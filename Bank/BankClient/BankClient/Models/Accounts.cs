namespace BankClient.Models
{
    public class Accounts
    {
        public int AccountID { get; set; }
        public string AccountType { get; set; }
        public System.DateTime CreateDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? UserID { get; set; }
        public Users User { get; set; }
    }
}

namespace BankClient.Models
{
    public class Transactions
    {
        public int TransactID { get; set; }
        public int? RequestID { get; set; }
        public int? ReceiverID { get; set; }
        public System.DateTime? TransferTime { get; set; }
        public string Reason { get; set; }
        public decimal? Amount { get; set; }
    }
}

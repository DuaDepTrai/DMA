//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transactions
    {
        public int TransactID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> ReceiverID { get; set; }
        public Nullable<System.DateTime> TransferTime { get; set; }
        public string Reason { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual Accounts Accounts1 { get; set; }
    }
}

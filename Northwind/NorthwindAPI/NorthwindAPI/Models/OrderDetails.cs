using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindAPI.Models
{
    [Table("Order Details")]

    public class OrderDetails
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Orders")]
        public int OrderID { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public virtual Orders? Orders { get; set; }
        public virtual Products? Products { get; set; }
    }
}

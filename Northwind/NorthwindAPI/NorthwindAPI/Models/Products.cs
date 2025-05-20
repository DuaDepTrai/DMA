using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindAPI.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("Suppliers")]
        public int? SupplierID { get; set; }
        [ForeignKey("Categories")]
        public int? CategoryID { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set ; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public virtual Suppliers? Suppliers { get; set; }
        public virtual Categories? Categories { get; set; }
    }
}

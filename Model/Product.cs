using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team21V4._5.Model
{
    public class Product
    {
        [ConfigurationKeyName("productID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(100, ErrorMessage = "Product Name must not exceed 100 characters.")]
        public string? ProductName { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Stock On Hand is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock On Hand must be a non-negative number.")]
        public int StockOnHand { get; set; }
        //public int DeliveredQuantity { get; set; }

        //[Required(ErrorMessage = "Sales Quantity is required.")]
        //[Range(0, int.MaxValue, ErrorMessage = "Sales Quantity must be a non-negative number.")]
        //public int SalesQuantity { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [StringLength(100, ErrorMessage = "Department must not exceed 100 characters.")]
        public string? Department { get; set; }

        //public int WasteQuantity { get; set; }
        //public int BoughtStock { get; set; }
        [Required(ErrorMessage = "Temperature is required.")]
        [Range(-100, 100, ErrorMessage = "Temperature must be between -100 and 100.")]
        public float Temperature { get; set; }
        //public float Precipitation { get; set; }
    }
}

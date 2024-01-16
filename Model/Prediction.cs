using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Team21V4._5.Model
{
    public class Prediction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public float ProductID { get; set; }
        public string ProductName { get; set; }
        public float Score { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class Supplier
    {
        [Key]
        [Required]
        public  int  supplierId { get; set; }
        public string supplierName { get; set;}
        public string supplierAddress { get; set;}
        public string supplierContactNumber { get; set;}
        public string supplierEmail { get; set;}

    }
}

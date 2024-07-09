using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Stockable_Backend.Model
{
    public class Branch
    {
        [Key]
        public int branchId { get; set; }
        public string? branchName { get; set; }
        public string? branchCode { get; set; }


        [ForeignKey("Client")]
        public int clientId { get; set; }
        public Client client { get; set; }

        [ForeignKey("City")]
        public int cityId { get; set; }
        public City city { get; set; }

        [ForeignKey("AssignedPrinter")]
        public int? assignedPrinterId { get; set; }
        public AssignedPrinter? assignedPrinter { get; set; }
    }

}


using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class AssignedPrinterViewModal
    {
        public string serialNumber { get; set; }
        public string printerModel { get; set; }
        public int clientId { get; set; }
        //public int printerModelId { get; set; }
        public int printerStatusId { get; set; }       

    }
}

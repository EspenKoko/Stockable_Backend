using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class RepairDiagnosticViewModal
    {

        public int repairId { get; set; }
        public int diagnosticsId { get; set; }
        public bool isComplete { get; set; }
    }
}

using Stockable_Backend.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.ViewModel
{
    public class BranchViewModal
    {
        public string? branchName { get; set; }
        public string? branchCode { get; set; }
        public int clientId { get; set; }
        public int cityId { get; set; }
        public int? assignedPrinterId { get; set; }
    }
}

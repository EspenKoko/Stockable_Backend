﻿namespace Stockable_Backend.ViewModel
{
    public class RepairOrdersViewModal
    {
        public int vat { get; set; }
        public int markUp { get; set; }
        public int labourRate { get; set; }
        public DateTime date { get; set; }
        public int total { get; set; }
        public string serialNumber { get; set; }
        public string client { get; set; }
        public string branchCode { get; set; }
        public int repairId { get; set; }
    }
}

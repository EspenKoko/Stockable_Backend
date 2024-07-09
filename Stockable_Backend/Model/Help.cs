using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class Help
    {
        [Key]
        public int helpId { get; set; }
        public string helpName { get; set; }
        public string helpDescription { get; set; }
    }
}

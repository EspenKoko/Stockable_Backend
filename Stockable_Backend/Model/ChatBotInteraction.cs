using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class ChatBotInteraction
    {
        [Key]
        public int botInteractionId { get; set; }
        public DateTime date { get; set; }
        public string message { get; set; }
        public string type { get; set; }
    }
}

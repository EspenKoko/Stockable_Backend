using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class ClientUser
    {
        [Key]
        public int clientUserId { get; set; }
        public string? clientUserPosition { get; set; }

        [ForeignKey("Client")]
        public int clientId { get; set; }
        public Client? client { get; set; }


        [ForeignKey("User")]
        public string userId { get; set; }
        public User? user { get; set; }

        [ForeignKey("Branch")]
        public int branchId { get; set; }
        public Branch? branch { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.Model
{
    public class HubUser
    {
        [Key]
        [Required]
        public int hubUserId { get; set; }
        public string hubUserName { get; set; }
        public string hubUserSurname { get; set; }
        public string hubUserPhoneNumber { get; set;}
        public string hubUserEmail { get; set;}
        public string hubUserPostion { get; set; }

        [ForeignKey("User")]
        [Required]
        public int userId { get; set; }
        public User user { get; set; }

        [ForeignKey("Hub")]
        [Required]
        public int hubId { get; set; }
        public Hub hub { get; set; }
    }
}

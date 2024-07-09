using Stockable_Backend.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockable_Backend.ViewModel
{
    public class HubUserViewModal
    {
        public string hubUserName { get; set; }
        public string hubUserSurname { get; set; }
        public string hubUserPhoneNumber { get; set; }
        public string hubUserEmail { get; set; }
        public string hubUserPostion { get; set; }
        public int userId { get; set; }
        public int hubId { get; set; }
    }
}

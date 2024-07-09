using System.ComponentModel.DataAnnotations;

namespace Stockable_Backend.Model
{
    public class ClientUserRequest
    {
        [Key]
        public int clientUserRequestId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string number { get; set; }
        public string email { get; set; }
        public string clientUserPosition { get; set; }
        public string role { get; set; }
        public bool userCreated { get; set; }

        public int branchId { get; set; }
        public Branch branch { get; set; }

        //public int clientId { get; set; }
        //public Client client { get; set; }
    }
}

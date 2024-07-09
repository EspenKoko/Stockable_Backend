using Stockable_Backend.Model;

namespace Stockable_Backend.Repository.IRepositories
{
    public interface IEmailRepository
    {
        Task<bool> SendMailAsync(MailData mailData); // can make an int for statuscodes
    }
}

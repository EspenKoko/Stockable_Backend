using Microsoft.AspNetCore.Mvc;
using Stockable_Backend.Model;
using Stockable_Backend.Repository;
using Stockable_Backend.Repository.IRepositories;
using System.Net;
using System.Net.Mail;

namespace Stockable_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailRepository _repository;

        public MailController(IEmailRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(MailData emailData)
        {
            try
            {
                var result = await _repository.SendMailAsync(emailData);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, "Successfully Sent");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Email could not be sent. ensure email is correct or please check network");
                }
            }
            catch (SmtpException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occured: " + e);
            }
        }

        // works but is no error handling
        [HttpPost("SendMailTest")]
        private async Task SendMailTest(MailData emailData)
        {
            //var fromEmail = new MailAddress(emailData.fromEmailAddress);
            var fromEmail = new MailAddress("merkooks@gmail.com");
            var toEmail = new MailAddress(emailData.toEmailAddress);

            using var message = new MailMessage(fromEmail, toEmail);
            message.Subject = emailData.subject;
            //message.Body = MessageText;
            message.Body = string.Format("Message: {0}", emailData.messageBody);
            //message.IsBodyHtml = true;

            using var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("merkooks@gmail.com", "ejxkdqyqrsusbyih"); // your own provided email and password
            await smtp.SendMailAsync(message);
        }
    }
}

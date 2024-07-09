using Stockable_Backend.Model;
using System.Net.Mail;
using System.Net;
using Stockable_Backend.Repository.IRepositories;

namespace Stockable_Backend.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);

                // Extract the domain
                var domain = addr.Host;

                // return valid email
                //return addr.Address == email;

                // Check if domain has valid MX records
                return HasValidMxRecords(domain);
            }
            catch
            {
                return false;
            }
        }

        private bool HasValidMxRecords(string domain)
        {
            try
            {
                var mxRecords = Dns.GetHostAddresses(domain);
                return mxRecords.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendMailAsync(MailData emailData)
        {
            try
            {
/*                if (!IsValidEmail(emailData.toEmailAddress))
                {
                    // Email format is invalid
                    return false;
                }*/

                //var fromEmail = new MailAddress(emailData.fromEmailAddress);
                var fromEmail = new MailAddress("merkooks@gmail.com");
                var toEmail = new MailAddress(emailData.toEmailAddress);

                using (var message = new MailMessage(fromEmail, toEmail))
                {
                    message.Subject = emailData.subject;
                    message.Body = emailData.messageBody;
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("merkooks@gmail.com", "ejxkdqyqrsusbyih"); // your own provided email and password
                        await smtp.SendMailAsync(message);
                    }
                }
                return true;
            }
            catch (SmtpException e)
            {
                Console.WriteLine("Email error: " + e.Message);
                return false;
            }

        }
    }
}

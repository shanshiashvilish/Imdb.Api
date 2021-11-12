using Imdb.Core.EmailService;
using System.Net;
using System.Net.Mail;

namespace Imdb.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress("from@mail.com");
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new("smtp.office365.com", 587);
            client.Credentials = new NetworkCredential("user@mail.com", "Password");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            client.Send(message);
        }
    }
}

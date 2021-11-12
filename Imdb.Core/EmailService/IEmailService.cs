
namespace Imdb.Core.EmailService
{
    public interface IEmailService
    {
        void Send(string to, string subject, string body);
    }
}

using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace BL.MailConfirmation
{
    public class MailSender
    {
        private Random random = new Random();
        private SmtpClient smtpClient = new SmtpClient((string)ConfigurationManager.AppSettings["EmailLogin"], 587);
        public int MailConfirm(string mail)
        {
            int random_number = random.Next(1000, 9999);
            string senderEmail = ConfigurationManager.AppSettings["EmailLogin"]; 
            string senderPassword = ConfigurationManager.AppSettings["EmailPassword"];
            string recipientEmail = mail;
            string subject = "Mail Confirmation ";
            string body = random_number.ToString();
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            var mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);
            mailMessage.IsBodyHtml = true;           
            smtpClient.Send(mailMessage);
            return random_number;
        }
    }
}

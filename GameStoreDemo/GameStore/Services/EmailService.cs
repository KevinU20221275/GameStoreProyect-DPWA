using GameStore.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace GameStore.Services
{
    public class EmailService
    {
        private static string _Host = "sandbox.smtp.mailtrap.io";
        private static int _Port = 587;
        private static string _EmailUserName = "Kevin Montano";
        private static string _userName = "b2e9684ae73ea6";
        private static string _EmailFrom = "kevinEnterprice@gmail.com";
        private static string _Password = "cde6c4850a41ab";

        public static bool SendEmail(Email userEmail)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_EmailUserName, _EmailFrom));
                email.To.Add(MailboxAddress.Parse(userEmail.EmailTo));
                email.Subject = userEmail.Subject;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = userEmail.Content
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_Host, _Port, false);
                    smtp.Authenticate(_userName, _Password);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }

    }
}

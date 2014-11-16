using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net.Mail;

namespace Brilliantech.Qmail.Framework.Util
{
    public class EmailUtil
    {

        public static string Host { get; set; }
        public static string User { get; set; }
        public static string Pwd { get; set; }
        public static string Address { get; set; }

        static EmailUtil()
        {
            var appSettings = ConfigurationManager.AppSettings;
            Host = appSettings["SmtpHost"];
            User = appSettings["EmailUser"];
            Pwd = appSettings["EmailPwd"];
            Address = appSettings["EmailAddress"];
        }

        public static void Send(string subject,string toEmail, string file)
        {
            using (SmtpClient server = new SmtpClient(Host))
            {
                server.Credentials = new System.Net.NetworkCredential(User, Pwd);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Address);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Attachments.Add(new Attachment(file));
                    server.Send(mail);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Konex.AutoEmailer.Library;

namespace Konex.AutoEmailer
{
    public class Emailer
    {
        public struct Email
        {
            public string to;
            public string from;
            public string subject;
            public string body;
        }

        public void SendEmail(Customer customer)
        {
            var email = ConstructEmail();
            SendMessage(email);
        }

        private Email ConstructEmail()
        {
            return new Email
            {
                to = "yiniski@outlook.com"
                ,from = "yiniski@outlook.com"
                ,subject = "Using the new SMTP client."
                ,body = @"Using this new feature, you can send an e-mail message from an application very easily."
            };
        }

        private void SendMessage(Email email)
        {
            var message = new MailMessage(email.from, email.to, email.subject, email.body);
            var client = new SmtpClient
            {
                Host = "smtp.live.com",
                Timeout = 10000, 
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("", @"")
            };

            //Host = "smtp.gmail.com",

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendMessage(): {0}", ex);
            }
        }
    }
}

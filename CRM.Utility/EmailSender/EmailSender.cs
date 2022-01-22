using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CRM.Utility
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.Credentials = new NetworkCredential("shirad@gmail.com", "Tal");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("shiraznaad@gmail.com", "Unique");
                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = message;

                client.Send(mail);
            }
            catch (Exception ex)
            {
                // log exception
            }
            await Task.CompletedTask;
        }

    }
}

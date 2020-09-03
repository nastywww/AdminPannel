﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Admin.Panel.Web.Servises
{
    public class EmailSender : IEmailSender
    {
        private bool TrustAllCertificateCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // return Task.CompletedTask;

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificateCallback;

                const string server = "mail.payberry.ru";
                const string password = "@Grysenk02";
                string mailFrom = "gryshenko@payberry.ru";
                const string userName = "shop.logs";
               // var firstMail = mailTo[0];
               //var newList = mailTo.ToList();
                //newList.RemoveAt(0);
                var msg = new MailMessage(new MailAddress(mailFrom, mailFrom), new MailAddress(email))
                {
                    Subject = subject,
                    Body = message,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true
                };
                //foreach (var mail in newList)
                //{
                //    msg.To.Add(mail);
                //}

                var smtp = new SmtpClient(server, 9025)
                {
                    Credentials = new NetworkCredential(userName, password),
                    Timeout = 600000,
                    EnableSsl = true,
                };
                smtp.Send(msg);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                //ignored
            }
            return Task.CompletedTask;
        }
    }
}
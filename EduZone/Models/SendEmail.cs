﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EduZone.Models
{
    public class SendEmail
    {

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<IFormFile> Attachments { get; set; }
        public SendEmail(string calback)
        {
            Email = "Shooping930@outlook.com";
            DisplayName = "Shopping System";
            Password = "Hesham256##";
            Host = "smtp-mail.outlook.com";
            Port = 587;
            Body = "Click down link To Reset Password \n" + calback;
            Subject = "Reset Password";

        }
        public async Task SendEmailAsync(string mailTo, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(Email),
                Subject = Subject
            };

            email.To.Add(MailboxAddress.Parse(mailTo));

            var builder = new BodyBuilder();

            if (attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        var ms = new MemoryStream();
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = Body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(DisplayName, Email));

            var smtp = new SmtpClient();
            
            smtp.Connect(Host, Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(Email, Password);
            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }

    }
}
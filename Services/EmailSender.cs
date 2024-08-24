#nullable disable

using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using gmail.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MailKit;

namespace gmail.Services
{
    public class EmailSender
    {
        public async Task SendEmailAsync(string to, string subject, string body, string cc = null, string bcc = null, List<IFormFile> attachments = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Utkarsh Kumar", "singhkushagra453@gmail.com"));
            message.To.Add(new MailboxAddress("", to));
            if (!string.IsNullOrEmpty(cc)) message.Cc.Add(new MailboxAddress("", cc));
            if (!string.IsNullOrEmpty(bcc)) message.Bcc.Add(new MailboxAddress("", bcc));
            message.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = body };
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        await attachment.CopyToAsync(ms);
                        builder.Attachments.Add(attachment.FileName, ms.ToArray(), ContentType.Parse(attachment.ContentType));
                    }
                }
            }
            message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("singhkushagra453@gmail.com", "vjlt llcj dgcw tevw");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        public async Task<List<Email>> ReceiveEmailsAsync()
        {
            var emails = new List<Email>();
            try
            {
                using (var client = new ImapClient())
                {
                    await client.ConnectAsync("imap.gmail.com", 993, true);
                    await client.AuthenticateAsync("singhkushagra453@gmail.com", "vjlt llcj dgcw tevw");
                    var inbox = client.Inbox;
                    await inbox.OpenAsync(FolderAccess.ReadOnly);
					var results = await inbox.SearchAsync(SearchQuery.All);
					var DATA = results.OrderByDescending(x => x.Id).Take(10);
					foreach (var uniqueId in DATA)
                    {
                        var message = await inbox.GetMessageAsync(uniqueId);
                        emails.Add(new Email
                        {
                            To = string.Join(", ", message.To.Select(t => t.ToString())),
                            Subject = message.Subject,
                            Body = message.HtmlBody ?? message.TextBody,
                            SentDate = message.Date.LocalDateTime
                        });
                    }
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return emails;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gmail.Data;
using gmail.Models;
using gmail.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace gmail.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        private readonly gmailDbContext _context;
        private readonly EmailSender _emailSender;

        public MailController(gmailDbContext context, EmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Compose(int? id)
        {
            if (id == null)
            {
                return View(new ComposeMailViewModel());
            }

            var email = _context.Emails.Find(id);
            if (email == null || !email.IsDraft)
            {
                return NotFound();
            }

            var model = new ComposeMailViewModel
            {
                Id = email.Id, // Set the Id
                To = email.To,
                Cc = email.Cc,
                Bcc= email.Bcc,
                Subject = email.Subject,
                Body = email.Body,
                Attachments = null // You need to handle displaying existing attachments if required
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Compose(ComposeMailViewModel model, string action, int? originalEmailId)
        {
            if (ModelState.IsValid)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var attachmentPaths = model.Attachments?.Select(attachment =>
                {
                    var filePath = Path.Combine(uploadsFolderPath, Path.GetFileName(attachment.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        attachment.CopyTo(stream);
                    }
                    return filePath;
                }).ToList();

                bool isDraft = action == "Save as Draft";

                Email email;
                if (model.Id == 0)
                {
                    email = new Email
                    {
                        To = model.To,
                        Cc = model.Cc,
                        Bcc = model.Bcc,
                        Subject = model.Subject,
                        Body = model.Body,
                        Attachments = attachmentPaths != null ? string.Join(";", attachmentPaths) : null,
                        SentDate = DateTime.Now,
                        IsDraft = isDraft,
                        IsReply = originalEmailId.HasValue,
                        OriginalEmailId = originalEmailId
                    };

                    _context.Emails.Add(email);
                }
                else
                {
                    email = _context.Emails.Find(model.Id);
                    if (email == null)
                    {
                        return NotFound();
                    }



                    email.To = model.To;
                    email.Cc = model.Cc;
                    email.Bcc = model.Bcc;
                    email.Subject = model.Subject;
                    email.Body = model.Body;
                    email.Attachments = attachmentPaths != null ? string.Join(";", attachmentPaths) : null;
                    email.SentDate = DateTime.Now;
                    email.IsDraft = isDraft;
                    email.IsReply = originalEmailId.HasValue;
                    email.OriginalEmailId = originalEmailId;

                    _context.Emails.Update(email);
                }

                await _context.SaveChangesAsync();

                if (!isDraft)
                {
                    // Send email
                    await _emailSender.SendEmailAsync(model.To, model.Subject, model.Body, model.Cc, model.Bcc, model.Attachments);
                    ViewBag.Message = "Mail sent successfully!";
                }
                else
                {
                    ViewBag.Message = "Mail saved as draft!";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Sent()
        {
            var emails = await _context.Emails.Where(e => !e.IsDraft).OrderByDescending(e => e.SentDate).ToListAsync();
            return View(emails);
        }

        public async Task<IActionResult> Drafts()
        {
            var drafts = await _context.Emails.Where(e => e.IsDraft).OrderByDescending(e => e.SentDate).ToListAsync();
            return View(drafts);
        }


        public async Task<IActionResult> Inbox()
        {
            //var emails = await _emailSender.ReceiveEmailsAsync();
            List<Email> emails = await _emailSender.ReceiveEmailsAsync();
            return View(emails);
        }

        public IActionResult Starred()
        {
            var starredEmails = _context.Emails.Where(e => e.IsStarred).ToList();
            return View(starredEmails);
        }

        [HttpPost]
        public IActionResult ToggleStar(int id)
        {
            var email = _context.Emails.Find(id);
            if (email != null)
            {
                email.IsStarred = !email.IsStarred;
                _context.SaveChanges();
            }
            return RedirectToAction("Sent");
        }

    }
}

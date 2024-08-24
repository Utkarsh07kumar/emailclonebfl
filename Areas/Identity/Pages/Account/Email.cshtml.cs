using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace gmail.Areas.Identity.Pages.Account
{
    public class EmailModel : PageModel
    {
        private readonly ILogger<EmailModel> _logger;

        public EmailModel(ILogger<EmailModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Recipient { get; set; }

            [Required]
            public string Subject { get; set; }

            [Required]
            public string Message { get; set; }
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var smtpClient = new SmtpClient("smtp.example.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("singhkushagra453@gmail.com", "uuul kuru rbfm uffa"),
                    EnableSsl = true,
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("singhkushagra@gmail.com"),
                    Subject = Input.Subject,
                    Body = Input.Message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(Input.Recipient);

                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email.");
                ModelState.AddModelError(string.Empty, "Error sending email.");
                
                return Page();
            }
            return RedirectToPage("EmailConfirmation");
        }
    }
}

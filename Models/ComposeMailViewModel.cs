using System.ComponentModel.DataAnnotations;

namespace gmail.Models
{
    public class ComposeMailViewModel
    {
        public int Id { get; set; }
        [EmailAddress]
        public string To { get; set; }

        [EmailAddress]
        public string Cc { get; set; }

        [EmailAddress]
        public string Bcc { get; set; }
        
        public string Subject { get; set; }

        
        public string Body { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}

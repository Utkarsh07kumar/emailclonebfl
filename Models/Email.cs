using System;
using System.ComponentModel.DataAnnotations;

namespace gmail.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        
        [EmailAddress]
        public string To { get; set; }

        [EmailAddress]
        public string Cc { get; set; }  

        [EmailAddress]
        public string Bcc { get; set; }
   
        public string Subject { get; set; }

        public string Body { get; set; }

        public string Attachments {  get; set; }

        public DateTime SentDate { get; set; }

        public bool IsDraft { get; set; }
        public bool IsStarred { get; set; }
        public bool IsReply { get; set; }
        public int? OriginalEmailId { get; set; }
    }
}

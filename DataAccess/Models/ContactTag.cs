using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class ContactTag
    {
        [Required]
        public Guid Id { get; set; }

        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }

        [ForeignKey("Tag")]
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}

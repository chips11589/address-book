using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Tag
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

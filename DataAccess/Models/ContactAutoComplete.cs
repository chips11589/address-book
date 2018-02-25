using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    [NotMapped]
    public class ContactAutoComplete
    {
        public string Suggestion { get; set; }
    }
}

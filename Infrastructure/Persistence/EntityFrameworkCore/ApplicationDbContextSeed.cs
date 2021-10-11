using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityFrameworkCore
{
    public static class ApplicationDbContextSeed
    {
        public static async Task CreateContactAsync(ApplicationDbContext context)
        {
            if (!context.Contacts.Any())
            {
                context.Contacts.Add(new Contact { CompanyName = "Chips Cor.", Email = "john.williams@gmail.com", FirstName = "John", LinkedIn = "http://Linked", Phone = "000", Skype = "JohnW", Surname = "William", Title = "Director" });
                context.Contacts.Add(new Contact { CompanyName = "Sample Company", Email = "susan.smith@gmail.com", FirstName = "Susan", LinkedIn = "http://Linked", Phone = "000", Skype = "SusanS", Surname = "Smith", Title = "Sales Manager" });
                context.Contacts.Add(new Contact { CompanyName = "Sample Company", Email = "hellen.williams@gmail.com", FirstName = "Hellen", LinkedIn = "http://Linked", Phone = "000", Skype = "HellenW", Surname = "William", Title = "Director" });
                context.Contacts.Add(new Contact { CompanyName = "Sample Company 2", Email = "allen.goodwill@gmail.com", FirstName = "Allen", LinkedIn = "http://Linked", Phone = "000", Skype = "AllenG", Surname = "Goodwill", Title = "Staff" });
                context.Contacts.Add(new Contact { CompanyName = "Sample Company 2", Email = "peter.welcome@gmail.com", FirstName = "Peter", LinkedIn = "http://Linked", Phone = "000", Skype = "PeterW", Surname = "Welcome", Title = "Receptionist" });

                await context.SaveChangesAsync();
            }
        }
    }
}

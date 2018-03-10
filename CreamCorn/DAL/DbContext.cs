using CreamCorn.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace CreamCorn.DAL
{
    public class DbContext_CreamCorn : DbContext
    {
        public DbContext_CreamCorn()
            : base("CreamCorn")
        {
            //Database.SetInitializer<DbContext_CreamCorn>(new DropCreateDatabaseIfModelChanges<DbContext_CreamCorn>());
            Database.SetInitializer<DbContext_CreamCorn>(null);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompanyCategory> CompanyCategories { get; set; }

        private List<Company> CompaniesGet(string phoneNumber)
        {
            var records = this.Companies
                        .Where(x => x.PhoneNumber.Equals(phoneNumber))
                        .OrderBy(x => x.Name)
                        .ToList();

            return records;
        }

        private List<Contact> ContactsGet(string companyId)
        {
            var records = this.Contacts
                        .Where(x => x.CompanyId.Equals(companyId))
                        .OrderBy(x => x.Name)
                        .ToList();

            return records;
        }

    }
}
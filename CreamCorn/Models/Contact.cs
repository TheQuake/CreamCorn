using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreamCorn.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }

    }


}
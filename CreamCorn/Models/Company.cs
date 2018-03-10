using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreamCorn.Models
{
    [Table("Company")]
    public class Company
    {
        public Company()
        {
            Categories = new List<Category>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###-###-####}")]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

    }


}
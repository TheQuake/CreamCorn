using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeechTree.Models
{
    [Table("PmEquip")]
    public class Equipment
    {
        // need actual job master table here

        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cust#")]
        public string Customer { get; set; }

        [Required]
        public string Item { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public decimal Price { get; set; }

    }

}
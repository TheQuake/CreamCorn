using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeechTree.Models
{
    public class Job
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

    }
    [Table("PmJobShift")]
    public class JobShift
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job#")]
        public string JobNo { get; set; }

        [Required]
        [Display(Name = "Shift Date")]
        public DateTime ShiftDate { get; set; }

    }


}
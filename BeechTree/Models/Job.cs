using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeechTree.Models
{
    [Table("JcJobs01")]
    public class Job
    {
        // need actual job master table here

        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job#")]
        public string JobNo { get; set; }

        [Required]
        [Display(Name = "Customer#")]
        public string CustNo { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Descrip { get; set; }

    }

    [Table("PmJobEmploy")]
    public class JobEmployee
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job#")]
        public string JobNo { get; set; }

        [Required]
        [Display(Name = "Shift#")]
        public int ShiftNo { get; set; }

        [Required]
        [Display(Name = "Shift Date")]
        public DateTime ShiftDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Title { get; set; }

    }

    [Table("PmJobEquip")]
    public class JobEquipment
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job#")]
        public string JobNo { get; set; }

        [Required]
        [Display(Name = "Shift#")]
        public int ShiftNo { get; set; }

        [Required]
        [Display(Name = "Shift Date")]
        public DateTime ShiftDate { get; set; }

        [Required]
        [Display(Name = "Qty")]
        public double UnitQty_Actual { get; set; }

        [Required]
        public string Item { get; set; }

        [Required]
        public decimal price_actual { get; set; }

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
        [Display(Name = "Shift#")]
        public int ShiftNo { get; set; }

        [Required]
        [Display(Name = "Shift Date")]
        public DateTime ShiftDate { get; set; }

        [Required]
        [Display(Name = "Shift Start")]
        public string ShiftStart { get; set; }
        [Required]
        [Display(Name = "Shift Stop")]
        public string ShiftStop { get; set; }

    }

    [Table("PmUnit")]
    public class Unit
    {
        // need actual job master table here

        [Column("Unit")]
        [Key]
        public string Name { get; set; }

        [Required]
        [Column("Desc")]
        public string Description { get; set; }

    }

}
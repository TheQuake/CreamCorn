﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeechTree.Models
{
    [Table("PmJobShift")]
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
        public decimal Qty { get; set; }

        [Required]
        public string Item { get; set; }

        [Required]
        public decimal price_actual { get; set; }

    }

    //[Table("PmJobShift")]
    public class JobShift
    {

        // once job master is located, this can become pmjobshift

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
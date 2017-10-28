using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeechTree.Models
{
    [Table("JcJobs01")]
    public class Job
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Job#")]
        public string JobNo { get; set; }

        [Display(Name = "Customer#")]
        public string CustNo { get; set; }

        [Display(Name = "Description")]
        public string Descrip { get; set; }

        [Display(Name = "Job Date")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime StartDate { get; set; }

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

	[Table("JobSearch")]
	public class JobSearch
	{

		[Column("lJobSearch_id")]
		[Key]
		public int Id { get; set; }

		[Column("szSearch_key")]
		public string SearchKey { get; set; }

		[Column("szJobName_txt")]
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


    // ViewModel
    public class JobAdd
    {
		[DataType(DataType.Date)]
		[Display(Name = "Start Date")]
		[DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
		[Range(typeof(DateTime), "1/1/2010", "12/31/2030")]
		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		[Display(Name = "Description")]
		public string Description { get; set; }
		[Required]
		public int SiteId { get; set; }
		public string SiteName { get; set; }

		[Required]
		public int ServiceCode { get; set; }

		public string JobNumber { get; set; }

		public int Id { get; set; }
		public string JobName { get; set; }
		public DateTime JobDate { get; set; }
		public string CustomerCode { get; set; }

		public int BranchId { get; set; }

		public string BranchCode { get; set; }

		public string SalesRepCode { get; set; }

		public int DepartmentId { get; set; }

		public string DistrictCode { get; set; }

		public string TaxGroupCode { get; set; }
		public int ZoneId { get; set; }

		public int PriorityId { get; set; }

		public string TermsCode { get; set; }
		public string CurrencysCode { get; set; }

		public int ConcurrencysId { get; set; }

		public string message { get; set; }

	}


}
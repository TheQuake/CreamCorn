using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    [Table("JobSite")]
    public class Site
    {
        public Site()
        {
        }

        [Display(Name = "Site Id")]
        [Column("lJobSite_Id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Site Name")]
        [Column("szSearch_Key")]
        public string Name { get; set; }

		[Display(Name = "Customer Code")]
        [Column("szCustId_tr")]
        public string CustomerCode { get; set; }

		[Display(Name = "Branch Id")]
		[Column("lBranch_id")]
		public int BranchId { get; set; }

		[Display(Name = "Salesrep Code")]
		[Column("szSalesRep_tr")]
		public string SalesRepCode { get; set; }

		[Display(Name = "Department Id")]
		[Column("lDepartment_id")]
		public int DepartmentId { get; set; }

		[Display(Name = "Tax Group Code")]
		[Column("szTaxGrpID_tr")]
		public string TaxGroupCode { get; set; }

		[Display(Name = "Tax Group Id")]
		[Column("szDistCode_tr")]
		public string DistrictCode { get; set; }

		[Display(Name = "Zone Id")]
		[Column("lZone_id")]
		public int ZoneId { get; set; }

		[Display(Name = "Priority Id")]
		[Column("lPriority_id")]
		public int PriorityId { get; set; }

		[Display(Name = "Terms Code")]
		[Column("szTermsCode_tr")]
		public string TermsCode { get; set; }

		[Display(Name = "Currency Code")]
		[Column("szCurrencyId_tr")]
		public string CurrencysCode { get; set; }

		[Display(Name = "Currency Code")]
		[Column("iConcurrency_id")]
		public Int16 ConcurrencysId { get; set; }

	}
}
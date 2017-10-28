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

        [Display(Name = "Customer Id")]
        [Column("szCustId_tr")]
        public string CustomerId { get; set; }

		[Display(Name = "Branch Id")]
		[Column("lBranch_id")]
		public int BranchId { get; set; }

		[Display(Name = "Branch Id")]
		[Column("szSalesRep_tr")]
		public string SalesRepCode { get; set; }



	}
}
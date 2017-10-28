using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    [Table("Branch")]
    public class Branch
    {
        public Branch()
        {
        }

        [Display(Name = "Branch Id")]
        [Column("lBranch_id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Branch Name")]
        [Column("szSearch_key")]
        public string Name { get; set; }

        [Display(Name = "GL Segment")]
        [Column("szBranchGLSegment_tr")]
        public string GlSegment { get; set; }

    }
}
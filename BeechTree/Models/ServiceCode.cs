using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeechTree.Models
{
    [Table("UserFieldValues")]
    public class ServiceCode
    {
        public ServiceCode()
        {
        }

        [Column("lUserFieldValues_id")]
        [Key]
        public int Id { get; set; }

        [Column("szFieldValue_txt")]
        public string Value { get; set; }


    }
}
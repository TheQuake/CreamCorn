using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreamCorn.Models
{
    [Table("CompanyCategories")]
    public class CompanyCategory
    {
        [Key]
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Brands")]
    public class Brand
    {
        public Brand()
        {
            Models = new HashSet<Model>();
        }

        [Key]
        [Display(Name = "Marca")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Marca")]
        public string BrandName { get; set; }

        public virtual ICollection<Model> Models { get; set; }
        public bool Deleted { get; set; }
    }
}
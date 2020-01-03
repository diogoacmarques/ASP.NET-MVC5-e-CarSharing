using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Types")]
    public class Type
    {
        public Type()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        [Display(Name = "Tipo")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string TypeName { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Models")]
    public class Model
    {
        public Model()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        [Display(Name = "Modelo")]
        public int ModelId { get; set; }

        [Required]
        [Display(Name = "Modelo")]
        public string ModelName { get; set; }

        [ForeignKey("Brand")]
        [Required]
        [Display(Name = "Marca")]
        public int BrandId { get; set; }
        [Display(Name = "Marca")]
        public Brand Brand { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
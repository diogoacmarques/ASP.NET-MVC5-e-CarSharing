using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Colours")]
    public class Colour
    {
        public Colour()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        [Required]
        [Display(Name = "Cor")]
        public int ColourId { get; set; }

        [Required(ErrorMessage = "Especifique uma cor")]
        [Display(Name = "Cor")]
        public string ColourName { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Locations")]
    public class Location
    {

        public Location()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Display(Name = "Localização")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "Especifique uma localização")]
        [StringLength(15, MinimumLength = 3,ErrorMessage = "A localização deve ter no mínimo 3 e maximo de 15 carateres.")]
        [Display(Name = "Localização")]
        public string LocationName { get; set; }

        [DataType(dataType: DataType.Url)]
        [Display(Name = "Link do GoogleMaps")]
        public string GoogleMapsURL { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
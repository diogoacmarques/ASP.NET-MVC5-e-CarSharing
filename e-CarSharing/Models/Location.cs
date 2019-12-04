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
        [Display(Name = "Locations")]
        public int LocationId { get; set; }

        [Required]
        [Display(Name = "Locations")]
        public string LocationName { get; set; }


        [DataType(dataType: DataType.Url)]
        [Display(Name = "Link do GoogleMaps")]
        public string GoogleMapsURL { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
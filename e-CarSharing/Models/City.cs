using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Cities")]
    public class City
    {
        public City()
        {
            VehicleLocalizations = new HashSet<VehicleLocalization>();
        }

        [Key]
        [Display(Name = "Cidade")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string CityName { get; set; }

        //[ForeignKey("Country")]
        //[Display(Name = "País")]
        //public int CountryId { get; set; }
        //public Country Country { get; set; }

        public virtual ICollection<VehicleLocalization> VehicleLocalizations { get; set; }

        public bool Deleted { get; set; }
    }
}
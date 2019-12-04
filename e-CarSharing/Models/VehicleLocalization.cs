using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class VehicleLocalization
    {
        public VehicleLocalization()
        {
            Vehicles = new HashSet<Vehicle>();
            RentPickUp = new HashSet<Rent>();
            RentDelivery = new HashSet<Rent>();
        }

        [Key]
        [Required]
        [Display(Name = "Nome")]
        public int VehicleLocalizationId { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string VehicleLocalizationName { get; set; }

        [Required]
        [DataType(dataType: DataType.Text)]
        [Display(Name = "Morada")]
        public string VehicleLocalizationAddress { get; set; }

        [DataType(dataType: DataType.PostalCode)]
        [Display(Name = "Código-Postal")]
        public string VehicleLocalizationPostalCode { get; set; }

        [DataType(dataType: DataType.Url)]
        [Display(Name = "Link do GoogleMaps")]
        public string GoogleMapsURL { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        [ForeignKey("City")]
        [Required]
        [Display(Name = "Cidade")]
        public int CityId { get; set; }
        public City City { get; set; }

        //[ForeignKey("Country")]
        //[Display(Name = "País")]
        //[Required]
        //public int CountryId { get; set; }
        //public Country Country { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [InverseProperty("PickUpLocation")]
        public virtual ICollection<Rent> RentPickUp { get; set; }

        [InverseProperty("DeliveryLocation")]
        public virtual ICollection<Rent> RentDelivery { get; set; }

        public bool Deleted { get; set; }
    }
}
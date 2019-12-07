using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
            //VehicleLocalizations = new HashSet<VehicleLocalization>();
            RentConditions = new HashSet<RentCondition>();
            //RentVehicleEvaluations = new HashSet<RentVehicleEvaluation>();
            Rents = new HashSet<Rent>();
        }

        [Key]
        [Required]
        public int VehicleId { get; set; }

        [Display(Name = "Tipo")]
        [ForeignKey("Type")]
        public int TypeId { get; set; } // trotinete/carro/biclicleta
        public Type Type { get; set; }

        [Display(Name = "Localização")]
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [Display(Name = "Marca")]
        [ForeignKey("Brand")]
        public Nullable<int> BrandId { get; set; }
        public Brand Brand { get; set; }

        [Display(Name = "Modelo")]
        [ForeignKey("Model")]
        public Nullable<int> ModelId { get; set; }
        public Model Model { get; set; }

        [Display(Name = "Matrícula")]
        public string VehiclePlate { get; set; }

        //public virtual ICollection<VehicleLocalization> VehicleLocalizations { get; set; }
        public virtual ICollection<RentCondition> RentConditions { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        //public virtual ICollection<RentVehicleEvaluation> RentVehicleEvaluations { get; set; }

        [Display(Name = "Cor")]
        [ForeignKey("Colour")]
        public Nullable<int> ColourId { get; set; }
        public Colour Colour { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Número de Lugares")]
        public int vehiclePassengers { get; set; }

        [Display(Name = "Estado")]
        public int VehicleState { get; set; }

        [Display(Name = "Preço Por Hora")]
        [DataType(DataType.Currency)]
        public float HourlyPrice { get; set; }

        [Display(Name = "Preço Diário")]
        [DataType(DataType.Currency)]
        public float DailyPrice { get; set; }

        public bool Deleted { get; set; }
    }
}
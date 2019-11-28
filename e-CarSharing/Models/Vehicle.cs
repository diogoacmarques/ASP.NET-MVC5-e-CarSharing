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

        //[Required]
        [Display(Name = "Matrícula")]
        public string VehiclePlate { get; set; }

        [Display(Name = "Marca")]
        [ForeignKey("Brand")]
        public Nullable<int> BrandId { get; set; }
        public Brand Brand { get; set; }

        [Display(Name = "Modelo")]
        [ForeignKey("Model")]
        public Nullable<int> ModelId { get; set; }
        public Model Model { get; set; }

        //public virtual ICollection<VehicleLocalization> VehicleLocalizations { get; set; }
        public virtual ICollection<RentCondition> RentConditions { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        //public virtual ICollection<RentVehicleEvaluation> RentVehicleEvaluations { get; set; }

        [Display(Name = "Type")]
        [ForeignKey("Type")]
        public Nullable<int> TypeId { get; set; } // trotinete/carro/biclicleta
        public Type Type { get; set; }

        [Display(Name = "Cor")]
        [ForeignKey("Colour")]
        public Nullable<int> ColourId { get; set; }
        public Colour Colour { get; set; }

        [Display(Name = "Número de Lugares")]
        [ForeignKey("VehicleSeat")]
        public Nullable<int> VehicleSeatId { get; set; }
        public VehiclePassengers VehicleSeat { get; set; }

        [Display(Name = "Área")]
        public string Area { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("VehicleState")]
        [Display(Name = "Estado")]
        public int VehicleStateId { get; set; }
        public VehicleState VehicleState { get; set; }

        [Display(Name = "Preço Diário")]
        [DataType(DataType.Currency)]
        public float DailyPrice { get; set; }

        [Display(Name = "Preço Mensal")]
        [DataType(DataType.Currency)]
        public float MonthlyPrice { get; set; }

        public bool Deleted { get; set; }
    }
}
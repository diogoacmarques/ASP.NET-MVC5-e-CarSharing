using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("NumberPassengers")]
    public class VehiclePassengers
    {
        public VehiclePassengers()
        {
            Vehicles = new HashSet<Vehicle>();
        }
        
        [Key]
        [Required]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
        [Display(Name = "Número de Passageiros")]
        public int NumberPassengersId { get; set; }

        [Required]
        [Display(Name = "Número de Passageiros")]
        public int NumberOfPassengers { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
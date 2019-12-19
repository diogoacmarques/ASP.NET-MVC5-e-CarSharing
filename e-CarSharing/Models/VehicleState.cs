using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Models
{
    [Table("VehicleStates")]
    public class VehicleState
    {
        public VehicleState()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Estado")]
        public int VehicleStateId { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string VehicleStateName { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }


        public const string VEHICLESTATE_PENDING = "Pendente";
        public const int VEHICLESTATE_PENDING_ID = 1;

        public const string VEHICLESTATE_DELETED = "Eliminado";
        public const int VEHICLESTATE_DELETED_ID = 2;

        public const string VEHICLESTATE_ACCEPTED = "Aceite";
        public const int VEHICLESTATE_ACCEPTED_ID = 3;

        public static SelectList GetStatesList()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = VEHICLESTATE_PENDING_ID.ToString(), Text = VEHICLESTATE_PENDING},
               new SelectListItem { Value = VEHICLESTATE_DELETED_ID.ToString(), Text = VEHICLESTATE_DELETED},
               new SelectListItem { Value = VEHICLESTATE_ACCEPTED_ID.ToString(), Text = VEHICLESTATE_ACCEPTED},
            }; 

            return new SelectList(lista, "Value", "Text");
        }

    }
}
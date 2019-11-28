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


        public const string VEHICLESTATE_ALL_STRING = "Todos";
        public const int VEHICLESTATE_ALL_ID = 0;
        public const string VEHICLESTATE_ALL_ID_STRING = "0";

        public const string VEHICLESTATE_PENDING_STRING = "Pendente";
        public const int VEHICLESTATE_PENDING_ID = 1;
        public const string VEHICLESTATE_PENDING_ID_STRING = "1";

        public const string VEHICLESTATE_DELETED_STRING = "Eliminado";
        public const int VEHICLESTATE_DELETED_ID = 2;
        public const string VEHICLESTATE_DELETED_ID_STRING = "2";

        public const string VEHICLESTATE_ACCEPTED_STRING = "Aceite";
        public const int VEHICLESTATE_ACCEPTED_ID = 3;
        public const string VEHICLESTATE_ACCEPTED_ID_STRING = "3";



        public static SelectList GetStatesList(int? choice = null)
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = VEHICLESTATE_PENDING_ID_STRING, Text = VEHICLESTATE_PENDING_STRING},
               new SelectListItem { Value = VEHICLESTATE_ACCEPTED_ID_STRING, Text = VEHICLESTATE_ACCEPTED_STRING}
            };

            if (choice != null && (choice == VEHICLESTATE_PENDING_ID || choice == VEHICLESTATE_ACCEPTED_ID))
                return new SelectList(lista, "Value", "Text", choice.ToString());
            else
                return new SelectList(lista, "Value", "Text");
        }

        public static SelectList GetStatesListForAdminDropdown()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = VEHICLESTATE_ALL_ID_STRING, Text = VEHICLESTATE_ALL_STRING},
               new SelectListItem { Value = VEHICLESTATE_PENDING_ID_STRING, Text = VEHICLESTATE_PENDING_STRING},
               new SelectListItem { Value = VEHICLESTATE_ACCEPTED_ID_STRING, Text = VEHICLESTATE_ACCEPTED_STRING}
            };
            return new SelectList(lista, "Value", "Text");
        }

    }
}
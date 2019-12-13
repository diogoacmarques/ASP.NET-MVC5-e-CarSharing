using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Models
{
    public class RentViewModel
    {
        [Display(Name = "Data de Inicio")]
        [Required(ErrorMessage = "Especifique uma data")]    
        public DateTime BeginDate { get; set; }

        [Display(Name = "Onde Iniciar")]
        [Required(ErrorMessage = "Especifique uma localização")]
        public int? PickUpLocationId { get; set; }

        public SelectList Locations { get; set; }
    }

    public class RentViewModelCreate
    {
       
        [Required]
        [Display(Name = "Data de Inicio")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Data de Término")]
        [Required]
        public DateTime EndDate { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public SelectList Locations { get; set; }

        [Required]
        [Display(Name = "Onde começar")]
        public int? PickUpLocationId { get; set; }

        [Required]
        [Display(Name = "Onde Terminar")]
        public int? DeliveryLocationId { get; set; }
    }
}
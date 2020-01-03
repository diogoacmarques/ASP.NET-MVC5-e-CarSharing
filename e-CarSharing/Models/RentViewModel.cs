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
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        [Required]
        [Display(Name = "Data de Inicio")]
        public DateTime BeginDate { get; set; }

        [Required]
        [Display(Name = "Data de Término")]   
        public DateTime EndDate { get; set; }

        [Display(Name = "Localizações")]
        public SelectList Locations { get; set; }

        [Required]
        [Display(Name = "Onde começar")]
        public int PickUpLocationId { get; set; }

        [Required]
        [Display(Name = "Onde Terminar")]
        public int DeliveryLocationId { get; set; }
    }


    public class RentViewModelChangeState
    {
        public Rent Rent { get; set; }

        public SelectList RentStateList { get; set; }
    }

    public class RentViewModelSearch
    {
        [Display(Name = "Aluguer")]
        public IEnumerable<Rent> Rents { get; set; }

        public SelectList RentStateList { get; set; }

        public int? RentSateId { get; set; }

        public bool IsMobility { get; set; }
    }
}
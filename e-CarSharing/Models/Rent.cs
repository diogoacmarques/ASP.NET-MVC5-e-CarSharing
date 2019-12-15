using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("Rents")]
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }

        [Required]
        [Display(Name = "Data de Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Data de Término")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey("RentState")]
        public int RentStateId { get; set; }
        public RentState RentState { get; set; }

        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        [Required]
        [ForeignKey("PickUpLocation")]
        [Display(Name = "Localização de Devolução")]
        public int PickUpLocationId { get; set; }
        public Location PickUpLocation { get; set; }

        [Required]
        [Display(Name = "Localização de Entrega")]
        [ForeignKey("DeliveryLocation")]
        public int DeliveryLocationId { get; set; }
        public Location DeliveryLocation { get; set; }

        public bool Deleted { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    [Table("RentConditions")]
    public class RentCondition
    {
        public RentCondition()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        [Key]
        [Display(Name = "Nome da Condição")]
        public int RentConditionId { get; set; }

        [Required]
        [MaxLength(300)]
        [Display(Name = "Nome da Condição")]
        public string ConditionTitle { get; set; }

        [Display(Name = "Descrição da Condição")]
        [Required]
        public string ConditionDescription { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool Deleted { get; set; }

    }
}
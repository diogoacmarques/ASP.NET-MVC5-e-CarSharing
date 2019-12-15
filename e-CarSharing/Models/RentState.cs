using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Models
{
    public class RentState
    {
        public RentState()
        {
            Rents = new HashSet<Rent>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RentStateId { get; set; }

        [Required]
        public string RentStateName { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        public const string RENTSTATE_ALL = "Todos";
        public const int RENTSTATE_ALL_ID = 0;

        public const string RENTSTATE_PENDING = "Pendente";
        public const int RENTSTATE_PENDING_ID = 1;

        public const string RENTSTATE_NOTACCEPTED = "Não Aceite";
        public const int RENTSTATE_NOTACCEPTED_ID = 2;

        public const string RENTSTATE_ACCEPTED = "Aceite";
        public const int RENTSTATE_ACCEPTED_ID = 3;

        public const string RENTSTATE_COMPLETED = "Concluído";
        public const int RENTSTATE_COMPLETED_ID = 3;



        public static SelectList GetStatesList()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = RENTSTATE_PENDING_ID.ToString(), Text = RENTSTATE_PENDING},
               new SelectListItem { Value = RENTSTATE_NOTACCEPTED_ID.ToString(), Text = RENTSTATE_NOTACCEPTED},
               new SelectListItem { Value = RENTSTATE_ACCEPTED_ID.ToString(), Text = RENTSTATE_ACCEPTED},
               new SelectListItem { Value = RENTSTATE_COMPLETED_ID.ToString(), Text = RENTSTATE_COMPLETED}

            };
     
            return new SelectList(lista, "Value", "Text");
        }

    }
}
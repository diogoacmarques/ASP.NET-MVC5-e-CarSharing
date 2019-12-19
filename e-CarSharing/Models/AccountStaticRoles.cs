using System.Collections.Generic;
using System.Web.Mvc;

namespace e_CarSharing.Models
{
    public class AccountStaticRoles
    {
        public const string ADMINISTRATOR = "Administrador";
        public const int ADMINISTRATOR_ID = 1;

        public const string PRIVATE = "Particular";
        public const int PRIVATE_ID = 2;

        public const string PROFESSIONAL = "Profissional";
        public const int PROFESSIONAL_ID = 3;

        public const string MOBILITY = "Mobilidade";
        public const int MOBILITY_ID = 4;


        public static SelectList GetRolesList()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = ADMINISTRATOR, Text = ADMINISTRATOR },
               new SelectListItem { Value = PRIVATE, Text = PRIVATE },
               new SelectListItem { Value = PROFESSIONAL, Text = PROFESSIONAL },
               new SelectListItem { Value = MOBILITY, Text = MOBILITY },
            };
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList GetRolesListForUser()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = PRIVATE, Text = PRIVATE },
               new SelectListItem { Value = PROFESSIONAL, Text = PROFESSIONAL },
               new SelectListItem { Value = MOBILITY, Text = MOBILITY },
            };
            return new SelectList(lista, "Value", "Text");
        }

        public static SelectList GetRolesListForVehicleSearch()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = PRIVATE, Text = PRIVATE },
               new SelectListItem { Value = PROFESSIONAL, Text = PROFESSIONAL },
            };
            return new SelectList(lista, "Value", "Text");
        }


    }
}
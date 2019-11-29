using System.Collections.Generic;
using System.Web.Mvc;

namespace e_CarSharing.Models
{
    public class AccountLevels
    {
        //professional
        public const string PROFESSIONAL = "Profissional";
        public const int PROFESSIONAL_ID = 3;

        public const string PRIVATE = "Particular";
        public const int PRIVATE_ID = 2;

        public const string ADMINISTRATOR = "Administrador";
        public const int ADMINISTRATOR_ID = 1;
  

        public static SelectList GetLevelsList()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = PROFESSIONAL, Text = PROFESSIONAL },
               new SelectListItem { Value = PRIVATE, Text = PRIVATE },
               new SelectListItem { Value = ADMINISTRATOR, Text = ADMINISTRATOR }
            };
            return new SelectList(lista, "Value", "Text");
        }

        //public static SelectList GetRolesListForUsersList()
        //{
        //    var lista = new List<SelectListItem>()
        //    {
        //       new SelectListItem { Value = "0", Text = "Todos" },
        //       new SelectListItem { Value = STRING_PROFISSIONAL_ID, Text = STRING_PROFISSIONAL },
        //       new SelectListItem { Value = STRING_PARTICULAR_ID, Text = STRING_PARTICULAR },
        //       new SelectListItem { Value = STRING_ADMINISTRATOR_ID, Text = STRING_ADMINISTRATOR }
        //    };
        //    return new SelectList(lista, "Value", "Text");
        //}

        public static SelectList GetLevelsListForRegister()
        {
            var lista = new List<SelectListItem>()
            {
               new SelectListItem { Value = PROFESSIONAL, Text = PROFESSIONAL },
               new SelectListItem { Value = PRIVATE, Text = PRIVATE }
            };
            return new SelectList(lista, "Value", "Text");
        }

        //public static SelectList GetRolesListForCarSearch()
        //{
        //    var lista = new List<SelectListItem>()
        //    {
        //       new SelectListItem { Value = STRING_PROFISSIONAL_ID, Text = STRING_PROFISSIONAL },
        //       new SelectListItem { Value = STRING_PARTICULAR_ID, Text = STRING_PARTICULAR }
        //    };
        //    return new SelectList(lista, "Value", "Text");
        //}
    }
}

// int.TryParse(string_here, out int int_here)
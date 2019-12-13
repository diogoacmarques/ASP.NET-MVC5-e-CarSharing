using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Areas.Administration.ViewModels
{
    public class BrandViewModel
    {
        [Required(ErrorMessage = "Insira a marca")]
        [Display(Name = "Marca")]
        public string BrandName { get; set; }

        public SelectList Types { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Especifique o tipo de veículo")]
        public int TypeId { get; set; }
    }
}
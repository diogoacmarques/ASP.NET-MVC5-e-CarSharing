using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_CarSharing.Areas.Administration.ViewModels
{
    public class ModelViewModel
    {
    }

    public class ModelViewModelCreate
    {
        [Required(ErrorMessage = "Insira o modelo")]
        [Display(Name = "Modelo")]
        public string ModelName { get; set; }

        public SelectList Brands { get; set; }
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Especifique a marca")]
        public int BrandId { get; set; }

    }
}
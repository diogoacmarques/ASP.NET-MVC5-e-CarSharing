using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace e_CarSharing.Models
{
    public class VehicleViewModel
    {

    }


    public class SearchVehicleViewModel
    {
        public IPagedList<Vehicle> Vehicles { get; set; }
    //    public SelectList States { get; set; }

    //    [Display(Name = "Estado")]
    //    public string StateId { get; set; }

    //    //public SelectList Roles { get; set; }
    //    //[Display(Name = "Tipo de Anunciante")]
    //    //public string RoleId { get; set; }

    //    public SelectList Colours { get; set; }
    //    [Display(Name = "Cor")]
    //    public string ColourId { get; set; }

    //    public SelectList Brands { get; set; }
    //    [Display(Name = "Marcas")]
    //    public string BrandId { get; set; }

    //    public SelectList Models { get; set; }
    //    [Display(Name = "Modelos")]
    //    public string ModelId { get; set; }
    }
}
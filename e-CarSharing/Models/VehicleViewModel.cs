using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class VehicleViewModelCreate
    {

        public SelectList Brands { get; set; }
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Escolha uma marca")]
        public int BrandId { get; set; }
    
        
        public SelectList Models { get; set; }
        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Especifique um modelo")]
        public int ModelId { get; set; }


        public SelectList Locations { get; set; }
        [Display(Name = "Localização")]
        [Required(ErrorMessage = "Especifique uma localização")]
        public int LocationId { get; set; }

        
        public SelectList Types { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Escolha um tipo de veículo")]
        public int TypeId { get; set; }

      
        public SelectList Colours { get; set; }
        [Display(Name = "Cor")]
        [Required(ErrorMessage = "Escolha uma cor")]
        public int ColourId { get; set; }

        [Display(Name = "Número de passageiros")]
        [Required(ErrorMessage = "Especifique o número de passageiros")]     
        [Range(1, 7, ErrorMessage = "Insira um número entre 1 e 7")]
        public int vehiclePassengers { get; set; }


        [Display(Name = "Preço por hora")]
        [Required(ErrorMessage = "Especifique o preço por hora")] 
        [Range(1, 1000, ErrorMessage = "Insira um preço entre 1 e 1000")]
        [DataType(DataType.Currency)]
        public float HourlyPrice { get; set; }


        [Display(Name = "Preço por dia")]
        [Required(ErrorMessage = "Especifique o preço por dia")]
        [Range(1, 10000, ErrorMessage = "Insira um preço entre 1 e 10000")]      
        [DataType(DataType.Currency)]
        public float DailyPrice { get; set; }


    }

    public class VehicleViewModelDetails
    {
        public int VehicleId { get; set; }

        [Display(Name = "Marca")]
        [ReadOnly(true)]
        public Brand Brand { get; set; }


        [Display(Name = "Modelo")]
        public Model Model { get; set; }


        [Display(Name = "Localização")]
        public Location Location { get; set; }


        [Display(Name = "Tipo")]
        public Type Type { get; set; }


        [Display(Name = "Cor")]
        public Colour Colour { get; set; }


        [Display(Name = "Número de passageiros")]
        public int vehiclePassengers { get; set; }


        [Display(Name = "Preço por hora")]
        [ReadOnly(true)]
        [DataType(DataType.Currency)]
        public float HourlyPrice { get; set; }


        [Display(Name = "Preço por dia")]
        [DataType(DataType.Currency)]
        public float DailyPrice { get; set; }

    }


    public class SearchVehicleViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }

        public SelectList Types { get; set; }
        [Display(Name = "Tipo de Veículo")]
        public int? TypeId { get; set; }

        public SelectList Roles { get; set; }
        [Display(Name = "Tipo de Anunciante")]
        public string RoleId { get; set; }

        public SelectList Colours { get; set; }
        [Display(Name = "Cor")]
        public int? ColourId { get; set; }

        public SelectList Brands { get; set; }
        [Display(Name = "Marcas")]
        public int? BrandId { get; set; }

        public SelectList Models { get; set; }
        [Display(Name = "Modelos")]
        public int? ModelId { get; set; }

        [Display(Name = "Número de Passageiros")]
        public int? VehiclePassengers { get; set; }

        public SelectList Locations { get; set; }
        [Display(Name = "Localização")]
        public int? LocationId { get; set; }


        [Display(Name = "Data de Inicio")]
        public DateTime BeginDate { get; set; }

        [Display(Name = "Data de Término")]
        public DateTime EndDate { get; set; }
    }
}
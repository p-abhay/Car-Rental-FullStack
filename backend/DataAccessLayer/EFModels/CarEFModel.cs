using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class CarEFModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Maker is required")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Maker must contain only alphabetic characters")]
        public string Maker { get; set; }

        [Required(ErrorMessage = "Model is required")]
        /*[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Model must contain alphanumeric characters")]*/
        public string Model { get; set; }

        [Required(ErrorMessage = "Availability status is required")]
        public bool AvailabilityStatus { get; set; }

        [Required(ErrorMessage = "Rental price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rental price must be greater than 0")]
        public decimal RentalPrice { get; set; }

        public string Image { get; set; }

    }
}

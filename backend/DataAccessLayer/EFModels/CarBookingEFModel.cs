using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public enum BookingStatus
    {
        Booked,
        ReturnRequested,
        ReturnSuccess
    }
    public class CarBookingEFModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CarId { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0")]
        public int Duration { get; set; }
      
        [Required(ErrorMessage = "Total Rental price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total Rental price must be greater than 0")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}

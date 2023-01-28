using System;
using System.Collections.Generic;
using VacationRental.Api.Domain.DTOs;

namespace VacationRental.Api.Models
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
        public List<UnitDto> PreparationTimes { get; set; }
    }
}

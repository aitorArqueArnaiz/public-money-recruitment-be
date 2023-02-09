using System;
using System.Collections.Generic;

namespace VacationRental.Api.Domain.DTOs
{
    public class CalendarDateDto
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingDto> Bookings { get; set; }
        public List<UnitDto> PreparationTimes { get; set; }
    }

    public class UnitDto
    {
        public int Unit { get; set; }
    }
}

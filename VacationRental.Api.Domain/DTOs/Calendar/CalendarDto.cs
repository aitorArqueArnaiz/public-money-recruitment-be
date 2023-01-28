using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Api.Domain.DTOs
{
    public class CalendarDto
    {
        public int RentalId { get; set; }
        public List<CalendarDateDto> Dates { get; set; }
        public List<int> PreparationTimes { get; set; }
    }
}

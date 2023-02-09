using System.Collections.Generic;
using VacationRental.Api.Domain.DTOs;

namespace VacationRental.Api.Domain.Interfaces
{
    public interface ICalendarService
    {
        GetCaldendatDtoResponse GetCaldendarInformation(GetCaldendarDtoRequest request, IDictionary<int, BookingDto> bookings, IDictionary<int, RentalDto> rentals);
    }
}

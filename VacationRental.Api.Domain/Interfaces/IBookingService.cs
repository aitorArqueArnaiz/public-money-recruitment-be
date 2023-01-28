
using VacationRental.Api.Domain.DTOs;

namespace VacationRental.Api.Domain.Interfaces
{
    public interface IBookingService
    {
        AddBookingDtoResponse AddBooking(AddBookingDtoRequest request);
    }
}

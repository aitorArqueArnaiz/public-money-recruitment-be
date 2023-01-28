using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Models;

namespace VacationRental.Api.Mappers
{
    public static class BookingMapper
    {
        public static BookingDto MapBookingModelIntoBookingDto(BookingViewModel model)
        {
            return new BookingDto()
            {
                Id = model.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start
            };
        }

        public static RentalDto MapRentalModelIntoRentalDto(RentalViewModel model)
        {
            return new RentalDto()
            {
                Id = model.Id,
                Units = model.Units
            };
        }
    }
}

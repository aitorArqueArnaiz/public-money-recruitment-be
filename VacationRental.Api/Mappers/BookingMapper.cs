using System.Linq;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Models;

namespace VacationRental.Api.Mappers
{
    public static class BookingMapper
    {
        /// <summary>
        /// Converts BookingViewModel into BookingDto object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts RentalViewModel into RentalDto object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static RentalDto MapRentalModelIntoRentalDto(RentalViewModel model)
        {
            return new RentalDto()
            {
                Id = model.Id,
                Units = model.Units
            };
        }

        /// <summary>
        /// Converts CalendarDateDto into CalendarDateViewModel object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CalendarDateViewModel MapCalendarModelIntoCalendarDto(CalendarDateDto model)
        {
            return new CalendarDateViewModel()
            {
                Bookings = model.Bookings.Select(elem => MapCalendarBookingModelIntoCalendarBookingDto(elem)).ToList(),
                Date = model.Date
            };
        }

        /// <summary>
        /// Converts CalendarBookingDto into CalendarBookingViewModel object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CalendarBookingViewModel MapCalendarBookingModelIntoCalendarBookingDto(CalendarBookingDto model)
        {
            return new CalendarBookingViewModel()
            {
                Id = model.Id
            };
        }
    }
}

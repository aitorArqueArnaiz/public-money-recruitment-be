using System;
using System.Collections.Generic;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;

namespace VacationRental.Api.Domain.Services
{
    public class BookingService : IBookingService
    {
        #region Constructor

        public BookingService()
        {
        }

        #endregion

        #region Public Methods

        public AddBookingDtoResponse AddBooking(AddBookingDtoRequest request, IDictionary<int, BookingDto> bookings, IDictionary<int, RentalDto> rentals)
        {
            for (var i = 0; i < request.Nights; i++)
            {
                var count = 0;
                foreach (var booking in bookings.Values)
                {
                    if (IsValidBooking(request, booking))
                    {
                        count++;
                    }
                }
                if (count >= rentals[request.RentalId].Units)
                    throw new ApplicationException("Not available");
            }
            return new AddBookingDtoResponse() { Id = bookings.Keys.Count + 1 };
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to check if the booking is valid.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        private bool IsValidBooking(AddBookingDtoRequest request, BookingDto booking)
        {
            return booking.RentalId == request.RentalId
                        && (booking.Start <= request.Start.Date && booking.Start.AddDays(booking.Nights) > request.Start.Date)
                        || (booking.Start < request.Start.AddDays(request.Nights) && booking.Start.AddDays(booking.Nights) >= request.Start.AddDays(request.Nights))
                        || (booking.Start > request.Start && booking.Start.AddDays(booking.Nights) < request.Start.AddDays(request.Nights));
        }

        #endregion
    }
}

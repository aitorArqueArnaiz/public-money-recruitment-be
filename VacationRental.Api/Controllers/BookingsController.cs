using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;
using VacationRental.Api.Mappers;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IBookingService _bookingService;

        public BookingsController(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings,
            IBookingService bookingService)
        {
            _rentals = rentals;
            _bookings = bookings;
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_rentals.ContainsKey(model.RentalId))
                throw new ApplicationException("Rental not found");

            try
            {
                var addBookingDtoRequest = new AddBookingDtoRequest()
                {
                    Nights = model.Nights,
                    RentalId = model.RentalId,
                    Start = model.Start
                };
                var domainBookings = new Dictionary<int, BookingDto>();
                var domainRentals = new Dictionary<int, RentalDto>();
                foreach(var booking in _bookings)
                {
                    domainBookings.Add(booking.Key, BookingMapper.MapBookingModelIntoBookingDto(booking.Value));
                }
                foreach (var rental in _rentals)
                {
                    domainRentals.Add(rental.Key, BookingMapper.MapRentalModelIntoRentalDto(rental.Value));
                }
                var response = _bookingService.AddBooking(addBookingDtoRequest, domainBookings, domainRentals);
                var key = new ResourceIdViewModel { Id = response.Id };

                _bookings.Add(key.Id, new BookingViewModel
                {
                    Id = key.Id,
                    Nights = model.Nights,
                    RentalId = model.RentalId,
                    Start = model.Start.Date
                });

                return key;
            }
            catch(Exception error)
            {
                throw new Exception($"Error calling post. Exception message is : {error.Message}");
            }          
        }
    }
}

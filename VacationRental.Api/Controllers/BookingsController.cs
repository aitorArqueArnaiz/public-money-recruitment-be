﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;
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

                for (var i = 0; i < model.Nights; i++)
                {
                    var count = 0;
                    foreach (var booking in _bookings.Values)
                    {
                        if (booking.RentalId == model.RentalId
                            && (booking.Start <= model.Start.Date && booking.Start.AddDays(booking.Nights) > model.Start.Date)
                            || (booking.Start < model.Start.AddDays(model.Nights) && booking.Start.AddDays(booking.Nights) >= model.Start.AddDays(model.Nights))
                            || (booking.Start > model.Start && booking.Start.AddDays(booking.Nights) < model.Start.AddDays(model.Nights)))
                        {
                            count++;
                        }
                    }
                    if (count >= _rentals[model.RentalId].Units)
                        throw new ApplicationException("Not available");
                }


                var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;
using VacationRental.Api.Mappers;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly ICalendarService _calendarService;

        public CalendarController(
            IDictionary<int, RentalViewModel> rentals,
            IDictionary<int, BookingViewModel> bookings,
            ICalendarService caldendarService)
        {
            _rentals = rentals;
            _bookings = bookings;
            _calendarService = caldendarService;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var getCalendarInfoDto = new GetCaldendarDtoRequest()
            {
                Nights = nights,
                RentalId = rentalId,
                Start = start
            };
            var domainBookings = new Dictionary<int, BookingDto>();
            foreach (var booking in _bookings)
            {
                domainBookings.Add(booking.Key, BookingMapper.MapBookingModelIntoBookingDto(booking.Value));
            }
            var response = _calendarService.GetCaldendarInformation(getCalendarInfoDto, domainBookings);
            var result = new CalendarViewModel()
            {
                Dates = response.Caldendar.Dates.Select(elem => BookingMapper.MapCalendarModelIntoCalendarDto(elem)).ToList(),
                RentalId = response.Caldendar.RentalId
            };

            return result;
        }
    }
}

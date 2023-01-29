using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;
using VacationRental.Api.Infrastructure.Repository;
using VacationRental.Api.Mappers;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
        private IBookingRepository _bookingRepositiry;
        private IRentalsRepository _rentalRepositiry;

        public CalendarController(
            IBookingRepository bookingRespository,
            IRentalsRepository rentalRespository,
            ICalendarService caldendarService)
        {
            _bookingRepositiry = bookingRespository;
            _rentalRepositiry = rentalRespository;
            _calendarService = caldendarService;
        }

        [HttpGet]
        public async Task<CalendarViewModel> Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentalRepositiry.GetRentalById(rentalId))
                throw new ApplicationException("Rental not found");

            try
            {
                var getCalendarInfoDto = new GetCaldendarDtoRequest()
                {
                    Nights = nights,
                    RentalId = rentalId,
                    Start = start
                };
                var domainBookings = new Dictionary<int, BookingDto>();
                var domainRentals = new Dictionary<int, RentalDto>();
                foreach (var booking in _bookingRepositiry.GetAllBookings())
                {
                    domainBookings.Add(booking.Key, BookingMapper.MapBookingModelIntoBookingDto(booking.Value));
                }
                foreach (var rental in _rentalRepositiry.GetAllRentals())
                {
                    domainRentals.Add(rental.Key, BookingMapper.MapRentalModelIntoRentalDto(rental.Value));
                }
                var response = _calendarService.GetCaldendarInformation(getCalendarInfoDto, domainBookings, domainRentals);
                var result = new CalendarViewModel()
                {
                    Dates = response.Caldendar.Dates.Select(elem => BookingMapper.MapCalendarModelIntoCalendarDto(elem)).ToList(),
                    RentalId = response.Caldendar.RentalId
                };

                return result;
            }
            catch(Exception error)
            {
                throw new Exception($"Error calling calendar get. Exception message is : {error.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;
using VacationRental.Api.Infrastructure.Repository;
using VacationRental.Api.Mappers;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalsRepository _rentalRepository;

        public BookingsController(
            IBookingRepository bookingRepository,
            IRentalsRepository rentalRepository,
            IBookingService bookingService)
        {
            _bookingRepository = bookingRepository;
            _rentalRepository = rentalRepository;
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<BookingViewModel> Get(int bookingId)
        {
            if (!_bookingRepository.KeyExist(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookingRepository.GetBookingById(bookingId);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_rentalRepository.KeyExist(model.RentalId))
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
                foreach(var booking in _bookingRepository.GetAllBookings())
                {
                    domainBookings.Add(booking.Key, BookingMapper.MapBookingModelIntoBookingDto(booking.Value));
                }
                foreach (var rental in _rentalRepository.GetAllRentals())
                {
                    domainRentals.Add(rental.Key, BookingMapper.MapRentalModelIntoRentalDto(rental.Value));
                }
                var response = _bookingService.AddBooking(addBookingDtoRequest, domainBookings, domainRentals);
                var key = new ResourceIdViewModel { Id = response.Id };

                _bookingRepository.AddBooking(key.Id, new BookingViewModel
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
                throw new ApplicationException($"Error calling post. Exception message is : {error.Message}");
            }          
        }
    }
}

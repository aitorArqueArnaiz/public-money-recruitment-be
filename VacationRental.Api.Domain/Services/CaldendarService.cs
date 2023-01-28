﻿using System.Collections.Generic;
using System.Linq;
using VacationRental.Api.Domain.DTOs;
using VacationRental.Api.Domain.Interfaces;

namespace VacationRental.Api.Domain.Services
{
    public class CaldendarService : ICalendarService
    {
        public GetCaldendatDtoResponse GetCaldendarInformation(GetCaldendarDtoRequest request, IDictionary<int, BookingDto> bookings, IDictionary<int, RentalDto> rentals)
        {
            var result = new CalendarDto
            {
                RentalId = request.RentalId,
                Dates = new List<CalendarDateDto>()
            };

            for (var i = 0; i < request.Nights; i++)
            {
                var date = new CalendarDateDto
                {
                    Date = request.Start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingDto>()
                };

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == request.RentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingDto { Id = booking.Id });
                        if(date.PreparationTimes?[i]?.Unit > 0 && date.PreparationTimes?[i]?.Unit == date.Bookings?.LastOrDefault()?.Unit)
                        {
                            date.Date.AddDays((double)(date.PreparationTimes?[i].Unit));
                        }
                    }
                }

                result.Dates.Add(date);
            }
            return new GetCaldendatDtoResponse() { Caldendar = result };
        }
    }
}

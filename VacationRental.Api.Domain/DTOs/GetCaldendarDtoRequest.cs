using System;

namespace VacationRental.Api.Domain.DTOs
{
    public class GetCaldendarDtoRequest
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
    }
}

using Newtonsoft.Json;
using System;

namespace VacationRental.Api.Models
{
    public class BookingBindingModel
    {
        [JsonProperty("RentalId")]
        public int RentalId { get; set; }

        [JsonProperty("DateTime")]
        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        private DateTime _startIgnoreTime;

        [JsonProperty("Nights")]
        public int Nights { get; set; }
    }
}

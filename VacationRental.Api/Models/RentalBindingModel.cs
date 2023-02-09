using Newtonsoft.Json;

namespace VacationRental.Api.Models
{
    public class RentalBindingModel
    {
        [JsonProperty("Units")]
        public int Units { get; set; }

        [JsonProperty("PreparationTimeInDays")]
        public int PreparationTimeInDays { get; set; }
    }
}

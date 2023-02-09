
using System.Collections.Generic;

namespace VacationRental.Api.Infrastructure.Repository
{
    public class BookingRespository : IBookingRepository
    {
        private IDictionary<int, dynamic> _bookings;

        public BookingRespository()
        {
            _bookings = new Dictionary<int, dynamic>() { };
        }

        public void AddBooking(int key, dynamic data)
        {
            _bookings.Add(key, data);
        }

        public IDictionary<int, dynamic> GetAllBookings()
        {
            return _bookings;
        }

        public dynamic GetBookingById(int id)
        {
            return _bookings[id];
        }

        public bool KeyExist(int key)
        {
            return _bookings.ContainsKey(key);
        }
    }
}

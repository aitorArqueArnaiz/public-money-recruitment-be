

using System.Collections.Generic;

namespace VacationRental.Api.Infrastructure.Repository
{
    public interface IBookingRepository
    {
        /// <summary>
        /// Add data into in memmory repository.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void AddBooking(int key, dynamic data);

        /// <summary>
        /// Gets the data by id from in memmory repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        dynamic GetBookingById(int id);

        /// <summary>
        /// Checks that the dictionary containd the provided key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExist(int key);

        /// <summary>
        /// Returns all bookings.
        /// </summary>
        /// <returns></returns>
        IDictionary<int, dynamic> GetAllBookings();
    }
}

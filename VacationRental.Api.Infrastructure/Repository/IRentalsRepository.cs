
using System.Collections.Generic;

namespace VacationRental.Api.Infrastructure.Repository
{
    public interface IRentalsRepository
    {
        /// <summary>
        /// Add data into in memmory repository.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        void AddRental(int key,  dynamic data);

        /// <summary>
        /// Gets the data by id from in memmory repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        dynamic GetRentalById(int id);

        /// <summary>
        /// Checks that the dictionary contains the provided key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExist(int key);

        /// <summary>
        /// Returns all rentals.
        /// </summary>
        /// <returns></returns>
        IDictionary<int, dynamic> GetAllRentals();
    }
}

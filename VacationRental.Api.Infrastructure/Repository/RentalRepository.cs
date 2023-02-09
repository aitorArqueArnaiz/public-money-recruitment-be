using System.Collections.Generic;

namespace VacationRental.Api.Infrastructure.Repository
{
    public class RentalRepository : IRentalsRepository
    {
        private IDictionary<int, dynamic> _rentals;

        public RentalRepository()
        {
            _rentals = new Dictionary<int, dynamic>() { };
        }

        public void AddRental(int key, dynamic data)
        {
            _rentals.Add(key, data);
        }

        public IDictionary<int, dynamic> GetAllRentals()
        {
            return _rentals;
        }

        public dynamic GetRentalById(int id)
        {
            return _rentals[id];
        }

        public bool KeyExist(int key)
        {
            return _rentals.ContainsKey(key);
        }
    }
}

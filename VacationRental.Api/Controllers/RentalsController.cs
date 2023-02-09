using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Infrastructure.Repository;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalsRepository _rentalRespository;

        public RentalsController(IDictionary<int, RentalViewModel> rentals, IRentalsRepository rentalRepository)
        {
            _rentalRespository = rentalRepository;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<RentalViewModel> Get(int rentalId)
        {
            if (!_rentalRespository.KeyExist(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentalRespository.GetRentalById(rentalId);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(RentalBindingModel model)
        {
            var key = new ResourceIdViewModel { Id = _rentalRespository.GetAllRentals().Keys.Count + 1 };

            _rentalRespository.AddRental(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return key;
        }
    }
}

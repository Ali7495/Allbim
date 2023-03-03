using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.Person;
using Models.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private PagingSettings _pagingSettings;

        public AddressService(IRepository<Address> addressRepository, IOptionsSnapshot<PagingSettings> pagingSettings)
        {
            _addressRepository = addressRepository; 
            _pagingSettings = pagingSettings.Value;


        }

        public async Task<Address> CreateAddress(AddressViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = new Address
            {
                CityId = viewModel.CityId,
                Code = viewModel.Code.Value,
                Name = viewModel.Name,
                Description = viewModel.Description,
                ZoneNumber = viewModel.ZoneNumber,

            };
            await _addressRepository.AddAsync(model, cancellationToken);
            
            return model;
        }
        
       
       
    }
}

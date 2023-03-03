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
    public class PersonAddressService : IPersonAddressService
    {
        //private readonly IRepository<PersonAddress> _personAddressRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPersonAddressRepository _personAddresRepository;
        private readonly IAddressService _addressService;

        private PagingSettings _pagingSettings;

        public PersonAddressService(IRepository<PersonAddress> personAddressRepository, IOptionsSnapshot<PagingSettings> pagingSettings,
            IPersonRepository personRepository, IPersonAddressRepository personAddresRepository,IAddressService addressService,IAddressRepository addressRepository)
        {
            _personRepository = personRepository;
            _personAddresRepository = personAddresRepository; 
            _pagingSettings = pagingSettings.Value;
            _addressService = addressService;
            _addressRepository = addressRepository;


        }

        public Task<PersonAddress> GetByPersonIdAddressId(long personId, long addressId, CancellationToken cancellationToken)
        {
            var model = _personAddresRepository.GetByPersonIdAddrerssIdNoTracking(personId, addressId, cancellationToken);
            return model;
        }
        
        public async Task<PersonAddress> AddOrUpdateByAddressId(long? addressId,long personId,AddressViewModel viewModel, CancellationToken cancellationToken)
        {
            Address address = await _addressRepository.GetByIdAsync(cancellationToken,addressId);
            if (address == null)
            {
                viewModel.Code=Guid.NewGuid();
                Address newAddress = await _addressService.CreateAddress(viewModel, cancellationToken);
                addressId = newAddress.Id;
            }
            else
            {
                address.CityId = viewModel.CityId;
                address.Description = viewModel.Description;
                address.Mobile = viewModel.Mobile;
                await _addressRepository.UpdateAsync(address, cancellationToken);
            }

            PersonAddress personAddress = await GetByPersonIdAddressId(personId, addressId.Value, cancellationToken);
            if (personAddress == null)
            {
                personAddress = new PersonAddress();
                personAddress.PersonId = personId;
                personAddress.AddressId = addressId.Value;
                personAddress.AddressTypeId = 3; // آدرس های مربوط به صدور بیمه نامه
                await _personAddresRepository.AddAsync(personAddress, cancellationToken);
            }
            return personAddress;
        }
    }
}

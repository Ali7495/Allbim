using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.Person;
using Services.ViewModels;

namespace Services
{
    public interface IAddressService
    {

        Task<Address> CreateAddress(AddressViewModel viewModel, CancellationToken cancellationToken);
        
    }
}

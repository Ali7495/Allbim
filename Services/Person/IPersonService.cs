using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.Attachment;
using Models.PageAble;
using Models.Person;
using Models.PolicyRequest;
using Services.ViewModels;

namespace Services
{
    public interface IPersonService
    {
        Task<Person> Create(PersonViewModel personViewModel, CancellationToken cancellationToken);
        Task<PersonResultViewModel> Update(Guid code, UpdatePersonInputViewModel personViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(Guid personCode, CancellationToken cancellationToken);
        Task<PersonResultViewModel> GetPersonDetail(Guid code, CancellationToken cancellationToken);
        Task<Person> GetDetailMine(long UserID, CancellationToken cancellationToken);
        Task<PagedResult<PersonResultViewModel>> all(PageAbleResult PageAbleResult, CancellationToken cancellationToken);
        Task<AddressViewModel> CreatePersonAddress(AddressViewModel viewModel, CancellationToken cancellationToken, string code);
        Task<AddressViewModel> UpdatePersonAddress(string code, string addressCode, AddressInputViewModel viewModel, CancellationToken cancellationToken);
        Task<AddressViewModel> UpdatePersonAddressMine(long UserID, string addressCode, AddressInputViewModel viewModel, CancellationToken cancellationToken);
        Task<AddressViewModel> CreatePersonAddressMine(long UserID, AddressViewModel viewModel, CancellationToken cancellationToken);
        Task<Address> UpdatePersonAddressCommon(PersonAddress address, AddressInputViewModel viewModel,
            CancellationToken cancellationToken);
        Task<bool> DeleteAddressByCodeMine(CancellationToken cancellationToken, long UserID, Guid address_code);
        Task<List<PersonAddressViewModel>> GetPersonAddresses(Guid code, CancellationToken cancellationToken);
        Task<List<PersonAddressViewModel>> GetPersonAddressesMine(long UserID, CancellationToken cancellationToken);
        Task<bool> DeleteAddress(CancellationToken cancellationToken, string code, int id);
        Task<bool> DeleteAddressByCode(CancellationToken cancellationToken, Guid person_code, Guid address_code);

        Task<PersonAttachmentViewModel> CreatePersonAttachment(CancellationToken cancellationToken, IFormFile file, string personCode, int typeId);
        Task<MyPersonViewModel> GetPersondata(long userId, CancellationToken cancellationToken);
        Task<MyPersonViewModel> GetPersondataMine(long userId, CancellationToken cancellationToken);
        Task<MyPersonViewModel> UpdatePersondata(long userId, MyPersonUpdateViewModel viewModel, CancellationToken cancellationToken);
        Task<MyPersonViewModel> UpdatePersondataMine(long userId, MyPersonUpdateViewModel viewModel, CancellationToken cancellationToken);
        Task<PersonResultViewModel> PersonPostMapperService(PersonViewModel personViewModel, CancellationToken cancellationToken);
        Task<PagedResult<PersonResultViewModel>> GetAllPersonsWithoutUser(string search_text, PageAbleResult PageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<PersonResultViewModel>> GetSearchedPersons(string search_text, PageAbleResult PageAbleResult, CancellationToken cancellationToken);

    }
}

using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.ContactUs;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.PageAble;

namespace Services.Contactus
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IMapper _mapper;
        private readonly PagingSettings _pagingSettings;
        public ContactUsService(IContactUsRepository contactUsRepository, IMapper mapper, IOptionsSnapshot<PagingSettings> pagingSettings)
        {
            _contactUsRepository = contactUsRepository;
            _mapper = mapper;
            _pagingSettings = pagingSettings.Value;
        }

        #region Front Side
        public async Task<ContactUsFrontResultViewModel> create(ContactUsInputPostViewModel ContactUsInputViewModel, CancellationToken cancellationToken)
        {
            long TrackingNumber = 0;
            var latest = await _contactUsRepository.GetLatestContactUs(cancellationToken);
            if (latest == null)
            {
                TrackingNumber = 1;
            }
            else
            {
                TrackingNumber = ++latest.TrackingNumber;
            }
            ContactUs contactUs = new ContactUs
            {
                CreatedDateTime = DateTime.Now,
                Description = ContactUsInputViewModel.Description,
                Email = ContactUsInputViewModel.Email,
                IsDeleted = false,
                Title = ContactUsInputViewModel.Title,
                TrackingNumber = TrackingNumber
            };
            await _contactUsRepository.AddAsync(contactUs, cancellationToken);
            return _mapper.Map<ContactUsFrontResultViewModel>(contactUs);
        }

        

        #endregion

        #region Dashboard Side
    public async Task<PagedResult<ContactUsDashboardResultViewModel>> all(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<ContactUs> categories = await _contactUsRepository.GetAllContactUs(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<ContactUsDashboardResultViewModel>>(categories);
        }


        public async Task<bool> Delete(long ID, CancellationToken cancellationToken)
        {
            ContactUs ContactUsItem = await _contactUsRepository.GetByIdAsync(cancellationToken, ID);
            if (ContactUsItem == null)
                throw new BadRequestException("موردی یافت نشد");

            ContactUsItem.IsDeleted = true;

            await _contactUsRepository.UpdateAsync(ContactUsItem, cancellationToken);

            return true;
        }

        public async Task<ContactUsDashboardResultViewModel> Update(long ID, ContactUsInputViewModel ContactUsInputViewModel, CancellationToken cancellationToken)
        {
            ContactUs ContactUsItem = await _contactUsRepository.GetByIdAsync(cancellationToken, ID);
            if (ContactUsItem == null)
                throw new BadRequestException("موردی یافت نشد");

            ContactUsItem.Answer = ContactUsInputViewModel.Answer;
            ContactUsItem.Description = ContactUsInputViewModel.Description;
            ContactUsItem.Email = ContactUsInputViewModel.Email;
            ContactUsItem.Title = ContactUsInputViewModel.Title;

            await _contactUsRepository.UpdateAsync(ContactUsItem, cancellationToken);

            return _mapper.Map<ContactUsDashboardResultViewModel>(ContactUsItem);
        }
        public async Task<ContactUsDashboardResultViewModel> Answer(long ID, ContactUsInputPutViewModel ContactUsInputViewModel, CancellationToken cancellationToken)
        {
            ContactUs ContactUsItem = await _contactUsRepository.GetByIdAsync(cancellationToken, ID);
            if (ContactUsItem == null)
                throw new BadRequestException("موردی یافت نشد");

            ContactUsItem.Answer = ContactUsInputViewModel.Answer;

            await _contactUsRepository.UpdateAsync(ContactUsItem, cancellationToken);

            return _mapper.Map<ContactUsDashboardResultViewModel>(ContactUsItem);
        }
        
        public async Task<ContactUsDashboardResultViewModel> GetById(long id,CancellationToken cancellationToken)
        {
            ContactUs ContactUsItem = await _contactUsRepository.GetByIdAsync(cancellationToken, id);
            if (ContactUsItem == null)
                throw new BadRequestException("موردی یافت نشد");
            return _mapper.Map<ContactUsDashboardResultViewModel>(ContactUsItem);
        }
        

        #endregion
    

    }
}

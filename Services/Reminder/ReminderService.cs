using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
using DAL.Models;
using Models.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Services
{
    public class ReminderService : IReminderService
    {

        #region Fields

        private readonly IMapper _mapper;
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ReminderPeriod> _reminderPeriodReposity;
        #endregion

        #region CTOR

        public ReminderService(IUserRepository userRepository, IMapper mapper, IReminderRepository reminderRepository, IRepository<ReminderPeriod> reminderPeriodReposity)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _reminderRepository = reminderRepository;
            _reminderPeriodReposity = reminderPeriodReposity;
        }

        public async Task<ReminderResultViewModel> Create(ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {

            Reminder model = new Reminder()
            {
                DueDate = viewModel.DueDate,
                CityId = viewModel.CityId,
                Description = viewModel.Description,
                InsuranceId = viewModel.InsuranceId,
                ReminderPeriodId = viewModel.ReminderPeriodId,

            };
            await _reminderRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<ReminderResultViewModel>(model);


        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var ReminderData = await _reminderRepository.GetByIdAsync(cancellationToken,id);
            if (ReminderData == null)
                throw new CustomException("داده یافت نشد");
            await _reminderRepository.DeleteAsync(ReminderData, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteMine(long UserID,long id, CancellationToken cancellationToken)
        {
            var ReminderData = await _reminderRepository.GetByIdAsync(cancellationToken,id);
            if (ReminderData == null)
                throw new CustomException("داده یافت نشد");
            await ValidateAuthor(ReminderData.PersonId, UserID, cancellationToken);
            await _reminderRepository.DeleteAsync(ReminderData, cancellationToken);
            return true;
        }

        public async Task<ReminderResultViewModel> detail(long id, CancellationToken cancellationToken)
        {
            try
            {
                var ReminderData = await _reminderRepository.GetReminderByID(id, cancellationToken);
                if (ReminderData == null)
                    throw new CustomException("موردی یافت نشد");
                return _mapper.Map<ReminderResultViewModel>(ReminderData);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }
         public async Task<ReminderResultViewModel> detailMine(long UserID,long id, CancellationToken cancellationToken)
        {
            try
            {
                var ReminderData = await _reminderRepository.GetReminderByID(id, cancellationToken);
                if (ReminderData == null)
                    throw new CustomException("موردی یافت نشد");
                await ValidateAuthor(ReminderData.PersonId, UserID, cancellationToken);
                return _mapper.Map<ReminderResultViewModel>(ReminderData);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<List<ReminderPeriodResultViewModel>> getAllPeriod(CancellationToken cancellationToken)
        {
            var model = await _reminderPeriodReposity.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReminderPeriodResultViewModel>>(model);
        }

        public async Task<List<ReminderResultViewModel>> GetAllReminder(CancellationToken cancellationToken)
        {
            var model = await _reminderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReminderResultViewModel>>(model);
        }

        public async Task<ReminderResultViewModel> Update(long id, ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var ReminderData = await _reminderRepository.GetReminderByID(id, cancellationToken);
                    if (ReminderData == null)
                        throw new BadRequestException("داده یافت نشد");
                    ReminderData.Description = viewModel.Description;
                    ReminderData.CityId = viewModel.CityId;
                    ReminderData.DueDate = viewModel.DueDate;
                    ReminderData.InsuranceId = viewModel.InsuranceId;
                    ReminderData.ReminderPeriodId = viewModel.ReminderPeriodId;
                    await _reminderRepository.UpdateAsync(ReminderData, cancellationToken);
                    transaction.Complete();
                    return _mapper.Map<ReminderResultViewModel>(ReminderData);
            }
        }

        public async Task<ReminderResultViewModel> UpdateMine(long UserID, long id, ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var ReminderData = await _reminderRepository.GetReminderByID(id, cancellationToken);
                if (ReminderData == null)
                    throw new BadRequestException("داده یافت نشد");
                await ValidateAuthor(ReminderData.PersonId, UserID, cancellationToken);
                ReminderData.Description = viewModel.Description;
                ReminderData.CityId = viewModel.CityId;
                ReminderData.DueDate = viewModel.DueDate;
                ReminderData.InsuranceId = viewModel.InsuranceId;
                ReminderData.ReminderPeriodId = viewModel.ReminderPeriodId;
                await _reminderRepository.UpdateAsync(ReminderData, cancellationToken);
                transaction.Complete();
                return _mapper.Map<ReminderResultViewModel>(ReminderData);
            }
        }

        public async Task ValidateAuthor(long? ReminderPersonID , long UserID, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
                throw new NotFoundException(" کاربر وجود ندارد");
            if(ReminderPersonID != User.PersonId)
                throw new NotFoundException("اطلاعات نامعتبر");
        }
        #endregion


    }
}

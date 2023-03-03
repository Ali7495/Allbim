using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using Models.CompanyCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Utilities;
using Models.PageAble;
using Models.Center;
using Models.CompanyCenterSchedule;

namespace Services.CompanyCenter
{
    public class CompanyCenterServices : ICompanyCenterServices
    {
        private readonly ICompanyCenterScheduleRepository _companyCenterScheduleRepository;
        private readonly ICompanyCenterRepository _companyCenterRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IUserRepository _userRepository;

        public CompanyCenterServices(IPersonCompanyRepository personCompanyRepository,ICompanyCenterScheduleRepository companyCenterScheduleRepository, ICompanyCenterRepository companyCenterRepository, ICompanyRepository companyRepository, 
            ICityRepository cityRepository, IMapper mapper,IUserRepository userRepository)
        {
            _personCompanyRepository = personCompanyRepository;
            _companyCenterScheduleRepository = companyCenterScheduleRepository;
            _companyCenterRepository = companyCenterRepository;
            _companyRepository = companyRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #region Schedule

        public async Task<CenterScheduleResultViewModel> CreateCenterSchedule(Guid code, long centerId, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            var center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            CompanyCenterSchedule centerSchedule = _mapper.Map<CompanyCenterSchedule>(viewModel);
            centerSchedule.CompanyCenterId = centerId;
            //await _companyCenterScheduleRepository.AddAsync(centerSchedule, cancellationToken);
            centerSchedule= await CreateCenterScheduleCommon(viewModel, centerId, cancellationToken);
            return _mapper.Map<CenterScheduleResultViewModel>(centerSchedule);
        }

        

        public async Task<PagedResult<CenterScheduleResultViewModel>> GetAllCenterSchedules(Guid code, long centerId,PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            var center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            // List<CompanyCenterSchedule> centerSchedules = await _companyCenterScheduleRepository.GetAllCenterSchedules(centerId, cancellationToken);
            //PagedResult<CompanyCenterSchedule> centerSchedules = await _companyCenterScheduleRepository
            //    .GetAsyncAdvanced(cancellationToken,pageAbleModel,
            //        s => s.CompanyCenterId == centerId,
            //        i => i.CompanyCenter);
            PagedResult<CompanyCenterSchedule> centerSchedules = await GetAllCenterSchedulesCommon(cancellationToken, pageAbleModel, centerId);
            return _mapper.Map<PagedResult<CenterScheduleResultViewModel>>(centerSchedules);
        }

        public async Task<CenterScheduleResultViewModel> GetCenterSchedule(Guid code, long centerId, long id, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            var center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            //CompanyCenterSchedule centerSchedule = await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken,id);
            CompanyCenterSchedule centerSchedule = await GetCenterScheduleCommon(id, cancellationToken);
            return _mapper.Map<CenterScheduleResultViewModel>(centerSchedule);
        }

        public async Task<CenterScheduleResultViewModel> UpdateCenterSchedule(Guid code, long centerId, long id, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            CompanyCenterSchedule centerSchedule = await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken,id);
            if (centerSchedule == null)
            {
                throw new BadRequestException("سانس وجود ندارد");
            }

            centerSchedule.Name = viewModel.Name;
            centerSchedule.Description = viewModel.Description;

            //await _companyCenterScheduleRepository.UpdateAsync(centerSchedule, cancellationToken);
            await UpdateCenterScheduleCommon(centerSchedule, cancellationToken);
            return _mapper.Map<CenterScheduleResultViewModel>(centerSchedule);
        }

        public async Task<string> DeleteCenterSchedule(Guid code, long centerId, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            CompanyCenterSchedule centerSchedule = await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken,id);
            if (centerSchedule == null)
            {
                throw new BadRequestException("سانس وجود ندارد");
            }

            //await _companyCenterScheduleRepository.DeleteAsync(centerSchedule, cancellationToken);
            await DeleteCenterScheduleCommon(centerSchedule, cancellationToken);

            return true.ToString();
        }



        
        public async Task<PagedResult<CenterScheduleResultViewModel>> GetAllCenterSchedulesMine(long UserID, long centerId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            var center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            //PagedResult<CompanyCenterSchedule> centerSchedules = await _companyCenterScheduleRepository.GetAsyncAdvanced(cancellationToken, pageAbleModel,s => s.CompanyCenterId == centerId,i => i.CompanyCenter);
            PagedResult<CompanyCenterSchedule> centerSchedules = await GetAllCenterSchedulesCommon(cancellationToken, pageAbleModel, centerId);

            return _mapper.Map<PagedResult<CenterScheduleResultViewModel>>(centerSchedules);
        }
        public async Task<PagedResult<CompanyCenterSchedule>> GetAllCenterSchedulesCommon(CancellationToken cancellationToken, PageAbleModel pageAbleModel , long centerId)
        {
            return await _companyCenterScheduleRepository.GetAsyncAdvanced(cancellationToken, pageAbleModel,s => s.CompanyCenterId == centerId,i => i.CompanyCenter);
        }
        public async Task<CenterScheduleResultViewModel> GetCenterScheduleMine(long UserID, long centerId, long id, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            var center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            //CompanyCenterSchedule centerSchedule = await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken, id);
            CompanyCenterSchedule centerSchedule = await GetCenterScheduleCommon(id, cancellationToken);

            return _mapper.Map<CenterScheduleResultViewModel>(centerSchedule);
        }
        public async Task<CompanyCenterSchedule> GetCenterScheduleCommon(long id, CancellationToken cancellationToken)
        {
            return await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken, id);
        }
        public async Task<CenterScheduleResultViewModel> CreateCenterScheduleMine(long UserID, long centerId, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            var center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            CompanyCenterSchedule centerSchedule = _mapper.Map<CompanyCenterSchedule>(viewModel);
            centerSchedule.CompanyCenterId = centerId;
            //await _companyCenterScheduleRepository.AddAsync(centerSchedule, cancellationToken);
            centerSchedule= await CreateCenterScheduleCommon(viewModel, centerId, cancellationToken);
            return _mapper.Map<CenterScheduleResultViewModel>(centerSchedule);
        }
        public async Task<CompanyCenterSchedule> CreateCenterScheduleCommon(CenterScheduleInputViewModel viewModel , long centerId, CancellationToken cancellationToken)
        {
            CompanyCenterSchedule centerSchedule = _mapper.Map<CompanyCenterSchedule>(viewModel);
            centerSchedule.CompanyCenterId = centerId;
            await _companyCenterScheduleRepository.AddAsync(centerSchedule, cancellationToken);
            return centerSchedule;
        }
        public async Task<CenterScheduleResultViewModel> UpdateCenterScheduleMine(long UserID, long centerId, long id, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            CompanyCenterSchedule centerSchedule = await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken, id);
            if (centerSchedule == null)
            {
                throw new BadRequestException("سانس وجود ندارد");
            }

            centerSchedule.Name = viewModel.Name;
            centerSchedule.Description = viewModel.Description;

            //await _companyCenterScheduleRepository.UpdateAsync(centerSchedule, cancellationToken);
            await UpdateCenterScheduleCommon(centerSchedule, cancellationToken);
            return _mapper.Map<CenterScheduleResultViewModel>(centerSchedule);
        }
        public async Task UpdateCenterScheduleCommon(CompanyCenterSchedule centerSchedule , CancellationToken cancellationToken)
        {
            await _companyCenterScheduleRepository.UpdateAsync(centerSchedule, cancellationToken);
        }
        #endregion

        
        
        
        

        #region Center

        public async Task<PagedResult<CompanyCenterResultViewModel>> GetAllCenters(Guid code,PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            //PagedResult<DAL.Models.CompanyCenter> companyCenters = await _companyCenterRepository.GetAllCentersByCompanyCode(code,pageAbleModel, cancellationToken);
            PagedResult<DAL.Models.CompanyCenter> companyCenters = await GetAllCentersCommon(company.Code, pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<CompanyCenterResultViewModel>>(companyCenters);
        }

        public async Task<CompanyCenterResultViewModel> GetCenter(Guid code, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            //DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetCentersWithAllData(id, cancellationToken);
            DAL.Models.CompanyCenter companyCenter = await GetCenterCommon(id, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }

        public async Task<CompanyCenterResultViewModel> CreateCenter(Guid code, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = _mapper.Map<DAL.Models.CompanyCenter>(viewModel);
            companyCenter.CompanyId = company.Id;
            //await _companyCenterRepository.AddAsync(companyCenter, cancellationToken);
            await CreateCenterCommon(companyCenter, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }

        public async Task<CompanyCenterResultViewModel> UpdateCenter(Guid code, long id, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetByIdAsync(cancellationToken, id);
            //companyCenter.Name = viewModel.Name;
            //companyCenter.Description = viewModel.Description;
            //companyCenter.CityId = viewModel.CityId;

            //await _companyCenterRepository.UpdateAsync(companyCenter, cancellationToken);
            await UpdateCenterCommon(companyCenter, viewModel, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }

        public async Task<string> DeleteCenter(Guid code, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetCenterWithSchedules(id,cancellationToken);
            if (companyCenter == null )
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            if (companyCenter.CompanyCenterSchedules.Count > 0)
            {
                throw new BadRequestException("مرکز دارای زمانبندی می باشد و قابل حذف نیست.");
            }

            //await _companyCenterRepository.DeleteAsync(companyCenter, cancellationToken);
            await DeleteCenterCommon(companyCenter, cancellationToken);
            return true.ToString();
        }


        public async Task<List<CompanyCenterViewModel>> GetCentersByCityAndCompanyCode(Guid code, long cityId, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.City city = await _cityRepository.GetByIdAsync(cancellationToken, cityId);
            if (city == null)
            {
                throw new BadRequestException("شهر وجود ندارد");
            }

            List<DAL.Models.CompanyCenter> companyCenters = await _companyCenterRepository.GetCentersByCityAndCompayId(company.Id, cityId, cancellationToken);
            if (companyCenters == null)
            {
                throw new BadRequestException("مرکزی وجود ندارد");
            }

            List<CompanyCenterViewModel> companyCenterViewModels = _mapper.Map<List<CompanyCenterViewModel>>(companyCenters);

            
            companyCenterViewModels = GetCentersByCityAndCompanyCodeCommon(companyCenterViewModels, companyCenters);
            return companyCenterViewModels;
        }


        public async Task<PagedResult<CompanyCenterResultViewModel>> GetAllCentersMine(long UserID, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            PersonCompany PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken,PersonCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            //PagedResult<DAL.Models.CompanyCenter> companyCenters = await _companyCenterRepository.GetAllCentersByCompanyCode(company.Code, pageAbleModel, cancellationToken);
            PagedResult<DAL.Models.CompanyCenter> companyCenters = await GetAllCentersCommon(company.Code, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<CompanyCenterResultViewModel>>(companyCenters);
        }



        public async Task<PagedResult<DAL.Models.CompanyCenter>> GetAllCentersCommon(Guid code , PageAbleModel pageAbleModel , CancellationToken cancellationToken)
        {
            return await _companyCenterRepository.GetAllCentersByCompanyCode(code, pageAbleModel, cancellationToken);
        }
        public async Task<CompanyCenterResultViewModel> GetCenterMine(long UserID, long id, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            PersonCompany PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken,PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            //DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetCentersWithAllData(id, cancellationToken);
            DAL.Models.CompanyCenter companyCenter = await GetCenterCommon(id, cancellationToken);

            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }
        public async Task<DAL.Models.CompanyCenter> GetCenterCommon(long id, CancellationToken cancellationToken)
        {
            return await _companyCenterRepository.GetCentersWithAllData(id, cancellationToken);
        }
        public async Task<CompanyCenterResultViewModel> CreateCenterMine(long UserID, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        { DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            var PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            DAL.Models.CompanyCenter companyCenter = _mapper.Map<DAL.Models.CompanyCenter>(viewModel);
            companyCenter.CompanyId = company.Id;
            //await _companyCenterRepository.AddAsync(companyCenter, cancellationToken);
            await CreateCenterCommon(companyCenter, cancellationToken);

            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }
        public async Task CreateCenterCommon(DAL.Models.CompanyCenter companyCenter , CancellationToken cancellationToken)
        {
            await _companyCenterRepository.AddAsync(companyCenter, cancellationToken);
        }
        public async Task<CompanyCenterResultViewModel> UpdateCenterMine(long UserID,long id, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }
            
            PersonCompany PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetByIdAsync(cancellationToken, id);
            //companyCenter.Name = viewModel.Name;
            //companyCenter.Description = viewModel.Description;
            //companyCenter.CityId = viewModel.CityId;

            //await _companyCenterRepository.UpdateAsync(companyCenter, cancellationToken);
            await UpdateCenterCommon(companyCenter, viewModel, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }
        public async Task UpdateCenterCommon(DAL.Models.CompanyCenter companyCenter, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            companyCenter.Name = viewModel.Name;
            companyCenter.Description = viewModel.Description;
            companyCenter.CityId = viewModel.CityId;

            await _companyCenterRepository.UpdateAsync(companyCenter, cancellationToken);
        }
        public async Task<List<CompanyCenterViewModel>> GetCentersByCityAndCompanyCodeMine(long UserID, long cityId, CancellationToken cancellationToken)
        {

            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }

            PersonCompany PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
                throw new BadRequestException("شرکت وجود ندارد");

            DAL.Models.City city = await _cityRepository.GetByIdAsync(cancellationToken, cityId);
            if (city == null)
            {
                throw new BadRequestException("شهر وجود ندارد");
            }

            List<DAL.Models.CompanyCenter> companyCenters = await _companyCenterRepository.GetCentersByCityAndCompayId(company.Id, cityId, cancellationToken);
            List<CompanyCenterViewModel> companyCenterViewModels = _mapper.Map<List<CompanyCenterViewModel>>(companyCenters);


            companyCenterViewModels = GetCentersByCityAndCompanyCodeCommon(companyCenterViewModels, companyCenters);
            return companyCenterViewModels;
        }
        public List<CompanyCenterViewModel> GetCentersByCityAndCompanyCodeCommon(List<CompanyCenterViewModel> companyCenterViewModels , List<DAL.Models.CompanyCenter> companyCenters)
        {
            for (int i = 0; i < companyCenterViewModels.Count; i++)
            {
                List<CenterSessionDataViewModel> centerSessionData = new List<CenterSessionDataViewModel>();
                for (int j = 1; j <= 7; j++)
                {
                    centerSessionData.Add(new CenterSessionDataViewModel()
                    {
                        Date = DateTime.Today.AddDays(j).ToString("yyyy/MM/dd"),
                        JalaliDate = PersianDateTime.Now.AddDays(j).ToString("yyyy/MM/dd"),
                        DayName = PersianDateTime.Now.AddDays(j).ToString("dddd")
                    });
                }
                companyCenterViewModels[i].CenterSessionData = centerSessionData;
                companyCenterViewModels[i].CenterSessionData.ForEach(
                    c => c.CenterSchedules = _mapper.Map<List<CenterScheduleViewModel>>(companyCenters.Single(c => c.Id == companyCenterViewModels[i].Id).CompanyCenterSchedules));
            }
            return companyCenterViewModels;
        }

        public async Task<string> DeleteCenterMine(long UserID, long id, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }

            PersonCompany PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetCenterWithSchedules(id, cancellationToken);
            if (companyCenter == null)
                throw new BadRequestException("مرکز وجود ندارد");

            if (companyCenter.CompanyCenterSchedules.Count > 0)
                throw new BadRequestException("مرکز دارای زمانبندی می باشد و قابل حذف نیست.");
            //await _companyCenterRepository.DeleteAsync(companyCenter, cancellationToken);
            await DeleteCenterCommon(companyCenter, cancellationToken);
            return true.ToString();
        }

        public async Task DeleteCenterCommon(DAL.Models.CompanyCenter companyCenter , CancellationToken cancellationToken)
        {
            await _companyCenterRepository.DeleteAsync(companyCenter, cancellationToken);
        }

        public async Task<string> DeleteCenterScheduleMine(long UserID ,long centerId, long id, CancellationToken cancellationToken)
        {
            DAL.Models.User User = await _userRepository.GetByIdAsync(cancellationToken, UserID);
            if (User == null)
            {
                throw new BadRequestException(" کاربر وجود ندارد");
            }

            PersonCompany PersonCompany = await _personCompanyRepository.GetByPersonId(User.PersonId, cancellationToken);
            if (PersonCompany == null)
                throw new BadRequestException("شما با هیچ شرکتی ارتباط ندارید");
            Company company = await _companyRepository.GetByIdAsync(cancellationToken, PersonCompany.CompanyId);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter center = await _companyCenterRepository.GetByIdAsync(cancellationToken, centerId);
            if (center == null)
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            CompanyCenterSchedule centerSchedule = await _companyCenterScheduleRepository.GetByIdAsync(cancellationToken, id);
            if (centerSchedule == null)
            {
                throw new BadRequestException("سانس وجود ندارد");
            }

            //await _companyCenterScheduleRepository.DeleteAsync(centerSchedule, cancellationToken);
            await DeleteCenterScheduleCommon(centerSchedule, cancellationToken);
            return true.ToString();
        }

        public async Task DeleteCenterScheduleCommon(CompanyCenterSchedule centerSchedule , CancellationToken cancellationToken)
        {
            await _companyCenterScheduleRepository.DeleteAsync(centerSchedule, cancellationToken);
        }
        #endregion
    }
}

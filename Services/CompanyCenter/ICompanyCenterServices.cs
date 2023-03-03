using Models.CompanyCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using Models.Center;
using Models.CompanyCenterSchedule;

namespace Services.CompanyCenter
{
    public interface ICompanyCenterServices
    {

        #region Schedule

        Task<PagedResult<CenterScheduleResultViewModel>> GetAllCenterSchedules(Guid code, long centerId,PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<CenterScheduleResultViewModel>> GetAllCenterSchedulesMine(long UserID, long centerId,PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<CompanyCenterSchedule>> GetAllCenterSchedulesCommon(CancellationToken cancellationToken,
            PageAbleModel pageAbleModel, long centerId);
        
        Task<CenterScheduleResultViewModel> GetCenterSchedule(Guid code, long centerId, long id, CancellationToken cancellationToken);
        Task<CenterScheduleResultViewModel> GetCenterScheduleMine(long UserID, long centerId, long id, CancellationToken cancellationToken);
        Task<CompanyCenterSchedule> GetCenterScheduleCommon(long id, CancellationToken cancellationToken);
        Task<CenterScheduleResultViewModel> CreateCenterSchedule(Guid code, long centerId, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken);
        Task<CenterScheduleResultViewModel> CreateCenterScheduleMine(long UserID, long centerId, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken);
        Task<CompanyCenterSchedule> CreateCenterScheduleCommon(CenterScheduleInputViewModel viewModel , long centerId, CancellationToken cancellationToken);
        
        Task<CenterScheduleResultViewModel> UpdateCenterSchedule(Guid code, long centerId, long id, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken);

        Task<CenterScheduleResultViewModel> UpdateCenterScheduleMine(long UserID, long centerId, long id, CenterScheduleInputViewModel viewModel, CancellationToken cancellationToken);
        Task UpdateCenterScheduleCommon(CompanyCenterSchedule centerSchedule, CancellationToken cancellationToken);
        
        Task<string> DeleteCenterSchedule(Guid code, long centerId, long id, CancellationToken cancellationToken);
        Task<string> DeleteCenterScheduleMine(long UserID, long centerId, long id, CancellationToken cancellationToken);

        #endregion

        #region Center

        Task<PagedResult<CompanyCenterResultViewModel>> GetAllCenters(Guid code,PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<CompanyCenterResultViewModel>> GetAllCentersMine(long UserID, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PagedResult<DAL.Models.CompanyCenter>> GetAllCentersCommon(Guid code, PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);
        
        Task<CompanyCenterResultViewModel> GetCenter(Guid code, long id, CancellationToken cancellationToken);
        Task<CompanyCenterResultViewModel> GetCenterMine(long UserID, long id, CancellationToken cancellationToken);
        Task<DAL.Models.CompanyCenter> GetCenterCommon(long id, CancellationToken cancellationToken);
        Task<CompanyCenterResultViewModel> CreateCenter(Guid code, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken);
        Task<CompanyCenterResultViewModel> CreateCenterMine(long UserID, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken);
        Task CreateCenterCommon(DAL.Models.CompanyCenter companyCenter, CancellationToken cancellationToken);
        Task<CompanyCenterResultViewModel> UpdateCenter(Guid code, long id, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken);
        Task<CompanyCenterResultViewModel> UpdateCenterMine(long UserID,long id, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken);

        Task UpdateCenterCommon(DAL.Models.CompanyCenter companyCenter, CompanyCenterInputViewModel viewModel,
            CancellationToken cancellationToken);
        
        Task<string> DeleteCenter(Guid code, long id, CancellationToken cancellationToken);
        Task<string> DeleteCenterMine(long UserID, long id, CancellationToken cancellationToken);

        Task<List<CompanyCenterViewModel>> GetCentersByCityAndCompanyCode(Guid code, long cityId, CancellationToken cancellationToken);
        Task<List<CompanyCenterViewModel>> GetCentersByCityAndCompanyCodeMine(long UserID, long cityId, CancellationToken cancellationToken);

        List<CompanyCenterViewModel> GetCentersByCityAndCompanyCodeCommon(
            List<CompanyCenterViewModel> companyCenterViewModels, List<DAL.Models.CompanyCenter> companyCenters);

        #endregion
    }
}

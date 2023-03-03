using Common.Utilities;
using Models.FAQ;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuranceFAQServices
    {
        Task<FAQResultViewModel> PostNewInsuranceFAQ(long insuranceId, FAQInputViewModel model, CancellationToken cancellationToken);
        Task<FAQResultViewModel> GetInsuranceFAQ(long insuranceId, long faqId, CancellationToken cancellationToken);
        Task<PagedResult<FAQResultViewModel>> GetAllInsuranceFAQs(long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<FAQResultViewModel> UpdateInsuranceFAQ(long insuranceId, long faqId, FAQInputViewModel model, CancellationToken cancellationToken);
        Task<bool> DeleteInsuranceFAQ(long insuranceId, long faqId, CancellationToken cancellationToken);
    }
}

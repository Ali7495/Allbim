using Common.Utilities;
using Models.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.PageAble;

namespace Services.Contactus
{
    public interface IContactUsService
    {
        Task<ContactUsFrontResultViewModel> create(ContactUsInputPostViewModel ContactUsInputViewModel, CancellationToken cancellationToken);

        Task<bool> Delete(long ID, CancellationToken cancellationToken);
        Task<ContactUsDashboardResultViewModel> Update(long ID, ContactUsInputViewModel ContactUsInputViewModel, CancellationToken cancellationToken);

        Task<PagedResult<ContactUsDashboardResultViewModel>> all(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);

        Task<ContactUsDashboardResultViewModel> Answer(long ID, ContactUsInputPutViewModel ContactUsInputViewModel,
            CancellationToken cancellationToken);

        Task<ContactUsDashboardResultViewModel> GetById(long id, CancellationToken cancellationToken);

    }
}

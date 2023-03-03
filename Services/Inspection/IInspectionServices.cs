using Models.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInspectionServices
    {
        Task<List<InspectionDataViewModel>> GetInspectionData(CancellationToken cancellationToken);
    }
}

using Models.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IIssueSessionService
    {
        Task<List<IssueSessionDataViewModel>> GetIssueSessionData(CancellationToken cancellationToken);
    }
}

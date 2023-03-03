using Models.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;
using Models.Person;
using Models.CompanyAgent;

namespace Services.Agent
{
    public interface IAgentService
    {
        Task<PagedResult<AgentViewModel>> GetAgents(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<PersonResultWithAgentCompanyViewModel>> GetAgentsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<List<AgentViewModel>> GetAgentsList(Guid code, CancellationToken cancellationToken);
        Task<AgentViewModel> GetAgent(Guid code, Guid personCode, CancellationToken cancellationToken);
        Task<AgentPersonViewModel> CreateAgent(Guid code, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken);
        Task<AgentPersonViewModel> UpdateAgent(Guid code, Guid personCode, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> DeleteAgent(Guid code, Guid personCode, CancellationToken cancellationToken);


        Task<PagedResult<CompanyAgentViewModel>> GetCompanyAgentsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<List<AgentViewModel>> GetAgentsListMine(long userId, CancellationToken cancellationToken);
        Task<PersonResultViewModel> CreateAgentMine(long userId, PersonViewModel viewModel, CancellationToken cancellationToken);
        Task<PersonResultViewModel> GetAgentMine(long userId, Guid personCode, CancellationToken cancellationToken);
        Task<PersonResultViewModel> UpdateAgentMine(long userId, Guid personCode, UpdatePersonInputViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> DeleteAgentMine(long userId, Guid personCode, CancellationToken cancellationToken);
    }
}

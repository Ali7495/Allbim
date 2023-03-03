using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using Models.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class IssueSessionService : IIssueSessionService
    {
        private readonly IRepository<IssueSession> _issueSessionRepository;
        private readonly IMapper _mapper;

        public IssueSessionService(IRepository<IssueSession> issueSessionRepository, IMapper mapper)
        {
            _issueSessionRepository = issueSessionRepository;
            _mapper = mapper;
        }

        public async Task<List<IssueSessionDataViewModel>> GetIssueSessionData(CancellationToken cancellationToken)
        {
            List<IssueSession> issueSessions = await _issueSessionRepository.GetAllAsync(cancellationToken);

            List<IssueSessionDataViewModel> issueSessionData = new List<IssueSessionDataViewModel>();

            for (int i = 1; i <= 7; i++)
            {
                issueSessionData.Add(new IssueSessionDataViewModel()
                {
                    Date = DateTime.Today.AddDays(i).ToString("yyyy/MM/dd"),
                    JalaliDate = PersianDateTime.Now.AddDays(i).ToString("yyyy/MM/dd"),
                    DayName = PersianDateTime.Now.AddDays(i).ToString("dddd"),
                    IssueSessions = _mapper.Map<List<IssueSessionsViewModel>>(issueSessions)
                });
            }

            return issueSessionData;
        }
    }
}

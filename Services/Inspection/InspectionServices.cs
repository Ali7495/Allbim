using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using Models.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class InspectionServices : IInspectionServices
    {
        private readonly IRepository<InspectionSession> _inspectionSessionRepository;
        private readonly IMapper _mapper;
        public InspectionServices(IRepository<InspectionSession> inspectionSessionRepository, IMapper mapper)
        {
            _inspectionSessionRepository = inspectionSessionRepository;
            _mapper = mapper;
        }

        public async Task<List<InspectionDataViewModel>> GetInspectionData(CancellationToken cancellationToken)
        {
            List<InspectionSession> sessions = await _inspectionSessionRepository.GetAllAsync(cancellationToken);

            List<InspectionDataViewModel> inspectionData = new List<InspectionDataViewModel>();

            List<InspectionDateAndTimeViewModel> inspectionDateAndTimes;



            for (int i = 1; i <= 7; i++)
            {
                inspectionDateAndTimes = new List<InspectionDateAndTimeViewModel>();

                for (int j = 0; j < sessions.Count; j++)
                {
                    inspectionDateAndTimes.Add(new InspectionDateAndTimeViewModel()
                    {
                        JalaliWithTime = sessions[j].Name + " - " + PersianDateTime.Now.AddDays(i).ToString("yyyy/MM/dd"),
                        DateWithTime = sessions[j].Name + " - " + DateTime.Today.AddDays(i).ToString("yyyy/MM/dd")
                    });
                }

                inspectionData.Add(new InspectionDataViewModel()
                {
                    Date = DateTime.Today.AddDays(i).ToString("yyyy/MM/dd"),
                    JalaliDate = PersianDateTime.Now.AddDays(i).ToString("yyyy/MM/dd"),
                    DayName = PersianDateTime.Now.AddDays(i).ToString("dddd"),
                    InspectionSessions = _mapper.Map<List<InspectionSessionViewModel>>(sessions),
                    DatesAndTimes = inspectionDateAndTimes
                });

                
            }


            return inspectionData;
        }
    }
}

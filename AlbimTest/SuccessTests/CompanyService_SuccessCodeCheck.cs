using albim;
using albim.Controllers.v1;
using AlbimTest.Methodes;
using AlbimTest.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services;
using Services.Agent;
using Services.CompanyCenter;
using Services.InsurerServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static AlbimTest.Models.TestingModel;

namespace AlbimTest.SuccessTests
{
    public class CompanyService_SuccessCodeCheck
    {
        CancellationToken cancellationToken;
        CmnMethodes _CmnMethodes;
        CompanyController _companyController;

        #region Property
        private readonly ICompanyService _companyService;
        private readonly IInsuranceService _insuranceService;
        private readonly IInsurerTermService _insurerTermService;
        private readonly IInsurerServices _insurerServices;
        private readonly IAgentService _agentService;
        private readonly ICompanyCenterServices _companyCenterServices;

        #endregion

        public CompanyService_SuccessCodeCheck()
        {
            cancellationToken = new CancellationToken();
            _CmnMethodes = new CmnMethodes();
            _companyController = new CompanyController(_companyService, _insuranceService, _insurerTermService, _agentService, _companyCenterServices, _insurerServices);
        }
       // [Fact]
        public void CompanyService_SuccessResult()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            Type t = typeof(CompanyController);
            MethodInfo[] mi = t.GetMethods();

            GetServiceMethodesResult result = _CmnMethodes.GetServicemethodesData(mi);
            for (int i = 0; i < result.MethodListResult.Count; i++)
            {
                int _MethodeID = result.MethodListResult[i].MethodeID;
                List<object> CurrentMethodParameters = result.MethodParametersResult.Where(x => x.MethodeID == _MethodeID).Count() > 0 ? result.MethodParametersResult.Where(x => x.MethodeID == _MethodeID).Select(x => x.Parameter).ToList() : new List<object>();

                object?[] MockDataArray = _CmnMethodes.GenerateFakeData(CurrentMethodParameters);
                var ResultTest = typeof(CompanyController).GetMethod(result.MethodListResult[i].MethodeName).Invoke(_companyController, MockDataArray);
            }
        }
        [Fact]
        public void StartupTest()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Xunit.Assert.NotNull(webHost);
            Xunit.Assert.NotNull(webHost.Services.GetRequiredService<IUserService>());
        }
    }
}

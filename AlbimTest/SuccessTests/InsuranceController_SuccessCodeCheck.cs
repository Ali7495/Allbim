using albim;
using albim.Configuration;
using albim.Controllers.v1;
using AlbimTest.Methodes;
using AlbimTest.Models;
using AlbimTest.SuccessTests;
using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Insurance;
using Models.Settings;
using Moq;
using Services;
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

namespace AlbimTest
{
    public class InsuranceController_SuccessCodeCheck
    {
        #region
        CancellationToken cancellationToken;
        CmnMethodes _CmnMethodes;
        InsuranceService _insuranceController;

        private readonly IInsuranceService _insuranceService;
        private readonly IInsuranceCenteralRuleService _insuranceCenteralRuleService;

        public InsuranceController_SuccessCodeCheck()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            var services = new ServiceCollection();
            services.AddIocConfig();
            cancellationToken = new CancellationToken();
            _CmnMethodes = new CmnMethodes();
        }
        #endregion
        [Fact]
        public void InsurersService_SuccessResult()
        {
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();
            Type t = typeof(InsuranceController);
            MethodInfo[] mi = t.GetMethods();
            GetServiceMethodesResult result = _CmnMethodes.GetServicemethodesData(mi);
            for (int i = 0; i < result.MethodListResult.Count; i++)
            {
                var RepositoryStop = new Mock<IInsuranceService>();
                RepositoryStop.Setup(repo => repo.GetInsuranceInsurer("88", cancellationToken)).Returns((Task<List<InsurerViewModel>>)null);
                var IInsuranceCenteralRuleService = new Mock<IInsuranceCenteralRuleService>();
                // var Controller = new InsuranceController(RepositoryStop.Object, IInsuranceCenteralRuleService.Object);
                // var result15 = Controller.GetInsurers("65", cancellationToken);
                // Xunit.Assert.IsType<Task<List<InsurerViewModel>>>(result15.Result);

                //int _MethodeID = result.MethodListResult[i].MethodeID;
                //List<object> CurrentMethodParameters = result.MethodParametersResult.Where(x => x.MethodeID == _MethodeID).Count() > 0 ? result.MethodParametersResult.Where(x => x.MethodeID == _MethodeID).Select(x => x.Parameter).ToList() : new List<object>();
                //object?[] MockDataArray = _CmnMethodes.GenerateFakeData(CurrentMethodParameters);
                //var ResultTest = typeof(InsuranceController).GetMethod(result.MethodListResult[i].MethodeName).Invoke(_insuranceController, MockDataArray);
            }
        }
    }
}

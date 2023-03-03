using albim.Result;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Attachment;
using Models.Person;
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System;
using Common.Exceptions;
using Models.QueryParams;
using Common.Extensions;
using Models.PageAble;
using Services.SmsService;

namespace albim.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class PersonController : BaseController
    {
        #region Property

        private readonly IConfiguration _configuration;
        private readonly IPersonService _personService;
        private readonly IPersonAddressService _personAddressService;
        private readonly IAttachmentService _attachmentService;
        private readonly ISmsService _smsService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PersonController(IConfiguration configuration, IPersonService personService,
            IPersonAddressService personAddressService, IAttachmentService attachmentService, ISmsService smsService)
        {
            _attachmentService = attachmentService;
            _personAddressService = personAddressService;
            _configuration = configuration;
            _personService = personService;
            _smsService = smsService;
        }

        #endregion

        #region Person Actions
        [HttpPut("MyInfo")]
        public async Task<ApiResult<MyPersonViewModel>> UpdatePersondata(MyPersonUpdateViewModel viewModel,CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var person = await _personService.UpdatePersondata(userId,viewModel, cancellationToken);
            return person;
        }
        [HttpGet("MyInfo")]
        public async Task<ApiResult<MyPersonViewModel>> GetPersondata( CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            var person = await _personService.GetPersondata(userId, cancellationToken);
            return person;
        }
        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="personViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ApiResult<PersonResultViewModel>> Create(PersonViewModel personViewModel,
            CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await _personService.PersonPostMapperService(personViewModel, cancellationToken);
            return person;
        }


        [HttpGet("{code}")]
        public async Task<ApiResult<PersonResultViewModel>> GetDetail(Guid code, CancellationToken cancellationToken)
        {
            PersonResultViewModel person = await _personService.GetPersonDetail(code, cancellationToken);
            return person;
        }

        [HttpGet()]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetAll([FromQuery] PageAbleResult pageAbleResult,
            [FromQuery] DateableViewModel dateableViewModel, CancellationToken cancellationToken)
        {
            var people =
                await _personService.all(pageAbleResult, cancellationToken);
            return people;
        }

        [HttpPut("{code}")]
        public async Task<ApiResult<PersonResultViewModel>> Update(Guid code, UpdatePersonInputViewModel personViewModel, CancellationToken cancellationToken)
        {
            return await _personService.Update(code, personViewModel, cancellationToken);
        }

        [HttpDelete("{code}")]
        public async Task<ApiResult<string>> Delete(Guid code, CancellationToken cancellationToken)
        {
            var res = await _personService.Delete(code, cancellationToken);
            return res.ToString();
        }


        [HttpGet("without_user")]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetWithoutUser([FromQuery] string search_text, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PersonResultViewModel> persons = await _personService.GetAllPersonsWithoutUser(search_text, pageAbleResult, cancellationToken);
            return persons;
        }

        [HttpGet("search")]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetSearchedPersons([FromQuery] string search_text, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<PersonResultViewModel> persons = await _personService.GetSearchedPersons(search_text, pageAbleResult, cancellationToken);
            return persons;
        }
        
        #endregion

        #region Address

        [AllowAnonymous]
        [HttpPost("{code}/address")]
        public async Task<ApiResult<AddressViewModel>> CreatePersonAddress(string code, AddressViewModel viewModel,
           CancellationToken cancellationToken)
        {
            var model = await _personService.CreatePersonAddress(viewModel, cancellationToken, code);

            return model;
        }


        [AllowAnonymous]
        [HttpGet("{code}/address")]
        public async Task<ApiResult<List<PersonAddressViewModel>>> GetAddresses(Guid code,
            CancellationToken cancellationToken)
        {
            try
            {
                var model = await _personService.GetPersonAddresses(code, cancellationToken);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPut("{code}/address/{addressCode}")]
        public async Task<ApiResult<AddressViewModel>> UpdateAddress(string code, string addressCode,
            AddressInputViewModel viewModel, CancellationToken cancellationToken)
        {

            var model = await _personService.UpdatePersonAddress(code, addressCode, viewModel, cancellationToken);
            return model;

        }

        [AllowAnonymous]
        [HttpDelete("{person_code}/address/{address_code}")]
        public async Task<ApiResult<string>> DeleteAddress(Guid person_code, Guid address_code, CancellationToken cancellationToken)
        {
            var result = await _personService.DeleteAddressByCode(cancellationToken, person_code, address_code);

            return result.ToString();
        }

        #endregion

        #region Attachment Actions

        [HttpPost("{code}/Attachment")]
        [AllowAnonymous]
        public async Task<ApiResult<PersonAttachmentViewModel>> CreatePersonAttachment([FromRoute] string code,
             [FromForm] PersonAttachmentRequestViewModel viewModel, CancellationToken cancellationToken)
        {
            var result =
                await _personService.CreatePersonAttachment(cancellationToken, viewModel.File, code, viewModel.TypeId);
            return result;
        }

        [HttpGet("{code}/Attachment/{AttachmentCode}")]
        public async Task<ApiResult<Attachment>> Get([FromRoute] Guid code, CancellationToken cancellationToken)
        {
            var fileupload = _configuration.GetValue<string>("AppConfig:FilePath");
            var result = await _attachmentService.GetByCode(code, cancellationToken, fileupload);
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Response;
            //using (WebClient web1 = new WebClient())
            //    web1.DownloadFile(fileupload, result?.Name);
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(result?.Name, fileupload);
            }

            return result;
        }

        #endregion


        #region person mine
        [HttpGet("mine/address")]
        public async Task<ApiResult<List<PersonAddressViewModel>>> GetAddressesMine(CancellationToken cancellationToken)
        {
            try
            {
                long UserID = long.Parse(HttpContext.User?.GetId());
                var model = await _personService.GetPersonAddressesMine(UserID, cancellationToken);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("mine/address")]
        public async Task<ApiResult<AddressViewModel>> CreatePersonAddressMine(AddressViewModel viewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var model = await _personService.CreatePersonAddressMine(UserID, viewModel, cancellationToken);
            return model;
        }

        [HttpPut("mine/address/{addressCode}")]
        public async Task<ApiResult<AddressViewModel>> UpdateAddressMine(string addressCode, AddressInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var model = await _personService.UpdatePersonAddressMine(UserID, addressCode, viewModel, cancellationToken);
            return model;

        }
        [HttpPut("mine")]
        public async Task<ApiResult<MyPersonViewModel>> UpdatePersondataMine(MyPersonUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var person = await _personService.UpdatePersondataMine(userId, viewModel, cancellationToken);
            return person;
        }
        [HttpGet("mine")]
        public async Task<ApiResult<MyPersonViewModel>> GetPersondataMine(CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var person = await _personService.GetPersondataMine(userId, cancellationToken);
            return person;
        }

        [HttpDelete("mine/address/{address_code}")]
        public async Task<ApiResult<string>> DeleteAddressMine(Guid address_code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var result = await _personService.DeleteAddressByCodeMine(cancellationToken, userId, address_code);

            return result.ToString();
        }

        #endregion
    }
}
using System;
using Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.CmnModels
{
    public class ApiResultStatusCodeCheck
    {
        public string StatusCode { get; set; } = "";
        public string Message { get; set; } = "خطا در سامانه";
        public ApiResultStatusCodeCheck apiResultStatusCodeCheckMethod(Common.ApiResultStatusCode _apiResultStatusCode)
        {
            ApiResultStatusCodeCheck _apiResultStatusCodeData = new ApiResultStatusCodeCheck();
            switch (_apiResultStatusCode)
            {
                case Common.ApiResultStatusCode.BadRequest:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.BadRequest.ToDisplay(), StatusCode = Common.ApiResultStatusCode.BadRequest.ToString() };
                    break;
                case Common.ApiResultStatusCode.ListEmpty:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.ListEmpty.ToDisplay(), StatusCode = Common.ApiResultStatusCode.ListEmpty.ToString() };
                    break;
                case Common.ApiResultStatusCode.LogicError:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.LogicError.ToDisplay(), StatusCode = Common.ApiResultStatusCode.LogicError.ToString() };
                    break;
                case Common.ApiResultStatusCode.NotFound:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.NotFound.ToDisplay(), StatusCode = Common.ApiResultStatusCode.NotFound.ToString() };
                    break;
                case Common.ApiResultStatusCode.ServerError:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.NotFound.ToDisplay(), StatusCode = Common.ApiResultStatusCode.NotFound.ToString() };
                    break;
                case Common.ApiResultStatusCode.Success:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.Success.ToDisplay(), StatusCode = Common.ApiResultStatusCode.Success.ToString() };
                    break;
                case Common.ApiResultStatusCode.UnAuthorized:
                    _apiResultStatusCodeData = new ApiResultStatusCodeCheck { Message = Common.ApiResultStatusCode.UnAuthorized.ToDisplay(), StatusCode = Common.ApiResultStatusCode.UnAuthorized.ToString() };
                    break;
            }
            return _apiResultStatusCodeData;
        }
    }
}

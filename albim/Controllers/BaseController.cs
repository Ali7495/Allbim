using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace albim.Controllers
{
    /// <summary>
    /// Base Controller 
    /// </summary>

    [Route("api/v{version:apiversion}/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        #region Protected Members

        //protected object DetailedException(Exception ex)
        //{
        //    var errormessage = ex.Message;
        //    if (ex.InnerException != null)
        //    {
        //        errormessage = "\n\nException: " + GetInnerException(ex);
        //    }
        //    var result = new ResultModel.Result
        //    {
        //        status = new Status
        //        {
        //            code = (int)HttpStatusCode.InternalServerError,
        //            message = errormessage
        //        }
        //    };
        //    return result;
        //}

        /// <summary>
        /// Get Inner Exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return
                    $"{ex.InnerException.Message + "( \n " + ex.Message + " \n )"} > {GetInnerException(ex.InnerException)} ";
            }
            return string.Empty;
        }
        #endregion
    }
}
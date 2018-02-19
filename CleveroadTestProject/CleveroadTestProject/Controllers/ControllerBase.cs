namespace CleveroadTestProject.Web.Controllers
{
    #region namespaces
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using CleveroadTestProject.ViewModel;
    using CleveroadTestProject.Infrastructure.Exceptions;
    using CleveroadTestProject.Infrastructure;
    using CleveroadTestProject.Web.Filters;
    #endregion

    [ApiExceptionFilter]
    public class ControllerBase : Controller
    {
        protected void ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ResponseCode.ValidationError, this.GetValidationSummary());
            }
        }

        protected string GetValidationSummary()
        {
            return string.Join(",", ModelState.Values.Where(e => e.Errors.Count > 0)
                                                     .SelectMany(e => e.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToArray());
        }

        protected ResponseBase Success(string message)
        {
            return new ResponseBase {
                Code = (int)ResponseCode.Success,
                Message = message
            };
        }

        protected ResponseBase Success(string message, object data)
        {
            return new ResponseBase {
                Code = (int)ResponseCode.Success,
                Data = data,
                Message = message
            };
        }
    }
}
namespace CleveroadTestProject.Web.Filters
{
    #region namespaces
    using Microsoft.AspNetCore.Mvc.Filters;
    using CleveroadTestProject.Infrastructure.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using CleveroadTestProject.ViewModel;
    using CleveroadTestProject.Infrastructure;
    #endregion

    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiException = context.Exception as ApiException;

            if (apiException != null)
            {
                var response = new ResponseBase
                {
                    Code = (int)apiException.Code,
                    Message = apiException.Message
                };
                context.Result = new JsonResult(response);
            }
            else
            {
                var response = new ResponseBase
                {
                    Code = (int)ResponseCode.UnhandledError,
                    Message = "Unhandled Error occured"
                };
                context.Result = new JsonResult(response);
            }

            base.OnException(context);
        }
    }
}

using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AuthApi.Filters
{
    public class LoggerAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {

                var returndata = actionExecutedContext.Response.Content.ReadAsStringAsync().Result.ToString();
                /*//keep Json response content
                // Do your own logging!
                if (LogOutboundRequest)
                {
                    ErrLog.Insert(ErrLog.type.OutboundResponse, actionExecutedContext.Response.Headers,
                       actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName
                      + "/"
                      + actionExecutedContext.ActionContext.ActionDescriptor.ActionName
                      , returndata);
                }*/
            }
            catch (Exception e)
            {

            }


        }
    }
}

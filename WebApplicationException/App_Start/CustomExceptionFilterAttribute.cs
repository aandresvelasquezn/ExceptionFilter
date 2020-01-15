using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApplicationException.App_Start
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                // we don't handle the http response exception just throw that
                if (exception is HttpResponseException)
                    return;


                if (exception is NotImplementedException)
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                else if (exception is ArgumentException)
                {
                    var message = exception.Message.Split(new[] { "\r\n" }, StringSplitOptions.None);
                    ExceptionHandling(HttpStatusCode.BadRequest, message[0]);
                }
                else if (exception is ValidationException)
                    ExceptionHandling(HttpStatusCode.BadRequest, exception.Message);
                else if (exception is ApplicationException)
                    ExceptionHandling(HttpStatusCode.InternalServerError, exception.Message);
                else
                {
                    ExceptionHandling(HttpStatusCode.InternalServerError, @"Internal Server Error");
                }
            }
            base.OnException(actionExecutedContext);
        }

        public void ExceptionHandling(HttpStatusCode statusCode, string message)
        {

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                message = "Internal Server Error. Unable to call downstream system.";
            }
            var errorResponse = new GenericErrorResponse
            {
                Message = message,
                StatusCode = ((int)statusCode).ToString()

            };
            var resp = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent<GenericErrorResponse>(errorResponse, new JsonMediaTypeFormatter(), "application/json")
            };

            throw new HttpResponseException(resp);
        }
    }

    public class GenericErrorResponse
    {
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}
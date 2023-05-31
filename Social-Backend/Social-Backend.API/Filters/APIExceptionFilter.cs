using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Application.Common.Models.CustomResponse;
using System.ComponentModel.DataAnnotations;

namespace Social_Backend.API.Filters
{
    public class APIExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public APIExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationModelException), HandleValidationModelException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);
            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            if (!context.ModelState.IsValid)
            {
                HandleValidationModelException(context);
                return;
            }
            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var response = CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status500InternalServerError, context.Exception.Message);
            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var response = CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status401Unauthorized, context.Exception.Message);
            context.Result = new UnauthorizedObjectResult(response);
            context.ExceptionHandled = true;
        }

        private void HandleValidationModelException(ExceptionContext context)
        {
            var response = CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, context.Exception.Message);
            context.Result = new BadRequestObjectResult(response);
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var response = CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status404NotFound, context.Exception.Message);
            context.Result = new NotFoundObjectResult(response);
            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var response = CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status403Forbidden, context.Exception.Message);
            context.Result = new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status403Forbidden,
            };
            context.ExceptionHandled = true;
        }
    }
}
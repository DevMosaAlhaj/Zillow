using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zillow.Core.Exceptions;
using Zillow.Core.ViewModel;

namespace Zillow.Core.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _delegate;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _delegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _delegate(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                ApiResponseViewModel responseViewModel;

                switch (ex)
                {
                    case EntityNotFoundException error:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        responseViewModel = new ApiResponseViewModel(false, error.Message);
                        break;

                    case EmptyFileException error:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        responseViewModel = new ApiResponseViewModel(false, error.Message);
                        break;

                    case LoginFailureException error:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        responseViewModel = new ApiResponseViewModel(false, error.Message);
                        break;

                    case UserRegistrationException error:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        responseViewModel = new ApiResponseViewModel(false, error.Message);
                        break;

                    case UpdateEntityException error:
                        response.StatusCode = (int) HttpStatusCode.Conflict;
                        responseViewModel = new ApiResponseViewModel(false, error.Message);
                        break;

                    default:

                        // Unhandled Exception

                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        responseViewModel = new ApiResponseViewModel(false, ex.Message);
                        break;
                }

                await response.WriteAsync(JsonSerializer.Serialize(responseViewModel));
            }
        }
    }
}
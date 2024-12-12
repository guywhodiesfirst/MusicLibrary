using Business.Exceptions;
using System.Net;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new MusicLibraryException(nameof(next));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new MusicLibraryException(nameof(context));
            }

            try
            {
                await _next.Invoke(context);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (MusicLibraryException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context == null)
            {
                throw new MusicLibraryException(nameof(context));
            }

            var code = GetStatusCode(exception);

            context.Response.ContentType = "application/text";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(exception.Message);
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
        {
            return exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentNullException or ArgumentException or MusicLibraryException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}

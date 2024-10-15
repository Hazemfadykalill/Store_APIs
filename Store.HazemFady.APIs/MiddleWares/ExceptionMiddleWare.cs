using Store.HazemFady.APIs.Errors;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Store.HazemFady.APIs.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleWare(RequestDelegate next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception Ex)
            {
                logger.LogError(Ex,Ex.Message);
                context.Response.ContentType = "Application/json";
                context.Response.StatusCode =StatusCodes.Status500InternalServerError;
                var res =env.IsDevelopment()?new APIExceptionErrorResponse(StatusCodes.Status500InternalServerError , Ex.Message , Ex.StackTrace!.ToString()): new APIExceptionErrorResponse(StatusCodes.Status500InternalServerError);
                var Options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var response = JsonSerializer.Serialize(res, Options);
                await context.Response.WriteAsync(response);
             
            }
        }
    }
}

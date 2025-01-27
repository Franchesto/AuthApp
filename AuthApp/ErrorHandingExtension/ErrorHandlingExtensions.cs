using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace AuthApp.ErrorHandingExtension
{
    public static class ErrorHandlingExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.Run(async context => 
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionDetails?.Error;

                logger.LogError(exception,
                    "Could not process a request on machine {Machine}. TranceId{TranceId}",
                    Environment.MachineName,
                    Activity.Current?.Id);

                await Results.Problem(
                    title: "Working on the problem",
                    statusCode: StatusCodes.Status500InternalServerError,
                    extensions: new Dictionary<string, object?>
                    {
                        {"TraceId", Activity.Current?.Id},
                    })
                .ExecuteAsync(context);

            });
        }
    }
}

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Infrastructure
{
    public class HeaderTelemetryInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HeaderTelemetryInitializer(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (!(telemetry is RequestTelemetry))
                return;

            // TODO: Huh???????????
            var context = contextAccessor.HttpContext;
        }
    }
}
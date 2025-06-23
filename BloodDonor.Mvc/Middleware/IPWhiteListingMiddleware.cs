using BloodDonor.Mvc.Configuration;
using Microsoft.Extensions.Options;

namespace BloodDonor.Mvc.Middleware
{
    public class IPWhiteListingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _allowedIPs;
        private EmailSettings _mailSettings;
        public IPWhiteListingMiddleware(RequestDelegate next, 
            IConfiguration configuration,
            IOptions<EmailSettings> options, 
            IOptionsMonitor<EmailSettings> monitor)
        {
            _next = next;
            _allowedIPs = configuration.GetSection("AllowedIPs").Get<List<string>>() ?? new List<string>();
            var mailSettings = options.Value;

            monitor.OnChange(settings =>
            {
                Console.WriteLine("Mail settings changed, updating middleware.");
                _mailSettings = settings;
            });
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress?.ToString();
            if (remoteIp == null || !_allowedIPs.Contains(remoteIp))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Your IP is not allowed.");
                return;
            }
            await _next(context);
        }
    }
}

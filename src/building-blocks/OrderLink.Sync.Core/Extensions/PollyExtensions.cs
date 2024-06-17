using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace OrderLink.Sync.Core.Extensions
{
    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> WaitAndRetry()
        {
            var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => (int)msg.StatusCode >= 408 && (int)msg.StatusCode <= 500 && (int)msg.StatusCode != 404)
                    .WaitAndRetryAsync(new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15),
                    TimeSpan.FromSeconds(20),
                    }, (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Trying for the {retryCount} time.");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

            return retry;
        }
    }
}

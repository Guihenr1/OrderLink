using EasyNetQ;
using EasyNetQ.Interception;
using Serilog;

namespace OrderLink.Sync.MessageBus;

public class LoggingInterceptor : IProduceConsumeInterceptor
{
    public LoggingInterceptor()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341", batchPostingLimit: int.MaxValue, eventBodyLimitBytes: int.MaxValue, queueSizeLimit: int.MaxValue)
            .CreateLogger();
    }

    public ConsumedMessage OnConsume(in ConsumedMessage message)
    {
        return message;
    }

    public ProducedMessage OnProduce(in ProducedMessage message)
    {
        return message;
    }
}

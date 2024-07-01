using OrderLink.Sync.Core.Messages.Integration.Events;

namespace OrderLink.Sync.Kitchen.Application.Interfaces.Services
{
    public interface IInvokeService
    {
        Task DoneOrderAsync(DoneOrderEvent doneOrderEvent);
    }
}

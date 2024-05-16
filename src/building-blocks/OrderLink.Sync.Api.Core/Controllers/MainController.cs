using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderLink.Sync.Core.Helpers;
using OrderLink.Sync.Core.Notifications;

namespace OrderLink.Sync.Api.Core.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotificator _notificator;

    protected MainController(INotificator notificator)
    {
        _notificator = notificator;
    }

    protected bool ValidOperation()
    {
        return !_notificator.HasNotification();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notificator.GetNotifications().Select(n => n.Mensagem)
        });
    }

    protected ActionResult CustomResponse<TResult>(Result<TResult> result)
    {
        ActionResult actionResult;

        if (ValidOperation() && result.IsSuccess)
        {
            result.IsSuccess = true;
            actionResult = result.StatusCode switch
            {
                HttpStatusCode.Created => Created($"{Request.Path.Value}{result.Data}", result),
                HttpStatusCode.NoContent => NoContent(),
                _ => Ok(result)
            };
        } else
        {
            result.IsSuccess = false;
            actionResult = result.StatusCode switch
            {
                HttpStatusCode.NotFound => NotFound(result),
                HttpStatusCode.Unauthorized => Unauthorized(result),
                _ => BadRequest(result)
            };
        }

        return actionResult;
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid) NotifyErrorInvalidModel(modelState);
        return CustomResponse();
    }

    protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(errorMsg);
        }
    }
    protected void NotifyError(string mensagem)
    {
        _notificator.Handle(new Notification(mensagem));
    }

}

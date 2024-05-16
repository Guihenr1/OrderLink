using System.Net;

namespace OrderLink.Sync.Core.Helpers;
public class Result<T>
{
    public Result() { }

    public Result(T data)
    {
        this.Data = data;
    }

    public T Data { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public bool IsSuccess { get; set; } = true;
}

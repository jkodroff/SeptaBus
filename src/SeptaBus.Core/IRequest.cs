namespace SeptaBus
{
    /// <summary>
    /// Marker interface for requests.
    /// </summary>
    /// <typeparam name="TResp">The type of response this request should return.</typeparam>
    public interface IRequest<TResp> : IMessage where TResp : IResponse { }
}
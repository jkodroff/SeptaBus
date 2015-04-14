namespace SeptaBus
{
    public abstract class RequestBase<TResp> : MessageBase, IRequest<TResp> where TResp : IResponse { }
}
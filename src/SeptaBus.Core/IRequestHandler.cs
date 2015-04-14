namespace SeptaBus
{
    public interface IRequestHandler<in TReq, out TResp> 
        where TReq : IRequest<TResp>
        where TResp : IResponse
    {
        TResp Handle(TReq message);
    }
}
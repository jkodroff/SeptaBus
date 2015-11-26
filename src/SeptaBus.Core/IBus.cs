namespace SeptaBus
{
    public interface IBus
    {
        void Send(params ICommand[] commands);
        void Publish(params IEvent[] events);
        TResp Send<TResp>(IRequest<TResp> request) where TResp : IResponse;
    }
}
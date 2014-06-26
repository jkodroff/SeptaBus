namespace SeptaBus
{
    public interface IHasHeaders
    {
        object GetHeader(string key);
        void SetHeader(string key, object value);
    }
}
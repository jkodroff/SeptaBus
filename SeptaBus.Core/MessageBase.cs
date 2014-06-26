using System.Collections.Generic;

namespace SeptaBus
{
    /// <summary>
    /// A message class which adds a dictionary of message headers.
    /// </summary>
    public abstract class MessageBase : IMessage, IHasHeaders
    {
        private IDictionary<string, object> _headers;

        protected MessageBase()
        {
            _headers = new Dictionary<string, object>();
        }

        public object GetHeader(string key)
        {
            return (_headers.ContainsKey(key)) ? _headers[key] : null;
        }

        public IEnumerable<string> GetHeaderKeys()
        {
            return _headers.Keys;
        }

        public void SetHeader(string key, object value)
        {
            if (_headers.ContainsKey(key))
                _headers[key] = value;
            else
                _headers.Add(key, value);
        }
    }
}
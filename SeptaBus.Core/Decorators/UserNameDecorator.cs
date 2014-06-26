namespace SeptaBus.Decorators
{
    public class UserNameDecorator : IMessageDecorator
    {
        private IUserNameProvider _userNameProvider;

        public UserNameDecorator(IUserNameProvider userNameProvider)
        {
            _userNameProvider = userNameProvider;
        }

        public void Decorate(IMessage message)
        {
            var headers = message as IHasHeaders;
            if (headers == null)
                return;

            headers.By(_userNameProvider.UserName());
        }
    }
}
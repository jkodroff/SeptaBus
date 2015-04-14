namespace SeptaBus.Decorators
{
    public class RolesDecorator : IMessageDecorator
    {
        private IRolesProvider _rolesProvider;

        public RolesDecorator(IRolesProvider rolesProvider)
        {
            _rolesProvider = rolesProvider;
        }

        public void Decorate(IMessage message)
        {
            var headers = message as IHasHeaders;
            if (headers == null)
                return;

            headers.Roles(_rolesProvider.CurrentUsersRoles());
        }
    }
}
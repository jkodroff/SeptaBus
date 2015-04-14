namespace SeptaBus.Decorators
{
    /// <summary>
    /// So we can provide usernames via methods other than HttpContext.User.Identity.Name.
    /// </summary>
    public interface IUserNameProvider
    {
        string UserName();
    }
}
using System.Collections.Generic;

namespace SeptaBus.Decorators
{
    /// <summary>
    /// So we're not stcitly bound to Roles.GetRolesForUser()
    /// </summary>
    public interface IRolesProvider
    {
        IEnumerable<string> CurrentUsersRoles();
    }
}
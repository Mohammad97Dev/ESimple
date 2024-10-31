using Abp.Authorization;
using ESimple.Authorization.Roles;
using ESimple.Authorization.Users;

namespace ESimple.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

using Abp.Authorization;
using NhapMonCNPM.Authorization.Roles;
using NhapMonCNPM.Authorization.Users;

namespace NhapMonCNPM.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

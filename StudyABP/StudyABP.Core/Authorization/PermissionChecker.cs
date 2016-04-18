using Abp.Authorization;
using StudyABP.Authorization.Roles;
using StudyABP.MultiTenancy;
using StudyABP.Users;

namespace StudyABP.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}

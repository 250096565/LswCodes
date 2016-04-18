using Abp.Authorization.Roles;
using StudyABP.MultiTenancy;
using StudyABP.Users;

namespace StudyABP.Authorization.Roles
{
    public class Role : AbpRole<Tenant, User>
    {

    }
}
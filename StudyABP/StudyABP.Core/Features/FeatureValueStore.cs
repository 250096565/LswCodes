using Abp.Application.Features;
using StudyABP.Authorization.Roles;
using StudyABP.MultiTenancy;
using StudyABP.Users;

namespace StudyABP.Features
{
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(TenantManager tenantManager)
            : base(tenantManager)
        {
        }
    }
}
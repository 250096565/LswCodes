using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using StudyABP.Authorization.Roles;
using StudyABP.Editions;
using StudyABP.Users;

namespace StudyABP.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager
            )
        {
        }
    }
}
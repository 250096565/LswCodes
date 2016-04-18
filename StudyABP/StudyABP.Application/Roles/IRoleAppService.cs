using System.Threading.Tasks;
using Abp.Application.Services;
using StudyABP.Roles.Dto;

namespace StudyABP.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}

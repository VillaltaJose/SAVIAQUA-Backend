using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Roles;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface IRolService
{
    Task<Result<IEnumerable<RolResponse>>> ObtenerRoles();
}
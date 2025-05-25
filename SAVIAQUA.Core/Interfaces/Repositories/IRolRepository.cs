using SAVIAQUA.Core.DTOs.Roles;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IRolRepository
{
    Task<IEnumerable<RolResponse>> ObtenerRoles();
}
using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Roles;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class RolService : IRolService
{
    private readonly IRolRepository _rolRepository;

    public RolService(IRolRepository rolRepository)
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<IEnumerable<RolResponse>>> ObtenerRoles()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var roles = await _rolRepository.ObtenerRoles();
        
        scope.Complete();

        return Result<IEnumerable<RolResponse>>.Ok(roles);
    }
}
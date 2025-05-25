using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Lugares;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class LugarService : ILugarService
{
    private readonly ILugarRepository _lugarRepository;

    public LugarService(ILugarRepository lugarRepository)
    {
        _lugarRepository = lugarRepository;
    }

    public async Task<Result<IEnumerable<ProvinciaResponse>>> ObtenerProvincias()
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var data = await _lugarRepository.ObtenerProvincias();
        
        scope.Complete();
        return Result<IEnumerable<ProvinciaResponse>>.Ok(data);
    }
    
    public async Task<Result<IEnumerable<CiudadResponse>>> ObtenerCiudades(int codigoProvincia)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var data = await _lugarRepository.ObtenerCiudades(codigoProvincia);
        
        scope.Complete();
        return Result<IEnumerable<CiudadResponse>>.Ok(data);
    }
    
    public async Task<Result<IEnumerable<ParroquiaResponse>>> ObtenerParroquias(int codigoProvincia, int codigoCiudad)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var data = await _lugarRepository.ObtenerParroquias(codigoProvincia, codigoCiudad);
        
        scope.Complete();
        return Result<IEnumerable<ParroquiaResponse>>.Ok(data);
    }
}
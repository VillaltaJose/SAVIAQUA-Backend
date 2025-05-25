using SAVIAQUA.Core.CustomEntities;
using SAVIAQUA.Core.DTOs.Lugares;

namespace SAVIAQUA.Core.Interfaces.Services;

public interface ILugarService
{
    Task<Result<IEnumerable<ProvinciaResponse>>> ObtenerProvincias();

    Task<Result<IEnumerable<CiudadResponse>>> ObtenerCiudades(int codigoProvincia);

    Task<Result<IEnumerable<ParroquiaResponse>>> ObtenerParroquias(int codigoProvincia, int codigoCiudad);
}
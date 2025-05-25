using SAVIAQUA.Core.DTOs.Lugares;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface ILugarRepository
{
    Task<IEnumerable<ProvinciaResponse>> ObtenerProvincias();

    Task<IEnumerable<CiudadResponse>> ObtenerCiudades(int codigoProvincia);

    Task<IEnumerable<ParroquiaResponse>> ObtenerParroquias(int codigoProvincia, int codigoCiudad);
}
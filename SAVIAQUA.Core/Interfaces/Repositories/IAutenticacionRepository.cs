using SAVIAQUA.Core.Entities;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IAutenticacionRepository
{
    Task<Usuario?> ObtenerUsuarioPorCorreo(string correo);
}
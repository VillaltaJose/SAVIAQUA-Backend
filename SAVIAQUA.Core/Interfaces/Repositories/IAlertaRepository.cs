using SAVIAQUA.Core.DTOs.Notificaciones;
using SAVIAQUA.Core.Entities;

namespace SAVIAQUA.Core.Interfaces.Repositories;

public interface IAlertaRepository
{
    Task InsertarNotificacion(Notificacion notificacion);
    Task<CategoriaNotificacionResponse?> ObtenerDatosCategoria(int codigoCategoria);

    Task<Dictionary<string, object>?> ObtenerParametros(string sql, int? referenciaInt = null,
        string? referenciaStr = null);

    Task<List<int>> ObtenerDestinatarios(string sql, int? referenciaInt = null, string? referenciaStr = null);
}
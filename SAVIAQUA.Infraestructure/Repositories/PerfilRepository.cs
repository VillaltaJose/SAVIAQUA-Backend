using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Perfil;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class PerfilRepository : IPerfilRepository
{
    private readonly IDbConnection _dbConnection;

    public PerfilRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<PerfilResponse> ObtenerPerfil(int codigoUsuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var perfil = await _dbConnection.QueryFirstAsync<PerfilResponse>(PerfilQueries.ObtenerPerfil, new
        {
            codigoUsuario
        });
        
        scope.Complete();
        return perfil;
    }

    public async Task<bool> ActualizarPerfil(Usuario usuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var filas = await _dbConnection.ExecuteAsync(PerfilQueries.ActualizarPerfil, usuario);
        
        scope.Complete();
        return filas > 0;
    }

    public async Task<string?> ObtenerHashClave(int codigoUsuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var clave = await _dbConnection.QueryFirstOrDefaultAsync<string?>(PerfilQueries.ObtenerHashClave, new
        {
            codigoUsuario
        });
        
        scope.Complete();
        return clave;
    }
    
    public async Task<bool> ActualizarClave(int codigoUsuario, string passwordHash)
    {
        using var scope = TransactionScopeHelper.StartTransaction();
        
        var rows = await _dbConnection.ExecuteAsync(PerfilQueries.ActualizarClave, new
        {
            codigoUsuario,
            passwordHash,
        });
        
        scope.Complete();
        return rows > 0;
    }
}
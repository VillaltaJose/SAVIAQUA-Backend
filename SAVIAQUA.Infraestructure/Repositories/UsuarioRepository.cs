using System.Data;
using Dapper;
using SAVIAQUA.Core.DTOs.Usuarios;
using SAVIAQUA.Core.Entities;
using SAVIAQUA.Core.Filters.Usuarios;
using SAVIAQUA.Core.Helpers;
using SAVIAQUA.Core.Interfaces.Repositories;
using SAVIAQUA.Infraestructure.Queries;

namespace SAVIAQUA.Infraestructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnection _dbConnection;

    public UsuarioRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> RegistrarNuevoUsuario(Usuario usuario)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var codigo = await _dbConnection.ExecuteScalarAsync<int>(UsuariosQueries.CrearUsuario, usuario);
        
        scope.Complete();
        return codigo;
    }

    public async Task<IEnumerable<UsuarioResponse>> ObtenerUsuarios(ObtenerUsuariosFilter filter)
    {
        using var scope = TransactionScopeHelper.StartTransaction();

        var builder = new SqlBuilder();
        
        if (filter.CodigoJunta is not null)
        {
            builder.Where("u.codigo_junta = :codigoJunta");
        }

        var sql = builder.AddTemplate(UsuariosQueries.ObtenerUsuarios);

        var usuarios = await _dbConnection.QueryAsync<UsuarioResponse>(sql.RawSql, filter);

        scope.Complete();
        return usuarios;
    }
}
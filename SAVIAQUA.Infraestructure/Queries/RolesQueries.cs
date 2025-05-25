namespace SAVIAQUA.Infraestructure.Queries;

public static class RolesQueries
{
    public const string ObtenerRoles = @"select
            r.codigo,
            r.nombre,
            r.fecha_creacion as FechaCreacion,
            r.activo
            from roles r
            order by r.fecha_creacion asc";
}
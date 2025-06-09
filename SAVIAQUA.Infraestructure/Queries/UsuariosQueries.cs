namespace SAVIAQUA.Infraestructure.Queries;

public static class UsuariosQueries
{
    public const string ObtenerUsuarios = @"select
                u.codigo,
                u.nombres,
                u.apellidos,
                u.correo,
                r.nombre as rol,
                j.nombre as junta,
                u.fecha_creacion as fechaCreacion
                FROM usuarios u
                inner join roles r
                on r.codigo = u.codigo_rol
                inner join juntas j
                on j.codigo = u.codigo_junta
                /**where**/
                order by nombres, apellidos asc
                limit 100";
}
namespace SAVIAQUA.Infraestructure.Queries;

public static class AutenticacionQueries
{
    public const string ObtenerUsuarioPorCorreo = @"select
                u.codigo,
                u.nombres,
                u.apellidos,
                u.clave as HashClave,
                u.codigo_junta as CodigoJunta,
                u.codigo_rol as CodigoRol,
                u.correo,
                u.fecha_cambio_clave as FechaCambioClave,
                u.fecha_creacion as FechaCreacion,
                u.fecha_edicion as FechaEdicion
                from usuarios u
                where u.correo = :correo";
}
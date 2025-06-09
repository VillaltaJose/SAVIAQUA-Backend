namespace SAVIAQUA.Infraestructure.Queries;

public static class PerfilQueries
{
    public const string ObtenerPerfil = @"select
            u.nombres,
            u.apellidos,
            u.correo,
            u.codigo_rol as codigoRol,
            r.nombre as rol,
            u.codigo_junta as codigoJunta,
            j.nombre as junta
            from usuarios u
            inner join roles r 
            on r.codigo = u.codigo_rol
            inner join juntas j
            on j.codigo = u.codigo_junta
            where
            u.codigo = :codigoUsuario";

    public const string ActualizarPerfil = @"update usuarios set
            nombres = :nombres,
            apellidos = :apellidos,
            correo = :correo,
            fecha_edicion = :fechaEdicion
            where
            codigo = :codigo";
}
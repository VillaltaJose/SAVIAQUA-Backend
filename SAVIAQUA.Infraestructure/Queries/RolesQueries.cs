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

    public const string ObtenerPermisosRol = @"select
            rp.codigo_permiso
            from roles_permisos rp 
            where
            rp.codigo_rol = :codigoRol";

    public const string ObtenerPermisos = @"select
                p.codigo,
                p.nombre,
                p.descripcion,
                p.codigo_categoria as CodigoCategoria,
                cp.nombre as Categoria
                from permisos p
                inner join categorias_permisos cp
                on cp.codigo = p.codigo_categoria
                order by
                cp.orden, cp.nombre, p.orden, p.nombre asc";
}
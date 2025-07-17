namespace SAVIAQUA.Infraestructure.Queries;

public static class RolesQueries
{
    public const string ObtenerRoles = @"select
            r.codigo,
            r.nombre,
            r.descripcion,
            r.fecha_creacion as FechaCreacion,
            r.activo
            from roles r
            order by r.fecha_creacion asc";
    
    public const string ObtenerRol = @"select
            r.codigo,
            r.nombre,
            r.descripcion,
            r.fecha_creacion as FechaCreacion,
            r.fecha_edicion as FechaEdicion,
            r.activo
            from roles r
            where codigo = :codigoRol";

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

    public const string EliminarPermisosRol = """
                                              delete from roles_permisos rp
                                              where rp.codigo_rol = :codigoRol
                                              """;

    public const string RegistrarPermiso = """
                                           insert into roles_permisos
                                           (codigo_rol, codigo_permiso)
                                           values
                                           (:codigoRol, :codigoPermiso)
                                           """;

    public const string ActualizarRol = """
                                        update roles
                                        set nombre = :nombre,
                                        descripcion = :descripcion,
                                        activo = :activo,
                                        fecha_edicion = now()
                                        where
                                        codigo = :codigo
                                        """;
    
    public const string CrearRol = """
                                        insert into roles
                                        (nombre, descripcion, fecha_creacion, fecha_edicion, activo)
                                        values
                                        (:nombre, :descripcion, now(), now(), true)
                                        returning codigo
                                        """;
}
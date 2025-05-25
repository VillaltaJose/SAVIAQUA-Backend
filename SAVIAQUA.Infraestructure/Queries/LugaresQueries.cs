namespace SAVIAQUA.Infraestructure.Queries;

public static class LugaresQueries
{
    public const string ObtenerProvincias = @"select
            p.codigo,
            p.nombre
            from provincias p
            order by p.nombre asc";

    public const string ObtenerCiudades = @"select
            c.codigo,
            c.nombre
            from ciudades c
            where c.codigo_provincia = :codigoProvincia
            order by c.nombre asc";

    public const string ObtenerParroquias = @"select
                p.codigo,
                p.nombre
                from parroquias p
                where
                p.codigo_provincia = :codigoProvincia and
                p.codigo_ciudad = :codigoCiudad
                order by p.nombre asc";
}
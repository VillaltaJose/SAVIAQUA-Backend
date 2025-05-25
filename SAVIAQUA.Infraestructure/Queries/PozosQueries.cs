namespace SAVIAQUA.Infraestructure.Queries;

public static class PozosQueries
{
    public const string ObtenerPozos = @"select
                p.codigo,
                p.nombre,
                j.nombre as Junta,
                p.codigo_junta as CodigoJunta,
                p.latitude,
                p.longitude,
                pp.nombre as Provincia,
                c.nombre as Ciudad,
                p3.nombre as Parroquia,
                p.fecha_creacion as FechaCreacion
                from pozos p
                inner join juntas j
                on j.codigo = p.codigo_junta
                inner join provincias pp
                on pp.codigo = p.codigo_provincia
                inner join ciudades c
                on c.codigo = p.codigo_ciudad
                inner join parroquias p3
                on p3.codigo = p.codigo_parroquia
                order by p.nombre, j.nombre, p.fecha_creacion";

    public const string ObtenerTotalPozos = @"select
                count(*)
                from pozos p
                inner join juntas j
                on j.codigo = p.codigo_junta
                inner join provincias pp
                on pp.codigo = p.codigo_provincia
                inner join ciudades c
                on c.codigo = p.codigo_ciudad
                inner join parroquias p3
                on p3.codigo = p.codigo_parroquia";
}
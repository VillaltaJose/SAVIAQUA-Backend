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
                p.fecha_creacion as FechaCreacion
                from pozos p
                inner join juntas j
                on j.codigo = p.codigo_junta
                order by p.nombre, j.nombre, p.fecha_creacion";

    public const string ObtenerTotalPozos = @"select
                count(*)
                from pozos p
                inner join juntas j
                on j.codigo = p.codigo_junta";
}
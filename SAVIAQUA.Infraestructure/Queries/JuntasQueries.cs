namespace SAVIAQUA.Infraestructure.Queries;

public static class JuntasQueries
{
    public const string ObtenerJuntasMin = @"select
            j.codigo,
            j.nombre,
            j.url_logo as UrlLogo
            from juntas j
            order by j.nombre, j.fecha_creacion asc";
}
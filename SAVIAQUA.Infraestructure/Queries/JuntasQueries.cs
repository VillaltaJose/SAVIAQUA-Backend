namespace SAVIAQUA.Infraestructure.Queries;

public static class JuntasQueries
{
    public const string ObtenerJuntasMin = @"select
            j.codigo,
            j.nombre,
            j.url_logo as UrlLogo
            from juntas j
            order by j.nombre, j.fecha_creacion asc";

    public const string ObtenerJuntas = @"select
            j.codigo,
            j.nombre,
            j.url_logo as urlLogo,
            j.latitude,
            j.longitude,
            j.fecha_creacion as fechaCreacion,
            p.nombre as provincia,
            c.nombre as ciudad,
            p2.nombre as parroquia
            from juntas j
            inner join provincias p
            on p.codigo = j.codigo_provincia
            inner join ciudades c
            on c.codigo = j.codigo_ciudad
            inner join parroquias p2
            on p2.codigo = j.codigo_parroquia
            /**where**/
            order by j.nombre, j.fecha_creacion
            limit :pageSize offset :offset";
    
    public const string ObtenerTotalJuntas = @"select
                count(*)
                from juntas j
                /**where**/";
    
    public const string CrearNuevaJunta = @"insert into juntas (
					nombre,
					latitude,
					longitude,
					observaciones,
					codigo_provincia,
					codigo_ciudad,
					codigo_parroquia,
					fecha_edicion,
					fecha_creacion
				) values (
					:nombre,
					:latitude,
					:longitude,
					:observaciones,
					:codigoProvincia,
					:codigoCiudad,
					:codigoParroquia,
					now(),
					now()
				) returning codigo";
}
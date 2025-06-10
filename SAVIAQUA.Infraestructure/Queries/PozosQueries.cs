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
                /**where**/
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
                on p3.codigo = p.codigo_parroquia
                /**where**/";

    public const string CrearNuevoPozo = @"insert into pozos (
					codigo_junta,
					nombre,
					latitude,
					longitude,
					observaciones,
					codigo_provincia,
					codigo_ciudad,
					codigo_parroquia,
					codigo_usuario_registra,
					fecha_edicion,
					fecha_creacion
				) values (
					:codigoJunta,
					:nombre,
					:latitude,
					:longitude,
					:observaciones,
					:codigoProvincia,
					:codigoCiudad,
					:codigoParroquia,
					:codigoUsuarioRegistra,
					now(),
					now()
				) returning codigo";

    public const string ObtenerMediciones = @"select
			mp.fecha_registro as FechaRegistro,
			mp.m1,
			mp.m2,
			mp.m3
			from mediciones_pozos mp 
			where
			mp.codigo_pozo = :codigoPozo
			and date(mp.fecha_registro) between date(:fechaInicio) and date(:fechaFin)
			order by mp.fecha_registro desc";

    public const string ObtenerPozo = @"select
			p.codigo,
			p.nombre,
			p.observaciones,
			p.fecha_creacion,
			p.latitude,
			p.longitude,
			p.codigo_junta  as codigoJunta,
			j.nombre as junta,
			p.codigo_provincia as codigoProvincia,
			p2.nombre as provincia,
			p.codigo_ciudad as codigoCiudad,
			c.nombre as ciudad,
			p.codigo_parroquia as codigoParroquia,
			p3.nombre as parroquia
			from pozos p 
			inner join provincias p2 
			on p2.codigo = p.codigo_provincia
			inner join ciudades c 
			on c.codigo = p.codigo_ciudad
			inner join parroquias p3 
			on p3.codigo = p.codigo_parroquia
			inner join juntas j
			on j.codigo = p.codigo_junta
			where
			p.codigo = :codigoPozo";
}
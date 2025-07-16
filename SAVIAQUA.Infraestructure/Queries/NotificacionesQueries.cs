namespace SAVIAQUA.Infraestructure.Queries;

public static class NotificacionesQueries
{
    public const string CountNotificaciones = """
                                              select
                                              count(*)
                                              from notificaciones n
                                              where codigo_usuario = :codigoUsuario
                                              """;
    
    public const string ObtenerNotificaciones = """
                                                select
                                                n.codigo as Codigo,
                                                n.codigo_categoria as CodigoCategoria,
                                                n.fecha as Fecha,
                                                n.referencia_int as ReferenciaInt, 
                                                n.referencia_str as ReferenciaStr,
                                                n.texto,
                                                n.leida,
                                                cn.nombre as Categoria
                                                from notificaciones n
                                                inner join categorias_notificaciones cn 
                                                on cn.codigo = n.codigo_categoria
                                                where codigo_usuario = :codigoUsuario
                                                order by n.fecha desc
                                                limit :limit offset :offset
                                                """;

    public const string TotalNoLeidas = """
                                        select
                                        count(*)
                                        from notificaciones n
                                        where codigo_usuario = :codigoUsuario
                                        and n.leida = false
                                        """;

    public const string MarcarLeida = """
                                      update notificaciones
                                      set leida = true
                                      where codigo_usuario = :codigoUsuario
                                      and codigo = :codigoNotificacion
                                      """;

    public const string InsertarNotificacion = """
                                               insert into notificaciones (
                                               	codigo_usuario,
                                               	codigo_categoria,
                                               	fecha,
                                               	referencia_int,
                                               	referencia_str,
                                               	leida,
                                               	texto
                                               ) values (
                                               	:codigoUsuario,
                                               	:codigoCategoria,
                                               	now(),
                                               	:referenciaInt,
                                               	:referenciaStr,
                                               	false,
                                               	:texto
                                               )
                                               """;

    public const string ObtenerCategoria = """
                                           select
                                           cn.plantilla as Plantilla,
                                           cn.sql_parametros as SqlParametros,
                                           cn.sql_destinatarios as SqlDestinatarios
                                           from categorias_notificaciones cn 
                                           where cn.codigo = :codigoCategoria
                                           """;
}
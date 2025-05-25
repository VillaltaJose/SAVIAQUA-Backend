namespace SAVIAQUA.Core.DTOs.Autenticacion;

public class SessionResponse
{
    public PerfilUsuarioResponse PerfilUsuario { get; set; }
    public string JWT { get; set; }
    public string RefreshToken { get; set; }
}
namespace SAVIAQUA.Core.Interfaces.Services;

public interface IPasswordService
{
    string Hash(string password);
    
    bool Check(string hash, string password);

    [Obsolete("Obsolete")]
    string GeneratePassword(int length);
}
using System.Security.Cryptography;
using System.Text;
using SAVIAQUA.Core.Interfaces.Services;

namespace SAVIAQUA.Application.Services;

public class PasswordService : IPasswordService
{
    public string Hash(string password)
    {
        var bytes = SHA512.HashData(Encoding.UTF8.GetBytes(password));

        var stringBuilder = new StringBuilder();
        
        foreach (var t in bytes)
        {
            stringBuilder.Append(t.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public bool Check(string hash, string password)
    {
        var bytes = SHA512.HashData(Encoding.UTF8.GetBytes(password));
   
        var stringBuilder = new StringBuilder();
        
        foreach (var t in bytes)
        {
            stringBuilder.Append(t.ToString("x2"));
        }

        return stringBuilder.Equals(hash);
    }

    [Obsolete("Obsolete")]
    public string GeneratePassword(int length)
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        var password = new StringBuilder();
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] byteBuffer = new byte[sizeof(uint)];

            while (password.Length < length)
            {
                rng.GetBytes(byteBuffer);
                var randomValue = BitConverter.ToUInt32(byteBuffer, 0);
                var index = randomValue % validChars.Length;
                password.Append(validChars[(int)index]);
            }
        }

        return password.ToString();
    }
}
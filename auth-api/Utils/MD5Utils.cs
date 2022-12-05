using System.Security.Cryptography;
using System.Text;

namespace AuthApi.Utils;

public class MD5Utils
{
    public static string GenerateHashMD5(string text)
    {
        var md5Hash = MD5.Create();
        var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
        var stringBuilder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
        {
            stringBuilder.Append(bytes[i]);
        }

        return stringBuilder.ToString();
    }
}

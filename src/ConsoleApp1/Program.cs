// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using CryptoNet;
using System.Diagnostics;
using System.Text;

Console.WriteLine("Hello, World!");

ICryptoNet GetCryptoNet(string user)
{
    var secret = user.Length > 16 ? user.Substring(0, 16) : user.PadRight(16, '0');
    var key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
    var iv = Encoding.UTF8.GetBytes(secret);
    return new CryptoNetAes(key, iv);
}
string Encrypt(string content, string user)
{
    var encrypt = GetCryptoNet(user).EncryptFromString(content);
    return Convert.ToBase64String(encrypt);
}
string Decrypt(string encrypt, string user)
{
    return GetCryptoNet(user).DecryptToString(Convert.FromBase64String(encrypt));
}

var content = "";
var user = "12345678901234567890";
var encryptStr = Encrypt(content, user);
var decrypt = Decrypt(encryptStr, user);

Console.WriteLine(encryptStr.ToString());
Console.WriteLine(decrypt.ToString());

Console.ReadLine();
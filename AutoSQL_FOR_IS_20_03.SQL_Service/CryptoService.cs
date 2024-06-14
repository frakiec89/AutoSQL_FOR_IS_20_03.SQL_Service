
namespace AutoSQL_FOR_IS_20_03.SQL_Service
{
    public class CryptoService
    {
        public static string CryptokeyText (string content , string key)
        {
            string s = key + content; // Написаить  нормальный алгоритм  шифрования 
            return s;
        }

        public static string DeCryptokeyText(string contentCrypt, string key)
        {
            string s = contentCrypt.TrimStart(key.ToCharArray()); // написать  нормальный алгоритм  дешифровки
            return s;
        }
    }
}

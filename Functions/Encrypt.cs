using System;

namespace animal_adoption.Functions
{
    public static class Encrypt
    {
        public static string Encrypted(this string _passwordEncrypt)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_passwordEncrypt);
            result = Convert.ToBase64String(encryted);
            return result;
        }
 
        public static string Decrypt(this string _passwordDecrypt)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_passwordDecrypt);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
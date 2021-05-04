using System;
using System.Text;

namespace MVCCore
{
    public static class Utils
    {
        public static string EncryptData(string text)
        {
            var byteArray = Encoding.UTF32.GetBytes(text);// UTF8.GetBytes(text);
            return Convert.ToBase64String(byteArray);
        }
        
        public static string DecryptData(string encryptedText)
        {
            var decode = new UTF8Encoding().GetDecoder();
            var bytesOfEncryptedText = Convert.FromBase64String(encryptedText);
            var charCount = decode.GetCharCount(bytesOfEncryptedText, 0, bytesOfEncryptedText.Length);
            var decodedChars = new char[charCount];
            decode.GetChars(bytesOfEncryptedText, 0, bytesOfEncryptedText.Length, decodedChars, 0);
            return new String(decodedChars);
        }
    }
}
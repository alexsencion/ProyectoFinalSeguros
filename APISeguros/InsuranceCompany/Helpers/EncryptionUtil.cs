using System;

namespace InsuranceCompany.Helpers
{
    public class EncryptionUtil
    {
        public static string Encode(string decodedText)
        {
            var byteText = System.Text.Encoding.UTF8.GetBytes(decodedText);
            return Convert.ToBase64String(byteText);
        }

        public static string Decode(string encodedText)
        {
            var byteText = Convert.FromBase64String(encodedText);
            return System.Text.Encoding.UTF8.GetString(byteText);
        }
    }
}
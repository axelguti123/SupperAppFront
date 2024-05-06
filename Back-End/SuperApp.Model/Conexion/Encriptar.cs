using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Model.Conexion
{
    internal class Encriptar
    {
        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            using MemoryStream ms = new();
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearData, 0, clearData.Length);
            }
            return ms.ToArray();
        }
        public static string Encrypt(string Data, string Password, int Bits)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(Data);
            using PasswordDeriveBytes pdb = new(Password, [0x0, 0x1, 0x2, 0x1C, 0x1D, 0x1E, 0x3, 0x4, 0x5, 0xF, 0x20, 0x21, 0xAD, 0xAF, 0xA4]);
            using Aes aes = Aes.Create();
            aes.Key = pdb.GetBytes(Bits / 8);
            aes.IV = pdb.GetBytes(16);
            byte[] encryptedData = Encrypt(clearBytes, aes.Key, aes.IV);
            return Convert.ToBase64String(encryptedData);

        }
        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            using MemoryStream ms = new();
            using (Aes alg = Aes.Create())
            {
                alg.Key = Key;
                alg.IV = IV;
                using CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
            }
            return ms.ToArray();
        }
        public string Decrypt(string Data, string Password, int Bits)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(Data);
                using PasswordDeriveBytes pdb = new(Password, [0x0, 0x1, 0x2, 0x1C, 0x1D, 0x1E, 0x3, 0x4, 0x5, 0xF, 0x20, 0x21, 0xAD, 0xAF, 0xA4]);
                using Aes alg = Aes.Create();
                alg.Key = pdb.GetBytes(Bits / 8);
                alg.IV = pdb.GetBytes(16);
                byte[] decryptedData = Decrypt(cipherBytes, alg.Key, alg.IV);
                return Encoding.Unicode.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

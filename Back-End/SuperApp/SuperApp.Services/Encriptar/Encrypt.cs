using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SuperApp.Services.Encriptar
{
    internal class Encrypt
    {
        protected internal static string EncriptarPassword(string? contraseña)
        {

            // Generar una sal aleatoria
            byte[] sal = new byte[16];
            RandomNumberGenerator.Fill(sal);

            // Parámetros para Argon2
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(contraseña))
            {
                Salt = sal,
                DegreeOfParallelism = 4, // Grado de paralelismo (número de hilos de procesamiento)
                Iterations = 4, // Número de iteraciones de hash
                MemorySize = 1024 * 1024 // Tamaño de memoria (en bytes)
            };

            // Calcular el hash de la contraseña
            byte[] hash = argon2.GetBytes(16); // 16 bytes es un tamaño de hash razonable

            // Convertir el hash en una cadena hexadecimal para almacenarlo en la base de datos
            string hashString = BitConverter.ToString(hash).Replace("-", string.Empty);
            return hashString;
        }
    }
}

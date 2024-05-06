using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model.Conexion
{
    internal class Decrypt
    {
        static private Encriptar _aes = new();
        static public string CnString;
        static string dbcnString;
        static public string appPwdUnique = "SUPERAPP.hospital";


        public static object checkServer()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            dbcnString = root.Attributes[0].Value;
            CnString = (_aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
            return CnString;

        }

        internal class label
        {

        }
        public static object UsuariosEncryp()
        {
            XmlDocument doc = new();
            label root = new();

            dbcnString = root.ToString();
            CnString = (_aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
            return CnString;

        }
    }
}

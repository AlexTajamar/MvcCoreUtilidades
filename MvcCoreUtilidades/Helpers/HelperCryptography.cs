using System.Security.Cryptography;
using System.Text;

namespace MvcCoreUtilidades.Helpers
{
    public class HelperCryptography
    {
        public static string Encrypt(string content)
        {
            //CIFRANDO EN UNA DIRECCION A NIVEL DE BYTES
            byte[] input;
            byte[] output;

            //NECESITAMOS UNA CLASE PARA CONVERTIR DE BYTE A STRING Y DE STRING A BYTE
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS U OBJETO SHA 
            SHA1 sha = SHA1.Create();
           //CONVERTIMOS EL STRING A BYTE
            input = encoding.GetBytes(content);
            //CIFRAMOS EL CONTENIDO
            output = sha.ComputeHash(input);
            //CONVERTIMOS EL BYTE A STRING
            return Convert.ToBase64String(output);

        }
    }
}

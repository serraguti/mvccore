using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class CypherService
    {
        //QUE NECESITO????
        public static String EncriptarTextoBasico(String contenido)
        {
            //NECESITAMOS TRABAJAR A NIVEL DE byte[]
            //DEBEMOS CONVERTIR A byte[] EL CONTENIDO DE ENTRADA
            byte[] entrada;
            //EL CIFRADO SE REALIZA A NIVEL DE byte[] Y DEVOLVERA
            //OTRO byte[] DE SALIDA
            byte[] salida;
            //NECESITAMOS UN CONVERSOR PARA TRANSFORMAR byte[]
            //A String Y VICEVERSA
            UnicodeEncoding encoding = new UnicodeEncoding();
            //NECESITAMOS EL OBJETO QUE SE ENCARGARA DE REALIZAR
            //EL CIFRADO
            SHA1Managed sha = new SHA1Managed();
            //DEBEMOS CONVERTIR EL CONTENIDO DE ENTRADA A byte[]
            entrada = encoding.GetBytes(contenido);
            //EL OBJETO Sha1Managed TIENE UN METODO
            //PARA DEVOLVER LOS byte[] DE SALIDA REALIZANDO EL CIFRADO
            salida = sha.ComputeHash(entrada);
            String res = encoding.GetString(salida);
            return res;
        }


        //METODO PARA GENERAR SALT
        public static String GetSalt()
        {
            Random random = new Random();
            String salt = "";
            for (int i = 1; i <= 50; i++)
            {
                int aleat = random.Next(0, 255);
                char letra = Convert.ToChar(aleat);
                salt += letra;
            }
            return salt;
        }

        public static String CifrarContenido(string contenido
            , int iteraciones, String salt)
        {
            return "";
        }

        public static byte[] CifrarContenido(string contenido, String salt)
        {
            //PARA EL SALT, PUES SE ALMACENA ENTRE MEDIAS
            //DEL CONTENIDO, EN POSICIONES QUE YO QUIERO...
            String contenidosalt = contenido + salt;
            SHA256Managed sha = new SHA256Managed();
            byte[] salida;
            salida = Encoding.UTF8.GetBytes(contenidosalt);
            //CIFRAMOS EL NUMERO DE ITERACIONES QUE NOS INDICAN
            for (int i = 1; i <= 100; i++)
            {
                //REALIZAMOS EL CIFRADO n VECES
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
    }
}

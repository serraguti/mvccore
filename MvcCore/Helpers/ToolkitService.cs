using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class ToolkitService
    {
        public static bool CompararArrayBytes(byte[] a, byte[] b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].Equals(b[i]) == false)
                {
                    iguales = false;
                    break;
                }
            }
            return iguales;
        }

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }
        }

        //METODO QUE RECIBIRA UN OBJETO Y LO TRANSFORMARA
        //EN String Json
        public static String SerializeJsonObject(object objeto)
        {
            String respuesta =
                JsonConvert.SerializeObject(objeto);
            return respuesta;
        }

        //METODO QUE RECIBIRA UN String Json Y DEVOLVERA EL OBJETO
        //QUE REPRESENTA DICHO JSON
        public static Object DeserializeJsonObject(String json
            , Type type)
        {
            Object respuesta =
                JsonConvert.DeserializeObject(json, type);
            return respuesta;
        }
    }
}

using Microsoft.AspNetCore.Http;
using MvcCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Extensions
{
    public static class SessionExtension
    {
        //QUE NECESITAMOS???
        //LO QUE QUEREMOS ES PODER ALMACENAR CUALQUIER OBJETO 
        //EN SESSION
        //DEBEMOS RECIBIR (COMO PRIMER PARAMETRO) EL OBJETO
        //QUE ESTAMOS EXTENDIENDO (ISession)
        public static void SetObject
            (this ISession session, String key, object value)
        {
            //CUANDO ALMACENAMOS ALGO EN Session
            //QUE NECESITAMOS???
            //HttpContext.Session.SetObject("key", value)
            String data =
                ToolkitService.SerializeJsonObject(value);
            session.SetString(key, data);
        }

        public static T GetObject<T>
            (this ISession session, String key)
        {
            //NOSOTROS TENEMOS UN STRING (JSON) GUARDADO
            //QUE DEBERIAMOS HACER??
            //DEVOLVER EL OBJETO MAPEADO DE DICHO STRING
            //var algo = HttpContext.Session.GetObject<T>("key");
            //int numero = HttpContext.Session.GetObject<int>("NUMERO");
            //RECUPERAMOS EL Json ALMACENADO EN Session
            String data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return ToolkitService.DeserializeJsonObject<T>(data);
        }
    }
}

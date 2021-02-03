using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Controllers
{
    public class CachingController : Controller
    {
        private IMemoryCache MemoryCache;

        public CachingController(IMemoryCache memorycache)
        {
            this.MemoryCache = memorycache;
        }

        public IActionResult HoraSistema(int? tiempo)
        {
            if (tiempo == null)
            {
                tiempo = 5;
            }
            String fecha =
                DateTime.Now.ToShortDateString()
                + ", "
                + DateTime.Now.ToLongTimeString();
            //PREGUNTAMOS SI EXISTE ALGO EN CACHE
            if (this.MemoryCache.Get("FECHA") == null)
            {
                //NO EXISTE, LO CREAMOS
                this.MemoryCache.Set("FECHA", fecha
                    , new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration
                    (TimeSpan.FromSeconds(tiempo.GetValueOrDefault())));
                ViewData["FECHA"] =
                    this.MemoryCache.Get("FECHA");
                ViewData["MENSAJE"] = "Almacenando en Cache. "
                    + tiempo.Value + " segundos.";
            }
            else
            {
                //RECUPERAMOS LA FECHA DEL CACHE
                fecha = this.MemoryCache.Get("FECHA").ToString();
                ViewData["MENSAJE"] = "Recuperando de Cache";
                ViewData["FECHA"] = fecha;
            }
            return View();
        }

        [ResponseCache(Duration = 15
            , VaryByQueryKeys = new string[] { "*" })]
        public IActionResult HoraSistemaDistribuida(String dato)
        {
            String fecha =
                DateTime.Now.ToShortDateString()
                + ", "
                + DateTime.Now.ToLongTimeString();
            ViewData["FECHA"] = fecha;
            return View();
        }
    }
}

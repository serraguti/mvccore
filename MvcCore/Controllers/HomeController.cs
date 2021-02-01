using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCore.Helpers;
using MvcCore.Models;

namespace MvcCore.Controllers
{
    public class HomeController : Controller
    {
        MailService MailService;
        UploadService UploadService;

        public HomeController(UploadService uploadservice
            , MailService mailservice)
        {
            this.MailService = mailservice;
            this.UploadService = uploadservice;
        }

        public IActionResult EjemploSession(String accion)
        {
            if (accion == "almacenar")
            {
                Persona person = new Persona();
                person.Nombre = "Alumno";
                person.Edad = 27;
                person.Hora = DateTime.Now.ToLongTimeString();
                //byte[] data = ToolkitService.ObjectToByteArray(person);
                //HttpContext.Session.Set("persona", data);
                String data =
                    ToolkitService.SerializeJsonObject(person);
                HttpContext.Session.SetString("persona", data);
                ViewData["MENSAJE"] = "Datos almacenados en Session "
                    + DateTime.Now.ToLongTimeString();
            }else if (accion == "mostrar")
            {
                //byte[] data = HttpContext.Session.Get("persona");
                //Persona person =
                //    ToolkitService.ByteArrayToObject(data) as Persona;
                String data = HttpContext.Session.GetString("persona");
                Persona person =
ToolkitService.DeserializeJsonObject(data, typeof(Persona)) as Persona;
                ViewData["autor"] = 
                    person.Nombre + ", Edad: " + person.Edad;
                ViewData["hora"] = person.Hora;
                ViewData["MENSAJE"] = "Mostrando datos";
            }
            return View();
        }

        public IActionResult AlmacenarMultipleSession(String accion)
        {
            if (accion == "almacenar")
            {
                List<Persona> personas = new List<Persona>();
                Persona p = new Persona
                {
                    Nombre = "Lucia",
                    Edad = 16,
                    Hora = DateTime.Now.ToLongTimeString()
                };
                personas.Add(p);
                p = new Persona
                {
                    Nombre = "Antonia",
                    Edad = 36,
                    Hora = DateTime.Now.ToLongTimeString()
                };
                personas.Add(p);
                //byte[] data =
                //    ToolkitService.ObjectToByteArray(personas);
                //HttpContext.Session.Set("personas", data);
                String data =
                    ToolkitService.SerializeJsonObject(personas);
                HttpContext.Session.SetString("personas", data);
                ViewData["MENSAJE"] = "Almacenando "
                    + DateTime.Now.ToLongTimeString();
            }
            else if (accion == "mostrar")
            {
                //byte[] data =
                //    HttpContext.Session.Get("personas");
                //List<Persona> personas =
                //    ToolkitService.ByteArrayToObject(data) as List<Persona>;
                String data =
                    HttpContext.Session.GetString("personas");
                List<Persona> personas =
                    ToolkitService.DeserializeJsonObject(data
                    , typeof(List<Persona>)) as List<Persona>;
                ViewData["MENSAJE"] = "Recuperando de Session";
                return View(personas);
            }
            return View();
        }

        public IActionResult EjemploMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> 
            EjemploMail(String receptor, String asunto
            , String mensaje, IFormFile fichero)
        {
            if (fichero != null)
            {
                String path = await this.UploadService
                    .UploadFileAsync(fichero, Folders.Temporal);
                this.MailService.SendMail(receptor, asunto, mensaje, path);
            }
            else
            {
                this.MailService.SendMail(receptor, asunto, mensaje);
            }
            
            ViewData["MENSAJE"] = "Mensaje enviado";
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async 
            Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            await this.UploadService.UploadFileAsync(fichero, Folders.Images);
            ViewData["MENSAJE"] = "Archivo subido";
            return View();
        }

        public IActionResult CifradoHash()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoHash
            (String contenido, String resultado, String accion)
        {
            String res = CypherService.EncriptarTextoBasico(contenido);
            //SOLAMENTE SI ESCRIBIMOS EL MISMO CONTENIDO
            //TENDRIAMOS LA MISMA SECUENCIA DE SALIDA
            if (accion.ToLower() == "cifrar")
            {
                ViewData["RESULTADO"] = res;
            }else if (accion.ToLower() == "comparar")
            {
                //COMPARAMOS LA CAJA DE TEXTO resultado 
                //CON EL DATO YA CIFRADO DE NUEVO res
                //contenido = 12345  --> resultado = 243rea
                //comparar: 
                //contenido = 1234556  -->  res = 243redsaa, resultado = 243rea
                if (resultado != res)
                {
                    ViewData["MENSAJE"] =
                        "<h1 style='color:red'>No son iguales</h1>";
                }
                else
                {
                    ViewData["MENSAJE"] =
                        "<h1 style='color:blue'>Iguales</h1>";
                }
            }
            

            return View();
        }

        public IActionResult CifradoHashEficiente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoHashEficiente(String contenido
            , int iteraciones, String salt, String resultado, String accion)
        {
            String cifrado =
                CypherService.CifrarContenido(contenido, iteraciones, salt);
            if (accion.ToLower() == "cifrar")
            {
                ViewData["RESULTADO"] = cifrado;
            }else if (accion.ToLower() == "comparar")
            {
                if (resultado == cifrado)
                {
                    ViewData["MENSAJE"] =
                        "<h1 style='color:blue'>Son Iguales!!!</h1>";
                }
                else
                {
                    ViewData["MENSAJE"] =
                        "<h1 style='color:red'>Resultado Incorrecto</h1>";
                }
            }
            return View();
        }
    }
}

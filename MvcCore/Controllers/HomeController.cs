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

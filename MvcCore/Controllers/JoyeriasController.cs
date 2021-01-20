using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.IO;
using MvcCore.Models;

namespace MvcCore.Controllers
{
    public class JoyeriasController : Controller
    {
        IWebHostEnvironment environment;

        public JoyeriasController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult Index()
        {
            String filename = "joyerias.xml";
            //CUANDO HABLAMOS DE RUTAS Y ESTAMOS EN EL MARAVILLOSO
            //MUNDO DE WINDOWS, TODOS PENSAMOS EN LA BARRA c:\\
            //PERO NO TODOS LOS SISTEMAS OPERATIVOS UTILIZAN ESA
            //BARRA Y EN CORE, PODEMOS INSTALAR LAS APPS
            //EN OTROS SISTEMAS QUE NO SON WINDOWS.
            //TENEMOS UNA CLASE LLAMADA Path DENTRO DE System.IO
            //QUE NOS SOLUCIONA LAS RUTAS EN DIFERENTES PLATAFORMAS
            String path = Path.Combine(this.environment.WebRootPath
                , "documents", filename);
            //PARA PODER REALIZAR CONSULTAS Linq To XML
            //SE UTILIZA EL OBJETO XDocument APUNTANDO A UNA 
            //RUTA O A UN String QUE LEAMOS DE ALGUN SITIO
            XDocument docxml = XDocument.Load(path);
            //EXTRACCION MANUAL
            //List<Joyeria> joyerias = new List<Joyeria>();
            ////PARA LEER UN DOCUMENTO XML, DEBEMOS ACCEDER
            ////A LAS ETIQUETAS QUE NOS INTERESAN MEDIANTE
            ////EL METODO Descendants
            //var consulta = from datos in docxml.Descendants("joyeria")
            //               select datos;
            //foreach (var dato in consulta)
            //{
            //    Joyeria joy = new Joyeria();
            //    joy.Nombre = dato.Element("nombrejoyeria").Value;
            //    joy.Direccion = dato.Element("direccion").Value;
            //    joy.Telefono = dato.Element("telf").Value;
            //    joy.Cif = dato.Attribute("cif").Value;
            //    joyerias.Add(joy);
            //}

            //CONSULTA AUTOMATICA CREACION DE OBJETOS Y EXTRACCION
            var consulta = from datos in docxml.Descendants("joyeria")
                           select new Joyeria
                           {
                               Nombre = datos.Element("nombrejoyeria").Value
                               ,
                               Direccion = datos.Element("direccion").Value
                               ,
                               Telefono = datos.Element("telf").Value
                               ,
                               Cif = datos.Attribute("cif").Value
                           };
            List<Joyeria> joyerias = consulta.ToList();
            return View(joyerias);
        }
    }
}

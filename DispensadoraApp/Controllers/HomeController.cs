using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DispensadoraApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DispensadoraApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Producto> productos = new List<Producto>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Producto producto1 = new Producto { codigo = "1", nombre = "Quesito", marca="Ernesto quesos", precio=2000, cantidad=12 };
            productos.Add(producto1);
            Producto producto2 = new Producto { codigo="2", nombre="Cuajada", marca="Ernestos Campo", precio=2100, cantidad=14};
            productos.Add(producto2);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Dispensadora() {
            HttpContext.Session.SetString("productos", JsonConvert.SerializeObject(productos));
            
            return View();
        }
        [HttpPost]
        public IActionResult Comprar(string codigop, string cantidadp, string monedasp) {
            int respuesta = 0;
          List<Producto> productos=JsonConvert.DeserializeObject<List<Producto>>(HttpContext.Session.GetString("productos"));
            //HttpContext.Session.Set<Producto>("Hola", producto);
            Producto p = new Producto();
            string codigoP = codigop;
            int cantidadP = Int32.Parse(cantidadp);
            string monedas = monedasp;
            
            int pos = productos.FindIndex(e=>e.codigo==codigoP);
            int valorMonedas = 0;
            if (pos != -1)
            {
                string[] monedaUsu = monedas.Split(',');
                for (int i = 0; i < monedaUsu.Length; i++)
                {
                    if (monedaUsu[i] == "500" || monedaUsu[i] == "100" || monedaUsu[i] == "50")
                    {
                        int[] monedasbuenas = Array.ConvertAll(monedaUsu, k => int.Parse(k));
                        valorMonedas = monedasbuenas.Sum();
                        if (valorMonedas >= productos[pos].precio)
                        {
                            respuesta = valorMonedas - productos[pos].precio;
                            p.Restarcantidad(cantidadP);
                            ViewData["monedasbuenas"] =valorMonedas;
                            ViewData["valorProducto"] = productos[pos].precio;
                            ViewData["devuelta"] = respuesta;
                            HttpContext.Session.SetString("productos", JsonConvert.SerializeObject(productos));
                            return View("Dispensadora");

                        }
                    }
                    else
                    {
                        int[] monedasmalas = Array.ConvertAll(monedaUsu, l => int.Parse(l));
                        int monedasM = monedasmalas.Sum();
                        ViewData["monedasmalas"] = monedasM;
                        return View("Dispensadora");
                    }
                }

            }
            else {
                ViewData["noPosicion"] = -3;
                return View("Dispensadora");
            }
            return View("Dispensadora");
        }

        public IActionResult Registrar(string codigon, string nombrep, string marcap, int preciop, int cantidadp) {
            List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(HttpContext.Session.GetString("productos"));
            Producto productoadd = new Producto { codigo = codigon, nombre = nombrep, marca = marcap, precio = preciop, cantidad = cantidadp };
            productos.Add(productoadd);
            ViewData["si"] = 1 ;
            return Redirect("Dispensadora");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

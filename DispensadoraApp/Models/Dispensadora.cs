using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispensadoraApp.Models
{
    public class Dispensadora
    {
        public List<Producto> productos = new List<Producto>();
        public string monedas { set; get; }

        public string CodigoProducto { set; get; }

        public Dispensadora() {
         
        }
        public Dispensadora(string monedas, string Codigoproductos) {
            this.monedas = monedas;
            this.CodigoProducto = Codigoproductos;
        
        }

        public void AgregarProductos() { 
        
        }
    }
}

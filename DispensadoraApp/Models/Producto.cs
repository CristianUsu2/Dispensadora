using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DispensadoraApp.Models
{
    public class Producto
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public int precio { get; set; }

        public int cantidad { get; set; }

        public Producto() {
           
        }
        public Producto(string codigo, string nombre, string marca, int precio, int cantidad) {
            this.codigo = codigo;
            this.nombre = nombre;
            this.marca = marca;
            this.precio = precio;
            this.cantidad = cantidad;
        }

      

        public void Restarcantidad(int cantidadrestar) {
            int respuesta = 0;
            respuesta = this.cantidad - cantidadrestar;
            this.cantidad = respuesta;
            
        }
    }
}

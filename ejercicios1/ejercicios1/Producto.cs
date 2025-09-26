using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejercicios1
{
    internal class Producto
    {

        private int cantidad;
        private double precio;

        public Producto(int cantidad, double precio)
        {
            this.cantidad = cantidad;
            this.precio = precio;
        }
        public int GetCantidad()
        {
            return cantidad;
        }
        public double GetPrecio()
        {
            return precio;
        }
        public double precioFinal()
        {
            return cantidad * precio;
        }
    }
}

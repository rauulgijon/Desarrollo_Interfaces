using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimientoMatriz
{
    internal class Utilidades
    {
        
        private static Random r = new Random();
        public static int NumeroAleatorio(int min, int max)
        {
            return r.Next(min, max);
        }

    }
}

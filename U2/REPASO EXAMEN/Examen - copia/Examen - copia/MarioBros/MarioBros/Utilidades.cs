using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioBros
{
    internal class Utilidades
    {
        public static Random r = new Random();
            public static int NumeroAleatorio()
            {
                return r.Next(0, 3);
        }

    }
}

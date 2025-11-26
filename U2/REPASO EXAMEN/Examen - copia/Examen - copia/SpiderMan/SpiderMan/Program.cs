using SpiderMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tablero tablero = new Tablero();
            Tablero.mostrarMenuNiveles();
            Tablero.rellenaMatriz();
            Tablero.mostrarMatriz();
            Tablero.moverTablero();

        }
    }
}

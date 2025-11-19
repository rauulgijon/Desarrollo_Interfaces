using System;

namespace MovimientoMatriz
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Tablero tablero1 = new Tablero();

            tablero1.MostrarMatriz(5, 5);

            tablero1.MoverTablero();
        }
    }
}

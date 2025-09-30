using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablero1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Variables Inicializacion
            String[,] Tablero = new String[4, 4];
            int posI = 0;
            int posJ = 0;
            Tablero[posI, posJ] = "O";
            bool salir = false;
            #endregion
            Operaciones.rellenarMatriz(Tablero, "X");

            while (!salir)
            {
                Operaciones.mostrarMenu();
                Operaciones.mostrarMatriz(Tablero);
                Operaciones.entradaMenu(Tablero, ref salir, ref posI, ref posJ);
            }

        }
    }
}

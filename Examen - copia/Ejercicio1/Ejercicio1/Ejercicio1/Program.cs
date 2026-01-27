
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Variables Inicializacion
            String[,] Tablero = new String[8, 8];
            int[,] tablero2 = new int[8, 8];
            int posI = 0;
            int posJ = 0;
            int ratones = Operaciones.randomNumber(2, 5);
            int paredes = Operaciones.randomNumber(1, 3);
            Tablero[posI, posJ] = "S";
            bool salir = false;
            #endregion
            Operaciones.rellenarMatriz(tablero2, 0, 3);
            Operaciones.rellenarMatriz(Tablero, "X");

            while (!salir)
            {
                Operaciones.mostrarMenu();
                Operaciones.mostrarMatriz(Tablero);
                Operaciones.entradaMenu(Tablero, tablero2, ref salir, ref posI, ref posJ, ref ratones, ref paredes);
                Operaciones.mostrarEstadisticas(ratones, paredes);
            }

        }
    }
}

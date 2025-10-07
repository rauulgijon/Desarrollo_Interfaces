using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TableroModular;

namespace tableroModular
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            #region Variables Inicialización
            String[,] tablero = new String[4, 4];
            int posI = 0;
            int posJ = 0;
            tablero[posI, posJ] = "0";
            bool salir = false;
            #endregion

            Operaciones.rellenarMatriz(tablero, "X");

            while (!salir)
            {
                Operaciones.MostrarMenu();
                Operaciones.mostrarMatriz(tablero);
                Operaciones.entradaMenu(tablero, ref salir, ref posI, ref posJ);
            }
        }
    }
}

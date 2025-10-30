using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minihito
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Variables Inicializacion
            String[,] tablero = new String[8, 8];
            int posI = 0;
            int posJ = 0;
            int tamaño = 0;
            bool salir = false;
            #endregion

            Operaciones.rellenarMatriz(tablero, ".");

            posI = Operaciones.RandomPos(0, 14);
            posJ = Operaciones.RandomPos(0, 14);


            
            while (!salir)
            {
                Operaciones.mostrarMatriz(tablero);
                Operaciones.mostrarMenu();
                Operaciones.entradaMenu(tablero, ref salir, ref posI, ref posJ, ref tamaño);
                Operaciones.cabezaBarco(tablero, posI, posJ, "B");
            }
        }
    }
}

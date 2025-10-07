using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TableroModular
{
    internal class Operaciones
    {
        /// <summary>
        /// Rellena la matriz con la cadena dada
        /// </summary>
        /// <param name="matriz"></param> Matriz sobre la que estamos jugando
        /// <param name="cadena"></param> Cadena con la que se rellena la matriz"ç
        public static void rellenarMatriz(string[,] matriz, string cadena)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = cadena;
                }
            }
        }
        /// <summary>
        /// Devuelve falso cuando nos hemos salido de la matriz, true si no nos hemos salido de la matriz
        /// </summary>
        /// <param name="posI"></param>
        /// <param name="posJ"></param>
        /// <param name="matriz"></param>
        /// <returns></returns>
        public static bool dentroMatriz(int posI, int posJ, string[,] matriz)
        {
            if (posI >= 0 && posI < matriz.GetLength(0) && posJ >= 0 && posJ < matriz.GetLength(1))
            {
                return true;
            }
            Console.WriteLine("No puedes salir del tablero");
            return false;
        }
        /// <summary>
        /// Muestra por pantalla la matriz
        /// </sumary>
        /// <param name="matriz"> matriz sobre la que estamos jugando </param>
        public static void mostrarMatriz(string[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Muestra el menú de opciones del juego
        /// </summary>
        public static void MostrarMenu()
        {
            Console.WriteLine("1. Derecha");
            Console.WriteLine("2. Izquierda");
            Console.WriteLine("3. Abajo");
            Console.WriteLine("4. Arriba");
            Console.WriteLine("5. Salir");
        }
        /// <summary>
        /// Mueve la X en funcion de la entrada de menu
        /// </summary>
        /// <param name="tablero">Matriz tablero sobre la que jugamos</param>
        /// <param name="salir">valor true cuando seleccionamos salir</param>"
        /// <param name="posI">Valor posición i sobre la matriz</param>
        /// <param name="posJ">Valor posición j sobre la matriz</param>

        public static void entradaMenu(string[,] tablero, ref bool salir, ref int posI, ref int posJ)
        {
            try
            {
                // definicion de variables
                int opcionInt = 0;

                if (int.TryParse(Console.ReadLine(), out opcionInt))
                {
                    switch (opcionInt)
                    {
                        case 1: // derecha
                            if (dentroMatriz(posI, posJ + 1, tablero))
                            {
                                tablero[posI, posJ] = "X";
                                posJ++;
                                tablero[posI, posJ] = "0";
                            }
                            break;
                        case 2: // izquierda
                            if (dentroMatriz(posI, posJ - 1, tablero))
                            {
                                tablero[posI, posJ] = "X";
                                posJ--;
                                tablero[posI, posJ] = "0";
                            }
                            break;
                        case 3: // abajo
                            if (dentroMatriz(posI + 1, posJ, tablero))
                            {
                                tablero[posI, posJ] = "X";
                                posI++;
                                tablero[posI, posJ] = "0";
                            }
                            break;
                        case 4: // arriba
                            if (dentroMatriz(posI - 1, posJ, tablero))
                            {
                                tablero[posI, posJ] = "X";
                                posI--;
                                tablero[posI, posJ] = "0";
                            }
                            break;
                        case 5: // salir
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Solo opciones entre 1 y 5");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Se ha producido un error: " + e.ToString());
            }
        }

        internal static void rellenarMatriz(object tableroModular, string v)
        {
            throw new NotImplementedException();
        }
    }
}


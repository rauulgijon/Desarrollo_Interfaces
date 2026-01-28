using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1
{
    internal class Operaciones
    {
        private static readonly Random random = new Random();

        public static int randomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public static void mostrarMatriz(int[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void rellenarMatriz(String[,] matriz, String cadena)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == null)
                    {
                        matriz[i, j] = cadena;
                    }
                }
            }
        }

        public static void rellenarMatriz(int[,] matriz, int min, int max)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = Operaciones.randomNumber(min, max);
                }
            }
        }

        public static bool dentroMatriz(int posI, int posJ, String[,] matriz)
        {
            if (posI >= 0 && posI < matriz.GetLength(0) && posJ >= 0 && posJ < matriz.GetLength(1))
            {
                return true;
            }
            else
            {
                Console.WriteLine("No puedes salir del tablero");
                return false;

            }
        }

        public static void mostrarMatriz(String[,] matriz)
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

        public static void mostrarMenu()
        {
            Console.WriteLine("1. Mover Derecha");
            Console.WriteLine("2. Mover Izquierda");
            Console.WriteLine("3. Mover Arriba");
            Console.WriteLine("4. Mover Abajo");
            Console.WriteLine("5. Salir");
        }

        public static void entradaMenu(
            string[,] tablero,
            int[,] tablero2,
            ref bool salir,
            ref int posI,
            ref int posJ,
            ref int ratones,
            ref int paredes
            )
        {
            try
            {
                int opcionInt = 0;
                if (int.TryParse(Console.ReadLine(), out opcionInt))
                {
                    int newI = posI, newJ = posJ;
                    switch (opcionInt)
                    {
                        case 1: newJ = posJ + 1; break; // Derecha
                        case 2: newJ = posJ - 1; break; // Izquierda
                        case 3: newI = posI - 1; break; // Arriba
                        case 4: newI = posI + 1; break; // Abajo
                        case 5: salir = true; return;
                        default:
                            Console.WriteLine("Opcion no valida");
                            return;
                    }

                    if (Operaciones.dentroMatriz(newI, newJ, tablero))
                    {
                        tablero[posI, posJ] = "X";
                        posI = newI;
                        posJ = newJ;
                        tablero[posI, posJ] = "S";

                        int valor = tablero2[posI, posJ];
                        if (valor == 0)
                        {

                        }
                        else if (valor == 1)
                        {
                            Console.WriteLine("Te has chocado con una pared, sigue jugando");
                        }
                        else
                        {
                            ratones --;
                            Console.WriteLine("Te has comido un raton");
                            tablero2[posI, posJ] = 0;
                        }



                        // Comprobación de victoria
                        if (ratones == 0)
                        {
                            Console.WriteLine("¡Enhorabuena! Has ganado");
                            salir = true;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Debes introducir un numero");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void mostrarEstadisticas(int ratones, int paredes)
        {
            Console.WriteLine("Ratones restantes: " + ratones);
            Console.WriteLine("Paredes (valor actual): " + paredes);
        }
    }
}

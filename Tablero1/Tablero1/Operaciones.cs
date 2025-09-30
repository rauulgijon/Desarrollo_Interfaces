using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablero1
{
    internal class Operaciones
    {
        public int randomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public void mostrarMatriz(int[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + "\t");
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
                    Console.Write(matriz[i, j] + "\t");
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

        public static void entradaMenu(string[,] tablero, ref bool salir, ref int posI, ref int posJ)
        {
            try
            {
                int opcionInt = 0;
                if (int.TryParse(Console.ReadLine(), out opcionInt))
                {
                    switch (opcionInt)
                    {
                        case 1:
                            if (Operaciones.dentroMatriz(posI, posJ + 1, tablero)){
                                tablero[posI, posJ] = "X";
                                posJ++;
                                tablero[posI, posJ] = "O";
                            }
                            break;
                        case 2:
                            if (Operaciones.dentroMatriz(posI, posJ - 1, tablero)){
                                tablero[posI, posJ] = "X";
                                posJ--;
                                tablero[posI, posJ] = "O";
                            }
                            break;
                        case 3:
                            if (Operaciones.dentroMatriz(posI - 1, posJ, tablero)){
                                tablero[posI, posJ] = "X";
                                posI--;
                                tablero[posI, posJ] = "O";
                            }
                            break;
                        case 4:
                            if (Operaciones.dentroMatriz(posI + 1, posJ, tablero)){
                                tablero[posI, posJ] = "X";
                                posI++;
                                tablero[posI, posJ] = "O";
                            }
                            break;
                        case 5:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opcion no valida");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Debes introducir un numero");
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }










        }
    }
}





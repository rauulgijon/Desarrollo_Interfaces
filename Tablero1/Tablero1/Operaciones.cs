using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablero1
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
            ref int vidas,
            ref int pocion)
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
                        tablero[posI, posJ] = "M";

                        int valor = tablero2[posI, posJ];
                        if (valor == 0)
                        {
                            vidas--;
                            Console.WriteLine("¡Has perdido una vida!");
                            tablero2[posI, posJ] = -1; // Marcar como visitada
                        }
                        else if (valor == 1)
                        {
                            vidas++;
                            Console.WriteLine("¡Has ganado una vida!");
                            tablero2[posI, posJ] = -1; // Marcar como visitada
                        }
                        else
                        {
                            pocion += 2;
                            Console.WriteLine("¡Has recogido 2ml de poción!");
                            tablero2[posI, posJ] = -1; // Marcar como visitada
                        }

                        if (vidas < 0)
                        {
                            vidas = 0;
                            Console.WriteLine("¡Has perdido! No te quedan vidas.");
                            salir = true;
                        }

                        // Comprobación de victoria
                        if (posI == 7 && posJ == 7 && vidas > 0 && pocion >= 5)
                        {
                            Console.WriteLine("¡Enhorabuena! Has llegado a la meta con vidas y poción suficiente. ¡Has ganado!");
                            salir = true;
                        } 
                        else if (posI == 7 && posJ == 7)
                        {
                            Console.WriteLine("Has llegado a la meta, pero no tienes suficientes vidas o poción. ¡Sigue intentando!");
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

        public static void mostrarEstadisticas(int vidas, int pocion)
        {
            Console.WriteLine("Vidas restantes: " + vidas);
            Console.WriteLine("Poción recogida: " + pocion + "ml");
        }
    }
}










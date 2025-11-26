using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioBros
{
    internal class Tablero
    {

        // Atributos
        private static int[,] matrizInterna = new int[8, 8];
        public static string[,] matriz = new string[8, 8];
        public static int fila = 0, columna = 0;


        public static int vidas = 3;
        public static int pocima = 0;


        public static void rellenaMatriz()
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = "X";
                    matrizInterna[i, j] = Utilidades.NumeroAleatorio();

                    matrizInterna[0, 0] = 3; 
                }
            }
        }

        public static void mostrarMatriz()
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

        public static void moverTablero()
        {
            while (true)
            {
                Console.Clear();

                // Mostrar matriz 
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (i == fila && j == columna)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Write(matriz[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }

                
                if (matriz[fila, columna] != "O")
                {
                    switch (matrizInterna[fila, columna])
                    {
                        case 0:
                            vidas--;
                            Console.WriteLine("¡Has perdido una vida!");
                            break;
                        case 1:
                            vidas++;
                            Console.WriteLine("¡Has ganado una vida!");
                            break;
                        case 2:
                            pocima += 2;
                            Console.WriteLine("¡Has ganado 2ml de pocima!");
                            break;
                        case 3: 
                            Console.WriteLine("¡Buena Suerte!");
                            break;
                    }

                    matriz[fila, columna] = "O"; 
                }

                // Mostrar vidas
                Console.WriteLine($"Vidas: {vidas}  Pocimas: {pocima}");

                if (vidas <= 0)
                {
                    Console.WriteLine("Has perdido todas tus vidas. Fin del juego.");
                    break;
                }

                if (pocima >= 5 && fila == 7 && columna == 7)
                {
                    Console.WriteLine("¡Enhorabuena! Has ganado el juego.");
                    break;
                }

                // Leer las teclas 
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (fila > 0) fila--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (fila < 7) fila++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (columna > 0) columna--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (columna < 7) columna++;
                        break;
                }
            }

            Console.ReadKey();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovimientoMatriz
{
    internal class Tablero
    {

        int col = 0;
        int fil = 0;
        int[,] matrizF = null;

        public Tablero()
        { 
        }


        public void MostrarMatriz(int y, int x)
        {
            matrizF = new int[y, x];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrizF[i, j] = Utilidades.NumeroAleatorio(1, 10);
                }
            }
        }


        public void MoverTablero()
        {
            int fila = 0;
            int columna = 0;
            

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (i == fila && j == columna)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(matrizF[i, j] + " ");
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (fila > 0) fila--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (fila < 4) fila++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (columna > 0) columna--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (columna < 4) columna++;
                        break;
                    case ConsoleKey.Escape:
                        return; // Salir del programa
                }
            }
            Console.ReadKey();
        }

        
    }
}

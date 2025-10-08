using System;

namespace Spiderman
{
    internal static class Operaciones
    {
        private static readonly Random random = new Random();

        // Devuelve un char aleatorio entre 'C', 'D', 'G', 'M', 'B', 'N', 'X'
        public static char RandomCharSinEnemigos()
        {
            char[] chars = { 'C','B', 'N' };
            int index = random.Next(chars.Length);
            return chars[index];
        }

        public static char RandomChar()
        {
            char[] chars = { 'C', 'D', 'G', 'M', 'B', 'N' };
            int index = random.Next(chars.Length);
            return chars[index];
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
                    matriz[i, j] = Operaciones.RandomChar();
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

        // Genera un evento aleatorio para una casilla
        public static char GenerarEvento()
        {
            // Puedes ajustar la probabilidad de cada evento
            char[] eventos = { 'C', 'D', 'G', 'M', 'B', 'N' };
            return eventos[random.Next(eventos.Length)];
        }
    }
}










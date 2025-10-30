using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace Minihito
{
    /**
     * Clase de utilidades que agrupa las operaciones de menú, azar y eventos.
     */
    internal static class Operaciones

    {
        private static readonly Random random = new Random();
        /**
         * Genera un numero aleatorio del 0,14
         */
        public static int RandomPos(int min, int max)
        {

            return random.Next(min, max);
        }


        /*
         * Ahora llamamos dos veces al metodo random pos para conseguir la posicion del tablero en la que se va a crear el barco
         */
        
        public static String [,] cabezaBarco(String[,] matriz, int i, int j, String c)
        {
            matriz[i, j] = c;
            return matriz;

        }
        

        /**
         * Rellenamos la matriz con puntos
         */
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


        /**
         * Mostramos el contenido de la matriz
         */
        public static void mostrarMatriz(String[,] matriz)
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

        /**
         * Muestra el menú de acciones disponibles.
         */
        public static void mostrarMenu()
        {
            
            Console.WriteLine("0. Salir");
            Console.WriteLine("Introduce el tamaño del barco (maximo 5): ");
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

        public static void entradaMenu(
            string[,] tablero,
            ref bool salir,
            ref int posI,
            ref int posJ, ref int tamaño)
        {
            try
            {
                int opcionInt = 0;
                if (int.TryParse(Console.ReadLine(), out opcionInt))
                {
                    int newI = posI, newJ = posJ;
                    int cont = 0;
                    switch (opcionInt)
                    {
                        case 2: newJ = posJ + 1; 
                            while (cont < tamaño)
                            {
                                tablero[newI, newJ] = "B";
                                cont++;
                                newJ = posJ + 1;
                            }
                            break; // Derecha
                        case 3: newJ = posJ - 1;
                            while (cont < tamaño)
                            {
                                tablero[newI, newJ] = "B";
                                cont++;
                                newJ = posJ - 1;
                            }
                            break; // Izquierda
                        case 4: newI = posI - 1;
                            while (cont< tamaño)
                            {
                                tablero[newI, newJ] = "B";
                                cont++;
                                newJ = posI - 1;
                            }
                            break; // Arriba
                        case 5: newI = posI + 1;
                            while(cont < tamaño)
                            {
                                tablero[newI, newJ] = "B";
                                cont++;
                                newJ = posI + 1;
                            }
                            break; // Abajo
                        case 0: salir = true; return;
                        default:
                            Console.WriteLine("Opcion no valida");
                            return;
                    }
                    if (Operaciones.dentroMatriz(newI, newJ, tablero))
                    {
                        tablero[posI, posJ] = "B";
                        posI = newI;
                        posJ = newJ;
                    }
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

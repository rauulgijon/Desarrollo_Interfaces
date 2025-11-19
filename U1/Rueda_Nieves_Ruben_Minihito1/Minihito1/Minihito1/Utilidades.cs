using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minihito1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Utilidades
    {        
        private static readonly Random random = new Random();

        public static void imprimirTablero(String[,] tablero)
        {
            for (int i = 0; i < tablero.GetLength(0); i++)
            {
                for (int j = 0; j < tablero.GetLength(1); j++)
                {
                    Console.Write(tablero[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static int generarNumeroALeatorio(int min, int max)
        {
            return random.Next(min, max);
        }

        //Este metodo genera una matriz con el relleno que le pasemos
        public static String[,] generarMatriz(String relleno, int filas, int columnas)
        {
            String[,] tablero = new String[filas, columnas];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    tablero[i, j] = relleno;
                }
            }
            return tablero;
        }

        //Este metodo sirve para colocar los barcos
        public static void colocarBarcos(String[,] tablero, int tamanioBarcoMax)
        {
            int tamanioBarco;
            Boolean sePuedeColocar = true;
            int contador = 0;
            String[] opciones = { "Derecha", "Izquierda", "Arriba", "Abajo" };            

            for (int i = 0; i < 5; i++)
            {
                do
                {
                    tamanioBarco = generarNumeroALeatorio(2, tamanioBarcoMax+1);
                    int fila = generarNumeroALeatorio(0, 15);
                    int columna = generarNumeroALeatorio(0, 15);
                    int idx = generarNumeroALeatorio(0, opciones.Length);

                    if (tablero[fila, columna].Equals("B"))
                    {
                        sePuedeColocar = false;
                    }
                    else
                    {
                        if ((fila + tamanioBarco) > 14 || (fila - tamanioBarco) < 0 || (columna + tamanioBarco) > 14 || (columna - tamanioBarco) < 0)
                        {
                            sePuedeColocar = false;
                        }
                        else
                        {
                            if (tablero[fila - 1, columna].Equals("B") || tablero[fila - 1, columna - 1].Equals("B") || tablero[fila, columna - 1].Equals("B") || tablero[fila + 1, columna - 1].Equals("B")
                            || tablero[fila + 1, columna].Equals("B") || tablero[fila + 1, columna + 1].Equals("B") || tablero[fila, columna + 1].Equals("B") || tablero[fila - 1, columna + 1].Equals("B"))
                            {
                                sePuedeColocar = false;
                            }
                            else
                            {
                                if (opciones[idx].Equals("Derecha"))
                                {
                                    if ((columna + tamanioBarco) > 14)
                                    {
                                        sePuedeColocar = false;
                                    }
                                    else
                                    {
                                        for (int j = 1; j <= tamanioBarco; j++)
                                        {
                                            if (tablero[fila, columna + j].Equals("B"))
                                            {
                                                contador++;
                                            }
                                        }

                                        if (contador > 0)
                                        {
                                            sePuedeColocar = false;
                                        }
                                        else
                                        {
                                            for (int k = 0; k < tamanioBarco; k++)
                                            {
                                                tablero[fila, columna + k] = "B";
                                            }
                                            sePuedeColocar = true;
                                        }
                                    }
                                }
                                else if (opciones[idx].Equals("Izquierda"))
                                {
                                    if ((columna - tamanioBarco) < 0)
                                    {
                                        sePuedeColocar = false;
                                    }
                                    else
                                    {
                                        for (int j = 1; j <= tamanioBarco; j++)
                                        {
                                            if (tablero[fila, columna - j].Equals("B"))
                                            {
                                                contador++;
                                            }
                                        }

                                        if (contador > 0)
                                        {
                                            sePuedeColocar = false;
                                        }
                                        else
                                        {
                                            for (int k = 0; k < tamanioBarco; k++)
                                            {
                                                tablero[fila, columna - k] = "B";
                                            }
                                            sePuedeColocar = true;
                                        }
                                    }
                                }
                                else if (opciones[idx].Equals("Arriba"))
                                {
                                    if ((fila - tamanioBarco) < 0)
                                    {
                                        sePuedeColocar = false;
                                    }
                                    else
                                    {
                                        for (int j = 1; j <= tamanioBarco; j++)
                                        {
                                            if (tablero[fila - j, columna].Equals("B"))
                                            {
                                                contador++;
                                            }
                                        }

                                        if (contador > 0)
                                        {
                                            sePuedeColocar = false;
                                        }
                                        else
                                        {
                                            for (int k = 0; k < tamanioBarco; k++)
                                            {
                                                tablero[fila - k, columna] = "B";
                                            }
                                            sePuedeColocar = true;
                                        }
                                    }
                                }
                                else if (opciones[idx].Equals("Abajo"))
                                {
                                    if ((fila + tamanioBarco) > 14)
                                    {
                                        sePuedeColocar = false;
                                    }
                                    else
                                    {
                                        for (int j = 1; j <= tamanioBarco; j++)
                                        {
                                            if (tablero[fila + j, columna].Equals("B"))
                                            {
                                                contador++;
                                            }
                                        }

                                        if (contador > 0)
                                        {
                                            sePuedeColocar = false;
                                        }
                                        else
                                        {
                                            for (int k = 0; k < tamanioBarco; k++)
                                            {
                                                tablero[fila + k, columna] = "B";
                                            }
                                            sePuedeColocar = true;
                                        }
                                    }
                                }
                            }
                        }                    
                    }
                } while (!sePuedeColocar);
            }
        }
    }
}




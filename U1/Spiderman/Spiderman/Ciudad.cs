using System;
using System.Text;

namespace Spiderman
{
    internal class Ciudad
    {
        private const int TAM = 15;
        private char[,] matrizInterna;   // Lo que realmente hay
        private char[,] matrizVisible;   // Lo que ve el jugador
        private int contador = 0;

        public Ciudad()
        {
            matrizInterna = new char[TAM, TAM];
            matrizVisible = new char[TAM, TAM];
            InicializarInterna();
            InicializarVisible();
            // Spiderman siempre arriba izquierda
            matrizVisible[0, 0] = 'S';
        }

        // Inicializa la matriz interna con caracteres aleatorios
        private void InicializarInterna()
        {
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    matrizInterna[i, j] = Operaciones.RandomChar();
                    if ((matrizInterna[i, j] == 'D') || (matrizInterna[i, j] == 'G') || (matrizInterna[i, j] == 'M'))
                    {
                        contador++;
                        if (contador > 5)
                        {
                            matrizInterna[i, j] = Operaciones.RandomCharSinEnemigos(); // Rellenamos con otro valor
                        }
                    }
                }
            }
            // Nos aseguramos de que la casilla inicial no sea un enemigo
            matrizInterna[0, 0] = 'N';
        }

        // Inicializa la matriz visible (todo oculto)
        private void InicializarVisible()
        {
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    matrizVisible[i, j] = 'O';
                }
            }
        }

        // Revela la posición actual de Spiderman
        public void RevelarCasilla(int fila, int columna)
        {
            // Primero, marcar la posición anterior como visitada
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    if (matrizVisible[i, j] == 'S')
                        matrizVisible[i, j] = 'X'; // marca la anterior con X
                }
            }

            // Ahora coloca Spiderman en la nueva posición
            matrizVisible[fila, columna] = 'S';
        }


        // Mostrar mapa visible al jugador
        public void MostrarVisible()
        {
            Console.Clear();
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    Console.Write(matrizVisible[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


    }
}

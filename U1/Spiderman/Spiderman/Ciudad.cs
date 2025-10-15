using System;

namespace Spiderman
{
    /**
     * Representa la ciudad donde se desarrolla el juego.
     * Contiene una matriz interna (con el contenido real) y una visible (lo que ve el jugador).
     * Gestiona el estado del mapa y permite revelar, restaurar y consultar casillas.
     */
    internal class Ciudad
    {
        private const int TAM = 15;          // Tamaño fijo del mapa
        private char[,] matrizInterna;       // Contenido real del mapa (enemigos, civiles, etc.)
        private char[,] matrizVisible;       // Lo que el jugador puede ver
        private int contadorVillanos = 0;    // Controla el número máximo de villanos

        /**
         * Constructor: inicializa la ciudad generando las matrices interna y visible.
         */
        public Ciudad()
        {
            matrizInterna = new char[TAM, TAM];
            matrizVisible = new char[TAM, TAM];
            InicializarInterna();
            InicializarVisible();
            matrizVisible[0, 0] = 'S'; // Spiderman inicia aquí
            matrizInterna[0, 0] = 'N'; // Casilla inicial segura
        }

        /**
         * Inicializa la matriz interna con contenido aleatorio.
         * Limita el número de villanos a un máximo de 5.
         */
        private void InicializarInterna()
        {
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                {
                    char c = Operaciones.RandomChar();

                    if (c == 'D' || c == 'G' || c == 'M')
                    {
                        contadorVillanos++;
                        if (contadorVillanos > 5)
                            c = Operaciones.RandomCharSinEnemigos();
                    }

                    matrizInterna[i, j] = c;
                }
            }
        }

        /**
         * Inicializa la matriz visible con 'O' (oculto).
         */
        private void InicializarVisible()
        {
            for (int i = 0; i < TAM; i++)
                for (int j = 0; j < TAM; j++)
                    matrizVisible[i, j] = 'O';
        }

        /**
         * Marca la posición actual de Spiderman con 'S'
         * y deja un rastro 'X' en las posiciones anteriores.
         */
        public void RevelarCasilla(int fila, int columna)
        {
            for (int i = 0; i < TAM; i++)
                for (int j = 0; j < TAM; j++)
                    if (matrizVisible[i, j] == 'S') matrizVisible[i, j] = 'X';

            matrizVisible[fila, columna] = 'S';
        }

        /**
         * Muestra en consola la parte visible del mapa.
         */
        public void MostrarVisible()
        {
            Console.WriteLine("\n===== MAPA DE NUEVA YORK =====");
            for (int i = 0; i < TAM; i++)
            {
                for (int j = 0; j < TAM; j++)
                    Console.Write(matrizVisible[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine("==============================\n");
        }

        /**
         * Devuelve el carácter real de una posición interna del mapa.
         */
        public char GetInterna(int fila, int columna)
        {
            return matrizInterna[fila, columna];
        }

        /**
         * Marca una posición como visitada (internamente 'X').
         */
        public void MarcarVisitadoInterno(int fila, int columna)
        {
            matrizInterna[fila, columna] = 'X';
        }

        /**
         * Devuelve el tamaño del mapa.
         */
        public int GetTamaño()
        {
            return TAM;
        }

        /**
         * Restaura una casilla como nueva (visible: 'O', interna: valor aleatorio).
         */
        public void RestaurarCasilla(int fila, int columna)
        {
            matrizVisible[fila, columna] = 'O';
            matrizInterna[fila, columna] = Operaciones.RandomChar();
        }
    }
}

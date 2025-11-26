using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderMan
{
    internal class Tablero
    {
        // Constantes
        protected const int NEUTRO = 0;
        protected const int OCTOPUS = 1;
        protected const int DUENDE = 2;
        protected const int MISTERIO = 3;
        protected const int CIVIL = 4;
        protected const int BONUS = 5;
        protected const int VIDAS = 6;

        // Atributos
        private static int[,] matrizInterna = new int[15, 15];
        public static string[,] matriz = new string[15, 15];

        public static int fila = 0, columna = 0;
        private static int ultimaFila = 0, ultimaColumna = 0;
        private static int penultimaFila = 0, penultimaColumna = 0;

        public static Jugador jugador = null;

        public static int vidas;
        public static int civilesSalvados;

        // Rellena el tablero con casillas aleatorias
        public static void rellenaMatriz()
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = "O"; 
                    matrizInterna[i, j] = Utilidades.NumeroAleatorio(); 

                    matrizInterna[0, 0] = NEUTRO; 
                }
            }
        }

        // Muestra un menú de selección de nivel al jugador
        public static void mostrarMenuNiveles()
        {
            Console.WriteLine("Selecciona un nivel:");
            Console.WriteLine("1. Nivel 1 (Fácil)");
            Console.WriteLine("2. Nivel 2 (Medio)");
            Console.WriteLine("3. Nivel 3 (Difícil)");
            Console.Write("Opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    jugador = new Jugador(10);
                    Console.WriteLine("Empiezas con 10 vidas");
                    break;
                case "2":
                    jugador = new Jugador(7);
                    Console.WriteLine("Empiezas con 7 vidas");
                    break;
                case "3":
                    jugador = new Jugador(5);
                    Console.WriteLine("Empiezas con 5 vidas");
                    break;
                default:
                    Console.WriteLine("Opción no válida, se selecciona Nivel 1 por defecto.");
                    jugador = new Jugador(10);
                    break;
            }

            vidas = jugador.getVidas();
            civilesSalvados = jugador.getCivilesSalvados();
        }

        // Muestra por consola el contenido del tablero visible
        public static void mostrarMatriz()
        {
            Console.WriteLine("Spiderman: Misión en Nueva York");

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    Console.Write(matriz[i, j] + " "); 
                }
                Console.WriteLine(); 
            }
        }

        // Controla el movimiento del jugador y la lógica principal del juego
        public static void moverTablero()
        {
            while (true)
            {
                Console.Clear(); // Limpia la consola antes de dibujar de nuevo el tablero

                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (i == fila && j == columna)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            matriz[fila, columna] = "S"; 
                        }

                        Console.Write(matriz[i, j] + " ");
                        Console.ResetColor(); 
                    }
                    Console.WriteLine();
                }

                // Solo entra si la casilla actual no fue visitada antes
                if (matriz[fila, columna] != "X")
                {
                    switch (matrizInterna[fila, columna])
                    {
                        case OCTOPUS:
                            vidas--;
                            fila = penultimaFila; 
                            columna = penultimaColumna;
                            Console.WriteLine("Te has cruzado con el Doctor Octopus. Pierdes una vida y retrocedes dos casillas!");
                            break;

                        case DUENDE:
                            vidas--; 
                            // Si tenías civiles, pierdes uno
                            if (civilesSalvados > 0) { civilesSalvados--; }
                            Console.WriteLine("¡Te has cruzado con el Duende Verde. Pierdes una vida y un Civil!");
                            break;

                        case MISTERIO:
                            // Te mueves a una casilla aleatoria
                            fila = new Random().Next(0, 15);
                            columna = new Random().Next(0, 15);
                            Console.WriteLine("¡Te has cruzado con Mysterio. Ahora te moverás a una casilla aleatoria!");
                            break;

                        case CIVIL:
                            civilesSalvados++; 
                            Console.WriteLine("¡Enhorabuena, rescataste a un civil!");
                            break;

                        case BONUS:
                            // Nos aseguramos que no nos salamos de la matriz
                            fila += 2;
                            if (fila > 14) fila = 14;
                            if (columna > 14) columna = 14;
                            Console.WriteLine("¡Has obtenido un Bonus!");
                            break;

                        case VIDAS:
                            vidas++;
                            Console.WriteLine("¡Has ganado una vida!");
                            break;

                        case NEUTRO:
                            Console.WriteLine("Casilla Neutra. Sigue adelante.");
                            break;
                    }

                    // Marca la casilla como visitada
                    matriz[fila, columna] = "X";
                    matrizInterna[fila, columna] = NEUTRO;
                }

                // Muestra las vidas y civiles salvados en cada turno
                Console.WriteLine($"Vidas: {vidas}  Civiles Salvados: {civilesSalvados}");

                // Condición de derrota
                if (vidas <= 0)
                {
                    Console.WriteLine("Has perdido todas tus vidas. Fin del juego.");
                    break; 
                }

                // Condición de victoria
                if (civilesSalvados >= 3 && fila == 14 && columna == 14)
                {
                    Console.WriteLine("¡Enhorabuena! Has ganado el juego.");
                    break;
                }

                // Espera al movimiento del jugador (teclas de flecha)
                var key = Console.ReadKey(true).Key;

                // Según la tecla pulsada, mueve al jugador
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (fila > 0) fila--; 
                        break;
                    case ConsoleKey.DownArrow:
                        if (fila < 14) fila++; 
                        break;
                    case ConsoleKey.LeftArrow:
                        if (columna > 0) columna--; 
                        break;
                    case ConsoleKey.RightArrow:
                        if (columna < 14) columna++; 
                        break;
                }

                // Guarda las posiciones anteriores para poder retroceder
                penultimaFila = ultimaFila;
                penultimaColumna = ultimaColumna;
                ultimaFila = fila;
                ultimaColumna = columna;
            }

            Console.ReadKey(); // Espera una tecla antes de cerrar
        }
    }
}

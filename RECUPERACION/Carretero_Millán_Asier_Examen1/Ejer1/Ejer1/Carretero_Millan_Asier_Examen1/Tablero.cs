using System;

namespace Carretero_Millan_Asier_Examen1
{
    internal class Tablero
    {
        private char[,] celdas;
        private int n;
        private readonly Random rnd = new Random();

        // Constantes
        private const char VACIO = 'O';
        private const char PARED = '1';
        private const char RATON = '2';
        private const char SERPIENTE = 'S';

        private int ratonesRestantes;
        private Serpiente serpiente;

        // Dirección actual (vector)
        private int dirFila = 0;
        private int dirCol = 1; // derecha por defecto

        public void Iniciar()
        {
            n = PedirTamano();
            celdas = new char[n, n];
            InicializarCeldas();
            serpiente = new Serpiente(4);
            ColocarElementosAleatorios();
            serpiente.Inicializar(0, 0, dirFila, dirCol);
            PintarSerpienteInicial();
            BucleJuego();
        }

        private int PedirTamano()
        {
            Console.Write("Introduce N (tamaño del tablero NxN): ");
            while (true)
            {
                var entrada = Console.ReadLine();
                int valor;
                if (int.TryParse(entrada, out valor) && valor >= 5 && valor <= 50)
                    return valor;
                Console.Write("Valor inválido. Introduce un entero entre 5 y 50: ");
            }
        }

        private void InicializarCeldas()
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    celdas[i, j] = VACIO;
        }

        private void ColocarElementosAleatorios()
        {
            int total = n;
            int paredesObjetivo = total + 2;
            int ratonesObjetivo = total;

            while (paredesObjetivo > 0)
            {
                int f = rnd.Next(n);
                int c = rnd.Next(n);
                if (EsZonaInicial(f, c)) continue;
                if (celdas[f, c] == VACIO)
                {
                    celdas[f, c] = PARED;
                    paredesObjetivo--;
                }
            }

            while (ratonesObjetivo > 0)
            {
                int f = rnd.Next(n);
                int c = rnd.Next(n);
                if (EsZonaInicial(f, c)) continue;
                if (celdas[f, c] == VACIO)
                {
                    celdas[f, c] = RATON;
                    ratonesObjetivo--;
                    ratonesRestantes++;
                }
            }
        }
        private bool EsZonaInicial(int f, int c)
        {
            int longitud = 4;
            bool horizontal = (f == 0 && c >= 0 && c < longitud);
            bool vertical = (c == 0 && f >= 0 && f < longitud);
            return horizontal || vertical;
        }

        // Pinta en pantalla la serpiente al iniciar el juego 
        private void PintarSerpienteInicial()
        {
            foreach (var p in serpiente.Cuerpo)
                if (Dentro(p.Fila, p.Columna))
                    celdas[p.Fila, p.Columna] = SERPIENTE;
            Imprimir();
        }

        // Metodo qeu usa el bucle para mostrar todo el rato la matriz del juego, haciendo comporbaciones y mostrando en pantalla
        private void BucleJuego()
        {
            while (true)
            {
                Console.WriteLine("Usa flechas para mover. ESC para salir.");
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.UpArrow:
                        dirFila = -1; dirCol = 0; break;
                    case ConsoleKey.DownArrow:
                        dirFila = 1; dirCol = 0; break;
                    case ConsoleKey.LeftArrow:
                        dirFila = 0; dirCol = -1; break;
                    case ConsoleKey.RightArrow:
                        dirFila = 0; dirCol = 1; break;
                }

                var cabeza = serpiente.Cabeza;
                int nuevaFila = cabeza.Fila + dirFila;
                int nuevaCol = cabeza.Columna + dirCol;

                if (!Dentro(nuevaFila, nuevaCol))
                {
                    Console.WriteLine("Movimiento inválido (fuera de límites).");
                    Imprimir();
                    continue;
                }

                if (celdas[nuevaFila, nuevaCol] == PARED)
                {
                    Console.WriteLine("Movimiento inválido (pared).");
                    Imprimir();
                    continue;
                }

                if (EstaEnSerpiente(nuevaFila, nuevaCol))
                {
                    Console.WriteLine("¡Has perdido! La serpiente chocó consigo misma.");
                    break;
                }

                bool comioRaton = celdas[nuevaFila, nuevaCol] == RATON;
                if (comioRaton)
                    ratonesRestantes--;

                LimpiarSerpiente();
                serpiente.MoverA(nuevaFila, nuevaCol);

                foreach (var p in serpiente.Cuerpo)
                    celdas[p.Fila, p.Columna] = SERPIENTE;

                Imprimir();

                if (ratonesRestantes == 0)
                {
                    Console.WriteLine("¡Has ganado! No quedan más ratones por comer.");
                    break;
                }
            }
        }

        private void LimpiarSerpiente()
        {
            foreach (var p in serpiente.Cuerpo)
                if (celdas[p.Fila, p.Columna] == SERPIENTE)
                    celdas[p.Fila, p.Columna] = VACIO;
        }

        private bool Dentro(int f, int c) => f >= 0 && f < n && c >= 0 && c < n;

        private bool EstaEnSerpiente(int fila, int col)
        {
            foreach (var p in serpiente.Cuerpo)
                if (p.Fila == fila && p.Columna == col)
                    return true;
            return false;
        }

        // Impriem la serpiente, las cuatro casillas con un backgroundd color azul para diferenciarla
        private void Imprimir()
        {
            Console.Clear();
            Console.WriteLine("Juego de la Serpiente (Simplificado)");
            Console.WriteLine($"Ratones restantes: {ratonesRestantes}");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    char celda = celdas[i, j];
                    if (celda == SERPIENTE)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(celda);
                        Console.ResetColor();
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(celda);
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

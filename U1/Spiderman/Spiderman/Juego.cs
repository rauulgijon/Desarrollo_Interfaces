using System;

namespace Spiderman
{
    internal class Juego
    {
        private const int TAM = 15;
        private const int META_FILA = TAM - 1; 
        private const int META_COLUMNA = TAM - 1;
        private const int META_CIVILES = 5; // Número de civiles a rescatar para ganar

        private Ciudad ciudad;
        private Spiderman spiderman;
        private int vidas;
        private int civilesRescatados;
        private bool turnoPerdido;
        private bool saltoDoble;
        private bool salir;
        private Random random;

        public Juego()
        {
            ciudad = new Ciudad();
            spiderman = new Spiderman(0, 0);
            ciudad.RevelarCasilla(spiderman.Fila, spiderman.Columna);

            

            vidas = 3;
            civilesRescatados = 0;
            turnoPerdido = false;
            saltoDoble = false;
            salir = false;
            random = new Random();
        }

        public void Iniciar()
        {
            Console.WriteLine("¡Bienvenido a Spiderman: Misión en Nueva York!");
            Console.WriteLine($"Objetivo: Llegar abajo izquierda rescatando {META_CIVILES} civiles.\n");

            while (!salir)
            {
                ciudad.MostrarVisible();
                MostrarEstadisticas();

                if (turnoPerdido)
                {
                    Console.WriteLine("Pierdes este turno por un enemigo.");
                    turnoPerdido = false;
                    continue;
                }

                MostrarMenu();
                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Debes ingresar un número válido.");
                    continue;
                }

                if (opcion == 0)
                {
                    salir = true;
                    Console.WriteLine("Has decidido salir del juego.");
                    continue;
                }

                int pasos = saltoDoble ? 2 : 1;
                saltoDoble = false;

                for (int i = 0; i < pasos; i++)
                {
                    ciudad.MarcarVisitada(spiderman.Fila, spiderman.Columna);
                    MoverSpiderman(opcion);
                    ciudad.RevelarCasilla(spiderman.Fila, spiderman.Columna);
                    ProcesarEvento();

                    // Mostrar mensaje y estadísticas tras cada movimiento
                    MostrarEstadisticas();
                    Console.ReadKey();

                    if (salir) break;
                }

                // Condición de victoria
                if (spiderman.Fila == META_FILA && spiderman.Columna == META_COLUMNA && civilesRescatados >= META_CIVILES)
                {
                    Console.WriteLine($"¡Has llegado a la meta y rescataste {civilesRescatados} civiles! ¡Victoria!");
                    salir = true;
                }

                // Condición de derrota
                if (vidas <= 0)
                {
                    Console.WriteLine("¡Has perdido todas tus vidas! Fin del juego.");
                    salir = true;
                }
            }
        }

        private void MostrarEstadisticas()
        {
            Console.WriteLine($"\n Vidas: {vidas} | Civiles rescatados: {civilesRescatados}\n");
        }

        private void MostrarMenu()
        {
            Console.WriteLine("1. Mover Arriba");
            Console.WriteLine("2. Mover Abajo");
            Console.WriteLine("3. Mover Izquierda");
            Console.WriteLine("4. Mover Derecha");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opción: ");
        }

        private bool MoverSpiderman(int opcion)
        {
            switch (opcion)
            {
                case 1: spiderman.Mover("arriba", TAM, TAM); break;
                case 2: spiderman.Mover("abajo", TAM, TAM); break;
                case 3: spiderman.Mover("izquierda", TAM, TAM); break;
                case 4: spiderman.Mover("derecha", TAM, TAM); break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            ciudad.RevelarCasilla(spiderman.Fila, spiderman.Columna);

            return false;
        }

        private void ProcesarEvento()
        {
            char evento = ciudad.GetValorInterno(spiderman.Fila, spiderman.Columna);

            switch (evento)
            {
                case 'C':
                    civilesRescatados++;
                    vidas++;
                    Console.WriteLine("¡Has rescatado un civil! +1 vida.");
                    ciudad.SetValorInterno(spiderman.Fila, spiderman.Columna, 'N');
                    break;
                case 'D':
                    vidas--;
                    Retroceder(2);
                    Console.WriteLine("¡Doctor Octopus! -1 vida y retrocedes 2 casillas.");
                    break;
                case 'G':
                    vidas--;
                    turnoPerdido = true;
                    Console.WriteLine("¡Duende Verde! -1 vida y pierdes turno.");
                    break;
                case 'M':
                    MoverAleatoriamente();
                    Console.WriteLine("¡Mysterio! Cambias de posición aleatoriamente.");
                    break;
                case 'B':
                    saltoDoble = true;
                    Console.WriteLine("¡Bonus! Puedes moverte doble en el próximo turno.");
                    ciudad.SetValorInterno(spiderman.Fila, spiderman.Columna, 'N');
                    break;
                case 'N':
                    Console.WriteLine("Casilla neutra. Nada sucede.");
                    break;
            }
        }

        private void Retroceder(int pasos)
        {
            for (int i = 0; i < pasos; i++)
            {
                if (spiderman.Fila > 0)
                    spiderman.Mover("arriba", TAM, TAM);
            }
            ciudad.RevelarCasilla(spiderman.Fila, spiderman.Columna);
        }

        private void MoverAleatoriamente()
        {
            string[] direcciones = { "arriba", "abajo", "izquierda", "derecha" };
            int intentos = 0;
            while (intentos < 10)
            {
                string dir = direcciones[random.Next(direcciones.Length)];
                int filaPrev = spiderman.Fila;
                int colPrev = spiderman.Columna;
                spiderman.Mover(dir, TAM, TAM);
                if (spiderman.Fila != filaPrev || spiderman.Columna != colPrev) break;
                intentos++;
            }
            ciudad.RevelarCasilla(spiderman.Fila, spiderman.Columna);
        }

        
    }
}

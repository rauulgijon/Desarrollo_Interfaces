using System;

namespace Spiderman
{
    /**
     * Controla la lógica principal del juego de Spiderman.
     * Maneja los turnos, movimientos, eventos y condiciones de victoria o derrota.
     */
    internal class Juego
    {
        private Ciudad ciudad;     // Mapa del juego
        private int posI, posJ;    // Posición actual de Spiderman
        private int vidas;         // Vidas del jugador
        private int civiles;       // Civiles rescatados
        private int turnos;        // Contador de turnos
        private bool salir;        // Control del bucle principal
        private bool bonusSalto;   // Indica si el jugador tiene un movimiento doble

        /**
         * Constructor: inicializa las variables del juego.
         */
        public Juego()
        {
            ciudad = new Ciudad();
            posI = 0;
            posJ = 0;
            vidas = 3;
            civiles = 0;
            turnos = 0;
            salir = false;
            bonusSalto = false;
        }

        /**
         * Bucle principal del juego.
         * Se ejecuta hasta que el jugador salga o pierda.
         */
        public void Iniciar()
        {
            while (!salir)
            {
                ciudad.MostrarVisible();
                Operaciones.MostrarEstadisticas(vidas, civiles, turnos);
                Operaciones.MostrarMenu();

                Console.Write("Elige opción: ");
                if (!int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.WriteLine("Entrada no válida.");
                    continue;
                }

                if (opcion == 5)
                {
                    salir = true;
                    Console.WriteLine("Saliendo del juego...");
                    break;
                }

                Mover(opcion);
                turnos++;
                ComprobarVictoriaODerrota();
            }
        }

        /**
         * Gestiona el movimiento de Spiderman según la opción elegida.
         */
        private void Mover(int opcion)
        {
            int pasos = bonusSalto ? 2 : 1;
            bonusSalto = false;
            int tamaño = ciudad.GetTamaño();

            int iActual = posI;
            int jActual = posJ;

            for (int paso = 1; paso <= pasos; paso++)
            {
                int nuevaI = iActual, nuevaJ = jActual;

                switch (opcion)
                {
                    case 1: nuevaJ++; break; // Derecha
                    case 2: nuevaJ--; break; // Izquierda
                    case 3: nuevaI--; break; // Arriba
                    case 4: nuevaI++; break; // Abajo
                    default:
                        Console.WriteLine("Opción inválida.");
                        return;
                }

                if (nuevaI < 0 || nuevaI >= tamaño || nuevaJ < 0 || nuevaJ >= tamaño)
                {
                    Console.WriteLine("No puedes salir del mapa.");
                    break;
                }

                ciudad.RevelarCasilla(nuevaI, nuevaJ);
                ciudad.MarcarVisitadoInterno(iActual, jActual);

                iActual = nuevaI;
                jActual = nuevaJ;
            }

            posI = iActual;
            posJ = jActual;

            ProcesarEvento(ciudad.GetInterna(posI, posJ));
        }

        /**
         * Determina qué ocurre al caer en una casilla (enemigos, civiles, bonus, etc.).
         */
        private void ProcesarEvento(char evento)
        {
            switch (evento)
            {
                case 'C':
                    civiles++;
                    Console.WriteLine("Has rescatado a un civil (+1 punto).");
                    break;

                case 'D':
                    vidas--;
                    Console.WriteLine("¡Doctor Octopus! Pierdes 1 vida y retrocedes 2 casillas.");
                    Retroceder(2);
                    break;

                case 'G':
                    vidas--;
                    Console.WriteLine("¡Duende Verde! Pierdes 1 vida.");
                    break;

                case 'M':
                    Console.WriteLine("¡Mysterio! Te teletransporta a un lugar aleatorio.");
                    Teletransportar();
                    break;

                case 'B':
                    bonusSalto = true;
                    Console.WriteLine("¡Bonus de salto! Te moverás 2 casillas el próximo turno.");
                    break;

                case 'N':
                    Console.WriteLine("Zona tranquila, no ocurre nada.");
                    break;

                case 'X':
                    Console.WriteLine("Casilla ya visitada.");
                    break;
            }

            ciudad.MarcarVisitadoInterno(posI, posJ);
        }

        /**
         * Retrocede una cantidad de casillas, restaurando las anteriores con valores nuevos.
         */
        private void Retroceder(int pasos)
        {
            for (int k = 1; k <= pasos; k++)
            {
                int retroI = posI - k;
                if (retroI >= 0)
                    ciudad.RestaurarCasilla(retroI, posJ);
            }

            posI = Math.Max(0, posI - pasos);
            ciudad.RevelarCasilla(posI, posJ);
        }

        /**
         * Teletransporta al jugador a una posición aleatoria.
         */
        private void Teletransportar()
        {
            Random rnd = new Random();
            int tamaño = ciudad.GetTamaño();
            posI = rnd.Next(0, tamaño);
            posJ = rnd.Next(0, tamaño);
            ciudad.RevelarCasilla(posI, posJ);
        }

        /**
         * Comprueba si el jugador ha ganado o perdido.
         */
        private void ComprobarVictoriaODerrota()
        {
            if (vidas < 0)
            {
                Console.WriteLine("¡Has perdido todas tus vidas! Fin del juego.");
                salir = true;
            }
            else if (civiles >= 10)
            {
                Console.WriteLine("¡Has rescatado a 10 civiles! ¡Victoria!");
                salir = true;
            }
        }
    }
}

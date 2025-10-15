using System;

namespace Spiderman
{
    /**
     * Clase de utilidades que agrupa las operaciones de menú, azar y eventos.
     */
    internal static class Operaciones
    {
        private static readonly Random random = new Random();

        /**
         * Genera un carácter aleatorio (C, D, G, M, N o B).
         */
        public static char RandomChar()
        {
            char[] opciones = { 'C', 'D', 'G', 'M', 'N', 'B' };
            return opciones[random.Next(opciones.Length)];
        }

        /**
         * Genera un carácter aleatorio sin enemigos (C, N o B).
         */
        public static char RandomCharSinEnemigos()
        {
            char[] opciones = { 'C', 'N', 'B' };
            return opciones[random.Next(opciones.Length)];
        }

        /**
         * Muestra el menú de acciones disponibles.
         */
        public static void MostrarMenu()
        {
            Console.WriteLine("1. Mover Derecha");
            Console.WriteLine("2. Mover Izquierda");
            Console.WriteLine("3. Mover Arriba");
            Console.WriteLine("4. Mover Abajo");
            Console.WriteLine("5. Salir");
            Console.Write("Selecciona una opción: ");
        }

        /**
         * Muestra las estadísticas actuales del jugador.
         */
        public static void MostrarEstadisticas(int vidas, int civiles, int turnos)
        {
            Console.WriteLine($"\nVidas: {vidas} | Civiles: {civiles} (10 para ganar) | Turnos: {turnos}\n");
        }
    }
}

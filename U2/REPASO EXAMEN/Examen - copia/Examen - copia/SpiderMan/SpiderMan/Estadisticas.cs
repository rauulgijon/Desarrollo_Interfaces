using System;

namespace SpiderMan
{
    internal class Estadisticas
    {
        // Relación con Tablero: accede a sus miembros estáticos
        public void MostrarResumen()
        {
            Console.WriteLine("----- Estadísticas de la Partida -----");
            Console.WriteLine($"Vidas restantes: {Tablero.vidas}");
            Console.WriteLine($"Civiles salvados: {Tablero.civilesSalvados}");
            Console.WriteLine($"Posición actual: Fila {Tablero.fila}, Columna {Tablero.columna}");
            Console.WriteLine("--------------------------------------");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tablero1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tablero con matrices 4x4 en el que podemos ir hacia todas las direcciones, sin embargo, si llegamos a un borde, salta un error y vuelve a solicitar la dirección,
            // se solicita un menu de opciones para movernos por el tablero, se muestra la posición actual y se puede salir del programa
            // Ademas, se puede salir del tablero solo en el nxn hacia la derecha
            int[,] tablero = new int[4, 4];
            int x = 0, y = 0;
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Posición actual: (" + x + ", " + y + ")");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Mover arriba");
                Console.WriteLine("2. Mover abajo");
                Console.WriteLine("3. Mover izquierda");
                Console.WriteLine("4. Mover derecha");
                Console.WriteLine("5. Salir");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        if (x > 0)
                            x--;
                        else
                            Console.WriteLine("No puedes moverte más arriba.");
                        break;
                    case "2":
                        if (x < 3)
                            x++;
                        else
                            Console.WriteLine("No puedes moverte más abajo.");
                        break;
                    case "3":
                        if (y > 0)
                            y--;
                        else
                            Console.WriteLine("No puedes moverte más a la izquierda.");
                        break;
                    case "4":
                        if (y < 3 || (y == 3 && x == 3))
                            y++;
                        else
                            Console.WriteLine("No puedes moverte más a la derecha.");
                        break;
                    case "5":
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                if (opcion != "5")
                {
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            } while (opcion != "5");
        }
    }
}

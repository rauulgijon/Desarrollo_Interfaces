using System;

namespace Spiderman
{
    internal class Spiderman
    {
        public int Fila { get; private set; }
        public int Columna { get; private set; }

        public Spiderman(int filaInicial, int columnaInicial)
        {
            Fila = filaInicial;
            Columna = columnaInicial;
        }

        // Mueve a Spiderman según la dirección, verificando límites
        public void Mover(string direccion, int maxFilas, int maxColumnas)
        {
            switch (direccion)
            {
                case "arriba":
                    if (Fila > 0) Fila--;
                    break;
                case "abajo":
                    if (Fila < maxFilas - 1) Fila++;
                    break;
                case "izquierda":
                    if (Columna > 0) Columna--;
                    break;
                case "derecha":
                    if (Columna < maxColumnas - 1) Columna++;
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Carretero_Millan_Asier_Examen1
{
    internal class Serpiente
    {
        private readonly int longitud;
        private readonly LinkedList<Posicion> cuerpo; // cabeza = First

        public Serpiente(int longitudInicial)
        {
            longitud = longitudInicial;
            cuerpo = new LinkedList<Posicion>();
        }

        public int Longitud => longitud;
        public IEnumerable<Posicion> Cuerpo => cuerpo;
        public Posicion Cabeza => cuerpo.First.Value;

        public bool Contiene(int fila, int col)
        {
            foreach (var p in cuerpo)
                if (p.Fila == fila && p.Columna == col)
                    return true;
            return false;
        }

        // dirFila, dirCol: (0,1)=derecha, (1,0)=abajo
        public void Inicializar(int filaInicial, int colInicial, int dirFila, int dirCol)
        {
            cuerpo.Clear();
            if (dirFila == 0 && dirCol == 1) // derecha
            {
                for (int c = longitud - 1; c >= 0; c--)
                    cuerpo.AddLast(new Posicion(filaInicial, colInicial + c));
            }
            else if (dirFila == 1 && dirCol == 0) // abajo
            {
                for (int f = longitud - 1; f >= 0; f--)
                    cuerpo.AddLast(new Posicion(filaInicial + f, colInicial));
            }
            else
            {
                // fallback a derecha
                for (int c = longitud - 1; c >= 0; c--)
                    cuerpo.AddLast(new Posicion(filaInicial, colInicial + c));
            }
        }

        public void MoverA(int nuevaFila, int nuevaColumna)
        {
            cuerpo.AddFirst(new Posicion(nuevaFila, nuevaColumna));
            while (cuerpo.Count > longitud)
                cuerpo.RemoveLast();
        }
    }

    internal struct Posicion
    {
        public int Fila;
        public int Columna;
        public Posicion(int f, int c)
        {
            Fila = f; Columna = c;
        }
    }
}

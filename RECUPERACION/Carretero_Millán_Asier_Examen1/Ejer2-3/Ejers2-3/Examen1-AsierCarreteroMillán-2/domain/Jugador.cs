using Examen1_AsierCarreteroMillán_2.persistence.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1_AsierCarreteroMillán_2.domain
{
    internal class Jugador
    {
        private JugadorPersistence jp { get; set; }

        public string nickname { get; set; }
        public DateTime fechadejuego { get; set; }
        public int nivel { get; set; }
        public int puntuacion { get; set; }

        public Jugador()
        {
            jp = new JugadorPersistence();
        }

        public Jugador(string nombre)
        {
            this.nickname = nombre;
            jp = new JugadorPersistence();
        }

        public Jugador(string nombre, DateTime fecha, int nivel, int puntos)
        {
            this.nickname = nombre;
            this.fechadejuego = fecha;
            this.nivel = nivel;
            this.puntuacion = puntos;
            jp = new JugadorPersistence();
        }


        public void insertar()
        {
            jp.insertarJugador(this);
        }

        public void actualizar()
        {
            jp.modificarJugador(this);
        }

        public void eliminar()
        {
            jp.eliminarJugador(this);
        }

    }
}

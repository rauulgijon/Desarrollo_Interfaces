using Ejercicio3.persistence;
using System;
using System.Collections.Generic;

namespace Ejercicio3.domain
{
    class Jugador
    {
        private JugadorPersistence pm { get; set; }
        private int id;
        private String nombre;
        private int puntuacion;
        private int nivel;
        private String fechanac;
        private List<Jugador> lspersonas;
        public int Id { get; set; }

        public String Nombre { get; set; }

        public int Puntuacion { get; set; }

        public int Nivel { get; set; }

        public String Fechanac { get; set; }

        public Jugador(int id, String nombre, int puntuacion, String fechanac, int nivel)
        {
            Id = id;
            Nombre = nombre;
            Nivel = nivel;
            Puntuacion = puntuacion;
            Fechanac = fechanac;
            pm = new JugadorPersistence();
        }

        public Jugador(String nombre, int puntuacion, String fechanac, int nivel)
        {
            Nombre = nombre;
            Nivel = nivel;
            Puntuacion = puntuacion;
            Fechanac = fechanac;
            pm = new JugadorPersistence();
        }

        public Jugador()
        {
            pm = new JugadorPersistence();
        }

        public Jugador(int id)
        {
            Id = id;
            pm = new JugadorPersistence();
        }

        public List<Jugador> getLspersonas()
        {
            lspersonas = JugadorPersistence.leerPersonas();
            return lspersonas;
        }

        public void insertar()
        {
            pm.insertarPersona(this);
        }

        public void actualizar()
        {
            pm.actualizarPersona(this);
        }

        public void eliminar()
        {
            pm.eliminarPersona(this.Id);
        }
    }
}
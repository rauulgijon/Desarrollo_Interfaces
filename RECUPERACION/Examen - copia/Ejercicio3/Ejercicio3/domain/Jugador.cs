using Ejercicio3.persistence;
using System;
using System.Collections.Generic;

namespace Ejercicio3.domain
{
    class Jugador
    {
        private JugadorPersistence pm { get; set; }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Puntuacion { get; set; }

        // --- NUEVOS CAMPOS ---
        public string Email { get; set; }
        public bool EsVip { get; set; }
        public string Turno { get; set; } // "Mañana" o "Tarde"
        // ---------------------

        public int Nivel { get; set; }
        public string Fechanac { get; set; }

        // Constructor COMPLETO (Para leer de BDD con ID)
        public Jugador(int id, string nombre, int puntuacion, string email, bool esVip, string turno, string fechanac, int nivel)
        {
            Id = id;
            Nombre = nombre;
            Puntuacion = puntuacion;
            Email = email;
            EsVip = esVip;
            Turno = turno;
            Fechanac = fechanac;
            Nivel = nivel;
            pm = new JugadorPersistence();
        }

        // Constructor para INSERTAR (Sin ID, autogenerado)
        public Jugador(string nombre, int puntuacion, string email, bool esVip, string turno, string fechanac, int nivel)
        {
            Nombre = nombre;
            Puntuacion = puntuacion;
            Email = email;
            EsVip = esVip;
            Turno = turno;
            Fechanac = fechanac;
            Nivel = nivel;
            pm = new JugadorPersistence();
        }

        // Constructor vacío auxiliar
        public Jugador()
        {
            pm = new JugadorPersistence();
        }

        // Constructor solo con Nick (usado para iniciar juego rápido)
        public Jugador(string nombre, int puntuacion, string fechanac, int nivel)
        {
            Nombre = nombre;
            Puntuacion = puntuacion;
            Fechanac = fechanac;
            Nivel = nivel;
            // Valores por defecto para lo demás
            Email = "";
            EsVip = false;
            Turno = "Mañana";
            pm = new JugadorPersistence();
        }

        public void insertar() => pm.insertarPersona(this);
        public void actualizar() => pm.actualizarPersona(this);
        public void eliminar() => pm.eliminarPersona(this.Id);
    }
}
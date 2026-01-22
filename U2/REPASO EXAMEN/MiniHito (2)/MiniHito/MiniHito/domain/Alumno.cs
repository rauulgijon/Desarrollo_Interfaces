using MiniHito.persistence;
using System;
using System.Collections.Generic;

namespace MiniHito.domain
{
    public class Alumno
    {
        private AlumnoPersistence am { get; set; }

        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public int Especialidad { get; set; }

<<<<<<< Updated upstream
        public int Grupo { get; set; }

=======
        // --- NUEVO: Propiedad Grupo ---
        public int Grupo { get; set; }

        // --- NUEVO: Helper para mostrar nombre completo en ListBox ---
>>>>>>> Stashed changes
        public string NombreCompleto
        {
            get { return Nombre + " " + Apellidos; }
        }

        public Alumno()
        {
            am = new AlumnoPersistence();
        }

        // Constructor completo actualizado
        public Alumno(int id, String nombre, String apellidos, int especialidad, int grupo)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            Especialidad = especialidad;
            Grupo = grupo; // Asignar grupo
            am = new AlumnoPersistence();
        }

<<<<<<< Updated upstream
=======
        // Constructor compatible con tu código anterior (asigna grupo 0)
>>>>>>> Stashed changes
        public Alumno(int id, String nombre, String apellidos, int especialidad)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            Especialidad = especialidad;
            Grupo = 0;
            am = new AlumnoPersistence();
        }

        public Alumno(String nombre, String apellidos, int especialidad)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Especialidad = especialidad;
            Grupo = 0;
            am = new AlumnoPersistence();
        }

        public void insertar() => am.insertarPersona(this);
        public void actualizar() => am.actualizarPersona(this);
        public void eliminar() => am.eliminarPersona(this.Id);
    }
}
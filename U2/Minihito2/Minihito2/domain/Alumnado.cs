using DatagridPersonas.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatagridPersonas.domain
{
    internal class Alumnado
    {
        private int id;
        private String nombre;
        private String apellidos;
        private int curso;
        private int especialidad;
        private static List<Alumnado> listaAlumnos;
        public PersonaPersistence pm;

        public Alumnado(int id, string nombre, string apellidos, int curso)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.curso = curso;
        }


        public Alumnado(string nombre, string apellidos, int curso)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.curso = curso;
            pm = new PersonaPersistence();

        }

        public Alumnado()
        {
            pm = new PersonaPersistence();
        }

        public Alumnado(int id)
        {
            pm = new PersonaPersistence();
            this.id = id;
        }

        public String Nombre { get => nombre; set => nombre = value; }
        public String Apellido { get => apellidos; set => apellidos = value; }
        public int Curso { get => curso; set => curso = value; }
        public int Id { get => id; set => id = value; }
        public int Especialidad { get => especialidad; set => especialidad = value; }



        public static List<Alumnado> getAlumnado()
        {
            // 1. Crear una instancia de PersonaPersistence
            PersonaPersistence persistencia = new PersonaPersistence();

            listaAlumnos = persistencia.leerAlumnado();

            return listaAlumnos;
        }

        public List<Alumnado> getListaAlumnado()
        {
            listaAlumnos = pm.leerAlumnado();
            return listaAlumnos;
        }

        public void insertar()
        {
            pm.insertarPersona(this);
        }

        public void last()
        {
            pm.lastId(this);
        }
    }
}
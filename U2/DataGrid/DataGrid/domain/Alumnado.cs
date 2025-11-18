using DataGrid.personapersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrid.domain
{
    internal class Alumnado
    {
        private int id;
        private String nombre;
        private String apellido;
        private int curso;
        private int especialidad;
        private List<Alumnado> alumnos;
        public Alumnado(String nombre, String apellido, int curso)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.curso = curso;

        }

        public Alumnado()
        {
            this.alumnos = new List<Alumnado>();
        }
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public String Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Curso
        {
            get { return curso; }
            set { curso = value; }
        }

        public int Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }


        public List<Alumnado> getAlumnado()
        {
            alumnos = PersonaPersistence.leerAlumnado();
            return alumnos;

        }
}
}

using System;
using System.Collections.Generic;
using MasterDetailMySqlTemplate.Models;

namespace MasterDetailMySqlTemplate.Persistence
{
    public class AlumnoPersistence
    {
        public static List<Alumno> leerAlumnos()
        {
            List<Alumno> listaAlumnos = new List<Alumno>();

            List<Object> aux = DBBroker.obtenerAgente().leer("SELECT ID_ALUMNO, NOMBRE, APELLIDO, CURSO, CORREO, CICLO, ID_GRUPO FROM alumno;");

            foreach (List<Object> fila in aux)
            {
                Alumno a = new Alumno();
                a.IdAlumno = Convert.ToInt32(fila[0]);
                a.Nombre = fila[1].ToString();
                a.Apellido = fila[2].ToString();
                a.Curso = fila[3].ToString();
                a.Correo = fila[4].ToString();
                a.Ciclo = fila[5].ToString();

                if (!string.IsNullOrEmpty(fila[6].ToString()))
                {
                    a.IdGrupo = Convert.ToInt32(fila[6]);
                }

                listaAlumnos.Add(a);
            }

            return listaAlumnos;
        }
    }
}
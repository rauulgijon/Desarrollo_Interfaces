using Examen.persistence.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.domain
{
    internal class Consulta
    {
        private ConsultaPersistence pm { get; set; }
        private int id;
        private int idmascota;
        private int idveterinario;
        private String diagnostico;
        private String fechaconsulta;
        private List<Consulta> lsConsultas;
        public int Id { get; set; }

        public String Diagnostico { get; set; }

        public String Fechaconsulta { get; set; }

        public int Idmascota { get; set; }
        public int Idveterinario { get; set; }


        public Consulta(int id, String fechaconsulta, String diagnostico, int idmascota, int idveterinario)
        {
            Id = id;
            Diagnostico = diagnostico;
            Fechaconsulta = fechaconsulta;
            Idmascota = idmascota;
            Idveterinario = idveterinario;

            pm = new ConsultaPersistence();
        }

        public Consulta(String fechaconsulta, String diagnostico, int idmascota, int idveterinario)
        {
            Diagnostico = diagnostico;
            Fechaconsulta = fechaconsulta;
            Idmascota = idmascota;
            Idveterinario = idveterinario;

            pm = new ConsultaPersistence();
        }
        public Consulta(String fechaconsulta, String diagnostico)
        {
            Diagnostico = diagnostico;
            Fechaconsulta = fechaconsulta;

            pm = new ConsultaPersistence();
        }

        public Consulta()
        {
            pm = new ConsultaPersistence();
        }

        public Consulta(int id)
        {
            Id = id;
            pm = new ConsultaPersistence();
        }

        public List<Consulta> getLsConsultas()
        {
            lsConsultas = ConsultaPersistence.leerConsultas();
            return lsConsultas;
        }

        public void insertar()
        {
            pm.insertarConsulta(this);
        }

        public void actualizar()
        {
            pm.actualizarConsulta(this);
        }

        public void eliminar()
        {
            pm.eliminarConsulta(this.Id);
        }
    }
}

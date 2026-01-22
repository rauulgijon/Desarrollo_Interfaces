using MiniHito.persistence;
using System;

namespace MiniHito.domain
{
    public class Empresa
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        private EmpresaPersistence pm;

        public Empresa()
        {
            pm = new EmpresaPersistence();
        }

        public Empresa(string razonSocial, string direccion, string ciudad, string telefono, string correo)
        {
            RazonSocial = razonSocial;
            Direccion = direccion;
            Ciudad = ciudad;
            Telefono = telefono;
            Correo = correo;
            pm = new EmpresaPersistence();
        }
        public Empresa(int id, string razonSocial, string direccion, string ciudad, string telefono, string correo)
        {
            Id = id;
            RazonSocial = razonSocial;
            Direccion = direccion;
            Ciudad = ciudad;
            Telefono = telefono;
            Correo = correo;
            pm = new EmpresaPersistence();
        }

        // Métodos CRUD que llaman a la persistencia
        public void insertar() => pm.InsertarEmpresa(this);
        public void actualizar() => pm.ActualizarEmpresa(this);
        public void eliminar() => pm.EliminarEmpresa(this.Id);

        public override string ToString()
        {
            return RazonSocial;
        }
    }
}
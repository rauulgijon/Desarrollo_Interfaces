using Examen.persistence;
using System;
using System.Collections.Generic;

namespace Examen.domain
{
    class Cliente
    {
        private ClientePersistence pm { get; set; }
        private int id;
        private String nombre;
        private String direccion;
        private String telefono;
        private String email;
        private List<Cliente> lsclientes;
        public int Id { get; set; }

        public String Nombre { get; set; }

        public String Direccion { get; set; }

        public String Telefono { get; set; }

        public String Email { get; set; }

        public Cliente(int id, String nombre, String direccion, String telefono, String email)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            pm = new ClientePersistence();
        }

        public Cliente(String nombre, String direccion, String telefono, String email)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            pm = new ClientePersistence();
        }

        public Cliente()
        {
            pm = new ClientePersistence();
        }

        public Cliente(int id)
        {
            Id = id;
            pm = new ClientePersistence();
        }

        public List<Cliente> getLspersonas()
        {
            lsclientes = ClientePersistence.leerClientes();
            return lsclientes;
        }

        public void insertar()
        {
            pm.insertarCliente(this);
        }

        public void actualizar()
        {
            pm.actualizarCliente(this);
        }

        public void eliminar()
        {
            pm.eliminarCliente(this.Id);
        }
    }
}
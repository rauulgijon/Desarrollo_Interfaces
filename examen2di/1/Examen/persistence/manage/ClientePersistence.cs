using Examen.domain;
using ExampleMVCnoDatabase.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Examen.persistence
{
    class ClientePersistence
    {
        
            private DataTable clienteTable { get; set; }
            public ClientePersistence()
            {
                clienteTable = new DataTable();

            }
            public static List<Cliente> leerClientes()
            {
                Cliente p = null;
                List<Object> aux = DBBroker.obtenerAgente().leer("Select * from examen.cliente;");
                List<Cliente> clientes = new List<Cliente>();
                foreach (List<Object> fila in aux)
                {
                    p = new Cliente(Convert.ToInt32(fila[0]), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString());
                    clientes.Add(p);
                    Console.WriteLine(p.ToString());
                }
                return clientes;
            }

            public void insertarCliente(Cliente cliente)
            {
                string sql = "INSERT INTO examen.cliente (nombre, direccion, telefono, email) VALUES ('" +
                             cliente.Nombre + "', '" +  
                             cliente.Direccion + "', '" +
                             cliente.Telefono + "', '" +
                             cliente.Email + "'); ";

                int filasAfectadas = DBBroker.obtenerAgente().modificar(sql);

                // Si filasAfectadas es 0, significa que hubo un error o no se insertó nada
                if (filasAfectadas == 0)
                {
                    throw new Exception("No se ha podido insertar el cliente en la base de datos.");
                }
            }

            public void actualizarCliente(Cliente cliente)
            {
                string sql = "UPDATE examen.cliente SET " +
                             "nombre = '" + cliente.Nombre + "', " +
                             "direccion = '" + cliente.Direccion + "', " +
                             "telefono = '" + cliente.Telefono + "', " +
                             "email = '" + cliente.Email + "' " +
                             "WHERE id = " + cliente.Id + ";";
                int a = DBBroker.obtenerAgente().modificar(sql);
            }

            public void eliminarCliente(int id)
            {
                string sql = "DELETE FROM examen.cliente WHERE id = " + id + ";";
                int a = DBBroker.obtenerAgente().modificar(sql);
            }

            /*
            //simulates reading from a database
            public static List<Persona> leerPersonas()
            {
                List<Persona> personas = new List<Persona>();
                personas.Add(new Persona("Luis", "Rodríguez", 40));
                personas.Add(new Persona("Pepe", "Sanchez", 60));
                personas.Add(new Persona("Jose", "Mondongo", 10));
                personas.Add(new Persona("Gabriel", "Hernandez", 86));
                personas.Add(new Persona("Asier", "Carretero", 32));
                personas.Add(new Persona("Cristobal", "Colon", 344));
                return personas;
            }*/
        }
        
    }
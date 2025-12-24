using DatagridPersonas.domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatagridPersonas.persistence
{
    internal class PersonaPersistence
    {
        private string path;
        private List<Persona> ListaPersonas { get; set; }

        // Clase auxiliar para la estructura del JSON
        private class RootObject
        {
            public List<PersonaData> Personas { get; set; }
        }

        private class PersonaData
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int Edad { get; set; }
        }

        public PersonaPersistence()
        {
            ListaPersonas = new List<Persona>();

            // Ruta del archivo JSON en la carpeta del proyecto
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "personas.json");

            // Si no existe el archivo, crear uno con datos de ejemplo
            if (!File.Exists(path))
            {
                CrearArchivoInicial();
            }
        }

        /// <summary>
        /// Crea un archivo JSON inicial con datos de ejemplo
        /// </summary>
        private void CrearArchivoInicial()
        {
            try
            {
                var datosIniciales = new RootObject
                {
                    Personas = new List<PersonaData>
                    {
                        new PersonaData { Id = 1, Nombre = "Manuel", Apellidos = "Ruiz", Edad = 19 },
                        new PersonaData { Id = 2, Nombre = "Ismael", Apellidos = "Navarro", Edad = 20 },
                        new PersonaData { Id = 3, Nombre = "Rubén", Apellidos = "Rueda", Edad = 21 },
                        new PersonaData { Id = 4, Nombre = "Raúl", Apellidos = "Gijón", Edad = 19 },
                        new PersonaData { Id = 5, Nombre = "Gabriel", Apellidos = "Hernández", Edad = 21 },
                        new PersonaData { Id = 6, Nombre = "Asier", Apellidos = "Carretero", Edad = 21 },
                        new PersonaData { Id = 7, Nombre = "Adrian", Apellidos = "Luque", Edad = 19 },
                        new PersonaData { Id = 8, Nombre = "Manuel Alejandro", Apellidos = "García", Edad = 24 }
                    }
                };

                string json = JsonConvert.SerializeObject(datosIniciales, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear archivo JSON inicial: {ex.Message}");
            }
        }

        /// <summary>
        /// Lee todas las personas del archivo JSON
        /// </summary>
        public List<Persona> leerPersonas()
        {
            ListaPersonas.Clear();

            try
            {
                if (!File.Exists(path))
                {
                    throw new Exception($"El archivo no existe en la ruta: {path}");
                }

                string jsonContent = File.ReadAllText(path);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    throw new Exception("El archivo JSON está vacío");
                }

                var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonContent);

                if (rootObject == null || rootObject.Personas == null)
                {
                    throw new Exception("El archivo JSON no tiene un formato válido");
                }

                ListaPersonas = rootObject.Personas
                    .OrderBy(p => p.Id)
                    .Select(p => new Persona(p.Id, p.Nombre, p.Apellidos, p.Edad))
                    .ToList();
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error al deserializar JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer personas: {ex.Message}");
            }

            return ListaPersonas;
        }

        /// <summary>
        /// Guarda todas las personas en el archivo JSON
        /// </summary>
        private void guardarPersonas(List<Persona> personas)
        {
            try
            {
                var rootObject = new RootObject
                {
                    Personas = personas.Select(p => new PersonaData
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Apellidos = p.Apellido,
                        Edad = p.Edad
                    }).ToList()
                };

                string json = JsonConvert.SerializeObject(rootObject, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar personas: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserta una nueva persona
        /// </summary>
        public void insertarPersona(Persona persona)
        {
            try
            {
                var personas = leerPersonas();
                personas.Add(persona);
                guardarPersonas(personas);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar persona: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza una persona existente
        /// </summary>
        public void actualizarPersona(Persona persona)
        {
            try
            {
                var personas = leerPersonas();
                var personaExistente = personas.FirstOrDefault(p => p.Id == persona.Id);

                if (personaExistente != null)
                {
                    personaExistente.Nombre = persona.Nombre;
                    personaExistente.Apellido = persona.Apellido;
                    personaExistente.Edad = persona.Edad;
                    guardarPersonas(personas);
                }
                else
                {
                    throw new Exception("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar persona: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una persona
        /// </summary>
        public void eliminarPersona(int id)
        {
            try
            {
                var personas = leerPersonas();
                var persona = personas.FirstOrDefault(p => p.Id == id);

                if (persona != null)
                {
                    personas.Remove(persona);
                    guardarPersonas(personas);
                }
                else
                {
                    throw new Exception("Persona no encontrada");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar persona: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene el siguiente ID disponible
        /// </summary>
        public void lastId(Persona p)
        {
            try
            {
                var personas = leerPersonas();

                if (personas.Count == 0)
                {
                    p.Id = 1;
                }
                else
                {
                    p.Id = personas.Max(per => per.Id) + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener último ID: {ex.Message}");
            }
        }

        /// <summary>
        /// Valida credenciales de usuario
        /// </summary>
        public bool ValidarUsuario(string usuario, string password)
        {
            // Usuarios predefinidos
            if (usuario == "admin" && password == "admin123")
                return true;

            if (usuario == "usuario" && password == "12345")
                return true;

            return false;
        }

        /// <summary>
        /// Obtiene el nombre del usuario
        /// </summary>
        public string ObtenerNombreUsuario(string usuario)
        {
            if (usuario == "admin")
                return "Administrador";

            if (usuario == "usuario")
                return "Usuario Normal";

            return usuario;
        }
    }
}
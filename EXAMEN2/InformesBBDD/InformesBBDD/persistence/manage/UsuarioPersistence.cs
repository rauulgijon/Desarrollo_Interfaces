using Minihito2.domain;
using Minihito2.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minihito2.persistence.manage
{
    internal class UsuarioPersistence
    {
        private DBBroker agente;

        public UsuarioPersistence()
        {
            agente = DBBroker.obtenerAgente();
        }

        /// <summary>
        /// Valida un usuario contra la base de datos
        /// </summary>
        /// <param name="username">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <returns>Objeto Usuario si es válido, null si no existe</returns>
        public Usuario validarUsuario(string username, string password)
        {
            try
            {
                string sql = $"SELECT ID_USUARIO, USERNAME, PASSWORD, ROLES, ACTIVO FROM usuario " +
                             $"WHERE USERNAME = '{username}' AND PASSWORD = '{password}' AND ACTIVO = 1";

                List<Object> resultado = agente.leer(sql);

                if (resultado.Count > 0)
                {
                    // Si hay resultados, retornamos el usuario encontrado
                    List<Object> fila = (List<Object>)resultado[0];
                    Usuario usuario = new Usuario(
                        int.Parse(fila[0].ToString()),
                        fila[1].ToString(),
                        fila[2].ToString(),
                        fila[3].ToString(),
                        fila[4].ToString() == "1" ? true : false
                    );
                    return usuario;
                }
                else
                {
                    // No se encontró el usuario o las credenciales son incorrectas
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al validar usuario: {ex.Message}", "Error", 
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios activos
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        public List<Usuario> obtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                string sql = "SELECT ID_USUARIO, USERNAME, PASSWORD, ROLES, ACTIVO FROM usuario WHERE ACTIVO = 1";
                List<Object> resultado = agente.leer(sql);

                foreach (List<Object> fila in resultado)
                {
                    Usuario usuario = new Usuario(
                        int.Parse(fila[0].ToString()),
                        fila[1].ToString(),
                        fila[2].ToString(),
                        fila[3].ToString(),
                        fila[4].ToString() == "1" ? true : false
                    );
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al obtener usuarios: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return usuarios;
        }

        /// <summary>
        /// Obtiene todos los usuarios (sin filtrar por estado activo)
        /// </summary>
        /// <returns>Lista de todos los usuarios</returns>
        public List<Usuario> obtenerTodosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                string sql = "SELECT ID_USUARIO, USERNAME, PASSWORD, ROLES, ACTIVO FROM usuario";
                List<Object> resultado = agente.leer(sql);

                foreach (List<Object> fila in resultado)
                {
                    Usuario usuario = new Usuario(
                        int.Parse(fila[0].ToString()),
                        fila[1].ToString(),
                        fila[2].ToString(),
                        fila[3].ToString(),
                        fila[4].ToString() == "1" ? true : false
                    );
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al obtener todos los usuarios: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return usuarios;
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos
        /// </summary>
        public void insertarUsuario(Usuario usuario)
        {
            try
            {
                string sql = $"INSERT INTO usuario (USERNAME, PASSWORD, ROLES, ACTIVO) " +
                             $"VALUES ('{usuario.username}', '{usuario.password}', '{usuario.roles}', 1)";
                agente.modificar(sql);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al insertar usuario: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        public void actualizarUsuario(Usuario usuario)
        {
            try
            {
                string sql = $"UPDATE usuario SET USERNAME = '{usuario.username}', " +
                             $"PASSWORD = '{usuario.password}', ROLES = '{usuario.roles}', " +
                             $"ACTIVO = {(usuario.activo ? 1 : 0)} WHERE ID_USUARIO = {usuario.idUsuario}";
                agente.modificar(sql);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al actualizar usuario: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Elimina un usuario de la base de datos
        /// </summary>
        public void eliminarUsuario(int idUsuario)
        {
            try
            {
                string sql = $"UPDATE usuario SET ACTIVO = 0 WHERE ID_USUARIO = {idUsuario}";
                agente.modificar(sql);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error al eliminar usuario: {ex.Message}", "Error",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}

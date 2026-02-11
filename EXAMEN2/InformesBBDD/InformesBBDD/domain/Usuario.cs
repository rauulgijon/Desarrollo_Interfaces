using Minihito2.persistence.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minihito2.domain
{
    public class Usuario
    {
        private UsuarioPersistence up { get; set; }

        public int idUsuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string roles { get; set; }
        public bool activo { get; set; }

        public Usuario()
        {
            up = new UsuarioPersistence();
        }

        public Usuario(string username, string password)
        {
            this.username = username;
            this.password = password;
            up = new UsuarioPersistence();
        }

        public Usuario(int idUsuario, string username, string password, string roles, bool activo)
        {
            this.idUsuario = idUsuario;
            this.username = username;
            this.password = password;
            this.roles = roles;
            this.activo = activo;
            up = new UsuarioPersistence();
        }

        /// <summary>
        /// Valida las credenciales del usuario contra la base de datos
        /// </summary>
        /// <returns>True si el usuario existe y es válido, False en caso contrario</returns>
        public Usuario validarCredenciales()
        {
            return up.validarUsuario(this.username, this.password);
        }
    }
}

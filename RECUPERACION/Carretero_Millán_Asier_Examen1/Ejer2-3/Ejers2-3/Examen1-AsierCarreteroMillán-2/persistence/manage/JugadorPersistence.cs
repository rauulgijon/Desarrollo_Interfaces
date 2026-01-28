using Examen1_AsierCarreteroMillán_2.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1_AsierCarreteroMillán_2.persistence.manage
{
    internal class JugadorPersistence
    {

        public DataTable table { get; set; }
        public List<Jugador> listaJugador { get; set; }

        public JugadorPersistence()
        {
            listaJugador = new List<Jugador>();
        }

        public List<Jugador> leerJugador()
        {
            listaJugador.Clear();
            var lpeople = DBBroker.obtenerAgente().leer(
                "select nickname, fechadejuego, nivel, puntuacion from jugador order by puntuacion DESC");

            foreach (List<object> aux in lpeople)
            {
                var j = new Jugador(aux[0].ToString());
                j.fechadejuego = DateTime.Parse(aux[1].ToString(), CultureInfo.GetCultureInfo("es-ES"));
                j.nivel = int.Parse(aux[2].ToString());
                j.puntuacion = int.Parse(aux[3].ToString());
                listaJugador.Add(j);
            }
            return listaJugador;
        }

        public void insertarJugador(Jugador j)
        {
            var cmd = new MySqlCommand("INSERT INTO jugador (nickname, fechadejuego, nivel, puntuacion) VALUES (@nickname, @fechadejuego, @nivel, @puntuacion)");
            cmd.Parameters.Add("@nickname", MySqlDbType.VarChar).Value = j.nickname;
            cmd.Parameters.Add("@fechadejuego", MySqlDbType.DateTime).Value = j.fechadejuego;
            cmd.Parameters.Add("@nivel", MySqlDbType.Int32).Value = j.nivel;
            cmd.Parameters.Add("@puntuacion", MySqlDbType.Int32).Value = j.puntuacion;
            DBBroker.obtenerAgente().modificar(cmd);
        }

        public void eliminarJugador(Jugador j)
        {
            var cmd = new MySqlCommand("DELETE FROM jugador WHERE nickname=@nombre");
            cmd.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = j.nickname;
            DBBroker.obtenerAgente().modificar(cmd);
        }

        public void modificarJugador(Jugador j)
        {
            var cmd = new MySqlCommand("UPDATE jugador SET nickname=@nickname, fechadejuego=@fechadejuego, nivel=@nivel, puntuacion=@puntuacion WHERE nickname=@nickname");
            cmd.Parameters.Add("@nickname", MySqlDbType.VarChar).Value = j.nickname;
            cmd.Parameters.Add("@fechadejuego", MySqlDbType.DateTime).Value = j.fechadejuego;
            cmd.Parameters.Add("@nivel", MySqlDbType.Int32).Value = j.nivel;
            cmd.Parameters.Add("@puntuacion", MySqlDbType.Int32).Value = j.puntuacion;
            DBBroker.obtenerAgente().modificar(cmd);
        }

    }
}

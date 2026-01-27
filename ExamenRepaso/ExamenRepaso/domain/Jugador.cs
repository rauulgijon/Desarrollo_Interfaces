using ExamenRepaso.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenRepaso.domain
{
    internal class Jugador
    {
        private JugadorPersistence pm {  get; set; }
        private int id;
        private string nombre;
        private int puntuacion;
        private int nivel;
        private String fechanac;
        private List<Jugador> lspersonas;

        
    }
}

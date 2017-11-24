using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.EstadoJuego
{
    [Serializable]
    public class Juego
    {

        public int[] puntajes;
        public int nivel;
        public int segundosAumento;
        public int[] puntosEnContra;
        public int aumentos;
        public int estado;

    }
}

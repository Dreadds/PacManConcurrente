using LibreriaComunes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.EstadoJuego
{
    [Serializable]
    public class EstadoJuego
    {

        public Tablero tablero;
        public Personaje[] personajes;
        public Dictionary<Tuple<int, int>, Galleta> galletas;
        public LinkedList<Fantasma> fantasmas;
        public Juego juego;

        public override string ToString()
        {
            string resultado = "";
            resultado += "nivel: " + juego.nivel + "\n";
            resultado += "posx: " + personajes.ElementAt(0).x + "\n";
            resultado += "posy: " + personajes.ElementAt(1).y + "\n";
            resultado += "numtblero: " + tablero.numero + "\n";
            resultado += "num fantasmas: " + fantasmas.Count + "\n";
            resultado += "num galletas: " + galletas.Count + "\n";

            return resultado;
        }

    }

}

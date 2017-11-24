using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Comunicacion
{
    [Serializable]
    public class MetodoEntrada
    {

        public int id;
        public bool arriba;
        public bool abajo;
        public bool izquierda;
        public bool derecha;

        /// <summary>
        ///     Dar la orden derecha (presionar)
        /// </summary>
        public void pDerecha()
        {
            derecha = true;
        }

        /// <summary>
        ///     Dar la orden izquierda (presionar)
        /// </summary>
        public void pIzquierda()
        {
            izquierda = true;
        }

        /// <summary>
        ///     Dar la orden arriba (presionar)
        /// </summary>
        public void pArriba()
        {
            arriba = true;
        }

        /// <summary>
        ///     Dar la orden abajo (presionar)
        /// </summary>
        public void pAbajo()
        {
            abajo = true;
        }

        /// <summary>
        ///     Deja el método como si no se hubiese ingresado ninguna orden, para que no permanezca como si continuamente se
        ///     dejara la misma.
        /// </summary>
        public void liberar()
        {
            izquierda = false;
            derecha = false;
            arriba = false;
            abajo = false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Entidades
{
    /// <summary>
    ///     Clase que corresponde al personaje que manejará el usuario, es un actor con operaciones con vidas y visibilidad.
    ///     Las velocidades se escalan con 0.3 porque si se manejan como en la tabla de requerimientos, el juego
    ///     no sería posible de jugar.
    /// </summary>
    [Serializable]
    public class Personaje : Actor
    {

        public bool visible;

        public int vidas;
        
        public int vMostrar()
        {
            return (int)(velocidad / 0.3);
        }
        
    }
}

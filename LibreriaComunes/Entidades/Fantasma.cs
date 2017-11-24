using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Entidades
{
    /// <summary>
    ///     Clase Fantasma, que es un autor con operaciones especificas de los fantasmas,
    ///     un modo asustado y propiedades como poder y nivel de velocidad.
    ///     Inicialmente se le da una dirección aleatoria, para esto se usa el Random.
    /// </summary>
    [Serializable]
    public class Fantasma : Actor
    {
        public int tipo;

        public int modo;

    }
}

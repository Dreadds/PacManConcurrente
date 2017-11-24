using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Comunicacion
{
    [Serializable]
    public class MensajeChat
    {
        public string mensaje { get; set; }
        public string nombreUsuario { get; set; }
        public int codigoUsuario { get; set; }
    }
}

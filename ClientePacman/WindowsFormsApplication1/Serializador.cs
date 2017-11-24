using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public static class Serializador
    {

        public static Mensaje Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return new Mensaje { Data = memoryStream.ToArray() };
            }
        }

        public static object Deserialize(Mensaje mensaje)
        {
            using (var memoryStream = new MemoryStream(mensaje.Data))
                return (new BinaryFormatter()).Deserialize(memoryStream);
        }

    }
}

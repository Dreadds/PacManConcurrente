using LibreriaComunes.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Cliente
    {

        public AyudanteCliente ac;
        public VentanaJuego f;
        public Panel panel;
        public static int id;
        public Queue<LibreriaComunes.Comunicacion.MensajeChat> mensajesChat;

        public Cliente(VentanaJuego f, TcpClient tc, string nombre)
        {
            this.f = f;
            //TcpClient tc = new TcpClient("localhost", 1993);
            recibirYEnviarDatos(tc, nombre);
            //recibir id
            /*byte[] datos = new byte[256];
            NetworkStream stream = tc.GetStream();
            string respuesta = string.Empty;
            Int32 bytes = stream.Read(datos, 0, datos.Length);
            respuesta = System.Text.Encoding.ASCII.GetString(datos, 0, bytes);
            id = int.Parse(respuesta);*/
            //termina recibir id

            ac = new AyudanteCliente(tc, this);
            mensajesChat = new Queue<MensajeChat>();
        }

        public void setPanel(Panel p)
        {
            panel = p;
        }

        public void actualizarJuego(LibreriaComunes.EstadoJuego.EstadoJuego ej)
        {
            f.actualizarEstadoJuego(ej);
            panel.Invalidate();
        }

        public void enviarInstruccion(MetodoEntrada e)
        {
            ac.enviarEntrada(e);
            e.liberar();
        }

        private void recibirYEnviarDatos(TcpClient tc, string nombre)
        {
            byte[] readMsgLen;
            int dataLen;
            byte[] readMsgData;

            NetworkStream stream = tc.GetStream();

            readMsgLen = new byte[4];
            stream.Read(readMsgLen, 0, 4);

            dataLen = BitConverter.ToInt32(readMsgLen, 0);
            readMsgData = new byte[dataLen];
            safeRead(readMsgData, dataLen, stream);

            procesarMensaje(readMsgData);

            enviarNombre(nombre, stream);
        }

        private void safeRead(byte[] userData, int len, NetworkStream stream)
        {
            int dataRead = 0;
            do
            {
                dataRead += stream.Read(userData, dataRead, len - dataRead);
            } while (dataRead < len);
        }

        private void procesarMensaje(byte[] bytes)
        {
            Mensaje m = new Mensaje { Data = bytes };
            object obj = Serializador.Deserialize(m);
            if (obj is int)
            {
                id = Convert.ToInt32(obj);
                f.entrada.id = id;
            }
        }

        private void enviarNombre(string nombre, NetworkStream stream)
        {
            // Send the message to the connected TcpServer. 
            // The write flushes the stream automatically here

            Mensaje m = Serializador.Serialize(nombre);

            //Escribir primero la longitud del mensaje
            byte[] userDataLen = BitConverter.GetBytes(m.Data.Length);
            stream.Write(userDataLen, 0, 4);

            //escribir el mensaje
            stream.Write(m.Data, 0, m.Data.Length);
            Console.WriteLine("Se recibe " + id + ", se envia nombre");
        }

        public void reproducirSonidoInicion()
        {
            f.reproducirSonido();
        }

        public void enviarMensajeChat(string mensaje)
        {
            LibreriaComunes.Comunicacion.MensajeChat mc = new MensajeChat { codigoUsuario = id, mensaje = mensaje };
            recibirMensajeChat(mc);
            ac.enviarMensajeChat(mc);
        }

        public void recibirMensajeChat(LibreriaComunes.Comunicacion.MensajeChat mc)
        {
            if (mensajesChat.Count == 3)
                mensajesChat.Dequeue();

            mensajesChat.Enqueue(mc);
            f.actualizarMensajes(mensajesChat);
        }

    }
}

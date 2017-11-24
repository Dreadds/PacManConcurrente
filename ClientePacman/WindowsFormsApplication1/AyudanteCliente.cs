using LibreriaComunes.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class AyudanteCliente
    {
        Cliente cliente;
        TcpClient tcpclient;
        NetworkStream stream;

        public AyudanteCliente(TcpClient tc, Cliente c)
        {
            tcpclient = tc;
            stream = tc.GetStream();
            cliente = c;
            new Thread(recibirMensaje).Start();
        }

        public void recibirMensaje()
        {
            byte[] readMsgLen;
            int dataLen;
            byte[] readMsgData;

            while (true)
            {
                Thread.Sleep(10);
                readMsgLen = new byte[4];
                stream.Read(readMsgLen, 0, 4);

                dataLen = BitConverter.ToInt32(readMsgLen, 0);
                readMsgData = new byte[dataLen];
                safeRead(readMsgData, dataLen);

                procesarMensaje(readMsgData);
            }
        }

        private void safeRead(byte[] userData, int len)
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
            if (obj is LibreriaComunes.EstadoJuego.EstadoJuego)
            {
                cliente.actualizarJuego(obj as LibreriaComunes.EstadoJuego.EstadoJuego);
            }
            if (obj is int)
            {
                Cliente.id = Convert.ToInt32(obj);

            }
            if (obj is string)
            {
                Console.WriteLine("reproduce?");
                cliente.reproducirSonidoInicion();
            }
            if (obj is LibreriaComunes.Comunicacion.MensajeChat)
            {
                cliente.recibirMensajeChat(obj as LibreriaComunes.Comunicacion.MensajeChat);
            }
        }

        public void enviarEntrada(MetodoEntrada me)
        {
            // Send the message to the connected TcpServer. 
            // The write flushes the stream automatically here

            Mensaje m = Serializador.Serialize(me);

            //Escribir primero la longitud del mensaje
            byte[] userDataLen = BitConverter.GetBytes(m.Data.Length);
            stream.Write(userDataLen, 0, 4);

            //escribir el mensaje
            stream.Write(m.Data, 0, m.Data.Length);

            me.liberar();
            Console.WriteLine("Se envio algo");
        }

        public void enviarMensajeChat(LibreriaComunes.Comunicacion.MensajeChat mc)
        {
            Mensaje m = Serializador.Serialize(mc);

            //Escribir primero la longitud del mensaje
            byte[] userDataLen = BitConverter.GetBytes(m.Data.Length);
            stream.Write(userDataLen, 0, 4);

            //escribir el mensaje
            stream.Write(m.Data, 0, m.Data.Length);

            Console.WriteLine("Se envio mensajeChat");
        }

    }
}

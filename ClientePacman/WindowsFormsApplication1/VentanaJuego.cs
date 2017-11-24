using LibreriaComunes.Comunicacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Speech.Recognition;
using System.Media;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class VentanaJuego : Form
    {

        public MetodoEntrada entrada;
        UtilidadDibujo ud;
        Cliente c;
        GifImage gif;
        
        public VentanaJuego(string nombre, TcpClient tc, bool teclado)
        {
            InitializeComponent();
            entrada = new MetodoEntrada();
            c = new Cliente(this, tc, nombre);
            gif = new GifImage(Properties.Resources.lP, panel1);
            c.setPanel(panel1);
            if (teclado)
            {
                KeyDown += VentanaJuego_KeyDown;
            }
            else
            {
                SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
                sre.SetInputToDefaultAudioDevice();
                Choices direcciones = new Choices();
                direcciones.Add(new string[] { "arriba", "abajo", "izquierda", "derecha" });

                GrammarBuilder gb = new GrammarBuilder();
                gb.Culture = sre.RecognizerInfo.Culture;
                gb.Append(direcciones);

                Grammar g = new Grammar(gb);
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(g);

                sre.RecognizeAsync(RecognizeMode.Multiple);

                sre.RequestRecognizerUpdate();

                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(hablaReconocida);
            }
        }

        private void hablaReconocida(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "arriba":
                    entrada.pArriba();
                    break;
                case "abajo":
                    entrada.pAbajo();
                    break;
                case "izquierda":
                    entrada.pIzquierda();
                    break;
                case "derecha":
                    entrada.pDerecha();
                    break;
            }
            c.enviarInstruccion(entrada);
        }

        private void VentanaJuego_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    entrada.pArriba();
                    break;
                case Keys.Down:
                    entrada.pAbajo();
                    break;
                case Keys.Left:
                    entrada.pIzquierda();
                    break;
                case Keys.Right:
                    entrada.pDerecha();
                    break;
                case Keys.Enter:
                    mostrarDialogoMensaje();
                    break;
            }
            c.enviarInstruccion(entrada);
        }

        private void mostrarDialogoMensaje() {
            dialogoMensaje dm = new dialogoMensaje(this) ;
            dm.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            typeof(Panel).InvokeMember("DoubleBuffered",
                                        BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                        null, panel1, new object[] { true });

            //c = new Cliente(this);
            //c.setPanel(panel1);
        }

        public void texto(LibreriaComunes.EstadoJuego.EstadoJuego ej)
        {
            string s = "";
            s += "vidas: " + ej.personajes.ElementAt(0).vidas + "\n";
            s += "x: " + ej.personajes.ElementAt(0).x + "\n";
            s += "y: " + ej.personajes.ElementAt(0).y + "\n";
            s += "galletas: " + ej.galletas.Count + "\n";
            s += "fantasmas: " + ej.fantasmas.Count + "\n";
            Console.WriteLine(s);
            //this.richTextBox1.AppendText(s);
        }

        public void actualizarEstadoJuego(LibreriaComunes.EstadoJuego.EstadoJuego ej)
        {
            if (ud == null)
            {
                ud = new UtilidadDibujo(ej, panel1);
                GifImage.terminar = false;
            }
            ud.ej = ej;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (ud == null)
            {
                g.FillRectangle(Brushes.Black, 0, 0, panel1.Width, panel1.Height);
                g.DrawString("Cargando ome", new Font("ARIAL", 12), Brushes.Yellow, 50, 50);
                g.DrawImage(gif.GetNextFrame(), 0, 0);
            }
            else
            {
                g.FillRectangle(Brushes.Black, 0, 0, panel1.Width, panel1.Height);
                int estado = ud.ej.juego.estado;
                if (estado == 0)
                {
                    ud.dibujarTablero(g);
                    ud.dibujarGalletas(g);
                    ud.dibujarFantasmas(g);
                    ud.dibujarPersonajes(g);
                    ud.dibujarInformacion(g);
                } else if(estado == 1)
                {
                    g.DrawString("Nivel Terminado.\nEl nuevo nivel comienza en contados segundos", new Font("RETRO COMPUTER", 12), Brushes.Yellow, 10, 10);
                }
                else if (estado == 2)
                {
                    g.DrawString("Se ha terminado el juego. Espere para continuar.", new Font("RETRO COMPUTER", 12), Brushes.Yellow, 10, 10);
                }
            }
        }

        public void reproducirSonido()
        {
            SoundPlayer sonidoFondo = new SoundPlayer(Properties.Resources.ELECTRON);
            sonidoFondo.PlayLooping();
            Thread.Sleep(5500);
            sonidoFondo.Stop();

        }

        public void enviarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
            c.enviarMensajeChat(mensaje);
        }

        public void actualizarMensajes(Queue<LibreriaComunes.Comunicacion.MensajeChat> colaMensajes)
        {
            ud.actualizarMensajes(colaMensajes);
        }


    }
}

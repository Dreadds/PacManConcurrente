using LibreriaComunes.Entidades;
using LibreriaComunes.EstadoJuego;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    ///     Clase que ayuda a graficar cada componente del juego en el panel.
    /// </summary>
    class UtilidadDibujo
    {
        int anchoCuadro = 15;
        int altoCuadro = 15;

        int offsetX;
        int offsetY;

        public EstadoJuego ej;
        public Panel panel;

        public Queue<LibreriaComunes.Comunicacion.MensajeChat> mensajesChat;

        Font font;

        RecursosGraficos rg;

        /// <summary>
        ///     Constructor que toma el control del juego y el panel sobre el que va a dibujar los componentes.
        /// </summary>
        /// <param name="j">
        ///     Control del que se sacará el juego
        /// </param>
        /// <param name="p">
        ///     Panel sobre el que se hará el dibujado
        /// </param>
        public UtilidadDibujo(EstadoJuego ej, Panel p)
        {
            rg = new RecursosGraficos();
            this.ej = ej;
            panel = p;
            calcularOffsets();
            font = new Font("RETRO COMPUTER", 10);
        }

        public void actualizarMensajes(Queue<LibreriaComunes.Comunicacion.MensajeChat> mensajes)
        {
            mensajesChat = mensajes;
        }

        /// <summary>
        ///     Calcula los offset para centrar los dibujos en el panel.
        /// </summary>
        public void calcularOffsets()
        {
            offsetX = (panel.Width - (anchoCuadro * ej.tablero.columnas)) / 2;
            offsetY = ((panel.Height - (altoCuadro * ej.tablero.filas)) / 2) - 40;
        }

        /// <summary>
        ///     Dibuja la cuadrícula del tablero según el contenido de la matriz de este.
        ///     Con cada número se asocia una porción del spritesheet de forma que se grafique acorde con el tablero.
        /// </summary>
        /// <param name="gr">
        ///     Objeto de tipo graphics sobre el que se hará el dibujado de las imagenes
        /// </param>
        public void dibujarTablero(Graphics gr)
        {
            calcularOffsets();
            Tablero t = ej.tablero;
            int x = offsetX;
            int y = offsetY;
            for (int i = 0; i < t.filas; i++)
            {
                for (int j = 0; j < t.columnas; j++)
                {
                    //TODO
                    gr.DrawImage(rg.ssTablero.obtenerImagen(),
                                new Rectangle(x, y, anchoCuadro, altoCuadro),
                                rg.ssTablero.obtenerRectangulo(t.casillas[i, j])
                                , GraphicsUnit.Pixel);
                    x += anchoCuadro;
                }
                x = offsetX;
                y += altoCuadro;
            }
        }

        /// <summary>
        ///     Dibuja la imagen del personaje de acuerdo con sus coordenadas y sus dimensiones.
        /// </summary>
        /// <param name="gr">
        ///     Objeto de tipo graphics sobre el que se hará el dibujado
        /// </param>
        public void dibujarPersonajes(Graphics gr)
        {
            for (int i = 0; i < ej.personajes.Length; i++)
            {
                Personaje p = ej.personajes.ElementAt(i);
                if (p.visible)
                {
                    if (Cliente.id == i)
                        gr.DrawImage(rg.obtenerImagenPersonaje(p), p.getX() + offsetX - 2, p.getY() + offsetY - 2, anchoCuadro + 4, altoCuadro + 4);
                    else
                    {
                        Bitmap b = ImageTransparency.ChangeOpacity(rg.obtenerImagenPersonajesContrincantes(p), (float)0.5);
                        gr.DrawImage(b, p.getX() + offsetX - 2, p.getY() + offsetY - 2, anchoCuadro + 4, altoCuadro + 4);
                    }
                }
            }
        }

        /// <summary>
        ///     Dibuja la imagen cada galleta según su posición y dimensiones.
        /// </summary>
        /// <param name="gr">
        ///     Objeto de tipo graphics sobre el que se hará el dibujado
        /// </param>
        public void dibujarGalletas(Graphics gr)
        {
            Dictionary<Tuple<int, int>, Galleta> galletas = ej.galletas;
            Tuple<int, int> tupla;
            Galleta galleta;
            for (int i = 0; i < ej.tablero.filas; i++)
            {
                for (int j = 0; j < ej.tablero.columnas; j++)
                {
                    try {
                        tupla = new Tuple<int, int>(j, i);
                        galleta = galletas[tupla];
                        gr.DrawImage(rg.ssGalleta.obtenerImagen(),
                                     new Rectangle(galleta.x + offsetX, galleta.y + offsetY, galleta.ancho, galleta.alto),
                                     rg.obtenerRectanguloGalleta(galleta),
                                     GraphicsUnit.Pixel);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.StackTrace);
                        return;
                    }
                }
            }
        }


        /// <summary>
        ///     Dibuja la imagen de cada fantasma de acuerdo con su posición y dimensiones
        /// </summary>
        /// <param name="gr">
        ///     Objeto de tipo graphics para hacer el dibujado
        /// </param>
        public void dibujarFantasmas(Graphics gr)
        {
            try
            {
                foreach (Fantasma fantasma in ej.fantasmas)
                {
                    gr.DrawImage(rg.obtenerImagenFantasma(fantasma), fantasma.getX() + offsetX - 2, fantasma.getY() + offsetY - 2, anchoCuadro + 4, altoCuadro + 4);
                }
            }
            catch (Exception e)
            {
                e.ToString();
                return;
            }
        }

        /// <summary>
        ///     Dibujado de la información del estado del juego que se quiere mostrar en el panel.
        ///     * Puntaje
        ///     * Velocidad
        ///     * Nivel
        ///     * Vidas
        ///     * Puntos en contra
        ///     * Segundos restantes para el siguiente aumento de velocidad
        ///     * Qué tan grave es la velocidad en la que se encuentra
        /// </summary>
        /// <param name="gr">
        ///     objeto de tipo Graphics sobre el que se haría la graficación
        /// </param>
        public void dibujarInformacion(Graphics gr)
        {
            int i = Cliente.id;
            LibreriaComunes.EstadoJuego.Juego j = ej.juego;
            string puntaje = Convert.ToString(j.puntajes.ElementAt(i));
            string velocidadP = Convert.ToString(ej.personajes.ElementAt(i).vMostrar());
            string nivel = Convert.ToString(j.nivel);
            string vidasP = Convert.ToString(ej.personajes.ElementAt(i).vidas);
            string segundosAumento = "";
            if (j.segundosAumento != -1)
                segundosAumento = Convert.ToString(j.segundosAumento);
            int aumentos = j.aumentos;

            string puntosEnContra = Convert.ToString(j.puntosEnContra.ElementAt(i));

            gr.DrawString("Puntos en contra: " + puntosEnContra, font, Brushes.Yellow, 5, panel.Height - 100);
            gr.DrawString("Vidas: " + vidasP, font, Brushes.Yellow, 5, panel.Height - 80);

            gr.DrawString("Velocidad Pacman: " + velocidadP, font, Brushes.Yellow, 5, panel.Height - 40);
            gr.DrawString("Puntaje: " + puntaje, font, Brushes.Yellow, 5, 0);

            Brush[] colores = { Brushes.White, Brushes.Turquoise, Brushes.Yellow, Brushes.Red };
            gr.FillRectangle(colores[aumentos], panel.Width - 2, 0, 2, panel.Height);
            gr.FillRectangle(colores[aumentos], 0, 0, 2, panel.Height);
            gr.DrawString(segundosAumento, font, Brushes.Yellow, panel.Width / 2 - font.Size, panel.Height - 100);
            gr.DrawString("Nivel: " + nivel, font, Brushes.Yellow, panel.Width / 2 - font.Size * 4, panel.Height - 60);

            int y = panel.Height - 100;
            gr.DrawString("Mensajes", font, Brushes.Yellow, 430, y);
            y += 20;
            if (mensajesChat != null)
            {
                foreach(LibreriaComunes.Comunicacion.MensajeChat mc in mensajesChat)
                {
                    if (mc.codigoUsuario == Cliente.id)
                        gr.DrawString("tu" + ": " + mc.mensaje, font, Brushes.Yellow, 435, y);
                    else
                        gr.DrawString(mc.nombreUsuario + ": " + mc.mensaje, font, Brushes.Yellow, 435, y);
                    
                    y += 20;
                }
            }
        }

    }
}


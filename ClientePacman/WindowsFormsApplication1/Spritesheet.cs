using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Spritesheet
    {

        public short filas;
        public short columnas;

        public int ancho;
        public int alto;

        public int anchoSprite;
        public int altoSprite;

        Image imagen;

        /// <summary>
        ///     Constructor que usa la imagen completa, el numero de filas y de columnas con las que se desea trabajar
        /// </summary>
        /// <param name="bmp">
        ///     Bitmap correspondiente a la imagen que se quiere usar.
        /// </param>
        /// <param name="filas">
        ///     Número de filas en las que se dividiría la imagen
        /// </param>
        /// <param name="columnas">
        ///     Número de columnas en las que se dividiría la imagen.
        /// </param>
        public Spritesheet(Bitmap bmp, short filas, short columnas)
        {
            this.filas = filas;
            this.columnas = columnas;
            imagen = bmp;

            ancho = imagen.Width;
            alto = imagen.Height;

            anchoSprite = ancho / columnas;
            altoSprite = alto / filas;
        }

        /// <summary>
        ///     Obtiene el rectángulo que rodearía la imagen en la sección solicitada
        /// </summary>
        /// <param name="pos">
        ///     La sección de la imagen que se quiere mostrar. Ej, para una de 3 filas y 3 columnas:
        /// 
        ///     |0|1|2|
        ///     |3|4|5|
        ///     |6|7|8|
        /// 
        /// </param>
        /// <returns>
        ///     El rectangulo que en el contexto del spritesheet demarcaría la porción solicitada.
        /// </returns>
        public Rectangle obtenerRectangulo(int pos)
        {
            int inX = pos % columnas;
            int inY = pos / columnas;

            return new Rectangle(inX * anchoSprite, inY * altoSprite, anchoSprite, altoSprite);
        }

        /// <summary>
        ///     Da la imagen completa del spritesheet.
        /// </summary>
        /// <returns></returns>
        public Image obtenerImagen()
        {
            return imagen;
        }

    }
}

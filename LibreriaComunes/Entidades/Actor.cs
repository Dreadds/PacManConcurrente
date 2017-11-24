using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Entidades
{
    [Serializable]
    public class Actor
    {

        /// <summary>
        ///    Clase actor, representa los actores del juego (Pac man, fantasmas)
        ///    que se pueden mover, tienen posicion, dirección y velocidad importantes
        ///    para la lógica del juego.
        /// </summary>
        //Coordenadas del actor
        public double x;
        public double y;

        public int dirX;
        public int dirY;

        //Velocidad del actor (siempre positiva)
        public double velocidad;

        //Posición actual para mostrar (determina que porcion del spritesheet se estará mostrando)
        public double posImagen;

        public double Velocidad
        {
            get;
            set;
        }

        /// <summary>
        ///    La posición en el eje x actual entera del Actor
        /// </summary>
        /// <returns>
        ///    El valor de x (posición en el eje x del actor) convertido a entero
        /// </returns>
        public int getX()
        {
            return Convert.ToInt32(x);
        }

        /// <summary>
        ///    La posición en el eje y actual entera del Actor
        /// </summary>
        /// <returns>
        ///    El valor de y (posición en el eje y del actor) convertido a entero
        /// </returns>
        public int getY()
        {
            return Convert.ToInt32(y);
        }

        public int getDireccion()
        {
            if (dirX == 1)
                return 0;
            if (dirX == -1)
                return 1;
            if (dirY == 1)
                return 2;
            if (dirY == -1)
                return 3;
            return -1;
        }

    }
}

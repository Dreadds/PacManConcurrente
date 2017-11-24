using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Entidades
{
    /// <summary>
    ///     Objeto consumible por el personaje, que se encuentra en todos los espacios de los tableros que no tengan paredes
    ///     Tiene una posición en x, una posición en y, un ancho y un alto; un spritesheet para mostrar imagenes asociadas,
    ///     y un indicador de si es biscuit o no, para operaciones dentro del juego y del que depende qué sección del spritesheet
    ///     se mostrará cuando se solicite graficar esta galleta. Se carga el mismo spritesheet para cada galleta.
    /// </summary>
    [Serializable]
    public class Galleta
    {

        public int x;
        public int y;

        public int ancho;
        public int alto;

        public bool biscuit;
        
    }
}

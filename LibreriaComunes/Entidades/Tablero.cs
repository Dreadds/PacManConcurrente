using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaComunes.Entidades
{

    /// <summary>
    ///     Clase que representa un tablero, cargado desde un archivo de texto especificado.
    ///     Implementado como una matriz, cada celda cuenta con un número correspondiente al tipo de casilla
    ///     que es, lo más relevante es que una casilla cero es transitable.
    ///     Cada tablero tiene un número que corresponde a que nivel es dentro de la dificultad a la que pertenece.
    ///     Según esto, el tablero también proveerá las configuraciones de cada color de fantasma, y se asignará una cantidad
    ///     de filas y una cantidad de columnas, útiles para operaciones en el juego.
    /// </summary>
    [Serializable]
    public class Tablero
    {

        public short[,] casillas;
        public int filas, columnas;

        public short numero;

    }
}


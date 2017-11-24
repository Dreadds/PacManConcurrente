using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Properties;

namespace WindowsFormsApplication1
{
    public class RecursosGraficos
    {

        public Spritesheet ssTablero;
        public Spritesheet ssPersonaje;
        public Spritesheet ssFantasmaRojo;
        public Spritesheet ssFantasmaAzul;
        public Spritesheet ssFantasmaRosa;
        public Spritesheet ssFantasmaNarajana;
        public Spritesheet ssGalleta;
        public Spritesheet ssAsustado;
        public Spritesheet ssContrincante;

        public RecursosGraficos()
        {
            ssTablero = new Spritesheet(Resources.Walls, 1, 19);
            ssPersonaje = new Spritesheet(Resources.Pac, 5, 3);
            ssFantasmaAzul = new Spritesheet(Resources.Blue, 4, 2);
            ssFantasmaNarajana = new Spritesheet(Resources.Orange, 4, 2);
            ssFantasmaRojo = new Spritesheet(Resources.Red, 4, 2);
            ssFantasmaRosa = new Spritesheet(Resources.Pink, 4, 2);
            ssGalleta = new Spritesheet(Resources.Cookie, 1, 2);
            ssAsustado = new Spritesheet(Resources.Scared, 4, 2);
            ssContrincante = new Spritesheet(Resources.Pacenemigo, 5, 3);
        }

        public Image obtenerImagenFantasma(LibreriaComunes.Entidades.Fantasma f)
        {
            Spritesheet spritesheetParaMostrar = null;
            if (f.modo == 0)
            {
                switch (f.tipo)
                {
                    case 0:
                        spritesheetParaMostrar = ssFantasmaRojo;
                        break;
                    case 1:
                        spritesheetParaMostrar = ssFantasmaRosa;
                        break;
                    case 2:
                        spritesheetParaMostrar = ssFantasmaNarajana;
                        break;
                    case 3:
                        spritesheetParaMostrar = ssFantasmaAzul;
                        break;
                }
            }
            else
                spritesheetParaMostrar = ssAsustado;
            Bitmap b = new Bitmap(spritesheetParaMostrar.obtenerImagen());
            return b.Clone(spritesheetParaMostrar.obtenerRectangulo(f.getDireccion() * spritesheetParaMostrar.columnas + (int)f.posImagen), spritesheetParaMostrar.obtenerImagen().PixelFormat);
        }

        public Image obtenerImagenPersonaje(LibreriaComunes.Entidades.Personaje p)
        {
            Bitmap b = new Bitmap(ssPersonaje.obtenerImagen());
            return b.Clone(ssPersonaje.obtenerRectangulo(p.getDireccion() * ssPersonaje.columnas + (int)p.posImagen), ssPersonaje.obtenerImagen().PixelFormat);
        }

        public Image obtenerImagenPersonajesContrincantes(LibreriaComunes.Entidades.Personaje p)
        {
            Bitmap b = new Bitmap(ssContrincante.obtenerImagen());
            return b.Clone(ssContrincante.obtenerRectangulo(p.getDireccion() * ssContrincante.columnas + (int)p.posImagen), ssContrincante.obtenerImagen().PixelFormat);
        }

        public Rectangle obtenerRectanguloGalleta(LibreriaComunes.Entidades.Galleta g)
        {
            if (g.biscuit)
                return ssGalleta.obtenerRectangulo(1);
            return ssGalleta.obtenerRectangulo(0);
        }

    }
}

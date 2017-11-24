using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class GifImage
    {

        private Image gifImage;
        private FrameDimension dimension;
        private int frameCount;
        private int currentFrame;
        private bool reverse;
        private int step = 1;
        private Panel vj;
        public static bool terminar;

        public GifImage(Image imagen, Panel ventana)
        {
            vj = ventana;
            gifImage = imagen; //initialize
            dimension = new FrameDimension(gifImage.FrameDimensionsList[0]); //gets the GUID
            frameCount = gifImage.GetFrameCount(dimension); //total frames in the animation
            terminar = false;
            new Thread(hiloGif).Start(); ;
            

        }
        public bool ReverseAtEnd //whether the gif should play backwards when it reaches the end
        {
            get { return reverse; }
            set { reverse = value; }
        }
        public Image GetNextFrame()
        {

            currentFrame += step;
            //if the animation reaches a boundary...
            if (currentFrame >= frameCount || currentFrame < 1)
            {
                if (reverse)
                {
                    step *= -1; //...reverse the count
                    currentFrame += step; //apply it
                }
                else
                {
                    currentFrame = 0; //...or start over
                    //doubleStep = 0.0;
                }


            }
            return GetFrame(currentFrame);
        }
        public Image GetFrame(int index)
        {
            gifImage.SelectActiveFrame(dimension, index); //find the frame
            return (Image)gifImage.Clone(); //return a copy of it
        }


        public void hiloGif()
        {
            while (!terminar)
            {
                vj.Invalidate();
                
                Thread.Sleep(50);
            }
        }

    }
}

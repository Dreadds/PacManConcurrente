using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class dialogoMensaje : Form
    {
        VentanaJuego vj;
        public dialogoMensaje(VentanaJuego ventana)
        {
            InitializeComponent();
            vj = ventana;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                    vj.enviarMensaje(textBox1.Text);

                this.Dispose();
            }
            if (e.KeyCode == Keys.Escape)
            {
                
                Dispose();
            }
        }

        private void dialogoMensaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
               
                this.Dispose();
            }
        }
    }
}

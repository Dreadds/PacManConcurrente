using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class VentanaInicio : Form
    {

        TcpClient conexion;

        public VentanaInicio()
        {
            InitializeComponent();
            txtNombre.Enabled = false;
            btnAceptar.Enabled = false;
            chbTeclado.Enabled = false;
            chbVoz.Enabled = false;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIP.Text) || string.IsNullOrWhiteSpace(txtPuerto.Text))
            {
                MessageBox.Show("Faltan datos por ingresar");
            }
            else
            {
                bool respuesta = realizarConexion();
                if (respuesta)
                {
                    MessageBox.Show("Conectado al servidor, ingrese su nombre");
                    btnConectar.Enabled = false;
                    txtNombre.Enabled = true;
                    btnAceptar.Enabled = true;
                    chbTeclado.Enabled = true;
                    chbVoz.Enabled = true;
                }
                else
                    MessageBox.Show("No se puede conectar con el servidor, verifique los datos");
            }
        }

        private bool realizarConexion()
        {
            try
            {
                conexion = new TcpClient(txtIP.Text, Convert.ToInt32(txtPuerto.Text));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese su nombre");
            }
            if (!chbTeclado.Checked && !chbVoz.Checked)
            {
                MessageBox.Show("Seleccione un método de entrada");
                return;
            }
            else
            {
                VentanaJuego vj = new VentanaJuego(txtNombre.Text, conexion, chbTeclado.Checked);
                vj.Visible = true;
            }
        }

        private void chbTeclado_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbTeclado.Checked)
                chbVoz.Checked = false;
        }

        private void chbVoz_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbVoz.Checked)
                chbTeclado.Checked = false;
        }
    }
}

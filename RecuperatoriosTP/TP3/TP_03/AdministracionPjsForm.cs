using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace TP_03
{
    public partial class AdministracionPjsForm : Form
    {
        /// <summary>
        /// Constructor del form AdministracionPjs. Setea el Icono del form y la imagen del background.
        /// </summary>
        public AdministracionPjsForm()
        {
            InitializeComponent();

            //Seteo el favicon y el background.
            this.Icon = Properties.Resources.menu;
            this.pictureBox_BackgroundAdministracionPjsForm.Image = Properties.Resources.F1;
        }

        /// <summary>
        /// Método que se ejecuta al presionar el boton Creacion. Muestra el form
        /// AltaPjs. Este form seguirá visible y activo en 2do plano.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CreacionAdministracionPjsForm_Click(object sender, EventArgs e)
        {
            //Creo y abro el form de ALTA
            AltaPjsForm altaPjsForm = new AltaPjsForm();
            altaPjsForm.ShowDialog();
        }

        /// <summary>
        /// Método que se ejecuta al presionar el boton Eliminacion. Muestra el form
        /// PedidoNombrePjs. Este form seguirá visible y activo en 2do plano.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_EliminacionAdministracionPjsForm_Click(object sender, EventArgs e)
        {
            //Creo y abro el form de PEDIDO-NOMBRE - Le indico que es una eliminación.
            PedidoNombrePjForm pedidoNombrePjsForm = new PedidoNombrePjForm(true);
            pedidoNombrePjsForm.ShowDialog();
  
        }

        /// <summary>
        /// Método que se ejecuta al presionar el boton Modificacion. Muestra el form
        /// AltaPjs. Este form seguirá visible y activo en 2do plano.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ModificacionAdministracionPjsForm_Click(object sender, EventArgs e)
        {
            //Creo y abro el form de PEDIDO-NOMBRE - Le indico que no es una eliminación. 
            PedidoNombrePjForm pedidoNombrePjsForm = new PedidoNombrePjForm(false);
            pedidoNombrePjsForm.ShowDialog();
        }

        /// <summary>
        /// Cierra este form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VolverAdminPjsForm_Click(object sender, EventArgs e)
        {
            //Cierro form
            this.Close();
        }

    }
}

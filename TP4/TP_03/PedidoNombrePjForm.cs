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
using Excepciones;

namespace TP_03
{
    public partial class PedidoNombrePjForm : Form
    {
        bool esEliminacion;

        /// <summary>
        /// Constructor del PedidoNombrePj. Recibe por parametro si es o no una eliminacion y se setea
        /// al atributo del form. Se setea el Icono del form y la imagen de background.
        /// </summary>
        /// <param name="esEliminacion"></param>
        public PedidoNombrePjForm(bool esEliminacion)
        {
            InitializeComponent();

            //Seteo el favicon y el background.
            this.Icon = Properties.Resources.pedidoId;
            this.pictureBox_BackgroundPedidoNombrePjForm.Image = Properties.Resources.F10;

            //Seteo al atributo esEliminacion el valor recibido por parametro. (Es una eliminacion).
            this.esEliminacion = esEliminacion;
        }

        /// <summary>
        /// Se inicia al presionar el boton Ingresar. Se guardan el nombre ingresado, se valida y
        /// si es valido se inicia la busqueda del personaje. Si se encontró, se actuará en cuestion
        /// de lo que valga EsEliminacion, seteada en el constructor de este form. Si es una eliminación
        /// se pedirá una confirmación para realizar la eliminación y posterior al Sí, se eliminará el personaje
        /// tanto de la lista como del archivo. En cambio, si es una modificación, se abrirá el form
        /// de ModificacionPjs, pasandole el personaje encontrado y la posicion del mismo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarPedidoNombrePjForm_Click(object sender, EventArgs e)
        {

            try
            {
                //Guardo el nombre ingresado.
                string nombreIngresado = textBox_NombreIngresadoPedidoNombrePjForm.Text;

                //Valido el nombre ingresado,
                bool resultadoValidacionNombre;
                nombreIngresado = nombreIngresado.Trim();
                resultadoValidacionNombre = Personaje.isValidNombrePersonaje(nombreIngresado);

                //Me fijo si la carga de la DB sigue activa... 
                if (PanelControlForm.flag == false)
                {
                    ExceptionIncompleteInformation exception = new ExceptionIncompleteInformation("No se puede buscar un personaje si está la carga de personajes activa.");
                    throw exception;
                }
                else //Si ya terminó la carga de personajes...
                {
                    //Si el nombre parece ser valido, inicio la búsqueda del personaje.
                    if (resultadoValidacionNombre == true)
                    {
                        bool resultadoBusquedaPj;
                        int posicionDelPj;
                        Personaje pjEncontrado;
                        DialogResult confirmacionDeAccion;

                        pjEncontrado = Universo.buscarExistenciaPersonajePorNombre(nombreIngresado, out resultadoBusquedaPj, out posicionDelPj);

                        //Si se encontró me fijo que hacer. Si eliminar o modificar.
                        if (resultadoBusquedaPj == true && pjEncontrado != null && posicionDelPj >= 0)
                        {

                            if (esEliminacion == true)
                            {
                                //ELIMINACIÓN PERSONAJE.

                                confirmacionDeAccion = MessageBox.Show("¿Seguro desea eliminar este personaje?", "Eliminacion de personaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (confirmacionDeAccion == DialogResult.Yes)
                                {
                                    //Lo elimino de la lista y aviso que fue eliminado.
                                    Universo.listaPersonajesExistentes.Remove(pjEncontrado);

                                    //Elimino de la DB
                                    DB_Stuff.EliminarUnPersonajeDB(pjEncontrado);

                                    //Elimino del archivo back-up.
                                    ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");

                                    MessageBox.Show("La eliminacion ha sido satisfactoria. Recuerde guardar cambios.", "Eliminacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                //MODIFICACIÓN PERSONAJE.

                                //Creo y abro el form de MODIFICACIÓN. (Oculto este form y lo muestro si en un futuro se cierra el form abierto).
                                ModificacionPjsForm modificacionPjForm = new ModificacionPjsForm(posicionDelPj, pjEncontrado);
                                this.Hide();
                                modificacionPjForm.ShowDialog();
                                this.Close();
                            }
                        }
                        else
                        {
                            //EXCEPCION - NO SE PUDO ENCONTRAR UN PERSONAJE CON ESE NOMBRE
                            Exception exception = new ExceptionCharacterNotFound("No se pudo encontrar un personaje con el nombre ingresado.");
                            throw exception;
                        }
                    }
                    else
                    {
                        //EXCEPCION - NO SE INGRESO UN NOMBRE VALIDO
                        Exception exception = new ExceptionInvalidInformation("No se puede filtrar por informacion no valida, nula o inexistente.");
                        throw exception;
                    }
                }            
            }
            catch (Exception ExcepcionRecibida)
            {
                if (ExcepcionRecibida.GetType() == typeof(Exception))
                {
                    MessageBox.Show("Ocurrió una excepcion inesperada. Error guardado en el archivo de errores.", "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ExcepcionRecibida.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ArchivosManagement.EscribirArchivoTXT(ExcepcionRecibida.Message, false, "Archivos-TP3-LopezGasal//Errores", ArchivosManagement.GenerarNombreFechaHoraMntsConExtension("Error ", ".txt"));
            }


        }

        /// <summary>
        /// Cierra este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VolverPedidoNombrePjForm_Click(object sender, EventArgs e)
        {
            //Cierro el form.
            this.Close();
        }
    }
   }

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
    public partial class ModificacionPjsForm : Form
    {
        int posicionPjAModificar;
        Personaje personajeAModificar;

        /// <summary>
        /// Constructor del form ModificacionPjs. Recibe la posicion en la lista del personaje que se desea modificar
        /// y el personaje como tal. También se setea el Icono, y la imagen de background. 
        /// Los valores recibidos por parametros son seteados a los atributos del form, que son equivalentes.
        /// </summary>
        /// <param name="posicionPjRecibido"></param>
        /// <param name="personajeRecibido"></param>
        public ModificacionPjsForm(int posicionPjRecibido, Personaje personajeRecibido)
        {
            InitializeComponent();

            //Seteo el favicon y el background.
            this.Icon = Properties.Resources.modificacion;
            this.pictureBox_BackgroundModificacionPjsForm.Image = Properties.Resources.F11;

            //Seteo a los atributos del form los valores recibidos por parametros. (Posicion del pj en la lista y el pj como tal).
            this.posicionPjAModificar = posicionPjRecibido;
            this.personajeAModificar = personajeRecibido;
        }

        /// <summary>
        /// Se inicia al presionar el boton Modificar, verifica que las combo-boxes estén seleccionadas, posterior a eso, que
        /// los datos ingresados sean válidos. Si son válidos se verifica que el personaje a modificar sea posible de ser 
        /// modificado por el nombre que se desea modificar. Los datos son modificados. Para verificar que la modificacion haya
        /// sido satisfactoria, se valida por segunda y última vez el personaje, mediante la interfaz.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ModificarModificacionPjsForm_Click(object sender, EventArgs e)
        {
 
            try
            {
                //Si todas las combo-boxes están con algo seleccionado.
                if (comboBox_ArmaSeleccionadaModificacionPjsForm.SelectedIndex >= 0 && comboBox_OrigenSeleccionadoModificacionPjsForm.SelectedIndex >= 0)
                {
                    //Guardo los datos ingresados.
                    string nombreIngresado = textBox_NombreIngresadoAltaPjsForm.Text;
                    Personaje.enumOrigenElemental origenSeleccionado = (Personaje.enumOrigenElemental)comboBox_OrigenSeleccionadoModificacionPjsForm.SelectedIndex;
                    Arma.enumTipoArma armaSeleccionada = (Arma.enumTipoArma)comboBox_ArmaSeleccionadaModificacionPjsForm.SelectedIndex;
                    int nivelSeleccionado = ((int)numericUpDown_NivelIngresadoModificacionPjsForm.Value);

                    //Valido cada uno de los datos ingresados.
                    nombreIngresado = nombreIngresado.Trim();
                    bool validacionNombre = Personaje.isValidNombrePersonaje(nombreIngresado);
                    bool validacionOrigenElemental = Personaje.isValidOrigenElemental(origenSeleccionado);
                    bool validacionArma = Arma.isValidTipoArma(armaSeleccionada);
                    bool validacionNivel = Personaje.isValidNivelTotalPersonaje(nivelSeleccionado);

                    //Si todos los datos ingresados son validos.
                    if (validacionNombre == true && validacionOrigenElemental == true && validacionArma == true && validacionNivel == true)
                    {
                        //LOS DATOS DEL NUEVO PERSONAJE INGRESADO PARA CREARSE PARECERÍAN SER CORRECTOS.

                        //Verifico si ya existe un personaje con esos datos.
                        bool esPosibleModificar = Personaje.esPosibleModificar(personajeAModificar, nombreIngresado);

                        if (esPosibleModificar == true) //Si es posible modificar. (Verifica si existe una replica al igual que el alta).
                        {
                            
                            //Valido por una ultima vez el personaje que va a ser modificado en sí como tal y su arma también ya modificada.
                            Personaje personajeModificado = Universo.listaPersonajesExistentes[posicionPjAModificar];

                            bool resultadoValidacionPjModificado;
                            bool resultadoValidacionArmaDelPjModificada;

                            resultadoValidacionPjModificado = ((IValidar)personajeModificado).Validar(personajeModificado);
                            resultadoValidacionArmaDelPjModificada = ((IValidar)personajeModificado.Arma).Validar(personajeModificado);

                            //Si ya pasó el doble proceso de validación, es efectivamente válido
                            if (resultadoValidacionPjModificado == true && resultadoValidacionArmaDelPjModificada == true)
                            {
                                //Modifico en la DB
                                DB_Stuff.ModificarUnPersonajeDB(Universo.listaPersonajesExistentes[posicionPjAModificar], nombreIngresado, nivelSeleccionado, origenSeleccionado, armaSeleccionada, personajeModificado.BatallasJugadas, personajeModificado.BatallasGanadas);

                                //Piso los datos del personaje existente con los "nuevos datos" ya validados.
                                Universo.listaPersonajesExistentes[posicionPjAModificar].NombrePersonaje = nombreIngresado;
                                Universo.listaPersonajesExistentes[posicionPjAModificar].OrigenElemental = origenSeleccionado;
                                Universo.listaPersonajesExistentes[posicionPjAModificar].Arma.TipoArma = armaSeleccionada;

                                //Modifico en el archivo back-up.
                                ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");

                                //Aviso que la modificacion fue valida y exitosa.
                                MessageBox.Show("La modificacion ha sido satisfactoria. Recuerde guardar cambios.", "Modificacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               
                                                   
                            }
                            else
                            {
                                //Aviso que la modificacion no fue del todo valida ya que existio algun dato invalido, solamente se pisaron los datos que fueron posibles.
                                MessageBox.Show("La modificacion no ha sido del todo satisfactoria. Se han modificado los datos que han sido posibles modificar, sin embargo existen datos invalidos ingresados que no han podido ser modificados.", "Modificacion realizada a medias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //EXCEPCION - YA EXISTE UN PERSONAJE CON ESE NOMBRE
                                Exception exception = new Exception();
                                throw exception;
                            }

                            this.Close();
                        }
                        else
                        {
                            //EXCEPCION - YA EXISTE UN PERSONAJE CON ESE NOMBRE
                            Exception exception = new ExceptionRepeatedName("No se puede modificar el personaje ya que existe otro con el mismo nombre.");
                            throw exception;
                        }
                    }
                    else
                    {
                        //EXCEPCION - LOS NUEVOS DATOS DEL PERSONAJE NO SON CORRECTOS
                        Exception exception = new ExceptionInvalidInformation("No se puede modificar al personaje debido a que existe informacion invalida.");
                        throw exception;
                    }
                }
                else
                {
                    //EXCEPCION - LOS NUEVOS DATOS DEL PERSONAJE NO SON CORRECTOS
                    Exception exception = new ExceptionComboBoxNotChoosen("No se puede modificar al personaje por un campo no seleccionado. Verifique combo-box.");
                    throw exception;
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
        private void button_VolverModificacionPjsForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

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
    public partial class AltaPjsForm : Form
    {
        /// <summary>
        /// Constructor del form AltaPjs. Setea el Icono del form y la imagen del background.
        /// </summary>
        public AltaPjsForm()
        {
            InitializeComponent();
            
            //Seteo el favicon y el background.
            this.Icon = Properties.Resources.add;
            this.pictureBox_BackgroundAltaPjsForm.Image = Properties.Resources.F8;
        }

        /// <summary>
        /// Se ejecuta al presionar el boton Crear. Verifica que se hayan seleccionado todas las combo-boxes, 
        /// que se hayan ingresado datos válidos, que no exista ya el personaje que se intenta crear y luego de una 
        /// segunda validación, crea el personaje, añadiendolo a la lista de personajes y al archivo de personajes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CrearAltaPjsForm_Click(object sender, EventArgs e)
        {
            try
            {
                //Si todas las combo-boxes están con algo seleccionado.
                if (comboBox_ArmaSeleccionadaAltaPjsForm.SelectedIndex >= 0 && comboBox_OrigenSeleccionadoAltaPjsForm.SelectedIndex >= 0)
                {
                    //Guardo los datos ingresados.
                    string nombreIngresado = textBox_NombreIngresadoAltaPjsForm.Text;
                    Personaje.enumOrigenElemental origenSeleccionado = (Personaje.enumOrigenElemental)comboBox_OrigenSeleccionadoAltaPjsForm.SelectedIndex;
                    Arma.enumTipoArma armaSeleccionada = (Arma.enumTipoArma)comboBox_ArmaSeleccionadaAltaPjsForm.SelectedIndex;
                    int nivelSeleccionado = ((int)numericUpDown_NivelIngresadoAltaPjsForm.Value);

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
                        bool resultadoReplicaPersonaje = Personaje.estaRepetido(nombreIngresado);

                        if (resultadoReplicaPersonaje == false) //Si no existe una replica.
                        {
                            //Creo el personaje validado como tal y lo agrego a la lista.
                            Personaje personajeValidado = new Personaje(nombreIngresado, nivelSeleccionado, origenSeleccionado, armaSeleccionada);

                            //Valido por una ultima vez el personaje en sí como tal y su arma.
                            bool resultadoValidacionPj;
                            bool resultadoValidacionArmaDelPj;

                            resultadoValidacionPj = ((IValidar)personajeValidado).Validar(personajeValidado);
                            resultadoValidacionArmaDelPj = ((IValidar)personajeValidado.Arma).Validar(personajeValidado);

                            //Si ya pasó el doble proceso de validación, es efectivamente válido
                            if (resultadoValidacionPj == true && resultadoValidacionArmaDelPj == true)
                            {
                                //Lo agrego a la lista de pjs y aviso que salió todo bien.
                                Universo.listaPersonajesExistentes.Add(personajeValidado);
                                MessageBox.Show("La creacion ha sido satisfactoria. Recuerde guardar cambios.", "Creacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");
                                this.Close();
                            }
                            else
                            {
                                //EXCEPCION - LOS DATOS DEL NUEVO PERSONAJE NO SON CORRECTOS
                                Exception exception = new ExceptionInvalidInformation("Existen datos invalidos.");
                                throw exception;
                            }
                        }
                        else
                        {
                            //EXCEPCION - YA EXISTE UN PERSONAJE CON ESE EXACTAMENTE EL MISMO NOMBRE. INTENTE CON OTRO.
                            Exception exception = new ExceptionRepeatedName("Ya existe un personaje con ese mismo nombre, intente con otro.");
                            throw exception;

                        }
                    }
                    else
                    {
                        //EXCEPCION - LOS DATOS DEL NUEVO PERSONAJE NO SON CORRECTOS
                        Exception exception = new ExceptionInvalidInformation("El nombre ingresado es invalido.");
                        throw exception;
                    }
                }
                else
                {
                    //EXCEPCION - LOS DATOS DEL NUEVO PERSONAJE NO SON CORRECTOS O SE ENCUENTRAN INCOMPLETOS.
                    Exception exception = new ExceptionComboBoxNotChoosen("No se puede crear un personaje con un campo no seleccionado. Verifique combo-box.");
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
        private void button_VolverAltaPjsForm_Click(object sender, EventArgs e)
        {
            //Cierro form.
            this.Close();
        }


    }
}

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
    public partial class BatallaPjsForm : Form
    {
        /// <summary>
        /// Constructor del form BatallaPjs. Setea el Icono del form y la imagen del background.
        /// </summary>
        public BatallaPjsForm()
        {
            InitializeComponent();

            //Seteo el favicon y el background.
            this.Icon = Properties.Resources.batalla;
            this.pictureBox_BackgroundBatallaPjsForm.Image = Properties.Resources.F9;
        }
        
        /// <summary>
        /// Cierra este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VolverBatallaPjsForm_Click(object sender, EventArgs e)
        {
            //Cierro form.
            this.Close();
        }

        /// <summary>
        /// Se inicia al presionar el boton batalla. Primeramente se validan los nombres
        /// ingresados. Si parecen ser válidos comienza la busqueda por nombre a de los dos 
        /// personajes ingresados, si son encontrados se verifica finalmente que no se trate
        /// de los mismos, si no que, sean distintos uno del otro. En ese caso, comienza la batalla
        /// y se determiuna a un ganador. Siendo mostrado al usuario mediante una MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IniciarBatallaBatallaPjsForm_Click(object sender, EventArgs e)
        {
            try
            {
                //LO PRIMERO QUE TENGO QUE HACER ES BUSCAR A LOS 2 PERSONAJES INGRESADOS.
                Personaje personaje1;
                Personaje personaje2;

                string nombrePj1 = textBox_NombreHeroe1BatallaPjsForm.Text;
                string nombrePj2 = textBox_NombreHeroe2BatallaPjsForm.Text;

                if (string.IsNullOrWhiteSpace(nombrePj1) == false || string.IsNullOrWhiteSpace(nombrePj2) == false)
                {
                    bool seEncontroElPj1;
                    personaje1 = buscarPersonajePorNombre(nombrePj1, out seEncontroElPj1);

                    bool seEncontroElPj2;
                    personaje2 = buscarPersonajePorNombre(nombrePj2, out seEncontroElPj2);

                    //Si efectivamente encontré a los 2 personajes, ahora sí los enfrento.
                    if (seEncontroElPj1 == true && seEncontroElPj2 == true)
                    {
                        int resultadoBatalla;
                        resultadoBatalla = Universo.Enfrentamiento(personaje1, personaje2);

                        //Si no coinciden su IDS, esta todo bien. Si no, el usuario ingreso el mismo personaje
                        if (personaje1.IdPersonaje != personaje2.IdPersonaje)
                        {
                            switch (resultadoBatalla)
                            {
                                case 0: //Si empata.
                                    {
                                        MessageBox.Show($"Hubo un empate. {personaje1.NombrePersonaje} murió sangrientamente por sus graves heridas luego de asesinar a {personaje2.NombrePersonaje}.", "Resultado batalla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                case 1: //Si gana el personaje 1.
                                    {
                                        MessageBox.Show($"El personaje {personaje1.NombrePersonaje} ganó la pelea justamente. Derribando por los suelos sin piedad alguna a {personaje2.NombrePersonaje} rematandolo violentamente utilizando su {personaje1.Arma.TipoArma}.", "Resultado batalla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                                case 2: //Si gana el personaje 2.
                                    {
                                        MessageBox.Show($"El personaje {personaje2.NombrePersonaje} ganó la pelea justamente. Derribando por los suelos sin piedad alguna a {personaje1.NombrePersonaje}, rematandolo violentamente utilizando su {personaje2.Arma.TipoArma}.", "Resultado batalla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            //EXCEPCION - PERSONAJE REPETIDO
                            Exception exception = new ExceptionRepeatedName("El nombre del personaje está repetido. No se puede realizar una batalla entre un mismo personaje.");
                            throw exception;
                        }
                    }
                    else
                    {
                        //EXCEPCION - NO SE ENCONTRÓ UN PERSONAJE
                        Exception exception = new ExceptionCharacterNotFound("No se encontró un personaje.");
                        throw exception;
                    }
                }
                else
                {
                    //EXCEPCION - NO SE INGRESO NADA
                    Exception exception = new ExceptionIncompleteInformation("No se ingresó ningun nombre.");
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
        /// Método del form, que recibe un nombre y una variable bool que será cargada con true
        /// si el personaje fue encontrado. Se encarga de buscar en la lista de personajes
        /// el nombre ingresado.
        /// </summary>
        /// <param name="nombreABuscar"></param>
        /// <param name="seEncontroElPj"></param>
        /// <returns>Retorna el personaje que fue encontrado</returns>
        private Personaje buscarPersonajePorNombre(string nombreABuscar, out bool seEncontroElPj)
        {
            Personaje personajeEncontrado = null;
            seEncontroElPj = false;


            int cantidadPersonajes = Universo.listaPersonajesExistentes.Count;

            for (int i = 0; i < cantidadPersonajes; i++)
            {
                if (Universo.listaPersonajesExistentes[i].NombrePersonaje == nombreABuscar)
                {
                    //DEVUELVO EL PERSONAJE QUE ENCONTRE Y EL RESULTADO DE QUE SALIO BIEN LA BUSQUEDA
                    personajeEncontrado = Universo.listaPersonajesExistentes[i];
                    seEncontroElPj = true;
                    break;
                }
            }


            return personajeEncontrado;
        }

    }
}

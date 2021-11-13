using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Entidades;

namespace TP_03
{
    public partial class PanelControlForm : Form
    {
        /// <summary>
        /// Constructor del form PanelControl. Setea el Icono del form y la imagen del background.
        /// </summary>
        public PanelControlForm()
        {
            InitializeComponent();

            //Seteo el favicon y el background.
            this.Icon = Properties.Resources.panelControl;
            this.pictureBox_BackgroundFormPanelControl.Image = Properties.Resources.F3;
        }

        /// <summary>
        /// Metodo que se ejecuta al cargar este form. Se encarga de verificar si existe el arhivo que contiene
        /// los últimos datos ingresados de personajes. En caso de no existir el archivo, se crea e inicializa
        /// con personajes ya hardcodeados y que son agregados a la lista mediante la lectura del archivo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelControlForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Este va a ser el PATH donde voy a guardar SIEMPRE los personajes. Tanto lectura como escritura.
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, "Archivos-TP3-LopezGasal");
                path = Path.Combine(path, "ArchivoPersonajes.json");
            
                //Si no existe el archivo... entonces mando los datos hardcodeados al archivo ya creado por primera vez y me los traigo del archivo.
                if (File.Exists(path) == false)
                {
                    //Creo pjs hardcodeados
                    Personaje personaje1 = new Personaje("Oryx", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
                    Personaje personaje2 = new Personaje("Gabriel", 33, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Escudo);
                    Personaje personaje3 = new Personaje("Fran", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
                    Personaje personaje4 = new Personaje("Julio", 40, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);
                    Personaje personaje5 = new Personaje("a", 90, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
                    Personaje personaje6 = new Personaje("123", 50, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
                    Personaje personaje7 = new Personaje("aaa", 80, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
                    Personaje personaje8 = new Personaje("test", 80, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Arco);

                    //Los añado a la lista
                    Universo.listaPersonajesExistentes.Add(personaje1);
                    Universo.listaPersonajesExistentes.Add(personaje2);
                    Universo.listaPersonajesExistentes.Add(personaje3);
                    Universo.listaPersonajesExistentes.Add(personaje4);
                    Universo.listaPersonajesExistentes.Add(personaje5);
                    Universo.listaPersonajesExistentes.Add(personaje6);
                    Universo.listaPersonajesExistentes.Add(personaje7);
                    Universo.listaPersonajesExistentes.Add(personaje8);


                    if (File.Exists(path) == false) //Si no existe el archivo
                    {
                        //Lo creo con el hardcodeo
                        ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");
                    }      
                                                 
                    //Los elimino de la lista
                    Universo.listaPersonajesExistentes.Clear();
                }

                //Me cargo los personajes que existan en el archivo
                Universo.listaPersonajesExistentes = ArchivosManagement.LeerArchivoSerializacionJSON<List<Personaje>>("Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");
          
            }
            catch (Exception ExcepcionRecibida)
            {
                MessageBox.Show("Ocurrió una excepcion inesperada. Error guardado en el archivo de errores.", "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ArchivosManagement.EscribirArchivoTXT(ExcepcionRecibida.Message, false, "Archivos-TP3-LopezGasal//Errores", ArchivosManagement.GenerarNombreFechaHoraMntsConExtension("Error ", ".txt"));
            }            
        }

        /// <summary>
        /// Método que se ejecuta al presionar el boton Batalla. Oculta este form y muestra el form
        /// BatallaPjs. En caso de cerrarse, vuelve a mostrarse este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BatallaPanelControlForm_Click(object sender, EventArgs e)
        {
            //Creo y abro el form de BATALLA (Oculto este form y lo muestro si en un futuro se cierra el form abierto).
            BatallaPjsForm batallaPjsForm = new BatallaPjsForm();
            this.Hide();
            batallaPjsForm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Método que se ejecuta al presionar el boton Administracion. Oculta este form y muestra el form
        /// AdministracionPjs. En caso de cerrarse, vuelve a mostrarse este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AdministracionPjs_Click(object sender, EventArgs e)
        {
            //Creo y abro el form de ADMINISTRACION (Oculto este form y lo muestro si en un futuro se cierra el form abierto).
            AdministracionPjsForm administracionPjsForm = new AdministracionPjsForm();
            this.Hide();
            administracionPjsForm.ShowDialog();
            this.Show();
        }

        /// <summary>
        /// Método que se ejecuta al presionar el boton Impresion. Oculta este form y muestra el form
        /// ImpresionPjs. En caso de cerrarse, vuelve a mostrarse este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ImpresionPjs_Click(object sender, EventArgs e)
        {
            //Creo y abro el form de IMPRESION (Oculto este form y lo muestro si en un futuro se cierra el form abierto).
            ImpresionPjsForm impresionPjsForm = new ImpresionPjsForm();
            this.Hide();
            impresionPjsForm.ShowDialog();
            this.Show();

        }

        /// <summary>
        /// Sale de la aplicación mediante el llamado al método Application.Exit();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CerrarPrograma_Click(object sender, EventArgs e)
        {
            //Cierro la aplicacion en su totalidad.
            Application.Exit();
        }

    }
}

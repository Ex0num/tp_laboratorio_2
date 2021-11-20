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
using System.Threading;
using Excepciones;

namespace TP_03
{
    public partial class PanelControlForm : Form
    {
        /// <summary>
        /// Este flag, representa el estado de la tarea cargarPersonajesDB.
        /// Si se encuentra en false, es que la tarea se encuentra en ejecución y sin terminar.
        /// Si se encuentra en true, es que la tarea ya ha terminado.
        /// </summary>
        public static bool flag = false;

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
        /// Se encarga de verificar si existe el archivo que contiene los personajes y si la DB pudo traer
        /// algo. En caso de no existir el archivo, se crea y se inicializa con los personajes que trajo la base de datos. 
        /// En caso de que la base de datos no traiga nada, voy a hardcodearle los personajes pre-creados en memoria y cargados 
        /// a la lista. No cargo nada a la lista de personajes, solo verifico que el programa pueda iniciar leyendo algun personaje.
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

                //Me traigo lo que tengo en mi DB para ver si está o no vacía.
                Universo.listaPersonajesExistentesClonada = DB_Stuff.TraerPersonajesDB("select * from dbo.TablaPersonajes");

                //Si no existe el archivo... entonces mando los datos hardcodeados al archivo ya creado por primera vez y me los traigo del archivo.
                if (File.Exists(path) == false || Universo.listaPersonajesExistentesClonada.Count == 0)
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

                    if (File.Exists(path) == false && Universo.listaPersonajesExistentesClonada.Count > 0) //Si no existe el archivo y mi DB tiene algun personaje
                    {
                        //Voy a cargar en el archivo lo que tenga en mi DB.
                        ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentesClonada, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");
                    }      
                    else if (File.Exists(path) == false && Universo.listaPersonajesExistentesClonada.Count == 0) //Si no existe el archivo, y mi DB tampoco tiene nada. HARDCODEO
                    {
                        //Creo el archivo, mandando el hardcodeo. Al igual en la DB
                        ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");

                        //Mando el hardcodeo a la db
                        DB_Stuff.MandarTodosPersonajesDB(Universo.listaPersonajesExistentes);
                    }
                   
                    //Por las dudas, limpio la lista clonada, para no dejar basura que leí solo para hacer la comprobación de la DB.
                    Universo.listaPersonajesExistentesClonada.Clear();

                    //Los elimino de la lista
                    Universo.listaPersonajesExistentes.Clear();
                }

                //Creo mi delegado que contiene el metodo de carga de datos
                DelegadoCargaDB delegadoCargaDB;

                //Le asigno la tarea de traer de mi DB y un método que simule un tiempo de carga para demostrar que ocurre en otro hilo.
                delegadoCargaDB = this.metodo_Atrasador;
                delegadoCargaDB += DB_Stuff.TraerPersonajesDB;
              
                //Creo un nuevo hilo que es el de cargar personajes de la DB y lo ejecuto. Esto es realizado con un método anónimo LAMBDA.
                Task taskTraerPersonajesDB = Task.Run( () => 
                {                               
                    Universo.listaPersonajesExistentes = delegadoCargaDB("select * from dbo.TablaPersonajes");
                    
                    //Pongo mi flag en true, para saber cuando el task ya terminó.
                    flag = true;
                });
                                                 
            }
            catch (Exception ExcepcionRecibida)
            {
                MessageBox.Show("Ocurrió una excepcion inesperada. Error guardado en el archivo de errores.", "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ArchivosManagement.EscribirArchivoTXT(ExcepcionRecibida.Message, false, "Archivos-TP3-LopezGasal//Errores", ArchivosManagement.GenerarNombreFechaHoraMntsConExtension("Error ", ".txt"));
            }            
        }

        /// <summary>
        /// Delegado de tipo carga de la DB. Retorna si o si una lista de personajes y recibe un string que 
        /// es representativo a una query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public delegate List<Personaje> DelegadoCargaDB(string query);

        /// <summary>
        /// Método que se ejecuta al presionar el boton Batalla. Oculta este form y muestra el form
        /// BatallaPjs. En caso de cerrarse, vuelve a mostrarse este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BatallaPanelControlForm_Click(object sender, EventArgs e)
        {
            //No va a poder acceder al form de batalla (que contiene la lista de personajes) si no se terminó la carga de personajes.
            try
            {
                if (flag == false)
                {
                    ExceptionIncompleteInformation exception = new ExceptionIncompleteInformation("No se puede acceder si está la carga de personajes activa");
                    throw exception;
                }
                else
                {
                    //Creo y abro el form de BATALLA (Oculto este form y lo muestro si en un futuro se cierra el form abierto).
                    BatallaPjsForm batallaPjsForm = new BatallaPjsForm();
                    this.Hide();
                    batallaPjsForm.ShowDialog();
                    this.Show();
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
            //No va a poder acceder al form de impresion (que contiene la lista de personajes) si no se terminó la carga de personajes.
            try
            {
                if (flag == false)
                {
                    ExceptionIncompleteInformation exception = new ExceptionIncompleteInformation("No se puede acceder si está la carga de personajes activa");
                    throw exception;
                }
                else
                {
                    //Creo y abro el form de IMPRESION (Oculto este form y lo muestro si en un futuro se cierra el form abierto).
                    ImpresionPjsForm impresionPjsForm = new ImpresionPjsForm();
                    this.Hide();
                    impresionPjsForm.ShowDialog();
                    this.Show();
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
        /// Sale de la aplicación mediante el llamado al método Application.Exit();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CerrarPrograma_Click(object sender, EventArgs e)
        {
            //Cierro la aplicacion en su totalidad.
            Application.Exit();
        }

        /// <summary>
        /// Este método solo se utiliza para mostrar como la tarea de la carga de la base de datos
        /// se ejecuta en otro hilo y no se podrá acceder a funcionalidades como 
        /// formImpresiones, formBatalla, no se podrá crear personajes, eliminar, ni modificar.
        /// Simula un tiempo de carga.
        /// </summary>
        /// <param name="stringInutil"></param>
        /// <returns></returns>
        private List<Personaje> metodo_Atrasador (string stringInutil)
        {
            //8 SEGUNDOS DE RETRASO. AUMENTABLE SI ES NECESARIO. :)
            Thread.Sleep(8000);
            List<Personaje> listaInutil = new List<Personaje>();
            return listaInutil;
        }
    }
}

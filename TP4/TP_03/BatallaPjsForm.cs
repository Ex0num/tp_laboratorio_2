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

                        //Le asocio al evento batallar, su manejador.
                        Universo.Batallar += Universo.ManejadorEventoBatallar;

                        //Llamo a mi metodo enfrentamiento, que es el que invoca al evento batallar y este (ya asociado a su manejador)
                        //actua en cuestion de la batalla.
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

                                        //Tiene una batalla ganada mas.
                                        personaje1.BatallasGanadas++;

                                        break;
                                    }
                                case 2: //Si gana el personaje 2.
                                    {
                                        MessageBox.Show($"El personaje {personaje2.NombrePersonaje} ganó la pelea justamente. Derribando por los suelos sin piedad alguna a {personaje1.NombrePersonaje}, rematandolo violentamente utilizando su {personaje2.Arma.TipoArma}.", "Resultado batalla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                                        //Tiene una batalla ganada mas.
                                        personaje2.BatallasGanadas++;

                                        break;
                                    }
                            }

                            //Ambos jugaron una batalla. Asi que lo contemplo.
                            personaje1.BatallasJugadas++;
                            personaje2.BatallasJugadas++;

                            //Guardo la modificacion de los 2 personajes en mi DB
                            DB_Stuff.ModificarUnPersonajeDB(personaje1, personaje1.NombrePersonaje, personaje1.NivelTotal, personaje1.OrigenElemental, personaje1.Arma.TipoArma, personaje1.BatallasJugadas, personaje1.BatallasGanadas);
                            DB_Stuff.ModificarUnPersonajeDB(personaje2, personaje2.NombrePersonaje, personaje2.NivelTotal, personaje2.OrigenElemental, personaje2.Arma.TipoArma, personaje2.BatallasJugadas, personaje2.BatallasGanadas);

                            //Guardo la modificacion de los 2 personajes en mi archivo back-up.
                            ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");

                            //Refresco la listbox por los pequeños cambios habidos.
                            RefreshListBoxPersonajes();

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

        /// <summary>
        /// Se inicia al cargar el formulario, llama el metodo RefreshListbox, para mostrar todos los personajes
        /// existentes hasta el momento junto a todos sus datos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BatallaPjsForm_Load(object sender, EventArgs e)
        {
            RefreshListBoxPersonajes();
        }

        /// <summary>
        /// Refresca la listbox de personajes, limpiandola y agregando todos los personajes existentes nuevamente. 
        /// Es utilizada para mostrar los cambios habidos en las victorias y batallas jugadas.
        /// </summary>
        private void RefreshListBoxPersonajes()
        {
            listBox_PersonajesBatallaPjsForm.Items.Clear();

            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
            {
                ImprimirPersonaje(personaje);
            }     
        }

        /// <summary>
        /// Recibe un personaje e imprime absolutamente todos sus datos en la listbox, incluyendo los de su arma.
        /// </summary>
        /// <param name="personaje"></param>
        public void ImprimirPersonaje(Personaje personaje)
        {
            listBox_PersonajesBatallaPjsForm.Items.Add($"Nombre: {personaje.NombrePersonaje}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Nivel: {personaje.NivelTotal}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Origen elemental: {personaje.OrigenElemental}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Arma: {personaje.Arma.TipoArma}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Pts ataque: {personaje.Arma.PtsAtaque}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Pts defensa: {personaje.Arma.PtsDefensa}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Batallas jugadas: {personaje.BatallasJugadas}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"Batallas ganadas: {personaje.BatallasGanadas}");
            listBox_PersonajesBatallaPjsForm.Items.Add($"ID: {personaje.IdPersonaje}");

            listBox_PersonajesBatallaPjsForm.Items.Add($"");
        }

        /// <summary>
        /// Al presionar el boton BatallaAutomatica se escribirá en la textbox de los nombres de los personajes
        /// 2 nombres aleatorios de la lista. (Con el plus de que no serán nombres repetidos uno del otro). Siempre
        /// serán personajes distintos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BatallaAutomaticaBatallaPjsForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Universo.listaPersonajesExistentes.Count > 1)
                {
                    //Creo la instancia de random
                    Random aleatorio = new Random();

                    //Obtengo mi maximo del numero, que va a ser el "maximo" de la lista
                    int maximoNumAleatorio = Universo.listaPersonajesExistentes.Count;

                    int numeroAleatorio1;
                    int numeroAleatorio2;

                    //Busco personaje desde el 0 hasta el "maximo"
                    numeroAleatorio1 = aleatorio.Next(0, maximoNumAleatorio);

                    //Me aseguro de que mi 2do numero aleatorio no sea el mismo que el primero. Para no enfrentar 2 personajes iguales.       
                    do
                    {
                        numeroAleatorio2 = aleatorio.Next(0, maximoNumAleatorio);

                    } while (numeroAleatorio1 == numeroAleatorio2);

                    //Escribo el nombre de los personajes obtenidos
                    textBox_NombreHeroe1BatallaPjsForm.Text = Universo.listaPersonajesExistentes[numeroAleatorio1].NombrePersonaje;
                    textBox_NombreHeroe2BatallaPjsForm.Text = Universo.listaPersonajesExistentes[numeroAleatorio2].NombrePersonaje;
                }
                else
                {
                    //EXCEPCION - NO HAY 2 PERSONAJES COMO MINIMO
                    Exception exception = new ExceptionIncompleteInformation("No se puede buscar 2 nombres aleatorios sin la existencia de 2 personajes como minimo.");
                    throw exception;
                }
            }
            catch (Exception ExcepcionRecibida)
            {
                if (ExcepcionRecibida.GetType() == typeof(Exception))
                {
                    MessageBox.Show("Ocurrió una excepcion inesperada.", "Error inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ExcepcionRecibida.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ArchivosManagement.EscribirArchivoTXT(ExcepcionRecibida.Message, false, "Archivos-TP3-LopezGasal//Errores", ArchivosManagement.GenerarNombreFechaHoraMntsConExtension("Error ", ".txt"));
            }
        }
    }
}

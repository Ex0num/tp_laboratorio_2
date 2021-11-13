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
using Entidades;
using Excepciones;

namespace TP_03
{
    public partial class ImpresionPjsForm : Form
    {
        /// <summary>
        /// Constructor de ImpresionPjs, setea el Icono y la imagen del background.
        /// </summary>
        public ImpresionPjsForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.save;
            this.pictureBox_BackgroundImpresionPjsForm.Image = Properties.Resources.F4;
        }

        /// <summary>
        /// Se ejecuta al cargar el form. Se reinicializa la lista de personajes existentes clonada
        /// con la lista original de personajes existentes, se actualiza el archivo que contiene 
        /// los personajes filtrados y se refresca lo que se muestra en la listBox del form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImpresionPjsForm_Load(object sender, EventArgs e)
        {
            //Reinicio la lista clonada copiando los valores de la lista "original".
            Universo.ReinicializarLista(Universo.listaPersonajesExistentesClonada, Universo.listaPersonajesExistentes);

            //Actualizo el archivo de filtraciones.
            EscribirArchivoFiltracionesYOrdenamientos();

            //Imprimo el estado actual de la lista clonada en la listbox.
            RefrescarListBoxPersonajes();

        }

        /// <summary>
        /// Se ejecuta al presionar el boton "?". Muestra un MessageBox con un mensaje de guía que
        /// informa brevemente como funciona la impresion de personajes el filtrado, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_HelperImpresionPjsForm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bienvenido al boton de ayuda \nAl momento de guardar cualquier " +
                "cambio en el filtrado de los personajes, será creado en la ruta correspondiente al disco " +
                "donde se vea contenido su usuario, dentro de la sección documentos.\n" +
                "Tenga en cuenta que si se precisa visualizar el filtrado de personajes en un archivo, este mismo se encontrará " +
                "guardado en la ruta mencionada anteriormente y podra visualizar el filtro aplicado sin necesidad de tener " +
                "la aplicación corriendo. Recuerde que si no existen personajes posiblemente no tenga acceso a la mayoria de funcinalidades " +
                "de filtrado. Además de que si no hay personajes con alguna batalla jugada, también perderá la posibilidad de utilizar " +
                "funcionalidades como calcular porcentajes de victoria.");
        }

        /// <summary>
        /// Cierra este form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VolverImpresionPjsForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Se inicia al presionar el botón Borrar filtros.
        /// Reinicializa la lista de personajes clonada con la lista de personajes original.
        /// Actualiza el archivo que contiene los personajes filtrados y refresca la listbox de
        /// personajes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BorrarFiltrosImpresionPjsForm_Click(object sender, EventArgs e)
        {
            //Reinicio la lista clonada copiando los valores de la lista "original".
            Universo.ReinicializarLista(Universo.listaPersonajesExistentesClonada, Universo.listaPersonajesExistentes);

            //Actualizo el archivo de filtraciones.
            EscribirArchivoFiltracionesYOrdenamientos();
            
            //Imprimo el estado actual de la lista clonada en la listbox.
            RefrescarListBoxPersonajes();
        }

        /// <summary>
        /// Se obtiene la informacion elegida/ingresada. Se verifica que haya
        /// algo seleccionado en la combo-box del campo a filtrar y en esta seleccionado el origen
        /// o el arma, se verificará tambien que la 2da combo-box tenga algun item seleccionado tal y como
        /// la primera. Si en la primer combo-box no se seleccionó origen ni arma, se seguirá mostrando el textbox
        /// y se validará la informacion que contenga.
        /// Una vez validada la informacion y el campo a filtrar, se mostraran todos aquellos personajes
        /// que cumplan con la informacion ingresada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_FiltrarImpresionPjsForm_Click(object sender, EventArgs e)
        {

            try
            {
                string informacion;
                int indiceComboBox;
                int indiceComboBoxOculta;

                //Obtengo la informacion.
                informacion = textBox_InformacionIngresadaImpresionPjsForm.Text;
                indiceComboBox = comboBox_SeccionElegidaImpresionPjsForm.SelectedIndex;
                indiceComboBoxOculta = comboBox_InformacionIngresadaImpresionPjsForm.SelectedIndex;

                //Le hago un trim
                informacion = informacion.Trim();

                //Valido que mi informacion sea logica y que mi combo-box tenga algo seleccionado para saber por que cosa filtrar
                if (indiceComboBox > -1)
                {
                    //NO ESTA SELECCIONADO NINGUN CAMPO QUE NECESITE OTRA COMBO-BOX
                    if (indiceComboBox != 2 && indiceComboBox != 4 && indiceComboBox != 5 && indiceComboBox != 6 && indiceComboBox != 7)
                    {
                        if (string.IsNullOrEmpty(informacion) == false)
                        {
                            //Si tengo todo válido, limpio la listbox, la lista clonada y comienzo el proceso de filtrado.
                            listBox_InformacionImpresionPjsForm.Items.Clear();
                            LimpiarListaClonadaYArchivo();

                            int maximo = Universo.listaPersonajesExistentes.Count;

                            switch (indiceComboBox)
                            {
                                case 0: //Filtrar por nombre.
                                    {
                                        //Añado a mi lista clonada todos aquellos personajes que cumplan la informacion ingresada.
                                        for (int i = 0; i < maximo; i++)
                                        {
                                            if (Universo.listaPersonajesExistentes[i].NombrePersonaje.Contains(informacion) == true)
                                            {
                                                //Añado en la lista de personajes vacia el personaje encontrado.
                                                Universo.listaPersonajesExistentesClonada.Add(Universo.listaPersonajesExistentes[i]);
                                            }
                                        }

                                        break;
                                    }
                                case 1: //Filtrar por nivel.
                                    {
                                        int informacionNumerica;

                                        if (int.TryParse(informacion, out informacionNumerica) == true)
                                        {
                                            for (int i = 0; i < maximo; i++)
                                            {
                                                if (Universo.listaPersonajesExistentes[i].NivelTotal == informacionNumerica)
                                                {
                                                    //Añado en la lista de personajes vacia el personaje encontrado.
                                                    Universo.listaPersonajesExistentesClonada.Add(Universo.listaPersonajesExistentes[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                            Exception exception = new ExceptionInvalidInformation("No se puede filtrar por informacion no valida, nula o inexistente.");
                                            throw exception;
                                        }

                                        break;
                                    }                              
                                case 3: //Filtrar por ID.
                                    {
                                        int informacionNumerica;

                                        if (int.TryParse(informacion, out informacionNumerica) == true)
                                        {
                                            for (int i = 0; i < maximo; i++)
                                            {
                                                if (Universo.listaPersonajesExistentes[i].IdPersonaje == informacionNumerica)
                                                {
                                                    //Añado en la lista de personajes vacia el personaje encontrado.
                                                    Universo.listaPersonajesExistentesClonada.Add(Universo.listaPersonajesExistentes[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                            Exception exception = new ExceptionInvalidInformation("No se puede filtrar por informacion no valida, nula o inexistente.");
                                            throw exception;
                                        }

                                        break;
                                    }                              
                            }

                            //Imprimo los personajes que me quedaron en la lista clonada (que serian los que cumplieron el filtro).
                            RefrescarListBoxPersonajes();

                            //Guardo los cambios del filtrado en el archivo
                            EscribirArchivoFiltracionesYOrdenamientos();
                        }
                        else
                        {
                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS O INEXISTENTES
                            Exception exception = new ExceptionIncompleteInformation("No se puede filtrar por informacion no valida, nula o inexistente.");
                            throw exception;
                        }
                    }
                    else
                    {
                        //Si tengo todo válido, limpio la listbox, la lista clonada y comienzo el proceso de filtrado.
                        listBox_InformacionImpresionPjsForm.Items.Clear();
                        LimpiarListaClonadaYArchivo();

                        int maximo = Universo.listaPersonajesExistentes.Count;

                        switch (indiceComboBox)
                        {
                            case 2: //Filtrar por Origen.
                                {
                                    if (comboBox_InformacionIngresadaImpresionPjsForm.SelectedIndex >= 0)
                                    {
                                        if (Universo.listaPersonajesExistentes.Count != 0)
                                        {
                                            for (int i = 0; i < maximo; i++)
                                            {
                                                if (Universo.listaPersonajesExistentes[i].OrigenElemental == (Personaje.enumOrigenElemental)indiceComboBoxOculta)
                                                {
                                                    //Añado en la lista de personajes vacia el personaje encontrado.
                                                    Universo.listaPersonajesExistentesClonada.Add(Universo.listaPersonajesExistentes[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                            Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                            RefrescarListBoxPersonajes();
                                            throw exception;
                                        }

                                        //Imprimo los personajes que me quedaron en la lista clonada (que serian los que cumplieron el filtro).
                                        RefrescarListBoxPersonajes();

                                    }
                                    else
                                    {
                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                        Exception exception = new ExceptionComboBoxNotChoosen("No se puede filtrar por una informacion inexistente. Se debe seleccionar algo obligatoriamente.");
                                        throw exception;
                                    }

                                    break;
                                }
                            case 4: //Filtrar por Arma.
                                {

                                    if (comboBox_InformacionIngresadaImpresionPjsForm.SelectedIndex >= 0)
                                    {
                                        if (Universo.listaPersonajesExistentes.Count != 0)
                                        {
                                            for (int i = 0; i < maximo; i++)
                                            {
                                                if (Universo.listaPersonajesExistentes[i].Arma.TipoArma == (Arma.enumTipoArma)indiceComboBoxOculta)
                                                {
                                                    //Añado en la lista de personajes vacia el personaje encontrado.
                                                    Universo.listaPersonajesExistentesClonada.Add(Universo.listaPersonajesExistentes[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                            Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                            RefrescarListBoxPersonajes();
                                            throw exception;
                                        }

                                        //Imprimo los personajes que me quedaron en la lista clonada (que serian los que cumplieron el filtro).
                                        RefrescarListBoxPersonajes();
                                    }
                                    else
                                    {
                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                        Exception exception = new ExceptionComboBoxNotChoosen("No se puede filtrar por una informacion inexistente. Se debe seleccionar algo obligatoriamente.");
                                        throw exception;
                                    }

                                    break;
                                }
                            case 5: //Filtrar por Mayor Winrate
                                {

                                    if (comboBox_InformacionIngresadaImpresionPjsForm.SelectedIndex >= 0)
                                    {

                                        switch (indiceComboBoxOculta)
                                        {
                                            case 0: //Personaje
                                                {
                                                    Personaje personajeMayorWinrate;
                                                    double mayorWinrate;

                                                    //Lamo a mi metodo de obtencion indicandole que quiero el MAYOR WINRATE
                                                    Universo.ObtenerWinratePersonaje(out personajeMayorWinrate, out mayorWinrate,true);
                                                
                                                    
                                                    if (Universo.listaPersonajesExistentes.Count != 0)
                                                    {
                                                        if (personajeMayorWinrate != null)
                                                        {
                                                            listBox_InformacionImpresionPjsForm.Items.Clear();
                                                            listBox_InformacionImpresionPjsForm.Items.Add("El personaje con mayor winrate es el siguiente:");
                                                            listBox_InformacionImpresionPjsForm.Items.Add("");
                                                            ImprimirPersonaje(personajeMayorWinrate);
                                                            listBox_InformacionImpresionPjsForm.Items.Add($"Con un winratio de: {mayorWinrate}%");
                                                        }
                                                        else
                                                        {
                                                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                            Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje con alguna batalla realizada.");
                                                            RefrescarListBoxPersonajes();
                                                            throw exception;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                        Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                                        RefrescarListBoxPersonajes();
                                                        throw exception;
                                                    }

                                                    //Añado mi unico personaje a la lista para que se guarde posteriormente en el archivo
                                                    Universo.listaPersonajesExistentesClonada.Add(personajeMayorWinrate);

                                                    break;
                                                }
                                            case 1: //Arma
                                                {
                                                  
                                                    if (Universo.listaPersonajesExistentes.Count > 0)
                                                    {
                                                        Arma.enumTipoArma armaMayorWinrate;
                                                        double mayorWinrate;

                                                        //Lamo a mi metodo de obtencion indicandole que quiero el MAYOR WINRATE
                                                        Universo.ObtenerWinrateArma(out armaMayorWinrate, out mayorWinrate, true);

                                                        if (mayorWinrate != -1 && mayorWinrate != -2)
                                                        {
                                                            listBox_InformacionImpresionPjsForm.Items.Clear();
                                                            listBox_InformacionImpresionPjsForm.Items.Add("El arma con mayor winrate es la siguiente:");
                                                            listBox_InformacionImpresionPjsForm.Items.Add("");
                                                            ImprimirArma(armaMayorWinrate);
                                                            listBox_InformacionImpresionPjsForm.Items.Add($"Con un winratio de {mayorWinrate}%");
                                                        }
                                                        else 
                                                        {

                                                            if (mayorWinrate == -1)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje con alguna batalla realizada.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }
                                                            else if (mayorWinrate == -2)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo obtener el arma con el mayor winrate ya que todas las armas tienen 0%.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }
                                                           
                                                        }  
                                                        
                                                    }
                                                    else
                                                    {
                                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                        Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                                        RefrescarListBoxPersonajes();
                                                        throw exception;
                                                    }

                                                    break;
                                                }
                                            case 2: //Poder
                                                {

                                                    if (Universo.listaPersonajesExistentes.Count > 0)
                                                    {
                                                        Personaje.enumOrigenElemental poderMayorWinrate;
                                                        double mayorWinrate;

                                                        //Lamo a mi metodo de obtencion indicandole que quiero el MAYOR WINRATE
                                                        Universo.ObtenerWinratePoder(out poderMayorWinrate, out mayorWinrate, true);
             
                                                        if (mayorWinrate != -1 && mayorWinrate != -2)
                                                        {
                                                            listBox_InformacionImpresionPjsForm.Items.Clear();
                                                            listBox_InformacionImpresionPjsForm.Items.Add("El poder con mayor winrate es el siguiente:");
                                                            listBox_InformacionImpresionPjsForm.Items.Add("");
                                                            ImprimirPoder(poderMayorWinrate);
                                                            listBox_InformacionImpresionPjsForm.Items.Add($"Con un winratio de {mayorWinrate}%");
                                                        }
                                                        else
                                                        {

                                                            if (mayorWinrate == -1)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje con alguna batalla realizada.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }
                                                            else if (mayorWinrate == -2)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo obtener el poder con el mayor winrate ya que todas las poderes tienen 0%.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                        Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                                        RefrescarListBoxPersonajes();
                                                        throw exception;
                                                    }

                                                    break;
                                                }
                                        }

                                    }
                                    else
                                    {
                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                        Exception exception = new ExceptionComboBoxNotChoosen("No se puede filtrar por una informacion inexistente. Se debe seleccionar algo obligatoriamente.");
                                        throw exception;
                                    }

                                        break;
                                }
                            case 6: //Filtrar por Menor Winrate
                                {

                                    if (comboBox_InformacionIngresadaImpresionPjsForm.SelectedIndex >= 0)
                                    {
                                        switch (indiceComboBoxOculta)
                                        {
                                            case 0: //Personaje
                                                {
                                                    Personaje personajeMenorWinrate;
                                                    double mayorWinrate;

                                                    //Lamo a mi metodo de obtencion indicandole que quiero el MENOR WINRATE
                                                    Universo.ObtenerWinratePersonaje(out personajeMenorWinrate, out mayorWinrate, false);

                                                    if (Universo.listaPersonajesExistentes.Count != 0)
                                                    {
                                                        if (personajeMenorWinrate != null)
                                                        {
                                                            listBox_InformacionImpresionPjsForm.Items.Clear();
                                                            listBox_InformacionImpresionPjsForm.Items.Add("El personaje con menor winrate es el siguiente:");
                                                            listBox_InformacionImpresionPjsForm.Items.Add("");
                                                            ImprimirPersonaje(personajeMenorWinrate);
                                                            listBox_InformacionImpresionPjsForm.Items.Add($"Con un winratio de: {mayorWinrate}%");
                                                        }
                                                        else
                                                        {
                                                            //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                            Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje con alguna batalla realizada.");
                                                            RefrescarListBoxPersonajes();
                                                            throw exception;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                        Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                                        RefrescarListBoxPersonajes();
                                                        throw exception;
                                                    }



                                                    //Añado mi unico personaje a la lista para que se guarde posteriormente en el archivo
                                                    Universo.listaPersonajesExistentesClonada.Add(personajeMenorWinrate);

                                                    break;
                                                }
                                            case 1: //Arma
                                                {
                                                    
                                                    if (Universo.listaPersonajesExistentes.Count > 0)
                                                    {
                                                        Arma.enumTipoArma armaMenorWinrate;
                                                        double menorWinrate;

                                                        //Lamo a mi metodo de obtencion indicandole que quiero el MENOR WINRATE
                                                        Universo.ObtenerWinrateArma(out armaMenorWinrate, out menorWinrate, false);
     
                                                        if (menorWinrate != -1 && menorWinrate != -2)
                                                        {
                                                            listBox_InformacionImpresionPjsForm.Items.Clear();
                                                            listBox_InformacionImpresionPjsForm.Items.Add("El arma con menor winrate es la siguiente:");
                                                            listBox_InformacionImpresionPjsForm.Items.Add("");
                                                            ImprimirArma(armaMenorWinrate);
                                                            listBox_InformacionImpresionPjsForm.Items.Add($"Con un winratio de {menorWinrate}%");
                                                        }
                                                        else
                                                        {

                                                            if (menorWinrate == -1)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje con alguna batalla realizada.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }
                                                            else if (menorWinrate == -2)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo obtener el arma con el menor winrate ya que todas las armas tienen 0%.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                        Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                                        RefrescarListBoxPersonajes();
                                                        throw exception;
                                                    }

                                                    break;
                                                }
                                            case 2: //Poder
                                                {

                                                    if (Universo.listaPersonajesExistentes.Count > 0)
                                                    {
                                                        Personaje.enumOrigenElemental poderMenorWinrate;
                                                        double menorWinrate;

                                                        //Lamo a mi metodo de obtencion indicandole que quiero el MENOR WINRATE
                                                        Universo.ObtenerWinratePoder(out poderMenorWinrate, out menorWinrate, false);
                  
                                                        if (menorWinrate != -1 && menorWinrate != -2)
                                                        {
                                                            listBox_InformacionImpresionPjsForm.Items.Clear();
                                                            listBox_InformacionImpresionPjsForm.Items.Add("El poder con menor winrate es el siguiente:");
                                                            listBox_InformacionImpresionPjsForm.Items.Add("");
                                                            ImprimirPoder(poderMenorWinrate);
                                                            listBox_InformacionImpresionPjsForm.Items.Add($"Con un winratio de {menorWinrate}%");
                                                        }
                                                        else
                                                        {

                                                            if (menorWinrate == -1)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje con alguna batalla realizada.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }
                                                            else if (menorWinrate == -2)
                                                            {
                                                                //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                                Exception exception = new ExceptionIncompleteInformation("No se pudo obtener el poder con el menor winrate ya que todas los poderes tienen 0%.");
                                                                RefrescarListBoxPersonajes();
                                                                throw exception;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS (NO EXISTE NINGUN PERSONAJE)
                                                        Exception exception = new ExceptionIncompleteInformation("No se pudo filtrar porque no existe ningun personaje.");
                                                        RefrescarListBoxPersonajes();
                                                        throw exception;
                                                    }

                                                    break;
                                                }
                                        }
                                    }
                                    else
                                    {
                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                        Exception exception = new ExceptionComboBoxNotChoosen("No se puede filtrar por una informacion inexistente. Se debe seleccionar algo obligatoriamente.");
                                        throw exception;
                                    }

                                    break;
                                }
                            case 7: //Filtrar por Porcentajes
                                {

                                    if (comboBox_InformacionIngresadaImpresionPjsForm.SelectedIndex >= 0)
                                    {
                                        switch (indiceComboBoxOculta)
                                        {
                                            case 0: //Poderes
                                                {
                                                    double porcentajeFuego;
                                                    double porcentajeAgua;
                                                    double porcentajeHielo;

                                                    //Lamo a mi metodo de obtencion indicandole que quiero el PORCENTAJE DE PODERES
                                                    Universo.ObtenerPorcentajes(false, out porcentajeFuego, out porcentajeAgua, out porcentajeHielo);

                                                    listBox_InformacionImpresionPjsForm.Items.Clear();
                                                    listBox_InformacionImpresionPjsForm.Items.Add("Los porcentajes de cada poder son los siguientes:");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");
                                                    listBox_InformacionImpresionPjsForm.Items.Add($"El poder fuego con un porcentaje de: {porcentajeFuego}%");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");
                                                    listBox_InformacionImpresionPjsForm.Items.Add($"El poder agua con un porcentaje de: {porcentajeAgua}%");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");
                                                    listBox_InformacionImpresionPjsForm.Items.Add($"El poder hielo con un porcentaje de: {porcentajeHielo}%");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");

                                                    break;
                                                }
                                            case 1: //Armas
                                                {
                                                    double porcentajeArco;
                                                    double porcentajeEscudo;
                                                    double porcentajeBaston;

                                                    //Lamo a mi metodo de obtencion indicandole que quiero el PORCENTAJE DE PODERES
                                                    Universo.ObtenerPorcentajes(false, out porcentajeArco, out porcentajeEscudo, out porcentajeBaston);

                                                    listBox_InformacionImpresionPjsForm.Items.Clear();
                                                    listBox_InformacionImpresionPjsForm.Items.Add("Los porcentajes de cada arma son los siguientes:");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");
                                                    listBox_InformacionImpresionPjsForm.Items.Add($"El arma Arco con un porcentaje de: {porcentajeArco}%");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");
                                                    listBox_InformacionImpresionPjsForm.Items.Add($"El arma Escudo con un porcentaje de: {porcentajeEscudo}%");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");
                                                    listBox_InformacionImpresionPjsForm.Items.Add($"El arma Baston Magico con un porcentaje de: {porcentajeBaston}%");
                                                    listBox_InformacionImpresionPjsForm.Items.Add("");

                                                    break;
                                                }
                                                
                                        }

                                    }
                                    else
                                    {
                                        //EXCEPCION - LOS DATOS DE LA INFORMACION PARA FILTRAR SON INVALIDOS
                                        Exception exception = new ExceptionComboBoxNotChoosen("No se puede filtrar por una informacion inexistente. Se debe seleccionar algo obligatoriamente.");
                                        throw exception;
                                    }

                                    break;
                                }
                        }
                    
                        //Guardo los cambios del filtrado en el archivo
                        EscribirArchivoFiltracionesYOrdenamientos();
                    }
                    
                }
                else
                {
                    //EXCEPCION - LOS DATOS DEL NUEVO PERSONAJE NO SON CORRECTOS
                    Exception exception = new ExceptionComboBoxNotChoosen("No se puede filtrar por un campo de filtro no seleccionado. Verifique combo-box.");
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
        /// Se ejecuta al detectar un cambio en el indice seleccionado de la combo-box.
        /// Si se eligió el indice 2 o 4 se oculta el textbox de informacion y se muestra
        /// una 2da combo-box para seleccionar la informacion a filtrar (ya pre-cargada).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SeccionElegidaImpresionPjsForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = comboBox_SeccionElegidaImpresionPjsForm.SelectedIndex;


            switch (indice)
            {
                case 0://Se eligió Nombre
                    {
                        //Muestro textbox de informacion
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = true;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        break;
                    }

                case 1://Se eligió Nivel
                    {
                        //Muestro textbox de informacion
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = true;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        break;
                    }
                case 2://Se eligió Origen
                    {
                        //Muestro combo-box de informacion (Con info de origenes)
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = true;

                        comboBox_InformacionIngresadaImpresionPjsForm.Text = "";
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Clear();    
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Fuego");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Agua");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Hielo");
                        break;
                    }
                case 3://Se eligió ID
                    {
                        //Muestro textbox de informacion
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = true;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        break;
                    }
                case 4://Se eligió arma
                    {
                        //Muestro combo-box de informacion (Con info de origenes)
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = true;

                        comboBox_InformacionIngresadaImpresionPjsForm.Text = "";
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Clear();
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Escudo");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Arco");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("BastonMagico");
                        break;
                    }
                case 5: //Se eligio Mayor Winrate
                    {
                        //Muestro combo-box de informacion (con opciones de calcular Winrate)
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = true;

                        comboBox_InformacionIngresadaImpresionPjsForm.Text = "";
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Clear();
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Personaje");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Arma");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Poder");
                        break;
                    }
                case 6: //Se eligio Menor Winrate
                    {
                        //Muestro combo-box de informacion (con opciones de calcular Winrate)
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = true;

                        comboBox_InformacionIngresadaImpresionPjsForm.Text = "";
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Clear();
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Personaje");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Arma");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Poder");
                        break;
                    }
                case 7: //Se eligio Porcentajes
                    {
                        //Muestro combo-box de informacion (con opciones de calcular Winrate)
                        textBox_InformacionIngresadaImpresionPjsForm.Visible = false;
                        comboBox_InformacionIngresadaImpresionPjsForm.Visible = true;

                        comboBox_InformacionIngresadaImpresionPjsForm.Text = "";
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Clear();
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Poderes");
                        comboBox_InformacionIngresadaImpresionPjsForm.Items.Add("Armas");
                        break;
                    }
            }
        }

        /// <summary>
        /// Se llama al metodo de ArchivosManagement que escribe el archivo que contiene las filtraciones de
        /// personajes. Se manda por parametros la ruta ya hardcodeada.
        /// </summary>
        private void EscribirArchivoFiltracionesYOrdenamientos()
        {
            ArchivosManagement.EscribirArchivoSerializacionXML<List<Personaje>>(Universo.listaPersonajesExistentesClonada, "Archivos-TP3-LopezGasal", "ArchivoPersonajesFiltradosYOrdenados.xml");
        }

        /// <summary>
        /// Se hace una limpieza de la listbox de informacion y se imprime cada personaje
        /// existente en la lista de personajes clonada.
        /// </summary>
        private void RefrescarListBoxPersonajes()
        {
            listBox_InformacionImpresionPjsForm.Items.Clear();

            foreach (Personaje personaje in Universo.listaPersonajesExistentesClonada)
            {
                ImprimirPersonaje(personaje);          
            }

        }

        /// <summary>
        /// Se imprime en la listbox un personaje ingresado, con todos sus datos.
        /// </summary>
        /// <param name="personaje"></param>
        public void ImprimirPersonaje(Personaje personaje)
        {
            listBox_InformacionImpresionPjsForm.Items.Add($"Nombre: {personaje.NombrePersonaje}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Nivel: {personaje.NivelTotal}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Origen elemental: {personaje.OrigenElemental}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Arma: {personaje.Arma.TipoArma}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Pts ataque: {personaje.Arma.PtsAtaque}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Pts defensa: {personaje.Arma.PtsDefensa}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Batallas jugadas: {personaje.BatallasJugadas}");
            listBox_InformacionImpresionPjsForm.Items.Add($"Batallas ganadas: {personaje.BatallasGanadas}");
            listBox_InformacionImpresionPjsForm.Items.Add($"ID: {personaje.IdPersonaje}");

            listBox_InformacionImpresionPjsForm.Items.Add($"");
        }

        /// <summary>
        /// Imprime en la listbox un tipo de arma ingresado.
        /// </summary>
        /// <param name="arma"></param>
        public void ImprimirArma(Arma.enumTipoArma arma)
        {
            listBox_InformacionImpresionPjsForm.Items.Add($"Tipo de arma: {arma}");

            listBox_InformacionImpresionPjsForm.Items.Add($"");
        }

        /// <summary>
        /// Imprime en la listbox un tipo de origen elemental ingresado.
        /// </summary>
        /// <param name="origenElemental"></param>
        public void ImprimirPoder(Personaje.enumOrigenElemental origenElemental)
        {
            listBox_InformacionImpresionPjsForm.Items.Add($"Tipo de poder: {origenElemental}");

            listBox_InformacionImpresionPjsForm.Items.Add($"");
        }

        /// <summary>
        /// Se limpia la lista de personajes clonada y se llama al metodo que escribe en el archivo que contiene
        /// los personajes filtrados. 
        /// </summary>
        private void LimpiarListaClonadaYArchivo()
        {
            Universo.listaPersonajesExistentesClonada.Clear();
            EscribirArchivoFiltracionesYOrdenamientos();

        }

    }
}

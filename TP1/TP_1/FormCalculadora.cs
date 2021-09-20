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

namespace TP_1
{
    public partial class FormCalculadora : Form
    {
        /// <summary>
        /// /Constructor del form Calculadora. Inicializa los componentes.
        /// </summary>
        public FormCalculadora()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Inicia el proceso de limpieza de los TextBoxes, ComboBox, Label y ListBox que contienen numeros, Operadores, Resultado y Lista de operaciones. (Utiliza el metodo Limpiar() ).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


        /// <summary>
        /// Inicia el proceso de salida de la aplicacion. (Utiliza el metodo .Exit() )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        /// <summary>
        /// Durante la carga del programa se cargan como VACIO todos los textos de los TextBoxs que contienen al Numero1 y Numero2, el Label que muestra el resultado, La lista de operaciones realizadas y en el ComboBox es seleccionada la opcion default " ".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_Load(object sender, EventArgs e)
        {
            txtNumero1.Text = string.Empty;
            txtNumero2.Text = string.Empty;
            lstOperaciones.Text = string.Empty;
            lblResultado.Text = "0";
            cmbOperador.SelectedIndex = 0;
        }


        /// <summary>
        /// Antes del cierre del form muestra un mensaje de confirmacion. Para que el usuario verifique si realmente desea abandonar el programa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalculadora_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirmacion;
            confirmacion = MessageBox.Show("¿Seguro de querer salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {

            }
            else if (confirmacion == DialogResult.No)
            {
                //Cancela el evento de "CLOSING". (EVENT CANCEL).
                e.Cancel = true;
            }
        }


        /// <summary>
        /// Inicia la operacion entre los dos numeros ingresados. (Utiliza la funcion Operar).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperar_Click(object sender, EventArgs e)
        {
            string numero1String;
            string numero2String;
            string operadorString;
            char operadorChar;
            double resultado;
            bool validacionChar;


            numero1String = txtNumero1.Text;
            numero2String = txtNumero2.Text;
            operadorString = cmbOperador.Text;

  
            Operando num1 = new Operando();
            Operando num2 = new Operando();

            //num1.Numero = numero1String;
            //num2.Numero = numero2String;


            validacionChar = char.TryParse(operadorString, out operadorChar);
            num1.Numero = numero1String;
            num2.Numero = numero2String;

            if (cmbOperador.SelectedIndex == 0 || cmbOperador.Text == " ")
            {
                operadorChar = '+';
                cmbOperador.SelectedIndex = 1;
            }


            int contadorSignoNegativo = 0;
            int contadorSignoComa = 0;
            bool validacion1 = true;

            //De antemano comprobar si es numerico. Si parece ser que lo es... hago el tryparse
            for (int i = 0; i < numero1String.Length; i++)
            {
                //Si el caracter del string es un valor menor o mayor de la tabla ASCII en chars. Me fijo que onda
                if (numero1String[i] < '0' || numero1String[i] > '9')
                {
                    //Si no es ni una coma ni un -
                    if (numero1String[i] != ',' && numero1String[i] != '-')
                    {
                        //Es invalido
                        validacion1 = false;
                        break;
                    }
                    else if (numero1String[i] == '-')
                    {
                        contadorSignoNegativo++;
                    }
                    else if (numero1String[i] == ',')
                    {
                        contadorSignoComa++;
                    }
                }
            }


            int contadorSignoNegativo2 = 0;
            int contadorSignoComa2 = 0;
            bool validacion2 = true;

            //De antemano comprobar si es numerico. Si parece ser que lo es... hago el tryparse
            for (int j = 0; j < numero2String.Length; j++)
            {
                //Si el caracter del string es un valor menor o mayor de la tabla ASCII en chars. Me fijo que onda
                if (numero2String[j] < '0' || numero2String[j] > '9')
                {
                    //Si no es ni una coma ni un -
                    if (numero2String[j] != ',' && numero2String[j] != '-')
                    {
                        //Es invalido
                        validacion2 = false;
                        break;
                    }
                    else if (numero2String[j] == '-')
                    {
                        contadorSignoNegativo2++;
                    }
                    else if (numero2String[j] == ',')
                    {
                        contadorSignoComa2++;
                    }
                }
            }

            if (contadorSignoComa > 1 || contadorSignoNegativo > 1 )
            {
                validacion1 = false;
            }

            if (contadorSignoNegativo2 > 1 || contadorSignoComa2 > 1)
            {
                validacion2 = false;
            }

                if (string.IsNullOrWhiteSpace(numero1String) == true || validacion1 == false)
            {
                numero1String = "0";
            }

            if (string.IsNullOrWhiteSpace(numero2String) == true || validacion2 == false)
            {
                numero2String = "0";
            }

            if (validacionChar == true && validacion1 == true && validacion2 == true)
            {
                if (numero2String == "0" && operadorChar == '/')
                {
                    resultado = 0;
                    lblResultado.Text = "Error. No se puede dividir por 0.";
                    lstOperaciones.Items.Add("Hubo error en el calculo.");
                }
                else
                {
                    resultado = Operar(numero1String, numero2String, operadorChar.ToString());
                    lblResultado.Text = resultado.ToString();
                    lstOperaciones.Items.Add($"{numero1String} {operadorChar} {numero2String} = {resultado}");

                }

                
                  
            }
            else
            {

                if (validacion1 == false)
                {
                    numero1String = "0";
                }

                if (validacion2 == false)
                {
                    numero2String = "0";
                }

                resultado = Operar(numero1String, numero2String, operadorChar.ToString());

                if (numero2String == "0" && operadorChar == '/')
                {
                    resultado = 0;
                    lblResultado.Text = "Error. No se puede dividir por 0.";
                    lstOperaciones.Items.Add("Hubo error en el calculo.");
                }
                else
                {
                    lstOperaciones.Items.Add($"{numero1String} {operadorChar} {numero2String} = {resultado}");
                }
                   
            }

        }


        /// <summary>
        /// Inicia el proceso de conversion del resultado de la operacion a Binario. (Utiliza la funcion DecimalBinario).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            string numero1String;
            string numero2String;

            string operadorString;
            char operadorChar;

            double resultado;
            string resultadoString;
            bool validacionChar;
           // string validacionNum1;
            string validacionNum2;

            numero1String = txtNumero1.Text;
            numero2String = txtNumero2.Text;
            operadorString = cmbOperador.Text;

            if (cmbOperador.SelectedIndex == 0 || cmbOperador.Text == " ")
            {
                cmbOperador.SelectedIndex = 1;
            }

            Operando resultadoConversion = new Operando();

            validacionChar = char.TryParse(operadorString, out operadorChar);
           // validacionNum1 = resultadoConversion.BinarioDecimal(numero1String);

            //Lo paso de binario a decimal para validar si es un decimal correcto
            validacionNum2 = resultadoConversion.BinarioDecimal(numero2String);
            validacionNum2 = resultadoConversion.DecimalBinario(numero2String);

            if (validacionChar == true)
            {
                if (operadorString == "/" && (numero2String == "0" || validacionNum2 == "Valor inválido") == true)
                {
                    lblResultado.Text = "Error. No se puede dividir por 0.";
                    lstOperaciones.Items.Add("Hubo error en el calculo.");
                }
                else
                {
                    resultado = Operar(numero1String, numero2String, operadorString);
                    resultadoString = resultadoConversion.DecimalBinario(resultado);
                    lblResultado.Text = resultadoString;


                    if (resultado == 0)
                    {
                        resultadoString = "0";
                        lstOperaciones.Items.Add($"Decimal: {resultado} = Binario: {resultadoString}");
                        lblResultado.Text = resultadoString;
                    }
                    else
                    {
                        lstOperaciones.Items.Add($"Decimal: {resultado} = Binario: {resultadoString}");
                    }
                }

            }

        }


        /// <summary>
        /// Inicia el proceso de conversion del resultado de la operacion a Decimal. (Utiliza la funcion BinarioADecimal).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            string numero1BinString;
            string numero1DecString;
            string numero2BinString;
            string numero2DecString;

            string operadorString;
            char operadorChar;
            bool validacionChar;

            double resultado;
            string resultadoBinario;

            numero1BinString = txtNumero1.Text;
            numero2BinString = txtNumero2.Text;
            operadorString = cmbOperador.Text;

            validacionChar = char.TryParse(operadorString, out operadorChar);

            if (cmbOperador.SelectedIndex == 0 || cmbOperador.Text == " ")
            {
                cmbOperador.SelectedIndex = 1;
                operadorChar = '+';
            }

            if (validacionChar == true)
            {
                Operando resultadoConversion = new Operando();

                numero1DecString = resultadoConversion.BinarioDecimal(numero1BinString);
                numero2DecString = resultadoConversion.BinarioDecimal(numero2BinString);

               // if (numero1DecString != "Valor inválido" && numero2DecString != "Valor inválido")
                //{

                    if (numero1DecString == "Valor inválido")
                    {
                        numero1DecString = "0";                    
                    }

                    if (numero2DecString == "Valor inválido")
                    {
                        numero2DecString = "0";
                    }

                    if (numero2DecString == "0" && operadorString == "/")
                    {
                        lblResultado.Text = "Error. No se puede dividir por 0.";
                        lstOperaciones.Items.Add("Hubo error en el calculo.");
                    }
                    else
                    {
                        resultado = Operar(numero1DecString, numero2DecString, operadorString);
                        resultadoBinario = resultadoConversion.DecimalBinario(resultado);

                        lblResultado.Text = resultado.ToString();

                        if (resultado == 0)
                        {
                            lstOperaciones.Items.Add($"Binario: {0} = Decimal: {0}");
                        }
                        else
                        {
                            lstOperaciones.Items.Add($"Binario: {resultadoBinario} = Decimal: {resultado}");
                        }

                    //}
                }


            }

        }


        /// <summary>
        /// Opera de forma distinta dependiendo del valor de operador. Si es "-" restara al atributo ".numero" perteneciente al N1 con el del N2. Asi respectivamente de cada valor permitido (+,-,/,*).
        /// </summary>
        /// <param name="numero1">Numero 1 el cual sera operado por el numero 2</param>
        /// <param name="numero2">Numero 2 el cual operara al Numero 1</param>
        /// <param name="operadorString">Retorna el resultado de la operacion realizada. (En formato Double)</param>
        /// <returns></returns>
        private double Operar(string numero1, string numero2, string operadorString)
        {
            char operadorChar;
            char.TryParse(operadorString, out operadorChar);

            Operando num1 = new Operando(numero1);
            Operando num2 = new Operando(numero2);

            double resultado;

            switch (operadorChar)
            {

                case '+':
                    {
                        resultado = num1 + num2;
                        break;

                    }
                case '-':
                    {
                        resultado = num1 - num2;
                        break;
                    }
                case '/':
                    {
                        resultado = num1 / num2;
                        break;
                    }
                case '*':
                    {
                        resultado = num1 * num2;
                        break;
                    }
                default:
                    {
                        resultado = num1 + num2;
                        break;
                    }
            }

            return resultado;

        }


        /// <summary>
        /// Limpia las cajas de texto que contienen el numero 1, numero 2. La seleccion del ComboBox que contiene un operador y tambien el Label superior que muestra el resultado. 
        /// </summary>
        private void Limpiar()
        {
            txtNumero1.Text = string.Empty;
            txtNumero2.Text = string.Empty;
            lblResultado.Text = "0";
            cmbOperador.SelectedIndex = 0;
        }
    }
}

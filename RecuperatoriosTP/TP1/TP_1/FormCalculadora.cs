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

            //Arreglado -> if (confirmacion == DialogResult.Yes) vacio? Para qué?
            if (confirmacion == DialogResult.No)
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
        /// Toma el valor del ultimo resultado que quedo y lo convierte a binario, para luego reemplazar 
        /// el valor tomado del label resultado por el resultado de la conversion. Tambien agrega a la 
        /// lista de resultados el valor tanto en binario como en decimal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            string numeroDecimalLeidoLblResultado;

            //Guardo el resultado del label resultado
            numeroDecimalLeidoLblResultado = lblResultado.Text;

            long numeroDecimalLeidoLblResultadoLong;
            long.TryParse(numeroDecimalLeidoLblResultado, out numeroDecimalLeidoLblResultadoLong);

            //SI EL VALOR DEL LABEL ES MAYOR A ESE NUMERO, YA NO ES POSIBLE SEGUIR CONVIRTIENDO. PORQUE DA 0. (Soluciona el error de que el programa devuelva 0 si el numero que se ingresa es tanta magnitud)
            if (numeroDecimalLeidoLblResultadoLong > 699999999) // -> SIGUIENTE VALOR -> 6000000000 (YA ES INVALIDO)
            {                                        
                MessageBox.Show("El numero decimal que intenta convertir a binario es demasiado grande.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string numeroDecimalFinal;
                string numeroBinarioFinal;

                //Necesito un operando aux para poder llamar a mis metodos de Conversion... si tuviera un get en mi propertie, usaria este Operando directamente... 
                Operando operandoAux = new Operando();

                numeroBinarioFinal = operandoAux.DecimalBinario(numeroDecimalLeidoLblResultado);
                numeroDecimalFinal = operandoAux.BinarioDecimal(numeroBinarioFinal);

                if (numeroDecimalFinal == "0")
                {
                    //Esto lo hago porque si no... la variable numeroBinarioFinal no me viene con nada
                    //y no es visualmente atractivo ver: Decimal: 0 = Binario: " (Es un pequeño parche a ese error visual)
                    numeroBinarioFinal = "0";
                }

                //Muestro en el label EL RESULTADO DE LA CONVERSION
                lblResultado.Text = numeroBinarioFinal;
                lstOperaciones.Items.Add($"Binario: {numeroBinarioFinal} = Decimal: {numeroDecimalFinal}");
            }
            
        }

        /// <summary>
        /// Toma el valor del ultimo resultado que quedo y lo convierte a decimal, para luego reemplazar 
        /// el valor tomado del label resultado por el resultado de la conversion. Tambien agrega a la 
        /// lista de resultados el valor tanto en decimal como en binario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            string numeroBinarioLeidoLblResultado;

            //Guardo el resultado del label resultado
            numeroBinarioLeidoLblResultado = lblResultado.Text;

            
            //SI EL NUMERO ESTA EXPRESADO CON UNA "E" SIGNIFICA QUE ES UN NUMERO ENORME (YA SEA O NO BINARIO...)                           
            if (numeroBinarioLeidoLblResultado.Contains("E") == true) //10000000000000000 -> SI TENGO UNA CIFRA MAS. ME EXPRESA EL NUMERO EN 1.E^+17...
            {
                MessageBox.Show("El numero binario que intenta convertir a decimal es demasiado grande. Verifique que su resultado no contenga una 'E'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string numeroDecimalFinal;
                string numeroBinarioFinal;

                //Necesito un operando aux para poder llamar a mis metodos de Conversion... si tuviera un get en mi propertie, usaria este Operando directamente... 
                Operando operandoAux = new Operando();
                
                numeroDecimalFinal = operandoAux.BinarioDecimal(numeroBinarioLeidoLblResultado);
                numeroBinarioFinal = operandoAux.DecimalBinario(numeroDecimalFinal);

                if (numeroDecimalFinal == "0")
                {
                    //Esto lo hago porque si no... la variable numeroBinarioFinal no me viene con nada
                    //y no es visualmente atractivo ver: Decimal: 0 = Binario: " (Es un pequeño parche a ese error visual)
                    numeroBinarioFinal = "0"; 
                }

                //Muestro en el label EL RESULTADO DE LA CONVERSION
                lblResultado.Text = numeroDecimalFinal;
                lstOperaciones.Items.Add($"Decimal: {numeroDecimalFinal} = Binario: {numeroBinarioFinal}");
            }

            

        }


        /// <summary>
        /// Opera de forma distinta dependiendo del valor de operador. Si es "-" restara al atributo ".numero" perteneciente al N1 con el del N2. Asi respectivamente de cada valor permitido (+,-,/,*).
        /// </summary>
        /// <param name="numero1">Numero 1 el cual sera operado por el numero 2</param>
        /// <param name="numero2">Numero 2 el cual operara al Numero 1</param>
        /// <param name="operadorString">Retorna el resultado de la operacion realizada. (En formato Double)</param>
        /// <returns></returns>
        private double Operar(string numero1Recibido, string numero2Recibido, string operadorStringRecibido)
        {
            double resultado;
            
            //Los numeros recibidos como string los creo como nuevos Operandos
            Operando numero1 = new Operando(numero1Recibido);
            Operando numero2 = new Operando(numero2Recibido);

            // Arreglado -> double Operar nunca valida el operador

            //Transformo el operador String a Char para poder pasarselo a mi funcion Operar (La cual va a validar mi operador dentro de su implementacion)
            char operador;
            char.TryParse(operadorStringRecibido, out operador);
          
            //Llamo a mi funcion Operar que tengo en la calculadora. (Anteriormente no lo hacia y repetia codigo creando de nuevo la funcion Operar)
            resultado = Calculadora.Operar(numero1, numero2,operador);

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

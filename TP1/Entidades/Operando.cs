using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Operando
    {
        //El atributo numero es privado.
        private double numero;


        /// <summary>
        /// Constructor 1 de una variable operando. Por defecto este constructor asigna un valor "0" al atributo ".numero".
        /// </summary>
        public Operando()
        {
            //El constructor por defecto (sin parámetros) asignará valor 0 al atributo numero.
            this.numero = 0;
        }


        /// <summary>
        ///  Constructor 2 de una variable operando. Al recibir un Double por parametro, llamara a la propiedad .Numero que validara y asignara el valor que estaba en fomato String al atributo ".numero" (en formato Double). 
        /// </summary>
        /// <param name="numero"></param>
        public Operando(double numero)
        {
            this.numero = numero;
        }


        /// <summary>
        /// Constructor 3 de una variable operando. Al recibir un String por parametro, llamara a la propiedad .Numero que validara y asignara el valor que estaba en fomato String al atributo ".numero" (en formato Double). 
        /// </summary>
        /// <param name="numero">Representa un numero (como String)</param>
        public Operando(string numero)
        {
            //double numeroDouble;

            /*
            if (double.TryParse(numero, out numeroDouble) == true)
            {
                this.numero = numeroDouble;
            }
            */

            Numero = numero;

        }


        /// <summary>
        /// Propiedad que se ocupa SOLO de Settear (Asignar) un valor al atributo ".numero" habiendo anteriormente pasado por la funcion de "Validar Operando".
        /// </summary>
        public string Numero
        {
            //La propiedad Numero asignará un valor al atributo número, previa validación.En
            //este lugar será el único en todo el código que llame al método ValidarOperando.

            set
            {
                numero = ValidarOperando(value);
            }
        
        }


        /// <summary>
        /// Se ocupa de validar un operando, es decir un numero (en formato String) del cual solo debe contener como maximo 1 signo "-" y o ",", que ademas no debera ser un espacio en blanco.
        /// </summary>
        /// <param name="strNumero">Numero como string que sera validado</param>
        /// <returns>Retorna el numero ya habiendo sido validado. (en formato Double)</returns>
        private double ValidarOperando(string strNumero)
        {
            //ValidarOperando comprobará que el valor recibido sea numérico, y lo retornará en
            //formato double. Caso contrario, retornará 0.

            double resultado = 0;
            double resultadoAux;
            bool validacion = true;
            int contadorSignoNegativo = 0;
            int contadorSignoComa = 0;

            //De antemano comprobar si es numerico. Si parece ser que lo es... hago el tryparse
            for (int i = 0; i < strNumero.Length ; i++)
            {
                //Si el caracter del string es un valor menor o mayor de la tabla ASCII en chars. Me fijo que onda
                if (strNumero[i] < '0' || strNumero[i] > '9')
                {
                    //Si no es ni una coma ni un -
                    if (strNumero[i] != ',' && strNumero[i] != '-')
                    {
                        //Es invalido
                        validacion = false;
                        break;
                    }
                    else if (strNumero[i] == '-')
                    {
                        contadorSignoNegativo++;
                    }
                    else if (strNumero[i] == ',')
                    {
                        contadorSignoComa++;
                    }
                }
            }

            //Si tengo mas de 2 comas o dos signos negativos. Hay error.
            if (contadorSignoNegativo > 1 || contadorSignoComa > 1)
            {
                validacion = false;
            }
            else if (validacion == true && double.TryParse(strNumero, out resultadoAux) == true)
            {
                resultado = resultadoAux;
            }

            return resultado;
        }


        /// <summary>
        /// Sobrecarga del operador "+". Al ser llamados dos variables con el formato "Operando" y utilizar + se sumaran los atributos ".numero" de cada clase. 
        /// </summary>
        /// <param name="n1">Numero 1 el cual sera sumado por el numero 2</param>
        /// <param name="n2">Numero 2 el cual sumara al numero 1</param>
        /// <returns></returns>
        public static double operator +(Operando n1, Operando n2)
        {
            double resultado;
            resultado = n1.numero + n2.numero;

            return resultado;
        }


        /// <summary>
        /// Sobrecarga del operador "-". Al ser llamados dos variables con el formato "Operando" y utilizar - se restaran los atributos ".numero" de cada clase. 
        /// </summary>
        /// <param name="n1">Numero 1 el cual sera restado por el numero 2</param>
        /// <param name="n2">Numero 2 el cual restara al numero 1</param>
        /// <returns></returns>
        public static double operator -(Operando n1, Operando n2)
        {
            double resultado;
            resultado = n1.numero - n2.numero;

            return resultado;
        }


        /// <summary>
        /// Sobrecarga del operador "/". Al ser llamados dos variables con el formato "Operando" y utilizar / se dividiran los atributos ".numero" de cada clase. (Si el .numero del segundo numero es igual a 0, devolvera 0 como resultado final).
        /// </summary>
        /// <param name="n1">Numero 1 que sera dividido por el numero 2</param>
        /// <param name="n2">Numero 2 que sera el divisor del numero 1</param>
        /// <returns>Retorna el resultado de la operacion "/" de los dos numeros.</returns>
        public static double operator /(Operando n1, Operando n2)
        {
            // Si se tratara de una división por 0, retornará double.MinValue.

            double resultado;

            if (n2.numero != 0)
            {
                resultado = n1.numero / n2.numero;
            }
            else
            {
                resultado = double.MinValue;
            }
           

            return resultado;
        }


        /// <summary>
        /// Sobrecarga del operador "*". Al ser llamados dos variables con el formato "Operando" y utilizar * se multiplicaran los atributos ".numero" de cada clase.
        /// </summary>
        /// <param name="n1">Numero 1 que sera multiplicado con el numero 2</param>
        /// <param name="n2">Numero 2 que multiplicara al numero 1</param>
        /// <returns>Retorna el resultado de la operacion "*" de los dos numeros.</returns>
        public static double operator *(Operando n1, Operando n2)
        {
            double resultado;
            resultado = n1.numero * n2.numero;

            return resultado;
        }


        /// <summary>
        /// Verifica si el numero ingresado por paramatro es un valor binario.
        /// </summary>
        /// <param name="binario">El "supuesto" numero en binario que sera verificado para comprobar si realmente es uno como tal.</param>
        /// <returns>Retorna true (Verdadero) o false (False) confirmando si se pudo validar o no.</returns>
        private bool EsBinario(string binario)
        {
            //El método privado EsBinario validará que la cadena de caracteres esté compuesta
            //SOLAMENTE por caracteres '0' o '1'.

            bool retorno = true;

            for (int i = 0; i < binario.Length ; i++)
            {
                if (binario[i] != '0' && binario[i] != '1')
                {
                    retorno = false;
                    break;
                }
            }

            return retorno;
        }


        /// <summary>
        /// Transforma el numero recibido por parametro (En sistema binario) a Decimal.
        /// </summary>
        /// <param name="binario">El numero binario (en formato String) que sera convertido a decimal</param>
        /// <returns>Retorna el numero (Como String) ya convertido al sistema decimal.</returns>
        public string BinarioDecimal(string binario)
        {

            //El método BinarioDecimal validará que se trate de un binario y luego
            //convertirá ese número binario a decimal, en caso de ser posible. Caso
            //contrario retornará "Valor inválido".

            string resultado = null;

            bool resultadoValidacionBinario = EsBinario(binario);

            if (resultadoValidacionBinario == true) //En la validacion binaria me encargo de validar que no solo sea un numero binario si que ademas no sea - o con .,
            {

                double potenciador = 0;
                double acumulador = 0;
                double numeritoBin = 0;

                //Recorro hasta el principio de la cadena. (Osea hasta el primer digito)
                for (int i = (binario.Length - 1); i >= 0; i--)
                {
                    //Me fijo si es un char 0 o 1 para transformarlo a int
                    if (binario[i] == '0')
                    {
                        numeritoBin = 0;
                    }
                    else if (binario[i] == '1')
                    {
                        numeritoBin = 1;
                    }

                    //Acumulo el resultado de la multiplicacion de ese (1 o 0)*2 elevado al potenciador
                    if (potenciador == 0 && numeritoBin == 0)
                    {
                        //Si yo tengo   0 * 2 ^ 0  = 0  y antes en el mathpow me arruinaba el resultado dandome 1.
                    }
                    else
                    {
                        acumulador += Math.Pow((numeritoBin * 2), potenciador);
                    }
                    
                    potenciador++;
                }
                //Los métodos BinarioDecimal y DecimalBinario convertirán el Resultado, trabajarán
                //con enteros positivos, quedándose para esto con el valor absoluto y entero del
                //double recibido:
                acumulador = (int)acumulador;
                resultado = acumulador.ToString();
            }
            else
            {
               resultado = "Valor inválido";
            }

            return resultado;
        }


        /// <summary>
        /// Transforma el numero recibido por parametro (En sistema decimal) a Binario.
        /// </summary>
        /// <param name="numero">El numero decimal (en formato String) que sera convertido a binario.</param>
        /// <returns>Retorna el numero (Como String) ya en convertido al sistema binario.</returns>
        public string DecimalBinario(string numero)
        {

            //El método DecimalBinario convertirá un número
            //decimal a binario, en caso de ser posible. Caso contrario retornará "Valor
            //inválido". Reutilizar código.

            string resultado;
            bool validacionNumero = true;
            int contadorSignoNegativo = 0;
            int contadorSignoComa = 0;
            bool flagEsNegativo = false;

            for (int i = 0; i < numero.Length; i++)
            {
                //Si el caracter del string es un valor menor o mayor de la tabla ASCII en chars. Me fijo que onda
                if (numero[i] < '0' || numero[i] > '9')
                {
                    //Si no es ni una coma ni un -
                    if (numero[i] != ',' && numero[i] != '-')
                    {
                        //Es invalido
                        validacionNumero = false;
                        break;
                    }
                    else if (numero[i] == '-')
                    {
                        contadorSignoNegativo++;
                    }
                    else if (numero[i] == ',')
                    {
                        contadorSignoComa++;
                    }
                }
            }

            //Si hay mas de 1 signo "-" o mas de 1 signo ","
            if (contadorSignoComa > 1 || contadorSignoNegativo > 1)
            {
                //Error
                validacionNumero = false;
            }
            else if (contadorSignoNegativo == 1) //Si hay UNO signo negativo, es un numero valido pero debo pasarlo a positivo.
            {
                //Si hay una coma solamente, no pasa nada. En el mismo parseo a INT se va.
                flagEsNegativo = true;

            }

            //SI ES UN NUMERO (YA SEA CON UNA COMA Y NEGATIVO) ESTA TODO BIEN. 
            if (validacionNumero == true)
            {
                int auxNumero;
                int.TryParse(numero, out auxNumero); //Aca se va la coma

                if (flagEsNegativo == true)
                {
                    auxNumero = auxNumero * (-1); //PASO EL NUMERO A POSITIVO.

                }

                //A ESTE PUNTO YA TENGO UN VALOR ABSOLUTO Y ENTERo
                //ACA LLAMO A MI FUNCION QUE RECIBE EL VALOR YA NUMERICO
                resultado = DecimalBinario(auxNumero);
            }
            else
            {
                resultado = "Valor inválido";
            }

            return resultado;
        }


        /// <summary>
        /// Transforma el numero recibido por parametro (En sistema decimal) a Binario.
        /// </summary>
        /// <param name="numero">El numero decimal (en formato Double) que sera convertido a binario.</param>
        /// <returns>Retorna el numero (Como String) ya en convertido al sistema binario.</returns>
        public string DecimalBinario(double numero)
        {
            string resultado = "";

            //Los métodos BinarioDecimal y DecimalBinario convertirán el Resultado, trabajarán
            //con enteros positivos, quedándose para esto con el valor absoluto y entero del
            //double recibido:

            //De todas formas vuelvo a validar que sea >= 0 (Porque desconfio SIEMPRE de lo que llega por param)
            if (numero >= 0)
            {
                //Le saco comas
                numero = (int)numero;

                while ((int)numero > 0)
                {
                    //Guardo en la variable string (AL PRINCIPIO) el ultimo valor. Reemplaza el uso de un STACK.
                    resultado = numero % 2 + resultado;
                    numero = (int)numero / 2;
                }
            }              

            return resultado;
        }

    }
}

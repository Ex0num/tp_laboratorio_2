using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class Calculadora
    {
        /// <summary>
        /// Valida que el operador recibido (en formato Char) sea valido. (Solo puede ser: "+","-","/","*"). En caso de no ser ninguno de estos. Sera por defecto "+".
        /// </summary>
        /// <param name="operador">Representa el caracter del operador proximo a ser validado.</param>
        /// <returns>Retorna el operador ya validado y o transformado en caso de haber no haber sido ninguno de los operadores constatados (+,-,/,*).</returns>
        private static char ValidarOperador(char operador)
        {
            //Validar que el operador recibido sea +, -, / o *.Caso contrario retornará +.
            char operadorValidado;

            switch (operador)
            {

                case '+':
                    {
                        operadorValidado = '+';
                        break;

                    }
                case '-':
                    {
                        operadorValidado = '-';
                        break;
                    }
                case '/':
                    {
                        operadorValidado = '/';
                        break;
                    }
                case '*':
                    {
                        operadorValidado = '*';
                        break;
                    }
                default:
                    {
                        operadorValidado = '+';
                        break;
                    }     
            }

            return operadorValidado;
        }


        /// <summary>
        /// Opera de forma distinta dependiendo del valor de operador. Si es "-" restara al atributo ".numero" perteneciente al N1 con el del N2. Asi respectivamente de cada valor permitido (+,-,/,*).
        /// </summary>
        /// <param name="num1">Numero 1 que sera operado por el Numero 2</param>
        /// <param name="num2">Numero 2 que operara con el numero 1</param>
        /// <param name="operador">Operador que definira a que sobrecarga se debe llamar. Ejemplo: Si es '+', se llamara a la sobrecarga del +.</param>
        /// <returns>Retorna el resultado de la operacion realizada. (En formato Double)</returns>
        public static double Operar(Operando num1, Operando num2, char operadorRecibido)
        {
            char operadorValidado;
            double resultado;

            //Ahora si -> "double Operar no valida el operador"
            operadorValidado = ValidarOperador(operadorRecibido);

            switch (operadorValidado)
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

    }
}

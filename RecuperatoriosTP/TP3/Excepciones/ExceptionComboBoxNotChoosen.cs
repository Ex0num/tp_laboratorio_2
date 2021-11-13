using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class ExceptionComboBoxNotChoosen : Exception
    {

        /// <summary>
        /// Constructor que recibe un mensaje y llama al constructor que recibe un mensaje y un inner, 
        /// que este último llama al constructor base de Exception al cual le pasa el mensaje recibido y un 
        /// valor nulo de inner.
        /// </summary>
        /// <param name="mensaje"></param>
        public ExceptionComboBoxNotChoosen(string mensaje) : this(mensaje, null)
        {

        }

        /// <summary>
        /// Constructor que llama al constructor base de Exceptions  al cual pasa un valor nulo de 
        /// inner y el mensaje recibido.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="inner"></param>
        public ExceptionComboBoxNotChoosen(string mensaje, Exception inner) : base(mensaje, inner)
        {

        }

    }
}

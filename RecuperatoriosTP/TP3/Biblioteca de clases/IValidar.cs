using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public interface IValidar
    {
        /// <summary>
        /// Interfaz que recibe un personaje a validar y será implementada y definida
        /// tanto por la clase personaje como por la clase arma. 
        /// </summary>
        /// <param name="cosaAValidar"></param>
        /// <returns>Retorna si lo que se valida
        /// es o no realmente válido. Sease el caso de un arma o un personaje.</returns>
        bool Validar(Personaje pjAValidar);
    }
}

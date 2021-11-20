using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Arma : IValidar
    {
        //-------------------------ATRIBUTOS-------------------------
        int ptsAtaque;
        int ptsDefensa;
        enumTipoArma tipoArma;

        //------------------------PROPIEDADES-------------------------
        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo PtsAtaque.
        /// </summary>
        public int PtsAtaque
        {
            get 
            {
                return this.ptsAtaque;
            }
            set 
            {
                this.ptsAtaque = value;
            }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo PtsDefensa.
        /// </summary>
        public int PtsDefensa
        {
            get
            {
                return this.ptsDefensa;
            }
            set
            {
                this.ptsDefensa = value;
            }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo tipoArma.
        /// </summary>
        public enumTipoArma TipoArma
        {
            get
            {
                return tipoArma;
            }
            set
            {
                this.tipoArma = value; 
            }
        }

        //-------------------------CONSTRUCTORES----------------------
        /// <summary>
        /// Constructor vacío, utilizado para la lectura y escritura de archivos que incluyan un arma.
        /// </summary>
        public Arma() //CONSTRUCTOR VACIO NECESARIO PARA LA LECTURA Y ESCRITURA DE ARCHIVOS
        {
                
        }

        /// <summary>
        /// Constructor de un arma, al recibir un tipo de arma (Del enumerado tipoArma)
        /// Setea un valor especifico en los campos de puntos de ataque y defensa dependiendo 
        /// de cuál sea el tipo recibido.
        /// </summary>
        /// <param name="tipoArmaRecibido">Definirá que puntos serán asignados al ataque y defensa</param>
        public Arma(enumTipoArma tipoArmaRecibido)
        {
            TipoArma = tipoArmaRecibido;

            switch (tipoArmaRecibido)
            {
                case enumTipoArma.Escudo:
                {
                    PtsAtaque = 350;
                    PtsDefensa = 1000;
                    break;
                }
                case enumTipoArma.Arco:
                {
                    PtsAtaque = 850;
                    PtsDefensa = 500;
                    break;
                }
                case enumTipoArma.BastonMagico:
                {
                    PtsAtaque = 1000;
                    PtsDefensa = 350;
                    break;
                }
            }
        }

        //-------------------------VALIDACIONES-----------------------
        /// <summary>
        /// Método validatorio que recibe un tipo de arma y valida si es o no algo coherente.
        /// </summary>
        /// <param name="armaRecibida"></param>
        /// <returns>Retorna true si es un tipo de arma válida, si no false</returns>
        public static bool isValidTipoArma(enumTipoArma armaRecibida) //ALTA Y MODF
        {
            bool retorno;

            if (armaRecibida != enumTipoArma.Arco && armaRecibida != enumTipoArma.BastonMagico && armaRecibida != enumTipoArma.Escudo)
            {
                retorno = false;
                //EXCEPCION - EL ARMA RECIBIDA NO ESTA DENTRO DE LAS EXISTENTES.
            }
            else
            {
                //EL ARMA PARECE SER CORRECTA :)
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Método validatorio que recibe un valor representativo a puntos de defensa y valida si es o no algo coherente.
        /// </summary>
        /// <param name="ptsDefensaRecibidos"></param>
        /// <returns>Retorna true si es un valor coherente de puntos de defensa, si no false</returns>
        public static bool isValidPtsDefensaArma(int ptsDefensaRecibidos) //ALTA Y MODF
        {
            bool retorno;

            if (ptsDefensaRecibidos >= 350 && ptsDefensaRecibidos <= 1000)
            {
                //EL ARMA PARECE SER CORRECTA :)
                retorno = true;   
            }
            else
            {
                retorno = false;
                //EXCEPCION
            }

            return retorno;
        }

        /// <summary>
        /// Método validatorio que recibe un valor representativo a puntos de ataque y valida si es o no algo coherente.
        /// </summary>
        /// <param name="ptsAtaqueRecibidos"></param>
        /// <returns>Retorna true si es un valor coherente de puntos de ataque, si no false</returns>
        public static bool isValidPtsAtaqueArma(int ptsAtaqueRecibidos) //ALTA Y MODF
        {
            bool retorno;

            if (ptsAtaqueRecibidos >= 350 && ptsAtaqueRecibidos <= 1000)
            {
                //EL ARMA PARECE SER CORRECTA :)
                retorno = true;
            }
            else
            {
                //EXCEPCION -
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Interfaz implementada y definida en la clase Arma que recibe un personaje y al verificar que
        /// su arma sea del tipo Arma como tal llama a todos los Métodos validatorios de un arma y verifica
        /// que el personaje ingresado contenga un arma válida teniendo en cuenta los resultados de todos
        /// los métodos validatorios.
        /// </summary>
        /// <param name="armaDelPersonaje"></param>
        /// <returns>Retorna si el arma es o no válida</returns>
        bool IValidar.Validar (Personaje armaDelPersonaje)
        {
            if (armaDelPersonaje.Arma.GetType() == typeof(Arma))
            {
                bool ptsAtaque;
                bool ptsDefensa;
                bool tipoArma;

                ptsAtaque = isValidPtsAtaqueArma(armaDelPersonaje.Arma.PtsAtaque);
                ptsDefensa = isValidPtsDefensaArma(armaDelPersonaje.Arma.PtsDefensa);
                tipoArma = isValidTipoArma(armaDelPersonaje.Arma.TipoArma);

                if (ptsAtaque == true && ptsDefensa == true && tipoArma == true)
                {
                    return true;
                }
            }

            return false;
        }

        //-------------------------ENUMERADOS------------------------
        /// <summary>
        /// Enumerado que define el tipo de un arma.
        /// </summary>
        public enum enumTipoArma
        {
            Escudo = 0,
            Arco = 1,
            BastonMagico = 2
        }

    }
}

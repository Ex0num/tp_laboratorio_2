using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Personaje : IValidar
    {
        static int numeroIdentificador = 0;

        //-----------CARACTERISTICAS DE UN PERSONAJE---------------
        private string nombrePersonaje;
        private int nivelTotal;
        private enumOrigenElemental origenElemental;
        private Arma arma;
        private int idPersonaje;
        private int batallasGanadas;
        private int batallasJugadas;

        //-------------------PROPIEDADES----------------------------
        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo Nombre personaje.
        /// </summary>
        public string NombrePersonaje
        {
            get
            {
                return this.nombrePersonaje;
            }
            set
            {
                this.nombrePersonaje = value;
            }

        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo Nivel total.
        /// </summary>
        public int NivelTotal
        {
            get
            {
                return this.nivelTotal;
            }

            set
            {
                this.nivelTotal = value;
            }

        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo Origen elemental.
        /// </summary>
        public enumOrigenElemental OrigenElemental
        {
            get
            {
                return this.origenElemental;
            }
            set
            {
                this.origenElemental = value;
            }
        }

        /// <summary>
        /// Propiedad que en el momento en que se  llame a esta misma asigna el valor que posee
        /// la variable estatica numeroIdentificador, además de poder obtener el valor del campo
        /// Id personaje.
        /// </summary>
        public int IdPersonaje
        {
            get
            {
                return this.idPersonaje;
            }
            set
            {
                //AUMENTO EN 1 EL VALOR STATIC DEL ID Y LO SETEO
                numeroIdentificador++;
                this.idPersonaje = numeroIdentificador;
            }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor el campo arma, que es de este mismo tipo.
        /// </summary>
        public Arma Arma
        {
            get
            {
                return this.arma;
            }
            set
            {
                this.arma = value;
            }
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo batallasGanadas que es de tipo int.
        /// </summary>
        public int BatallasGanadas
        {
            get 
            {
                return this.batallasGanadas;
            }
            set
            {
                this.batallasGanadas = value;
            }
           
        }

        /// <summary>
        /// Propiedad que asigna y obtiene el valor del campo batallasJugadas que es de tipo int.
        /// </summary>
        public int BatallasJugadas
        {
            get
            {
                return this.batallasJugadas;
            }
            set
            {
                this.batallasJugadas = value;
            }

        }

        //-------------------------CONSTRUCTORES--------------------
        /// <summary>
        /// Constructor vacío, utilizado para la lectura y escritura de archivos que incluyan un personaje.
        /// </summary>
        public Personaje() //CONSTRUCTOR VACIO NECESARIO PARA LA LECTURA Y ESCRITURA DE ARCHIVOS
        {

        }

        /// <summary>
        /// Constructor de un personaje que recibe todos su campos y los asigna a los atributos del mismo.
        /// Este constructor llama a la propiedad ID personaje que incrementa el valor de la variable estatica
        /// Num identificador utilizada para IDS.
        /// </summary>
        /// <param name="nombrePersonajeRecibido">Nombre del personaje</param>
        /// <param name="nivelTotalRecibido">Nivel del personaje</param>
        /// <param name="origenElementalRecibido">Origen del personaje</param>
        /// <param name="tipoArmaRecibido">Tipo de arma del personaje</param>
        public Personaje(string nombrePersonajeRecibido, int nivelTotalRecibido, enumOrigenElemental origenElementalRecibido, Arma.enumTipoArma tipoArmaRecibido)
        {
            NombrePersonaje = nombrePersonajeRecibido;
            NivelTotal = nivelTotalRecibido;
            OrigenElemental = origenElementalRecibido;
            Arma = new Arma(tipoArmaRecibido);

            BatallasJugadas = 0;  //Nunca jugó ninguna batalla, por eso comienza en 0.
            BatallasGanadas = 0; //Nunca ganó ninguna batalla, por eso comienza en 0.

            IdPersonaje = 0; // MANDO UN 0. DE TODAS FORMAS NO SE VA A TENER EN CUENTA.
        }

        /// <summary>
        /// Constructor de un personaje que recibe todos su campos y los asigna a los atributos del mismo.
        /// Este constructor llama a la propiedad ID personaje que incrementa el valor de la variable estatica
        /// Num identificador utilizada para IDS. SOLO UTILIZADO PARA UNI-TESTING. 
        /// </summary>
        /// <param name="nombrePersonajeRecibido">Nombre del personaje</param>
        /// <param name="nivelTotalRecibido">Nivel del personaje</param>
        /// <param name="origenElementalRecibido">Origen del personaje</param>
        /// <param name="tipoArmaRecibido">Tipo de arma del personaje</param>
        public Personaje(string nombrePersonajeRecibido, int nivelTotalRecibido, enumOrigenElemental origenElementalRecibido, Arma.enumTipoArma tipoArmaRecibido, int batallasGandasRecibido)
        {
            NombrePersonaje = nombrePersonajeRecibido;
            NivelTotal = nivelTotalRecibido;
            OrigenElemental = origenElementalRecibido;
            Arma = new Arma(tipoArmaRecibido);
            BatallasGanadas = batallasGandasRecibido; //Le seteo las batallas ganadas que me plazcan, para testear.

            IdPersonaje = 0; // MANDO UN 0. DE TODAS FORMAS NO SE VA A TENER EN CUENTA.
        }

        //-------------------------METODOS--------------------------
        /// <summary>
        /// Método que calcula el poder total del personaje que recibe. 
        /// Accede a los valores tanto del ataque como la defensa y los 
        /// multiplica según el nivel del personaje. El multiplicador
        /// tiene un valor minimo de x1 y maximo de x2.
        /// </summary>
        /// <param name="personajeRecibido"></param>
        /// <returns>Retorna el poder total de personaje ya habiendo considerado su nivel.</returns>
        public static int calcularPoderTotal(Personaje personajeRecibido)
        {
            //RANGOS -> NIVEL 1 a 100
            // ej NIVEL -> 1 = * 1.00
            // ej NIVEL -> 100 = * 2
            // ej NIVEL -> 78 = *1.78

            int ptsAtaqueArma = personajeRecibido.Arma.PtsAtaque;
            int ptsDefensaArma = personajeRecibido.Arma.PtsDefensa;

            float ptsFinalesAtaqueArma = ptsAtaqueArma;
            float ptsFinalesDefensaArma = ptsDefensaArma;

            //SIEMPRE VOY A TENER VALORES DEL MULTIPLICADOR: ->   >= 1 && <= 2
            float multiplicador = ((float) personajeRecibido.NivelTotal / 100) + 1;

            if (multiplicador >= 1 && multiplicador <= 2)
            {
                //BOOSTEO EL ATAQUE Y DEFENSA EN CUESTION DEL NIVEL
                ptsFinalesAtaqueArma = (float) ptsAtaqueArma * multiplicador;
                ptsFinalesDefensaArma = (float) ptsDefensaArma * multiplicador;
            }

            //PODER TOTAL VA A SER LA SUMA DE LOS PUNTOS DE ATAQUE Y DEFENSA YA BOOSTEADOS
            float poderTotal = ptsFinalesAtaqueArma + ptsFinalesDefensaArma;

            return (int)poderTotal;
        }

        //-------------------------VALIDACIONES----------------------
        /// <summary>
        ///  Método validatorio que recibe un nombre de personaje y valida si es o no algo coherente.
        /// </summary>
        /// <param name="nombreRecibido">Nombre recibido a validar.</param>
        /// <returns>Retorna si es un nombre válido o no.</returns>
        public static bool isValidNombrePersonaje(string nombreRecibido)
        {
            bool retorno;

            if (string.IsNullOrEmpty(nombreRecibido) == false && string.IsNullOrWhiteSpace(nombreRecibido) == false)
            {
                if (nombreRecibido.Length < 18)
                {
                    //EL NOMBRE PARECE SER CORRECTO :)
                    retorno = true;
                }
                else
                {
                    //EXCEPCION - NOMBRE DEMASIADO LARGO 
                    retorno = false;
                }
            }
            else
            {
                //EXCEPCION - NO SE INGRESO NINGUN NOMBRE
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Método validatorio que recibe un valor representativo al nivel de un personaje y valida si es o no algo coherente.
        /// </summary>
        /// <param name="nivelTotalRecibido">Nivel recibido a validar.</param>
        /// <returns>Retorna si es un nivel válido o no.</returns>
        public static bool isValidNivelTotalPersonaje(int nivelTotalRecibido)
        {
            bool retorno;

            if (nivelTotalRecibido >= 0 && nivelTotalRecibido <= 100)
            {
                //EL NIVEL PARECE SER CORRECTO :)
                retorno = true;     
            }
            else
            {
                retorno = false;
                //EXCEPCION - EL NUMERO DE NIVEL ESTA POR FUERA DE LOS PARAMETROS ESTABLECIDOS. 
            }

            return retorno;
        }

        /// <summary>
        /// Método validatorio que recibe un tipo de origen y valida si es o no algo coherente.
        /// </summary>
        /// <param name="origenElementalRecibido">Tipo de Origen elemental recibido a validar.</param>
        /// <returns>Retorna si es un tipo de origen válido o no.</returns>
        public static bool isValidOrigenElemental(enumOrigenElemental origenElementalRecibido)
        {
            bool retorno;

            if (origenElementalRecibido != Personaje.enumOrigenElemental.Fuego && origenElementalRecibido != Personaje.enumOrigenElemental.Agua && origenElementalRecibido != Personaje.enumOrigenElemental.Hielo)
            {
                retorno = false;
                //EXCEPCION - EL ORIGEN RECIBIDO NO ESTA DENTRO DE LOS PARAMETROS ESTABLECIDOS.
            }
            else
            {
                //EL ORIGEN ELEMENTAL PARECE SER CORRECTO :)
                retorno = true; 
            }

            return retorno;
        }

        /// <summary>
        /// Método que recibe un nombre y busca en la lista estática de la clase universo si se
        /// encuentra alguien con este nombre recibido.
        /// </summary>
        /// <param name="nombrePersonajeAVerificar">Nombrel del personaje a verificar si está repetido o no.</param>
        /// <returns>Retorna si efectivamente el personaje con el nombre ingresado existe o no.</returns>
        public static bool estaRepetido(string nombrePersonajeAVerificar)
        {
            bool retorno = false;

            int limite = Universo.listaPersonajesExistentes.Count;

            for (int i = 0; i < limite; i++)
            {
                if (Universo.listaPersonajesExistentes[i].nombrePersonaje == nombrePersonajeAVerificar)
                {
                    //EXCEPCION - EL NOMBRE DEL PERSONAJE QUE SE INTENTA INGRESAR YA EXISTE EN LA LISTA DE PERSONAJES EXISTENTES.
                    retorno = true;
                    break;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Método que recibe un personaje a ser modificado y el nombre por el que se lo desea modificar.
        /// Se validará que no exista otra personaje (exceptuandose a él mismo), comparando las IDS.
        /// </summary>
        /// <param name="personajeAModificar"></param>
        /// <param name="nombreAModificar"></param>
        /// <returns>Retorna si es posible o no modificar el personaje con el nombre recibido ya que no existe otro con este último. </returns>
        public static bool esPosibleModificar(Personaje personajeAModificar, string nombreAModificar)
        {
            bool retorno = true;
            int limite = Universo.listaPersonajesExistentes.Count;

            for (int i = 0; i < limite; i++)
            {
                //SI CONCIDE EL NOMBRE CON ALGUNO DE LA LISTA (AUN EL MISMO PERSONAJE EN SI MISMO)
                if (Universo.listaPersonajesExistentes[i].NombrePersonaje == nombreAModificar)
                {

                    if (Universo.listaPersonajesExistentes[i].IdPersonaje != personajeAModificar.IdPersonaje)
                    {
                        retorno = false;
                        break;
                    }
                  
                }

            }

            return retorno;
        }

        /// <summary>
        /// Interfaz implementada y definida en la clase Personaje que recibe un personaje y
        /// y verifica que este personaje ingresado sea válido, llamando a todos sus métodos validatorios
        /// teniendo en cuenta los resultados de todos los métodos.
        /// </summary>
        /// <param name="personajeAValidar"></param>
        /// <returns>Retorna si el personaje recibido es o no válido.</returns>
        bool IValidar.Validar(Personaje personajeAValidar)
        {
            if (personajeAValidar.GetType() == typeof(Personaje))
            {
                bool nombreValido;
                bool nivelValido;
                bool origenValido;

                nombreValido = isValidNombrePersonaje(personajeAValidar.NombrePersonaje);
                nivelValido = isValidNivelTotalPersonaje(personajeAValidar.NivelTotal);
                origenValido = isValidOrigenElemental(personajeAValidar.OrigenElemental);

                if (nombreValido == true && nivelValido == true && origenValido == true)
                {
                    return true;
                }
            }

            return false;
        }

        //-------------------------ENUMERADOS--------------------------
        /// <summary>
        /// Enumerado que define el tipo de un origen elemental.
        /// </summary>
        public enum enumOrigenElemental
        { 
            Fuego = 0,
            Agua = 1,
            Hielo = 2
        }
    }
}

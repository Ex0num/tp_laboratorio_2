using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class Universo
    {
        //LISTA DE PERSONAJES
        public static List<Personaje> listaPersonajesExistentes = new List<Personaje>();
        public static List<Personaje> listaPersonajesExistentesClonada = new List<Personaje>();
     
        /// <summary>
        /// Este método recibe 2 personajes que enfrentará. Tendrá el cuenta el tipo de origen elemental, el nivel y 
        /// el arma de estos. Al determinar quien gana el enfrentamiento (es decir, quien tiene más poder) devuelve 1, 2 o 3
        /// dependiendo del ganador.
        /// </summary>
        /// <param name="personaje1">Personaje 1 que se enfrentará</param>
        /// <param name="personaje2">Personaje 2 que se enfrentará</param>
        /// <returns>Retorna 1 si el ganador es el personaje 1, retorna 2 si el ganador es  el personaje 2 
        /// retorna 0 si hubo un empate y retorna -1 si hubo un error en el enfrentamiento.</returns>
        public static int Enfrentamiento(Personaje personaje1, Personaje personaje2)
        {
            //HUBO ERROR
            int retorno = -1;

            //SACO EL PODER TOTAL DE LOS 2 PERSONAJES Y LOS GUARDO
            int poderPersonaje1;
            int poderPersonaje2;

            poderPersonaje1 = Personaje.calcularPoderTotal(personaje1);
            poderPersonaje2 = Personaje.calcularPoderTotal(personaje2);

            //---------------------- DESCUENTOS DE PODER -----------------------------------------

            //DETERMINO A QUIEN LE VOY A RESTAR PARTE DE SU PODER POR SER DEL ORIGEN CONTRARRESTADO
            int resultadoPersonajePerdedorOrigen;
            resultadoPersonajePerdedorOrigen = determinarBatallaOrigen(personaje1,personaje2);

            //RESTO EL 25% DE SU PODER AL PERSONAJE PERDEDOR LUEGO DE COMPARAR ORIGENES (HAGO REGLA DE 3)
            int porcentajePoderDescontadoPorOrigen = 25;

            if (resultadoPersonajePerdedorOrigen == 1) //SI PERDIO EL PJ1
            {
                poderPersonaje1 = restarPorcentaje(poderPersonaje1, porcentajePoderDescontadoPorOrigen);
            }
            else if (resultadoPersonajePerdedorOrigen == 2) //SI PERDIO EL PJ2
            {
                poderPersonaje2 = restarPorcentaje(poderPersonaje2, porcentajePoderDescontadoPorOrigen);
            }


            //DETERMINO A QUIEN LE VOY A RESTAR PARTE DE SU PODER POR SER DEL EQUIPAMIENTO CONTRARESTADO
            int resultadoPersonajePerdedorEquipamiento;
            resultadoPersonajePerdedorEquipamiento = determinarBatallaEquipamiento(personaje1, personaje2);

            //RESTO EL 15% DE SU PODER AL PERSONAJE PERDEDOR LUEGO DE COMPARAR EQUIPAMIENTO (HAGO REGLA DE 3)
            int porcentajePoderDescontadoPorEquipamiento = 15;

            if (resultadoPersonajePerdedorEquipamiento == 1) //SI PERDIO EL PJ1
            {
                poderPersonaje1 = restarPorcentaje(poderPersonaje1, porcentajePoderDescontadoPorEquipamiento);
            }
            else if (resultadoPersonajePerdedorEquipamiento == 2) //SI PERDIO EL PJ2
            {
                poderPersonaje2 = restarPorcentaje(poderPersonaje2, porcentajePoderDescontadoPorEquipamiento);
            }

            //------------------------------------------------------------------------------------------------


            if (poderPersonaje1 > poderPersonaje2)
            {
                //GANO HEROE 1
                retorno = 1;
            }
            else if (poderPersonaje1 == poderPersonaje2)
            {
                //HUBO EMPATE
                retorno = 0;
            }
            else if (poderPersonaje1 < poderPersonaje2)
            {
                //GANO HEROE 2
                retorno = 2;
            }

            return retorno;
        }

        /// <summary>
        /// Recibe 2 listas de personajes, limpia la lista a reinicializar, y añade todos los personajes
        /// que contenga la lista inicializadora. Parecido a un copy-paste.
        /// </summary>
        /// <param name="listaAReinicializar">Lista que será limpiada y reinicilizada con la lista inicializadora</param>
        /// <param name="listaInicializadora">Lista inicializadora que añadirá todos sus valores a la lista a reinicializar</param>
        public static void ReinicializarLista(List<Personaje> listaAReinicializar, List<Personaje> listaInicializadora)
        {
            //Limpia lista a reinicializar.
            listaAReinicializar.Clear();

            int maximo = listaInicializadora.Count;

            for (int i = 0; i < maximo; i++)
            {
                listaAReinicializar.Add(listaInicializadora[i]);
            }

        }

        /// <summary>
        /// Recibe un nombre y variables a ser cagadas con el resultado de búsqueda y la posicion del personaje a buscar. Busca
        /// en la lista un nombre que coincida con el ingresado y si se encuentra, se devuelve el personaje. Si no, null.
        /// </summary>
        /// <param name="nombreRecibido">Nombre de un personaje</param>
        /// <param name="fueEncontrado">Se carga con un valor booleano, respondiendo a si fue o no encontrado.</param>
        /// <param name="posicionUbicado">Se carga con un valor int mayor o igual a 0, representativo a la posicion del 
        /// personaje en la lista si fue encontrado.</param>
        /// <returns></returns>
        public static Personaje buscarExistenciaPersonajePorNombre(string nombreRecibido, out bool fueEncontrado, out int posicionUbicado)
        {
            Personaje retorno = null;
            fueEncontrado = false;
            posicionUbicado = -1;
            int limite = listaPersonajesExistentes.Count;

            for (int i = 0; i < limite; i++)
            {
                if (listaPersonajesExistentes[i].NombrePersonaje == nombreRecibido)
                {
                    retorno = listaPersonajesExistentes[i];
                    fueEncontrado = true;
                    posicionUbicado = i;
                    break;
                }
            }

            return retorno;
        }

        //METODOS QUE UTILIZA ENFRENTAMIENTO PERO LOS NECESITO PUBLICOS PARA TESTEARLOS
        /// <summary>
        /// Método que determina quien es el PERDEDOR de la batalla de orígenes entre 2 personajes.
        ///  | FUEGO vs AGUA = GANA AGUA 
        ///  | FUEGO vs HIELO = GANA FUEGO 
        ///  | HIELO vs AGUA = GANA HIELO |
        /// </summary>
        /// <param name="personaje1">Personaje 1</param>
        /// <param name="personaje2">Personaje 2</param>
        /// <returns>Retorna 1 si el perdedor es el personaje 1, retorna 2 si el perdedor es  el personaje 2 
        /// retorna 0 si hubo un empate y retorna -1 si hubo un error al determinar el resultado de la batalla de origen.</returns>
        public static int determinarBatallaOrigen(Personaje personaje1, Personaje personaje2)
        {
            //AHORA VOY A TENER EN CUENTA EL ORIGEN-ELEMENTAL Y COMPARARLO UNO CON OTRO (ALGO ASI COMO EL PIEDRA PAPEL O TIJERA)
            //DETERMINA QUIEN PIERDE EL 40% DE SU PODER.
            
            int numeroPersonajePerdedor = -1;

            // FUEGO vs AGUA = GANA AGUA
            // FUEGO vs HIELO = GANA FUEGO
            // HIELO vs AGUA = GANA HIELO

            if (personaje1.OrigenElemental == Personaje.enumOrigenElemental.Fuego && personaje2.OrigenElemental == Personaje.enumOrigenElemental.Agua)
            {
                numeroPersonajePerdedor = 1;
            }
            else if (personaje1.OrigenElemental == Personaje.enumOrigenElemental.Fuego && personaje2.OrigenElemental == Personaje.enumOrigenElemental.Hielo)
            {
                numeroPersonajePerdedor = 2;
            }
            else if (personaje1.OrigenElemental == Personaje.enumOrigenElemental.Agua && personaje2.OrigenElemental == Personaje.enumOrigenElemental.Fuego)
            {
                numeroPersonajePerdedor = 2;
            }
            else if (personaje1.OrigenElemental == Personaje.enumOrigenElemental.Agua && personaje2.OrigenElemental == Personaje.enumOrigenElemental.Hielo)
            {
                numeroPersonajePerdedor = 1;
            }
            else if (personaje1.OrigenElemental == Personaje.enumOrigenElemental.Hielo && personaje2.OrigenElemental == Personaje.enumOrigenElemental.Fuego)
            {
                numeroPersonajePerdedor = 1;
            }
            else if (personaje1.OrigenElemental == Personaje.enumOrigenElemental.Hielo && personaje2.OrigenElemental == Personaje.enumOrigenElemental.Agua)
            {
                numeroPersonajePerdedor = 2;
            }
            else if (personaje1.OrigenElemental == personaje2.OrigenElemental)
            {
                numeroPersonajePerdedor = 0;
            }

            return numeroPersonajePerdedor;

        }

        /// <summary>
        /// Método que determina quien es el PERDEDOR de la batalla de equipamiento entre 2 personajes.
        ///   | ESCUDO vs ARCO = GANA ARCO
        ///   | ESCUDO vs BASTON-MAGICO = GANA ESCUDO
        ///   | BASTON-MAGICO vs ARCO = GANA BASTON-MAGICO |
        /// </summary>
        /// <param name="personaje1">Personaje 1</param>
        /// <param name="personaje2">Personaje 2</param>
        /// <returns>Retorna 1 si el perdedor es el personaje 1, retorna 2 si el perdedor es  el personaje 2 
        /// retorna 0 si hubo un empate y retorna -1 si hubo un error al determinar el resultado de la batalla de equipamiento.</returns>
        public static int determinarBatallaEquipamiento(Personaje personaje1, Personaje personaje2)
        {
            //AHORA VOY A TENER EN CUENTA EL ARMAMENTO Y COMPARARLO UNO CON OTRO (ALGO ASI COMO EL PIEDRA PAPEL O TIJERA)
            //DETERMINA QUIEN PIERDE EL 15% DE SU PODER.

            int numeroPersonajePerdedor = -1;

            // ESCUDO vs ARCO = GANA ARCO
            // ESCUDO vs BASTON-MAGICO = GANA ESCUDO
            // BASTON-MAGICO vs ARCO = GANA BASTON-MAGICO

            if (personaje1.Arma.TipoArma == Arma.enumTipoArma.Escudo && personaje2.Arma.TipoArma == Arma.enumTipoArma.Arco)
            {
                numeroPersonajePerdedor = 1;
            }
            else if (personaje1.Arma.TipoArma == Arma.enumTipoArma.Escudo && personaje2.Arma.TipoArma == Arma.enumTipoArma.BastonMagico)
            {
                numeroPersonajePerdedor = 2;
            }
            else if (personaje1.Arma.TipoArma == Arma.enumTipoArma.BastonMagico && personaje2.Arma.TipoArma == Arma.enumTipoArma.Arco)
            {
                numeroPersonajePerdedor = 2;
            }
            else if (personaje1.Arma.TipoArma == Arma.enumTipoArma.BastonMagico && personaje2.Arma.TipoArma == Arma.enumTipoArma.Escudo)
            {
                numeroPersonajePerdedor = 1;
            }
            else if (personaje1.Arma.TipoArma == Arma.enumTipoArma.Arco && personaje2.Arma.TipoArma == Arma.enumTipoArma.Escudo)
            {
                numeroPersonajePerdedor = 2;
            }
            else if (personaje1.Arma.TipoArma == Arma.enumTipoArma.Arco && personaje2.Arma.TipoArma == Arma.enumTipoArma.BastonMagico)
            {
                numeroPersonajePerdedor = 1;
            }
            else if (personaje1.Arma.TipoArma == personaje2.Arma.TipoArma)
            {
                numeroPersonajePerdedor = 0;
            }

            return numeroPersonajePerdedor;

        }

        /// <summary>
        /// Método que retorna el valor entero del numero ingresado, restado el porcentaje ingresado de este número.
        /// Ej: El 50 de 100 retornará 50.
        /// </summary>
        /// <param name="numero">Número a ser restado por el porcentaje ingresado</param>
        /// <param name="porcentajeARestar">Porcentaje que restará al número ingresado</param>
        /// <returns>Retorna el nuevo valor del número.</returns>
        public static int restarPorcentaje(int numero, int porcentajeARestar)
        {
            // numero -> 100%
            //   x    -> porcentajeARestar

            //CALCULO CUANTO VALE ESE PORCENTAJE EN CUESTION DEL NUMERO
            int x = (numero * porcentajeARestar) / 100;

            //RESTO EL PORCENTAJE AL NUMERO
            numero = numero - x;

            return numero;
        }

        /// <summary>
        /// Constructor vacío, utilizado para la lectura y escritura de archivos que incluyan un personaje.
        /// </summary>
        static Universo()
        {
                
        }
    }
}

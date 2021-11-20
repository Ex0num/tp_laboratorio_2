using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class Universo
    {
        //-------------------------------ATRIBUTOS-----------------------------------------
        public static List<Personaje> listaPersonajesExistentes = new List<Personaje>();
        public static List<Personaje> listaPersonajesExistentesClonada = new List<Personaje>();

        //--------------------------------METODOS-------------------------------------------
        /// <summary>
        /// Este método invoca al evento batallar, llamando así a su manejador asociado
        /// que enfrentará a los 2 heroes recibidos en este metodo.
        /// </summary>
        /// <param name="personaje1">Personaje 1 que se enfrentará</param>
        /// <param name="personaje2">Personaje 2 que se enfrentará</param>
        /// <returns>Retorna 1 si el ganador es el personaje 1, retorna 2 si el ganador es  el personaje 2 
        /// retorna 0 si hubo un empate y retorna -1 si hubo un error en el enfrentamiento.</returns>
        public static int Enfrentamiento(Personaje personaje1, Personaje personaje2)
        {
            int resultadoBatalla;
        
            //Llamo/Invoco a mi evento batallar, este me devuelve el resultado de la batalla, y lo retorno
            Universo.Batallar(personaje1, personaje2, out resultadoBatalla);

            return resultadoBatalla;          
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
        /// Método que devuelve por referencia el tipo de arma con más o menos winrate segun se indique en su parametro booleano.
        /// Recorre la lista para ir sabiendo cuantas partidas jugadas y ganadas tiene cada arma.
        /// </summary>
        /// <param name="tipoArmasWinrate">Tipo de arma con más o menos winrate que se devuelve por OUT</param>
        /// <param name="winrateDelTipoArma">Winrate del tipo de arma con más o menos winrate que se devuelve por OUT</param>
        /// <param name="esMayorWinrate">Booleano recibido que indica al método si se debe buscar el mayor o el menor winrate.</param>
        public static void ObtenerWinrateArma(out Arma.enumTipoArma tipoArmasWinrate, out double winrateDelTipoArma, bool esMayorWinrate)
        {
            //Asigno valores de inicialización. 
            tipoArmasWinrate = Arma.enumTipoArma.Arco;
            winrateDelTipoArma = 0;

            //Me tengo que guardas las ganadas y jugadas de cada arma
            double ganadasArcos = 0;
            double jugadasArcos = 0;

            double ganadasEscudos = 0;
            double jugadasEscudos = 0;

            double ganadasBastones = 0;
            double jugadasBastones = 0;

            //Recorro la lista de personajes guardandome las jugadas y ganadas de todos los personajes
            //para posteriormente hacer un promedio de winrate de cada arma y devolver la de mayor condicion de victoria.
            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
            {

                //Me fijo de que tipo es el arma del personaje y me fijo si por lo menos tiene alguna partida jugada.
                if (personaje.Arma.TipoArma == Arma.enumTipoArma.Arco && personaje.BatallasJugadas > 0)
                {

                    ganadasArcos = ganadasArcos + personaje.BatallasGanadas;
                    jugadasArcos = jugadasArcos + personaje.BatallasJugadas;

                }
                else if (personaje.Arma.TipoArma == Arma.enumTipoArma.Escudo && personaje.BatallasJugadas > 0)
                {

                    ganadasEscudos = ganadasEscudos + personaje.BatallasGanadas;
                    jugadasEscudos = jugadasEscudos + personaje.BatallasJugadas;

                }
                else if (personaje.Arma.TipoArma == Arma.enumTipoArma.BastonMagico && personaje.BatallasJugadas > 0)
                {

                    ganadasBastones = ganadasBastones + personaje.BatallasGanadas;
                    jugadasBastones = jugadasBastones + personaje.BatallasJugadas;
                }

            }

            //Una vez que recorri todos los personajes hago regla de 3 simple.

            //----------------------------------------------------------------------------------------//
            //           partidasJugadasConArco ->>> 100%                                             //
            //           partidasGanadas        ->>> ??   <- Me interesa el porcentaje de victorias   //
            //                                                                                        //
            //           Fórmula:  partidasGanadas * 100 / partidasJugadasConArco = x                 //
            //----------------------------------------------------------------------------------------//

            double winrateArcos;
            double winrateEscudos;
            double winrateBastones;

            if (jugadasArcos != 0)
            {
                winrateArcos = ganadasArcos * 100 / jugadasArcos;
            }
            else
            {
                winrateArcos = 0;
            }

            if (jugadasEscudos != 0)
            {
                winrateEscudos = ganadasEscudos * 100 / jugadasEscudos;
            }
            else
            {
                winrateEscudos = 0;
            }

            if (jugadasBastones != 0)
            {
                winrateBastones = ganadasBastones * 100 / jugadasBastones;
            }
            else
            {
                winrateBastones = 0;
            }


            //Me fijo si se pidió el mayor o menor winrate.
            switch (esMayorWinrate)
            {
                case true:
                    {
                        //DEVUELVO EL MAYOR WINRATE

                        if (winrateArcos >= winrateEscudos && winrateArcos >= winrateBastones) //SI WINRATE ARCOS ES EL MAYOR
                        {
                            //Me guardo el tipo del arma con su winratio.
                            tipoArmasWinrate = Arma.enumTipoArma.Arco;
                            winrateDelTipoArma = winrateArcos;
                        }
                        else if (winrateEscudos >= winrateArcos && winrateEscudos >= winrateBastones) //SI WINRATE ESCUDOS ES EL MAYOR
                        {
                            //Me guardo el tipo del arma con su winratio.
                            tipoArmasWinrate = Arma.enumTipoArma.Escudo;
                            winrateDelTipoArma = winrateEscudos;
                        }
                        else if (winrateBastones >= winrateArcos && winrateBastones >= winrateEscudos) //SI WINRATE BASTONES ES EL MAYOR
                        {
                            //Me guardo el tipo del arma con su winratio.
                            tipoArmasWinrate = Arma.enumTipoArma.BastonMagico;
                            winrateDelTipoArma = winrateBastones;
                        }

                        break;
                    }
                case false:
                    {
                        //DEVUELVO EL MENOR WINRATE

                        if (winrateArcos <= winrateEscudos && winrateArcos <= winrateBastones) //SI WINRATE ARCOS ES EL MENOR
                        {
                            //Me guardo el tipo del arma con su winratio.
                            tipoArmasWinrate = Arma.enumTipoArma.Arco;
                            winrateDelTipoArma = winrateArcos;
                        }
                        else if (winrateEscudos <= winrateArcos && winrateEscudos <= winrateBastones) //SI WINRATE ESCUDOS ES EL MENOR
                        {
                            //Me guardo el tipo del arma con su winratio.
                            tipoArmasWinrate = Arma.enumTipoArma.Escudo;
                            winrateDelTipoArma = winrateEscudos;
                        }
                        else if (winrateBastones <= winrateArcos && winrateBastones <= winrateEscudos) //SI WINRATE BASTONES ES EL MENOR
                        {
                            //Me guardo el tipo del arma con su winratio.
                            tipoArmasWinrate = Arma.enumTipoArma.BastonMagico;
                            winrateDelTipoArma = winrateBastones;
                        }

                        break;
                    }
            }

            if (winrateArcos == 0 && winrateEscudos == 0 & winrateBastones == 0)
            {
                winrateDelTipoArma = -2;
            }

            if (jugadasArcos == 0 && jugadasEscudos == 0 && jugadasBastones == 0)
            {
                winrateDelTipoArma = -1;
            }
        }

        /// <summary>
        /// Método que devuelve por referencia el tipo de poder con más o menos winrate segun se indique en su parametro booleano.
        /// Recorre la lista para ir sabiendo cuantas partidas jugadas y ganadas tiene cada poder.
        /// </summary>
        /// <param name="tipoPoderWinrate">Tipo de poder con más o menos winrate que se devuelve por OUT</param>
        /// <param name="winrateDelPoder">Winrate del tipo de poder con más o menos winrate que se devuelve por OUT</param>
        /// <param name="esMayorWinrate">Booleano recibido que indica al método si se debe buscar el mayor o el menor winrate.</param>
        public static void ObtenerWinratePoder(out Personaje.enumOrigenElemental tipoPoderWinrate, out double winrateDelPoder, bool esMayorWinrate)
        {
            //Asigno valores de inicialización. 
            tipoPoderWinrate = Personaje.enumOrigenElemental.Fuego;
            winrateDelPoder = 0;

            //Me tengo que guardas las ganadas y jugadas de cada poder
            double ganadasFuego = 0;
            double jugadasFuego = 0;

            double ganadasAgua = 0;
            double jugadasAgua = 0;

            double ganadasHielo = 0;
            double jugadasHielo = 0;

            //Recorro la lista de personajes guardandome las jugadas y ganadas de todos los personajes
            //para posteriormente hacer un promedio de winrate de cada poder y devolver la de mayor condicion de victoria.
            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
            {

                //Me fijo de que tipo es el poder del personaje y me fijo si por lo menos tiene alguna partida jugada.
                if (personaje.OrigenElemental == Personaje.enumOrigenElemental.Fuego && personaje.BatallasJugadas > 0)
                {

                    ganadasFuego = ganadasFuego + personaje.BatallasGanadas;
                    jugadasFuego = jugadasFuego + personaje.BatallasJugadas;

                }
                else if (personaje.OrigenElemental == Personaje.enumOrigenElemental.Agua && personaje.BatallasJugadas > 0)
                {

                    ganadasAgua = ganadasAgua + personaje.BatallasGanadas;
                    jugadasAgua = jugadasAgua + personaje.BatallasJugadas;

                }
                else if (personaje.OrigenElemental == Personaje.enumOrigenElemental.Hielo && personaje.BatallasJugadas > 0)
                {

                    ganadasHielo = ganadasHielo + personaje.BatallasGanadas;
                    jugadasHielo = jugadasHielo + personaje.BatallasJugadas;

                }
            }

            //Una vez que recorri todos los personajes hago regla de 3 simple.

            //----------------------------------------------------------------------------------------//
            //           partidasJugadasConFuego ->>> 100%                                            //
            //           partidasGanadas        ->>> ??   <- Me interesa el porcentaje de victorias   //
            //                                                                                        //
            //           Fórmula:  partidasGanadas * 100 / partidasJugadasConFuego = x                //
            //----------------------------------------------------------------------------------------//

            double winrateFuego;
            double winrateAgua;
            double winrateHielo;

            if (jugadasFuego != 0)
            {
                winrateFuego = ganadasFuego * 100 / jugadasFuego;
            }
            else
            {
                winrateFuego = 0;
            }

            if (jugadasAgua != 0)
            {
                winrateAgua = ganadasAgua * 100 / jugadasAgua;
            }
            else
            {
                winrateAgua = 0;
            }

            if (jugadasHielo != 0)
            {
                winrateHielo = ganadasHielo * 100 / jugadasHielo;
            }
            else
            {
                winrateHielo = 0;
            }

            //Me fijo si se pidió el mayor o menor winrate.
            switch (esMayorWinrate)
            {
                case true:
                    {
                        //DEVUELVO EL MAYOR WINRATE

                        if (winrateFuego >= winrateAgua && winrateFuego >= winrateHielo) //SI WINRATE ARCOS ES EL MAYOR
                        {
                            //Me guardo el tipo del poder con su winratio.
                            tipoPoderWinrate = Personaje.enumOrigenElemental.Fuego;
                            winrateDelPoder = winrateFuego;
                        }
                        else if (winrateAgua >= winrateFuego && winrateAgua >= winrateHielo) //SI WINRATE ESCUDOS ES EL MAYOR
                        {
                            //Me guardo el tipo del poder con su winratio.
                            tipoPoderWinrate = Personaje.enumOrigenElemental.Agua;
                            winrateDelPoder = winrateAgua;
                        }
                        else if (winrateHielo >= winrateFuego && winrateHielo >= winrateAgua) //SI WINRATE BASTONES ES EL MAYOR
                        {
                            //Me guardo el tipo del poder con su winratio.
                            tipoPoderWinrate = Personaje.enumOrigenElemental.Hielo;
                            winrateDelPoder = winrateHielo;
                        }

                        break;

                    }
                case false:
                    {
                        //DEVUELVO EL MENOR WINRATE

                        if (winrateFuego <= winrateAgua && winrateFuego <= winrateHielo) //SI WINRATE ARCOS ES EL MENOR
                        {
                            //Me guardo el tipo del poder con su winratio.
                            tipoPoderWinrate = Personaje.enumOrigenElemental.Fuego;
                            winrateDelPoder = winrateFuego;
                        }
                        else if (winrateAgua <= winrateFuego && winrateAgua <= winrateHielo) //SI WINRATE ESCUDOS ES EL MENOR
                        {
                            //Me guardo el tipo del poder con su winratio.
                            tipoPoderWinrate = Personaje.enumOrigenElemental.Agua;
                            winrateDelPoder = winrateAgua;
                        }
                        else if (winrateHielo <= winrateFuego && winrateHielo <= winrateAgua) //SI WINRATE BASTONES ES EL MENOR
                        {
                            //Me guardo el tipo del poder con su winratio.
                            tipoPoderWinrate = Personaje.enumOrigenElemental.Hielo;
                            winrateDelPoder = winrateHielo;
                        }

                        break;
                    }
            }

            if (winrateFuego == 0 && winrateAgua == 0 && winrateHielo == 0)
            {
                winrateDelPoder = -2;
            }

            if (jugadasFuego == 0 && jugadasAgua == 0 && jugadasHielo == 0)
            {
                winrateDelPoder = -1;
            }
        }

        /// <summary>
        /// Método que devuelve por referencia el personaje con más o menos winrate segun se indique en su parametro booleano.
        /// Recorre la lista para ir sabiendo cuantas partidas jugadas y ganadas tiene cada personaje.
        /// </summary>
        /// <param name="personajeMAXoMINWinrate">Personaje con más o menos winrate que se devuelve por OUT</param>
        /// <param name="winrateMAXoMINDelPersonaje">Winrate del personaje con más o menos winrate que se devuelve por OUT</param>
        /// <param name="esMayorWinrate">Booleano recibido que indica al método si se debe buscar el mayor o el menor winrate.</param>
        public static void ObtenerWinratePersonaje(out Personaje personajeMAXoMINWinrate, out double winrateMAXoMINDelPersonaje, bool esMayorWinrate)
        {
            personajeMAXoMINWinrate = null;
            winrateMAXoMINDelPersonaje = 0;

            //Me fijo si se pidió el mayor o menor winrate.

            //Hago regla de 3 simple.

            //--------------------------------------------------------------------------------------------------//
            //           partidasJugadasTotalesDelPersonaje ->>> 100%                                           //
            //                  partidasGanadas             ->>>  ??  <- Me interesa el porcentaje de victorias //
            //                                                                                                  //
            //           Fórmula:  partidasGanadas * 100 / partidasJugadasTotalesDelPersonaje = x               //
            //--------------------------------------------------------------------------------------------------//

            if (Universo.listaPersonajesExistentes.Count >= 1)
            {
                switch (esMayorWinrate)
                {
                    case true:
                        {

                            //Si existe 1 personaje como minimo, ese va a ser el maximo por el momento.
                            if (Universo.listaPersonajesExistentes.Count >= 1 && Universo.listaPersonajesExistentes[0].BatallasJugadas >= 1)
                            {
                                personajeMAXoMINWinrate = Universo.listaPersonajesExistentes[0];
                                winrateMAXoMINDelPersonaje = personajeMAXoMINWinrate.BatallasGanadas * 100 / personajeMAXoMINWinrate.BatallasJugadas;
                            }
                            else
                            {
                                personajeMAXoMINWinrate = null;
                                winrateMAXoMINDelPersonaje = 0;
                            }

                            double winratePersonajeLeido;

                            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
                            {
                                if (personaje.BatallasJugadas >= 1)
                                {
                                    //Calculo el winrate de cada personaje en la lista y lo comparo con el maximo.
                                    winratePersonajeLeido = personaje.BatallasGanadas * 100 / personaje.BatallasJugadas;

                                    //Si el winrate del personaje leido es mayor al winrate maximo
                                    if (winratePersonajeLeido > winrateMAXoMINDelPersonaje)
                                    {
                                        //Piso los datos del maximo, porque ahora hay un nuevo maximo.
                                        winrateMAXoMINDelPersonaje = winratePersonajeLeido;
                                        personajeMAXoMINWinrate = personaje;
                                    }
                                }

                            }


                            break;
                        }
                    case false:
                        {
                            //Si existe 1 personaje como minimo, ese va a ser el minimo por el momento.
                            if (Universo.listaPersonajesExistentes.Count >= 1 && Universo.listaPersonajesExistentes[0].BatallasJugadas >= 1)
                            {
                                personajeMAXoMINWinrate = Universo.listaPersonajesExistentes[0];
                                winrateMAXoMINDelPersonaje = personajeMAXoMINWinrate.BatallasGanadas * 100 / personajeMAXoMINWinrate.BatallasJugadas;
                            }
                            else
                            {
                                personajeMAXoMINWinrate = null;
                                winrateMAXoMINDelPersonaje = 0;
                            }

                            double winratePersonajeLeido;

                            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
                            {

                                if (personaje.BatallasJugadas >= 1)
                                {
                                    //Calculo el winrate de cada personaje en la lista y lo comparo con el minimo.
                                    winratePersonajeLeido = personaje.BatallasGanadas * 100 / personaje.BatallasJugadas;

                                    //Si el winrate del personaje leido es mayor al winrate maximo
                                    if (winratePersonajeLeido < winrateMAXoMINDelPersonaje)
                                    {
                                        //Piso los datos del maximo, porque ahora hay un nuevo maximo.
                                        winrateMAXoMINDelPersonaje = winratePersonajeLeido;
                                        personajeMAXoMINWinrate = personaje;
                                    }
                                }

                            }

                            break;
                        }
                }
            }


        }

        /// <summary>
        /// Constructor vacío, utilizado para la lectura y escritura de archivos que incluyan un personaje.
        /// </summary>
        static Universo()
        {

        }

        /// <summary>
        /// Delegado que recibe 2 personajes y retorna int. Se utiliza principalmente para el enfrentamiento de personajes
        /// </summary>
        /// <param name="personaje1"></param>
        /// <param name="personaje2"></param>
        /// <returns></returns>
        public delegate void DelegadoEnfrentamiento(Personaje personaje1, Personaje personaje2, out int resultadoBatalla);

        /// <summary>
        /// Evento que ocurre al momento de ser presionado el botón batallar luego de haber validado los 2 personajes para batallar.
        /// </summary>
        public static event DelegadoEnfrentamiento Batallar;

        /// <summary>
        /// Este manejador del evento batallar recibe 2 personajes que enfrentará. Tendrá el cuenta el tipo de origen elemental, el nivel y 
        /// el arma de estos. Al determinar quien gana el enfrentamiento (es decir, quien tiene más poder) devuelve 1, 2 o 3
        /// dependiendo del ganador.
        /// </summary>
        /// <param name="personaje1">Personaje 1 que se enfrentará</param>
        /// <param name="personaje2">Personaje 2 que se enfrentará</param>
        /// <returns>Retorna 1 si el ganador es el personaje 1, retorna 2 si el ganador es  el personaje 2 
        /// retorna 0 si hubo un empate y retorna -1 si hubo un error en el enfrentamiento.</returns>
        public static void ManejadorEventoBatallar(Personaje personaje1, Personaje personaje2, out int resultadoBatalla)
        {
            //HUBO ERROR
            resultadoBatalla = -1;

            //SACO EL PODER TOTAL DE LOS 2 PERSONAJES Y LOS GUARDO
            int poderPersonaje1;
            int poderPersonaje2;

            poderPersonaje1 = Personaje.calcularPoderTotal(personaje1);
            poderPersonaje2 = Personaje.calcularPoderTotal(personaje2);

            //---------------------- DESCUENTOS DE PODER -----------------------------------------

            //DETERMINO A QUIEN LE VOY A RESTAR PARTE DE SU PODER POR SER DEL ORIGEN CONTRARRESTADO
            int resultadoPersonajePerdedorOrigen;
            resultadoPersonajePerdedorOrigen = determinarBatallaOrigen(personaje1, personaje2);

            //RESTO EL 25% DE SU PODER AL PERSONAJE PERDEDOR LUEGO DE COMPARAR ORIGENES (HAGO REGLA DE 3)
            int porcentajePoderDescontadoPorOrigen = 25;

            if (resultadoPersonajePerdedorOrigen == 1) //SI PERDIO EL PJ1
            {
                //ESTO ES LO QUE HACIA ANTES
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
                resultadoBatalla = 1;
            }
            else if (poderPersonaje1 == poderPersonaje2)
            {
                //HUBO EMPATE
                resultadoBatalla = 0;
            }
            else if (poderPersonaje1 < poderPersonaje2)
            {
                //GANO HEROE 2
                resultadoBatalla = 2;
            }
        }
    }
}

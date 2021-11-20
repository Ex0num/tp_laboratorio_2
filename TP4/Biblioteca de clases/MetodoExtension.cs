using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Entidades
{
    public static class MetodoExtension
    {
        /// <summary>
        /// Método (de extension para una Lista que contenga Personajes que obtiene los porcentajes de cada arma o poder, 
        /// segun se indique en su parametro booleano
        /// y los devuelve por out. En el respectivo orden.
        /// Porcentaje1 -> Fuego/Arco
        /// Porcentaje2 -> Agua/Escudo
        /// Porcentaje3 -> Hielo/BastonMagico
        /// </summary>
        /// <param name="esPorcentajeArma"></param>
        /// <param name="porcentaje1">Porcentaje sease del arma Arco o poder Fuego</param>
        /// <param name="porcentaje2">Porcentaje sease del arma Escudo o poder Agua</param>
        /// <param name="porcentaje3">Porcentaje sease del arma BastonMagico o poder Hielo</param>
        public static void ObtenerPorcentajes(this List<Personaje> ListaTipoPersonajes, bool esPorcentajeArma, out double porcentaje1, out double porcentaje2, out double porcentaje3)
        {
            porcentaje1 = 0;
            porcentaje2 = 0;
            porcentaje3 = 0;

            if (Universo.listaPersonajesExistentes.Count >= 1)
            {
                //Me fijo si se pidió el porcentaje de arma o de poder.
                switch (esPorcentajeArma)
                {
                    case true:
                        {

                            //TENGO QUE CALCULAR EL PORCENTAJE DE ARMAS
                            int cantidadPersonajes = Universo.listaPersonajesExistentes.Count;

                            int cantidadArcos = 0;
                            int cantidadEscudos = 0;
                            int cantidadBastones = 0;

                            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
                            {
                                //Me fijo de que tipo de arma es la del personaje actual y la contemplo.

                                if (personaje.Arma.TipoArma == Arma.enumTipoArma.Arco)
                                {
                                    cantidadArcos++;
                                }
                                else if (personaje.Arma.TipoArma == Arma.enumTipoArma.Escudo)
                                {
                                    cantidadEscudos++;
                                }
                                else if (personaje.Arma.TipoArma == Arma.enumTipoArma.BastonMagico)
                                {
                                    cantidadBastones++;
                                }

                            }

                            //Calculo el porcentaje de cada arma

                            //--------------------------------------------------------------------------------------------------//
                            //           cantidadPersonajes ->>> 100%                                                           //
                            //              cantidadArcos   ->>> porcentajeArcos  <- Me interesa el porcentaje de arcos         //
                            //                                                                                                  //
                            //           Fórmula:  partidasGanadas * 100 / partidasJugadasTotalesDelPersonaje = x               //
                            //--------------------------------------------------------------------------------------------------//

                            double porcentajeArcos = cantidadArcos * 100 / cantidadPersonajes;
                            double porcentajeEscudos = cantidadEscudos * 100 / cantidadPersonajes;
                            double porcentajeBastones = cantidadBastones * 100 / cantidadPersonajes;

                            porcentaje1 = porcentajeArcos;
                            porcentaje2 = porcentajeEscudos;
                            porcentaje3 = porcentajeBastones;

                            break;
                        }
                    case false:
                        {
                            //TENGO QUE CALCULAR EL PORCENTAJE DE PODERES
                            int cantidadPersonajes = Universo.listaPersonajesExistentes.Count;

                            int cantidadFuego = 0;
                            int cantidadAgua = 0;
                            int cantidadHielo = 0;

                            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
                            {
                                //Me fijo de que tipo de arma es la del personaje actual y la contemplo.

                                if (personaje.OrigenElemental == Personaje.enumOrigenElemental.Fuego)
                                {
                                    cantidadFuego++;
                                }
                                else if (personaje.OrigenElemental == Personaje.enumOrigenElemental.Agua)
                                {
                                    cantidadAgua++;
                                }
                                else if (personaje.OrigenElemental == Personaje.enumOrigenElemental.Hielo)
                                {
                                    cantidadHielo++;
                                }

                            }

                            //Calculo el porcentaje de cada poder

                            //--------------------------------------------------------------------------------------------------//
                            //           cantidadPersonajes ->>> 100%                                                           //
                            //              cantidadFuego   ->>> porcentajeFuegos  <- Me interesa el porcentaje de fuegos       //
                            //                                                                                                  //
                            //           Fórmula:  cantidadFuego * 100 / cantidadPersonajes = x                                 //
                            //--------------------------------------------------------------------------------------------------//

                            double porcentajeFuego = cantidadFuego * 100 / cantidadPersonajes;
                            double porcentajeAgua = cantidadAgua * 100 / cantidadPersonajes;
                            double porcentajeHielo = cantidadHielo * 100 / cantidadPersonajes;

                            porcentaje1 = porcentajeFuego;
                            porcentaje2 = porcentajeAgua;
                            porcentaje3 = porcentajeHielo;

                            break;
                        }
                }
            }



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests_Unitarios
{
    [TestClass]
    public class TesterMetodosDeUniverso
    {

        /// <summary>
        /// Testeo del metodo enfrentamiento. Se prueban enfrentamientos PREDECIBLES entre varios personaje,
        /// como el caso de un personaje nivel 100 vs nivel 0, entre otros.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoEnfrentamiento()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 1, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Alfonsito", 99, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje3 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Warrior123", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje5 = new Personaje("Alfonsin", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje6 = new Personaje("Fede", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.BastonMagico);
            Personaje personaje7 = new Personaje("Federico", 2, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.BastonMagico);
            Personaje personaje8 = new Personaje("Alfonsa", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.BastonMagico);

            int validacionBatalla1;
            int validacionBatalla2;
            int validacionBatalla3;
            int validacionBatalla4;
            int validacionBatalla5;
            int validacionBatalla6;

            //ACT
            validacionBatalla1 = Universo.Enfrentamiento(personaje1, personaje2);
            validacionBatalla2 = Universo.Enfrentamiento(personaje3, personaje4);
            validacionBatalla3 = Universo.Enfrentamiento(personaje1, personaje8);
            validacionBatalla4 = Universo.Enfrentamiento(personaje5, personaje1);
            validacionBatalla5 = Universo.Enfrentamiento(personaje5, personaje6);
            validacionBatalla6 = Universo.Enfrentamiento(personaje6, personaje7);

            //ASSERT
            Assert.AreEqual(2, validacionBatalla1);
            Assert.AreEqual(0, validacionBatalla2);
            Assert.AreEqual(2, validacionBatalla3);
            Assert.AreEqual(1, validacionBatalla4);
            Assert.AreEqual(1, validacionBatalla4);
            Assert.AreEqual(2, validacionBatalla5);
            Assert.AreEqual(2, validacionBatalla6);
        }

        /// <summary>
        /// Testeo del metodo ReinicializarLista. Se verifica que la reinicializacion verdaderamente cargue todos
        /// los personajes de una lista a otra, sin ningun problema.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoReinicializarLista()
        {
            //ARRANGE
            List<Personaje> lista1 = new List<Personaje>();
            List<Personaje> lista2 = new List<Personaje>();

            Personaje personaje1 = new Personaje("Alfonso", 1, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Alfonsito", 99, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje3 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Warrior123", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje5 = new Personaje("Alfonsin", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje6 = new Personaje("Fede", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.BastonMagico);

            lista1.Add(personaje1);
            lista1.Add(personaje2);

            lista2.Add(personaje3);
            lista2.Add(personaje4);
            lista2.Add(personaje5);
            lista2.Add(personaje6);

            //ACT
            Universo.ReinicializarLista(lista1, lista2);

            //ASSERT
            Assert.IsTrue(lista1.Count == lista2.Count);
        }

        /// <summary>
        /// Testeo del metodo BuscarPersonajePorNombre. Se agrega personajes a la lista estática y se busca por nombre
        /// a ciertos personajes. Se verifica que el método cargue, como se espera, todas las variables que recibe como
        /// out en sus parametros.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoBuscarPersonajePorNombre()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 1, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Alfonsito", 99, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje3 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Warrior123", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje5 = new Personaje("Alfonsin", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje6 = new Personaje("Fede", 1, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.BastonMagico);

            Universo.listaPersonajesExistentesClonada.Clear();

            Universo.listaPersonajesExistentes.Add(personaje1);
            Universo.listaPersonajesExistentes.Add(personaje2);
            Universo.listaPersonajesExistentes.Add(personaje3);
            Universo.listaPersonajesExistentes.Add(personaje4);
            Universo.listaPersonajesExistentes.Add(personaje5);
            Universo.listaPersonajesExistentes.Add(personaje6);

            string nombrePersonajeABuscar = "Warrior123";
            bool fueEncontrado = false;
            int posicionEnLista = -1;
            Personaje pjEncontrado = null;

            string nombrePersonajeABuscar2 = "GabrielLopezG";
            bool fueEncontrado2 = false;
            int posicionEnLista2 = -1;
            Personaje pjEncontrado2 = null;

            //ACT
            pjEncontrado = Universo.buscarExistenciaPersonajePorNombre(nombrePersonajeABuscar, out fueEncontrado, out posicionEnLista);
            pjEncontrado2 = Universo.buscarExistenciaPersonajePorNombre(nombrePersonajeABuscar2, out fueEncontrado2, out posicionEnLista2);

            //ASSERT
            Assert.IsTrue(pjEncontrado != null && posicionEnLista >= 0 && fueEncontrado == true);
            Assert.IsFalse(pjEncontrado2 != null && posicionEnLista2 >= 0 && fueEncontrado2 == true);
        }

        /// <summary>
        /// Testeo del metodo DeterminarBatallaOrigen. Se verifica que devuelva resultados PREDECIBLES, entre la
        /// batallas de origenes de ciertos personajes. AGUA vs FUEGO, que devuelva a personaje perdedor como FUEGO.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoDeterminarBatallaOrigen()
        {
            //ARRANGE

            // FUEGO vs AGUA = GANA AGUA
            // FUEGO vs HIELO = GANA FUEGO
            // HIELO vs AGUA = GANA HIELO

            Personaje personaje1 = new Personaje("SoyFuego", 50, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);
            Personaje personaje2 = new Personaje("SoyAgua", 20, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje3 = new Personaje("SoyHielo", 80, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);

            int resultadoBatalla1Origen;
            int resultadoBatalla2Origen;
            int resultadoBatalla3Origen;
            int resultadoBatalla4Origen;

            //ACT
            resultadoBatalla1Origen = Universo.determinarBatallaOrigen(personaje1, personaje2);
            resultadoBatalla2Origen = Universo.determinarBatallaOrigen(personaje1, personaje3);
            resultadoBatalla3Origen = Universo.determinarBatallaOrigen(personaje2, personaje3);
            resultadoBatalla4Origen = Universo.determinarBatallaOrigen(personaje1, personaje1);

            //ASSERT
            Assert.AreEqual(1, resultadoBatalla1Origen);
            Assert.AreEqual(2, resultadoBatalla2Origen);
            Assert.AreEqual(1, resultadoBatalla3Origen);
            Assert.AreEqual(0, resultadoBatalla4Origen);
        }

        /// <summary>
        /// Testeo del metodo DeterminarBatallaEquipamiento. Se verifica que devuelva resultados PREDECIBLES, entra la batalla
        /// de equipamiento de ciertos personajes. ARCO VS ESCUDO, que devuelve al personaje perdedor como ESCUDO.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoDeterminarBatallaEquipamiento()
        {
            //ARRANGE

            // ESCUDO vs ARCO = GANA ARCO
            // ESCUDO vs BASTON-MAGICO = GANA ESCUDO
            // BASTON-MAGICO vs ARCO = GANA BASTON-MAGICO

            Personaje personaje1 = new Personaje("SoyEscudo", 50, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);
            Personaje personaje2 = new Personaje("SoyArco", 20, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje3 = new Personaje("SoyBastonMagico", 80, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);

            int resultadoBatalla1Equipamiento;
            int resultadoBatalla2Equipamiento;
            int resultadoBatalla3Equipamiento;

            //ACT
            resultadoBatalla1Equipamiento = Universo.determinarBatallaEquipamiento(personaje1,personaje2);
            resultadoBatalla2Equipamiento = Universo.determinarBatallaEquipamiento(personaje1,personaje3);
            resultadoBatalla3Equipamiento = Universo.determinarBatallaEquipamiento(personaje2,personaje3);

            //ASSERT
            Assert.AreEqual(1, resultadoBatalla1Equipamiento);
            Assert.AreEqual(2, resultadoBatalla2Equipamiento);
            Assert.AreEqual(1, resultadoBatalla3Equipamiento);
        }

        /// <summary>
        /// Testeo del metodo RestarPorcentaje. Se verifica que los valores numericos que devuelve este metodo
        /// sean válidos y coherentes. Calculados anteriormente con una calculadora.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoRestarPorcentaje()
        {
            //ARRANGE
            int numero1 = 100;
            int numero2 = 1000;
            int numero3 = 100000;

            int resultadoNumero1;
            int resultado2Numero1;

            int resultadoNumero2;
            int resultado2Numero2;

            int resultadoNumero3;
            int resultado2Numero3;

            //ACT
            resultadoNumero1 = Universo.restarPorcentaje(numero1,50);
            resultado2Numero1 = Universo.restarPorcentaje(numero1,25);

            resultadoNumero2 = Universo.restarPorcentaje(numero2, 50);
            resultado2Numero2 = Universo.restarPorcentaje(numero2, 25);

            resultadoNumero3 = Universo.restarPorcentaje(numero3, 50);
            resultado2Numero3 = Universo.restarPorcentaje(numero3, 25);

            //ASSERT
            Assert.AreEqual(50,resultadoNumero1);
            Assert.AreEqual(75, resultado2Numero1);
                
            Assert.AreEqual(500,resultadoNumero2);
            Assert.AreEqual(750,resultado2Numero2);

            Assert.AreEqual(50000,resultadoNumero3);
            Assert.AreEqual(75000,resultado2Numero3);
        }
    }
}

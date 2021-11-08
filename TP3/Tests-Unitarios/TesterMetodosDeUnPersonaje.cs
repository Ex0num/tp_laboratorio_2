using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace Tests_Unitarios
{
    [TestClass]
    public class TesterMetodosDeUnPersonaje
    {
        /// <summary>
        /// Se testea el metodo RepeticionDePersonaje. Se verifica que devuelva si un personaje realmente
        /// está o no repetido.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoRepeticionDePersonaje()
        {

            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            Universo.listaPersonajesExistentes.Clear();

            Universo.listaPersonajesExistentes.Add(personaje1);
            Universo.listaPersonajesExistentes.Add(personaje2);
            Universo.listaPersonajesExistentes.Add(personaje3);
            Universo.listaPersonajesExistentes.Add(personaje4);

            bool validacionNvl1;
            bool validacionNvl2;
            bool validacionNvl3;
            bool validacionNvl4;
            bool validacionNvl5;

            //ACT
            validacionNvl1 = Personaje.estaRepetido("Lopecito");
            validacionNvl2 = Personaje.estaRepetido("Martin");
            validacionNvl3 = Personaje.estaRepetido(personaje3.NombrePersonaje);
            validacionNvl4 = Personaje.estaRepetido(personaje4.NombrePersonaje);
            validacionNvl5 = Personaje.estaRepetido("CreandoNuevoPj");

            //ASSERT
            Assert.AreEqual(false, validacionNvl1);
            Assert.AreEqual(true, validacionNvl2);
            Assert.AreEqual(true, validacionNvl3);
            Assert.AreEqual(true, validacionNvl4);
            Assert.AreEqual(false, validacionNvl5);
        }

        /// <summary>
        /// Se testea el metodo EsPosibleModificar. Se verifica que el metodo devuelva si realmente es posible
        /// modificar el personaje ingresado por el nombre ingresado.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoEsPosibleModificarDePersonaje()
        {

            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Federico", 10, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            Universo.listaPersonajesExistentes.Clear();

            Universo.listaPersonajesExistentes.Add(personaje1);
            Universo.listaPersonajesExistentes.Add(personaje2);
            Universo.listaPersonajesExistentes.Add(personaje3);
            Universo.listaPersonajesExistentes.Add(personaje4);

            bool validacionNvl1;
            bool validacionNvl2;
            bool validacionNvl3;
            bool validacionNvl4;

            //ACT
            validacionNvl1 = Personaje.esPosibleModificar(personaje1, "Gabriel Lopez");
            validacionNvl2 = Personaje.esPosibleModificar(personaje1, "Alfoncito");
            validacionNvl3 = Personaje.esPosibleModificar(personaje1, "Alfonso");
            validacionNvl4 = Personaje.esPosibleModificar(personaje1, "Federico");

            //ASSERT
            Assert.AreEqual(true, validacionNvl1);
            Assert.AreEqual(true, validacionNvl2);
            Assert.AreEqual(true, validacionNvl3);
            Assert.AreEqual(false, validacionNvl4);
        }

        /// <summary>
        /// Se testea el metodo Validar de la interfaz implementada tanto en personaje como en arma. Se crean tanto
        /// personajes válidos como inválidos y se esperan las repuestas del metodo implementado de la interfaz validar.
        /// Se verifica tanto el metodo validar de un arma como de un personaje. 
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoValidarDePersonajeInterfazImplementada()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Warrior", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Federico", 10, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            Personaje personaje5 = new Personaje("jsdfhkjlasdhjfajsdhfhjklasdfasdfhjk", 999999, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje6 = new Personaje("  ", -999999, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            Universo.listaPersonajesExistentes.Clear();

            Universo.listaPersonajesExistentes.Add(personaje1);
            Universo.listaPersonajesExistentes.Add(personaje2);
            Universo.listaPersonajesExistentes.Add(personaje3);
            Universo.listaPersonajesExistentes.Add(personaje4);

            bool validacionNvl1;
            bool validacionNvl1Arma;

            bool validacionNvl2;
            bool validacionNvl2Arma;

            bool validacionNvl3;
            bool validacionNvl3Arma;

            bool validacionNvl4;
            bool validacionNvl4Arma;

            bool validacionNvl5;
            bool validacionNvl5Arma;

            bool validacionNvl6;
            bool validacionNvl6Arma;

            //ACT
            validacionNvl1 = ((IValidar)personaje1).Validar(personaje1);
            validacionNvl1Arma = ((IValidar)personaje1.Arma).Validar(personaje1);

            validacionNvl2 = ((IValidar)personaje2).Validar(personaje2);
            validacionNvl2Arma = ((IValidar)personaje2.Arma).Validar(personaje2);

            validacionNvl3 = ((IValidar)personaje3).Validar(personaje3);
            validacionNvl3Arma = ((IValidar)personaje3.Arma).Validar(personaje3);

            validacionNvl4 = ((IValidar)personaje4).Validar(personaje4);
            validacionNvl4Arma = ((IValidar)personaje4.Arma).Validar(personaje4);

            validacionNvl5 = ((IValidar)personaje5).Validar(personaje5);
            validacionNvl5Arma = ((IValidar)personaje5.Arma).Validar(personaje5);

            validacionNvl6 = ((IValidar)personaje6).Validar(personaje6);
            validacionNvl6Arma = ((IValidar)personaje6.Arma).Validar(personaje6);

            //ASSERT
            Assert.AreEqual(true, validacionNvl1);
            Assert.AreEqual(true, validacionNvl1Arma);

            Assert.AreEqual(true, validacionNvl2);
            Assert.AreEqual(true, validacionNvl2Arma);

            Assert.AreEqual(true, validacionNvl3);
            Assert.AreEqual(true, validacionNvl3Arma);

            Assert.AreEqual(true, validacionNvl4);
            Assert.AreEqual(true, validacionNvl4Arma);

            //Los 2 que estan mal.
            Assert.AreEqual(false, validacionNvl5);
            Assert.AreEqual(true, validacionNvl5Arma);

            Assert.AreEqual(false, validacionNvl6);
            Assert.AreEqual(true, validacionNvl6Arma);

        }

        /// <summary>
        /// Se testea el metodo CalcularPoderTotal. Se ingresan personajes esperando un resultado PREDECIBLE. Como niveles mínimos,
        /// máximos, entre otros.
        /// </summary>
        [TestMethod]
        public void TesteoDelMetodoCalcularPoderTotal()
        {

            //VALOR MINIMO DE PUNTOS DE PODER TOTAL ES DE 1350 x 1 (NIVEL 0)
            //VALOR MAXIMO DE PUNTOS DE PODER TOTAL ES DE 1350 x 2 = 2700 (NIVEL 100)

            //ARRANGE
            int valorMinimoPoder = 1350;
            int valorMaximoPoder = 2700;

            Personaje personaje1 = new Personaje("soynivelminimo",0, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("soynivelmaximo",100, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Warrior",50, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.BastonMagico);
            Personaje personaje4 = new Personaje("Federico",32, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);
            Personaje personaje5 = new Personaje("Federiquito",31, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            int validacionPj1;
            int validacionPj2;
            int validacionPj3;
            int validacionPj4;
            int validacionPj5;

            //ACT
            validacionPj1 = Personaje.calcularPoderTotal(personaje1);
            validacionPj2 = Personaje.calcularPoderTotal(personaje2);
            validacionPj3 = Personaje.calcularPoderTotal(personaje3);
            validacionPj4 = Personaje.calcularPoderTotal(personaje4);
            validacionPj5 = Personaje.calcularPoderTotal(personaje5);

            //ASSERT
            Assert.AreEqual(valorMinimoPoder, validacionPj1);
            Assert.AreEqual(valorMaximoPoder, validacionPj2);
            Assert.IsTrue(validacionPj3 > valorMinimoPoder && validacionPj3 < valorMaximoPoder);
            Assert.IsTrue(validacionPj4 > valorMinimoPoder && validacionPj4 < valorMaximoPoder);
            Assert.IsTrue(validacionPj4 > validacionPj5);
        }
    }
}

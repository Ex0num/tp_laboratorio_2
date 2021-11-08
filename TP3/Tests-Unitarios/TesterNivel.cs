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
    public class TesterNivel
    {
        /// <summary>
        /// Se testean distintos valores que se esperarían ser VALIDOS de niveles de personajes.
        /// </summary>
        [TestMethod]
        public void TesteoDeNivelesValidosDePjs()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso",100, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin",0, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Federico",50, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            bool validacionNvl1;
            bool validacionNvl2;
            bool validacionNvl3;
            bool validacionNvl4;
            bool validacionNvl5;
            bool validacionNvl6;
            bool validacionNvl7;
            bool validacionNvl8;
            bool validacionNvl9;

            //ACT
            validacionNvl1 = Personaje.isValidNivelTotalPersonaje(0);
            validacionNvl2 = Personaje.isValidNivelTotalPersonaje(100);
            validacionNvl3 = Personaje.isValidNivelTotalPersonaje(99);
            validacionNvl4 = Personaje.isValidNivelTotalPersonaje(55);
            validacionNvl5 = Personaje.isValidNivelTotalPersonaje(1);
            validacionNvl6 = Personaje.isValidNivelTotalPersonaje((int)99.9);
            validacionNvl7 = Personaje.isValidNivelTotalPersonaje(personaje1.NivelTotal);
            validacionNvl8 = Personaje.isValidNivelTotalPersonaje(personaje2.NivelTotal);
            validacionNvl9 = Personaje.isValidNivelTotalPersonaje(personaje3.NivelTotal);

            //ASSERT
            Assert.AreEqual(true, validacionNvl1);
            Assert.AreEqual(true, validacionNvl2);
            Assert.AreEqual(true, validacionNvl3);
            Assert.AreEqual(true, validacionNvl4);
            Assert.AreEqual(true, validacionNvl5);
            Assert.AreEqual(true, validacionNvl6);
            Assert.AreEqual(true, validacionNvl7);
            Assert.AreEqual(true, validacionNvl8);
            Assert.AreEqual(true, validacionNvl9);

        }

        /// <summary>
        /// Se testean distintos valores que se esperarían ser INVALIDOS de niveles de personajes.
        /// </summary>
        [TestMethod]
        public void TesteoDeNivelesInvalidosDePjs()
        {
            //ARRANGE
            bool validacionNvl1;
            bool validacionNvl2;
            bool validacionNvl3;
            bool validacionNvl4;
            bool validacionNvl5;
            bool validacionNvl6;

            //ACT
            validacionNvl1 = Personaje.isValidNivelTotalPersonaje(-10);
            validacionNvl2 = Personaje.isValidNivelTotalPersonaje(101);
            validacionNvl3 = Personaje.isValidNivelTotalPersonaje(100000);
            validacionNvl4 = Personaje.isValidNivelTotalPersonaje(-05);
            validacionNvl5 = Personaje.isValidNivelTotalPersonaje(10000);
            validacionNvl6 = Personaje.isValidNivelTotalPersonaje(int.MinValue);

            //ASSERT
            Assert.AreEqual(false, validacionNvl1);
            Assert.AreEqual(false, validacionNvl2);
            Assert.AreEqual(false, validacionNvl3);
            Assert.AreEqual(false, validacionNvl4);
            Assert.AreEqual(false, validacionNvl5);
            Assert.AreEqual(false, validacionNvl6);

        }
    }
}

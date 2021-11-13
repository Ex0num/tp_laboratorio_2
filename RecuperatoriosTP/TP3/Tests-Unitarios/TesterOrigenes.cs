using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Tests_Unitarios
{
    [TestClass]
    public class TesterOrigenes
    {
        /// <summary>
        /// Se testean distintos origenes posibles de personajes que se esperarían ser VALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoDeOrigenesPosibles()
        {

            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 10, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            bool validacionPj1;
            bool validacionPj2;
            bool validacionPj3;

            //ACT
            validacionPj1 = Personaje.isValidOrigenElemental(personaje1.OrigenElemental);
            validacionPj2 = Personaje.isValidOrigenElemental(personaje2.OrigenElemental);
            validacionPj3 = Personaje.isValidOrigenElemental(Personaje.enumOrigenElemental.Agua);

            //ASSERT
            Assert.AreEqual(true, validacionPj1);
            Assert.AreEqual(true, validacionPj2);
            Assert.AreEqual(true, validacionPj3);
        }
    }
}

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
    class TesterIDS
    {
        /// <summary>
        /// Testea valores validos de ID
        /// </summary>
        [TestMethod]
        public void TesteoDeValoresValidosIDS()
        {
            //PRIMER VALOR DEL ID: 1


            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 100, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 0, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Federico", 50, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            //ACT

            //ASSERT
            Assert.AreEqual(1, personaje1.IdPersonaje);
            Assert.AreEqual(2, personaje2.IdPersonaje);
            Assert.AreEqual(3, personaje3.IdPersonaje);

        }

        /// <summary>
        /// Testea varios valores invalidos de ID
        /// </summary>
        [TestMethod]
        public void TesteoDeValoresInvalidosIDS()
        {
            //PRIMER VALOR DEL ID: 1

            
            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 100, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 0, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);
            Personaje personaje3 = new Personaje("Federico", 50, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            //ACT

            //ASSERT
            Assert.IsFalse(personaje1.IdPersonaje == 0);
            Assert.IsFalse(personaje2.IdPersonaje.ToString() == "A");
            Assert.IsTrue(personaje3.IdPersonaje.GetType() == typeof(int));
        }
    }

}

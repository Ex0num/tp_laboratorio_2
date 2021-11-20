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
    public class TesterTipoArma
    {
        /// <summary>
        /// Se testean distintos valores de tipo de armas de personaje que se esperarían ser VALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoTipoArma()
        {
            //ARRANGE
            Arma arma1 = new Arma(Arma.enumTipoArma.Arco);
            Arma arma2 = new Arma(Arma.enumTipoArma.BastonMagico);
            Arma arma3 = new Arma(Arma.enumTipoArma.Escudo);

            bool validacionArma1;
            bool validacionArma2;
            bool validacionArma3;

            //ACT
            validacionArma1 = Arma.isValidTipoArma(arma1.TipoArma);
            validacionArma2 = Arma.isValidTipoArma(arma2.TipoArma);
            validacionArma3 = Arma.isValidTipoArma(arma3.TipoArma);

            //ASSERT
            Assert.AreEqual(true, validacionArma1);       
            Assert.AreEqual(true, validacionArma2);      
            Assert.AreEqual(true, validacionArma3);      
        }
    }
}

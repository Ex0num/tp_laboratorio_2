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
    public class TesterPtsAtaqueYDefensaArmaYConstructor
    {
        /// <summary>
        /// Se testean distintos valores de puntos de ataque de un arma de personaje que se esperarían ser VALIDOS.
        /// </summary>
        [TestMethod] 
        public void TesteoPtsAtaqueDeUnArma()
        {
            //VALOR MINIMO DE PUNTOS DE ATAQUE DE UN ARMA ES DE 350 (ESCUDO)
            //VALOR MAXIMO DE PUNTOS DE ATAQUE DE UN ARMA ES DE 1000 (BASTONMAGICO)

            //ARRANGE
            int valorMinimoAtaque = 350;
            int valorMaximoAtaque = 1000;

            Personaje personaje1 = new Personaje("Alfonso", 100, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);
            Personaje personaje2 = new Personaje("Martin", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje3 = new Personaje("Warrior", 0, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje4 = new Personaje("Federico", 66, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            int validacionPj1;
            int validacionPj2;
            int validacionPj3;
            int validacionPj4;

            //ACT
            validacionPj1 = personaje1.Arma.PtsAtaque;
            validacionPj2 = personaje2.Arma.PtsAtaque;
            validacionPj3 = personaje3.Arma.PtsAtaque;
            validacionPj4 = personaje4.Arma.PtsAtaque;

            //ASSERT
            Assert.AreEqual(valorMinimoAtaque, validacionPj1);
            Assert.AreEqual(valorMaximoAtaque, validacionPj2);
            Assert.IsTrue(validacionPj3 > valorMinimoAtaque && validacionPj3 < valorMaximoAtaque);
            Assert.IsTrue(validacionPj4 > valorMinimoAtaque && validacionPj4 < valorMaximoAtaque);
        }

        /// <summary>
        /// Se testean distintos valores de puntos de ataque de un arma de personaje que se esperarían ser INVALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoPtsAtaqueInvalidosDeUnArma()
        {
            //VALOR MINIMO DE PUNTOS DE ATAQUE DE UN ARMA ES DE 350 (ESCUDO)
            //VALOR MAXIMO DE PUNTOS DE ATAQUE DE UN ARMA ES DE 1000 (BASTONMAGICO)

            //ARRANGE
            bool validacion1;
            bool validacion2;
            bool validacion3;
            bool validacion4;


            int valorInvalido1;
            int valorInvalido2;
            int valorInvalido3;
            int valorInvalido4;

            valorInvalido1 = 1100;
            valorInvalido2 = 200;
            valorInvalido3 = 30;
            valorInvalido4 = 3000;

            //ACT
            validacion1 = Arma.isValidPtsAtaqueArma(valorInvalido1);
            validacion2 = Arma.isValidPtsAtaqueArma(valorInvalido2);
            validacion3 = Arma.isValidPtsAtaqueArma(valorInvalido3);
            validacion4 = Arma.isValidPtsAtaqueArma(valorInvalido4);

            //ASSERT
            Assert.AreEqual(false,validacion1);
            Assert.AreEqual(false,validacion2);
            Assert.AreEqual(false,validacion3);
            Assert.AreEqual(false,validacion4);
        }

        /// <summary>
        /// Se testean distintos valores de puntos de defensa de un arma de personaje que se esperarían ser VALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoPtsDefensaDeUnArma()
        {

            //VALOR MINIMO DE PUNTOS DE DEFENSA DE UN ARMA ES DE 350 (BASTONMAGICO)
            //VALOR MAXIMO DE PUNTOS DE DEFENSA DE UN ARMA ES DE 1000 (ESCUDO)

            //ARRANGE
            int valorMinimoDefensa = 350;
            int valorMaximoDefensa = 1000;

            Personaje personaje1 = new Personaje("Alfonso", 100, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);
            Personaje personaje2 = new Personaje("Martin", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
            Personaje personaje3 = new Personaje("Warrior", 0, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje4 = new Personaje("Federico", 66, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Arco);

            int validacionPj1;
            int validacionPj2;
            int validacionPj3;
            int validacionPj4;

            //ACT
            validacionPj1 = personaje1.Arma.PtsDefensa;
            validacionPj2 = personaje2.Arma.PtsDefensa;
            validacionPj3 = personaje3.Arma.PtsDefensa;
            validacionPj4 = personaje4.Arma.PtsDefensa;

            //ASSERT
            Assert.AreEqual(valorMaximoDefensa, validacionPj1);
            Assert.AreEqual(valorMinimoDefensa, validacionPj2);
            Assert.IsTrue(validacionPj3 > valorMinimoDefensa && validacionPj3 < valorMaximoDefensa);
            Assert.IsTrue(validacionPj4 > valorMinimoDefensa && validacionPj4 < valorMaximoDefensa);
        }

        /// <summary>
        /// Se testean distintos valores de puntos de defensa de un arma de personaje que se esperarían ser INVALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoPtsDefensaInvalidosDeUnArma()
        {
            //VALOR MINIMO DE PUNTOS DE DEFENSA DE UN ARMA ES DE 350 (BASTONMAGICO)
            //VALOR MAXIMO DE PUNTOS DE DEFENSA DE UN ARMA ES DE 1000 (ESCUDO)

            //ARRANGE
            bool validacion1;
            bool validacion2;
            bool validacion3;
            bool validacion4;

            int valorInvalido1;
            int valorInvalido2;
            int valorInvalido3;
            int valorInvalido4;

            valorInvalido1 = 1100;
            valorInvalido2 = 200;
            valorInvalido3 = 30;
            valorInvalido4 = 3000;

            //ACT
            validacion1 = Arma.isValidPtsDefensaArma(valorInvalido1);
            validacion2 = Arma.isValidPtsDefensaArma(valorInvalido2);
            validacion3 = Arma.isValidPtsDefensaArma(valorInvalido3);
            validacion4 = Arma.isValidPtsDefensaArma(valorInvalido4);

            //ASSERT
            Assert.AreEqual(false, validacion1);
            Assert.AreEqual(false, validacion2);
            Assert.AreEqual(false, validacion3);
            Assert.AreEqual(false, validacion4);
        }

        /// <summary>
        /// Se testea el constructor vacío de un arma.
        /// </summary>
        [TestMethod]
        public void TesteoConstructorVacioArma()
        {
            //ARRANGE
            Arma armaVacia = new Arma();

            //ASSERT
            Assert.IsTrue(armaVacia != null);
        }

    }

}

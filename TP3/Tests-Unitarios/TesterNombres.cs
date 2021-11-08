using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace Tests_Unitarios
{
    [TestClass]
    public class TesterNombres
    {
        /// <summary>
        /// Se testean distintos nombres de personajes que se esperarían ser VALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoDeNombresValidosDePjs()
        {

            //ARRANGE
            bool validacionPj1;
            bool validacionPj2;
            bool validacionPj3;
            bool validacionPj4;
            bool validacionPj5;
            bool validacionPj6;

            //ACT
            validacionPj1 = Personaje.isValidNombrePersonaje("Oryx");
            validacionPj2 = Personaje.isValidNombrePersonaje("123");
            validacionPj3 = Personaje.isValidNombrePersonaje("WARRIOR");
            validacionPj4 = Personaje.isValidNombrePersonaje("dragon");
            validacionPj5 = Personaje.isValidNombrePersonaje("ProTectOr123");
            validacionPj6 = Personaje.isValidNombrePersonaje("a");

            //ASSERT
            Assert.AreEqual(true, validacionPj1);
            Assert.AreEqual(true, validacionPj2);
            Assert.AreEqual(true, validacionPj3);
            Assert.AreEqual(true, validacionPj4);
            Assert.AreEqual(true, validacionPj5);
            Assert.AreEqual(true, validacionPj6);
        }

        /// <summary>
        /// Se testean distintos nombres de personajes que se esperarían ser INVALIDOS.
        /// </summary>
        [TestMethod]
        public void TesteoDeNombresInvalidosDePjs()
        {

            //ARRANGE
            string nombre1 = string.Empty;
            string nombre2 = null;
            string nombre3 = "     ";
            string nombre4 = "SuperCaballerorecontrerefuerte123123anajaasd";

            bool validacionNombre1;
            bool validacionNombre2;
            bool validacionNombre3;
            bool validacionNombre4;

            //ACT
            validacionNombre1 = Personaje.isValidNombrePersonaje(nombre1);
            validacionNombre2 = Personaje.isValidNombrePersonaje(nombre2);
            validacionNombre3 = Personaje.isValidNombrePersonaje(nombre3);
            validacionNombre4 = Personaje.isValidNombrePersonaje(nombre4);
           
            //ASSERT
            Assert.AreEqual(false, validacionNombre1);
            Assert.AreEqual(false, validacionNombre2);
            Assert.AreEqual(false, validacionNombre3);
            Assert.AreEqual(false, validacionNombre4);
        }
    }
}

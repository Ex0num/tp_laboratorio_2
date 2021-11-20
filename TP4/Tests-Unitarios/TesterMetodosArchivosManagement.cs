using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_Unitarios
{
    [TestClass]
    public class TesterMetodosArchivosManagement
    {

        //------------------------------ESCRITURA Y LECTURA XML-----------------------------
        /// <summary>
        /// Testea la escritura y lectura de un archivo XML
        /// </summary>
        [TestMethod]
        public void TesteoDeEscrituraYLecturaXML()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Alfonso", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Martin", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            string nombreDirectorio;
            nombreDirectorio = Path.Combine("Archivos-TP3-LopezGasal", "Unitesting-Files");

            string nombreArchivo;
            nombreArchivo = "TestingArchivo.xml";

            Universo.listaPersonajesExistentesClonada.Clear();

            Universo.listaPersonajesExistentes.Add(personaje1);
            Universo.listaPersonajesExistentes.Add(personaje2);

            //ACT
            ArchivosManagement.EscribirArchivoSerializacionXML<List<Personaje>>(Universo.listaPersonajesExistentes, nombreDirectorio, nombreArchivo);

            Universo.listaPersonajesExistentes.Clear();

            Universo.listaPersonajesExistentes = ArchivosManagement.LeerArchivoSerializacionXML<List<Personaje>>(nombreDirectorio, nombreArchivo);

            //ASSERT
            Assert.IsTrue(Universo.listaPersonajesExistentes.Count > 0);
            Assert.IsTrue(Universo.listaPersonajesExistentes[0] != null);

        }

        //------------------------------ESCRITURA Y LECTURA JSON-----------------------------

        /// <summary>
        /// Testea la lectura y lectura de un archivo JSON
        /// </summary>
        [TestMethod]
        public void TesteoDeEscrituraYLecturaJSON()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Federico", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Gabriel", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            string nombreDirectorio;
            nombreDirectorio = Path.Combine("Archivos-TP3-LopezGasal", "Unitesting-Files");

            string nombreArchivo;
            nombreArchivo = "TestingArchivo.json";

            Universo.listaPersonajesExistentesClonada.Clear();

            Universo.listaPersonajesExistentes.Add(personaje1);
            Universo.listaPersonajesExistentes.Add(personaje2);

            //ACT
            ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, nombreDirectorio, nombreArchivo);

            Universo.listaPersonajesExistentes.Clear();

            Universo.listaPersonajesExistentes = ArchivosManagement.LeerArchivoSerializacionJSON<List<Personaje>>(nombreDirectorio, nombreArchivo);

            //ASSERT
            Assert.IsTrue(Universo.listaPersonajesExistentes.Count > 0);
            Assert.IsTrue(Universo.listaPersonajesExistentes[0] != null);
        }

        //------------------------------ESCRITURA Y LECTURA ARCHIVO TXT-----------------------------
        /// <summary>
        /// Testea la escritura y lectura de unarchivo TXT
        /// </summary>
        [TestMethod]
        public void TesteoDeEscrituraYLecturaTXT()
        {
            //ARRANGE
            Personaje personaje1 = new Personaje("Federico", 10, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
            Personaje personaje2 = new Personaje("Gabriel", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Escudo);

            string nombreDirectorio;
            nombreDirectorio = Path.Combine("Archivos-TP3-LopezGasal", "Unitesting-Files");

            string nombreArchivo;
            nombreArchivo = "TestingArchivo.txt";

            string informacionMandada = "Hola mundo";
            string informacionLeida = string.Empty;

            //ACT
            ArchivosManagement.EscribirArchivoTXT(informacionMandada, false, nombreDirectorio, nombreArchivo);
             
            ArchivosManagement.LeerArchivoTXT(out informacionLeida, nombreDirectorio, nombreArchivo);

            //ASSERT
            Assert.IsTrue(informacionLeida.Length > 1);
            Assert.IsTrue(informacionLeida == informacionMandada);
        }

        /// <summary>
        /// Testea el metodo GenerarNombreHoraMntsConExtension, verificando que devuelva un valor coherente y o esperado.
        /// </summary>
        [TestMethod]
        public void TesteoDeMetodoGenerarNombreFechaHoraMntsConExtension()
        {
            //ARRANGE
            string nombreArchivo = "TestingName";

            string extensionXML = ".xml";
            string extensionJSON = ".json";
            string extensionTXT = ".txt";

            string nombreGenerado1;
            string nombreGenerado2;
            string nombreGenerado3;

            //ACT
            nombreGenerado1 = ArchivosManagement.GenerarNombreFechaHoraMntsConExtension(nombreArchivo,extensionXML);
            nombreGenerado2 = ArchivosManagement.GenerarNombreFechaHoraMntsConExtension(nombreArchivo,extensionJSON);
            nombreGenerado3 = ArchivosManagement.GenerarNombreFechaHoraMntsConExtension(nombreArchivo,extensionTXT);

            //ASSERT
            Assert.IsTrue(nombreGenerado1.Contains(".xml") == true && nombreGenerado1.Length > 6 && nombreGenerado1.Contains("TestingName") == true);
            Assert.IsTrue(nombreGenerado2.Contains(".json") == true && nombreGenerado2.Length > 6 && nombreGenerado2.Contains("TestingName") == true);
            Assert.IsTrue(nombreGenerado3.Contains(".txt") == true && nombreGenerado3.Length > 6 && nombreGenerado3.Contains("TestingName") == true);

        }
      
    }
}

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.IO;
using System.Xml.Serialization;
using System.Text.Json;

namespace Entidades
{
    public static class ArchivosManagement
    {

        //------------------------------ESCRITURA Y LECTURA XML-------------------------------------------------
        /// <summary>
        /// Recibe un objeto generico, un nombre de directorio y de archivo. Escribe en la carpeta del usuario
        /// Creando una carpeta si no existe y el archivo con la informacion del objeto como tal. (En formato XML).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto">Objeto generico</param>
        /// <param name="nombreDirDestino">Nombre del directorio donde se va a guardar el nombre del archivo</param>
        /// <param name="nombreRecibidoArchivo">Nombre del archivo a escribir</param>
        public static void EscribirArchivoSerializacionXML<T>(T objeto, string nombreDirDestino, string nombreRecibidoArchivo) where T : class
        {
            //Si el nombre de la carpeta no es nulo ni vacio y el nombre del archivo tampoco.
            if (string.IsNullOrEmpty(nombreDirDestino) == false && string.IsNullOrEmpty(nombreRecibidoArchivo) == false)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, nombreDirDestino);

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, nombreRecibidoArchivo);

                using (StreamWriter escritor = new StreamWriter(path))
                {
                    XmlSerializer serializadorXML = new XmlSerializer(typeof(T));

                    serializadorXML.Serialize(escritor, objeto);
                }
            }

        }

        /// <summary>
        /// Recibe un tipo generico que será el que devolverá al realizar la lectura del directorio que contiene el nombre
        /// del archivo ingresado. (La lectura del archivo es en formato XML).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directorioDelDestino">Nombre del directorio donde se va a leer el archivo</param>
        /// <param name="nombreRecibidoArchivo">Nombre del archivo a leer</param>
        /// <returns>Retorna el objeto leído</returns>
        public static T LeerArchivoSerializacionXML<T>(string directorioDelDestino, string nombreRecibidoArchivo) where T : class
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, directorioDelDestino);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, nombreRecibidoArchivo);

            using (StreamReader lector = new StreamReader(path))
            {
                XmlSerializer serializadorXML = new XmlSerializer(typeof(T));

                T objeto = serializadorXML.Deserialize(lector) as T;
                return objeto;
            }
        }

        //------------------------------ESCRITURA Y LECTURA JSON-------------------------------------------------
        /// <summary>
        /// Recibe un objeto generico, un nombre de directorio y de archivo. Escribe en la carpeta del usuario
        /// Creando una carpeta si no existe y el archivo con la informacion del objeto como tal. (En formato JSON).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto">Objeto generico</param>
        /// <param name="nombreDir">Nombre del directorio donde se va a guardar el nombre del archivo</param>
        /// <param name="nombreArchivoConExtension">Nombre del archivo a escribir</param>
        public static void EscribirArchivoSerializacionJSON<T>(T objeto, string nombreDir, string nombreArchivoConExtension) where T : class
        {
            //Si el nombre de la carpeta no es nulo ni vacio y el nombre del archivo tampoco.
            if (string.IsNullOrEmpty(nombreDir) == false && string.IsNullOrEmpty(nombreArchivoConExtension) == false)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, nombreDir); //"Archivos-TP3-LopezGasal"

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, nombreArchivoConExtension); //"ArchivoPersonajes.json"

                using (StreamWriter escritor = new StreamWriter(path))
                {
                    JsonSerializerOptions opcionesJSON = new JsonSerializerOptions();
                    opcionesJSON.WriteIndented = true;

                    string jsonString = JsonSerializer.Serialize(objeto, opcionesJSON);

                    escritor.Write(jsonString);
                }
            }

        }

        /// <summary>
        /// Recibe un tipo generico que será el que devolverá al realizar la lectura del directorio que contiene el nombre
        /// del archivo ingresado. (La lectura del archivo es en formato JSON).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nombreDir"></param>
        /// <param name="nombreArchivoConExtension"></param>
        /// <returns></returns>
        public static T LeerArchivoSerializacionJSON<T>(string nombreDir, string nombreArchivoConExtension) where T : class
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, nombreDir); //"Archivos-TP3-LopezGasal"

            if (Directory.Exists(path) == false)
            { 
               Directory.CreateDirectory(path);
            }
   
            path = Path.Combine(path, nombreArchivoConExtension); //"ArchivoPersonajes.json"

            if (File.Exists(path) == false)
            {
                ArchivosManagement.EscribirArchivoSerializacionJSON(Universo.listaPersonajesExistentes, nombreDir, nombreArchivoConExtension);
            }

            using (StreamReader lector = new StreamReader(path))
            {
                string jsonString = lector.ReadToEnd();

                T objeto = JsonSerializer.Deserialize<T>(jsonString);
                return objeto;
            }
        }

        //------------------------------ESCRITURA Y LECTURA ARCHIVO TXT--------------------------------------------
        /// <summary>
        /// Recibe la informacion a escribir en el archivo txt, una opcion de append, el nombre del directorio 
        /// y el nombre del archivo del cual se encontrará dentro del directorio. (Se escribirá en formato TXT).
        /// </summary>
        /// <param name="informacion">Informacion a escribir</param>
        /// <param name="append">Opcion para agregar o no informacion</param>
        /// <param name="nombreDir">Nombre del directorio que contendrá al archivo TXT</param>
        /// <param name="nombreArchivo">Nombre del archivo que contendrá la informacion escrita</param>
        public static void EscribirArchivoTXT(string informacion, bool append, string nombreDir, string nombreArchivo)
        {
            //Si el nombre de la carpeta no es nulo ni vacio y el nombre del archivo tampoco.
            if (string.IsNullOrEmpty(nombreDir) == false && string.IsNullOrEmpty(nombreArchivo) == false)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, nombreDir);

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, nombreArchivo);

                using (StreamWriter escritor = new StreamWriter(path, append))
                {
                    escritor.Write(informacion);
                }

            }  

        }

        /// <summary>
        /// Recibe una variable donde será cargada la informacion leída que el nombre del directorio y del archivo indicarán.
        /// (La lectura será de un archivo en formato TXT).
        /// </summary>
        /// <param name="informacionObtenida">Informaciín leída</param>
        /// <param name="nombreDir">Nombre del directorio que contendrá al archivo TXT</param>
        /// <param name="nombreArchivo">Nombre del archivo que contendrá la informacion leída</param>
        public static void LeerArchivoTXT(out string informacionObtenida, string nombreDir, string nombreArchivo)
        {
            informacionObtenida = string.Empty;

            //Si el nombre de la carpeta no es nulo ni vacio y el nombre del archivo tampoco.
            if (string.IsNullOrEmpty(nombreDir) == false && string.IsNullOrEmpty(nombreArchivo) == false)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, nombreDir);

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, nombreArchivo);

                using (StreamReader lector = new StreamReader(path))
                {
                    informacionObtenida = lector.ReadToEnd();
                }

            }
        }

        /// <summary>
        /// Recibe un nombre de archivo y una extension. Por ej: ".xml". Este metodo se ocupara de concatenar
        /// el nombre del archivo con la hora, minutos y segundos del actuales (cuando sea llamado el método) y
        /// concatenará la extension al nombre del archivo con la hora, siendo un nombre valido y único para el
        /// archivo.
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="extension"></param>
        /// <returns>Retorna el nombre generado</returns>
        public static string GenerarNombreFechaHoraMntsConExtension(string nombreArchivo, string extension)
        { 
            string nombre = nombreArchivo + " " + DateTime.Now.ToString("HH_mm_ss") + extension;
            return nombre;
        }

    }
}

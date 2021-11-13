using System;
using System.Collections.Generic;
using System.IO;
using Entidades;
using Excepciones;

namespace Test
{
    class ConsolaTesteo
    {
        static void Main(string[] args)
        {
            try
            {
                //Creo un personaje.
                Personaje nuevoPersonaje = new Personaje("Federico", 100, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);
                Console.WriteLine("----------- CREACION DE UN PERSONAJE HARDCODEADO ------------- \n");

                Console.WriteLine
                    ($" Nombre: {nuevoPersonaje.NombrePersonaje} " +
                    $"\n Origen elemental: {nuevoPersonaje.OrigenElemental} " +
                    $"\n Nivel: {nuevoPersonaje.NivelTotal} " +
                    $"\n Arma: {nuevoPersonaje.Arma.TipoArma} " +
                    $"\n PtsAtaque: {nuevoPersonaje.Arma.PtsAtaque} " +
                    $"\n PtsDefensa: {nuevoPersonaje.Arma.PtsDefensa} " +
                    $"\n ID: {nuevoPersonaje.IdPersonaje}\n");

                //Valido el personaje con mi INTERFAZ validadora.
                bool resultadoValidacion;

                resultadoValidacion = ((IValidar)nuevoPersonaje).Validar(nuevoPersonaje);

             
                if (resultadoValidacion == true) //Si el personaje es valido.
                {
                    Console.WriteLine("----------- PERSONAJE VALIDADO GRACIAS A LA INTERFAZ VALIDADORA DEL MISMO ------------- \n");

                    Console.WriteLine("Presione para continuar\n");
                    Console.ReadKey();

                    //Lo agrego a la lista
                    Universo.listaPersonajesExistentes.Add(nuevoPersonaje);
                    Console.WriteLine($"\nLa cantidad de personajes cargados en la lista es de: {Universo.listaPersonajesExistentes.Count}\n");

                    //Este va a ser el nombre de mi directorio para todos los archivos de la ConsolaTesteo.
                    string nombreDirectorio;
                    nombreDirectorio = Path.Combine("Archivos-TP3-LopezGasal", "ConsolaTesting-Files");

                    // ------------- Voy a guardarlo y serializarlo de forma XML. ----------------------------
                    Console.WriteLine("----------- GUARDADO Y LECTURA XML ------------- \n");

                    Console.WriteLine("Presione para continuar\n");
                    Console.ReadKey();

                    string nombreArchivoXML;
                    nombreArchivoXML = "ConsolaTestingArchivo.xml";

                    //Escribo la lista en el archivo XML.
                    //Dandole el tipo al parametro GENERICO de los métodos.
                    ArchivosManagement.EscribirArchivoSerializacionXML<List<Personaje>>(Universo.listaPersonajesExistentes,nombreDirectorio,nombreArchivoXML);
                    Console.WriteLine("\nLista de personajes guardada en el archivo XML.\n");

                    //Limpio la lista para perder el personaje de la memoria.
                    Universo.listaPersonajesExistentes.Clear();

                    Console.WriteLine("Lista limpiada en su totalidad.\n");
                    Console.WriteLine($"La cantidad de personajes cargados en la lista es de: {Universo.listaPersonajesExistentes.Count} \n");
                    Console.WriteLine("Lista cargada con el contenido leido del archivo XML \n");

                    //Cargo la lista con el contenido leido y deserealizado del archivo XML
                    Universo.listaPersonajesExistentes = ArchivosManagement.LeerArchivoSerializacionXML<List<Personaje>>(nombreDirectorio,nombreArchivoXML);
                    Console.WriteLine($"La cantidad de personajes cargados en la lista es de: {Universo.listaPersonajesExistentes.Count} \n");

                    // ------------- Voy a guardarlo y serializarlo de forma JSON. ----------------------------
                    Console.WriteLine("----------- GUARDADO Y LECTURA JSON ------------- \n");

                    Console.WriteLine("Presione para continuar\n");
                    Console.ReadKey();

                    string nombreArchivoJSON;
                    nombreArchivoJSON = "ConsolaTestingArchivo.json";

                    //Escribo la lista en el archivo JSON.              
                    //Dandole el tipo al parametro GENERICO de los métodos.
                    ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, nombreDirectorio, nombreArchivoJSON);
                    Console.WriteLine("\nLista de personajes guardada en el archivo JSON. \n");

                    //Limpio la lista para perder el personaje de la memoria.
                    Universo.listaPersonajesExistentes.Clear();

                    Console.WriteLine("Lista limpiada en su totalidad. \n");
                    Console.WriteLine($"La cantidad de personajes cargados en la lista es de: {Universo.listaPersonajesExistentes.Count} \n");
                    Console.WriteLine("Lista cargada con el contenido leido del archivo JSON \n");

                    //Cargo la lista con el contenido leido y deserealizado del archivo JSON
                    Universo.listaPersonajesExistentes = ArchivosManagement.LeerArchivoSerializacionJSON<List<Personaje>>(nombreDirectorio, nombreArchivoJSON);
                    Console.WriteLine($"La cantidad de personajes cargados en la lista es de: {Universo.listaPersonajesExistentes.Count} \n");

                    // ------------- Voy a guardar en forma de TXT. ----------------------------
                    Console.WriteLine("----------- GUARDADO Y LECTURA TXT ------------- \n");

                    Console.WriteLine("Presione para continuar\n");
                    Console.ReadKey();

                    string nombreArchivoTXT;
                    nombreArchivoTXT = "ConsolaTestingArchivo.txt";
   
                    string informacionAEscribir = "Afortunadamente la lectura de los archivos XML y JSON salió perfecta. Solo queda TXT.";
                    string informacionLeida = string.Empty;

                    Console.WriteLine($"\nLa información a escribir en el TXT es: \n\n{informacionAEscribir}\n");

                    ArchivosManagement.EscribirArchivoTXT(informacionAEscribir, false, nombreDirectorio, nombreArchivoTXT);
                    Console.WriteLine($"La información fue escrita. \n");

                    ArchivosManagement.LeerArchivoTXT(out informacionLeida, nombreDirectorio, nombreArchivoTXT);

                    Console.WriteLine($"La información leida del TXT es: \n\n{informacionLeida}\n\n");

                    //Creo un 2do personaje.
                    Personaje nuevoPersonaje2 = new Personaje("Gabriel", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.Arco);
                    Console.WriteLine("----------- CREACION DE UN 2DO PERSONAJE HARDCODEADO ------------- \n");

                    Console.WriteLine
                    ($" Nombre: {nuevoPersonaje2.NombrePersonaje} " +
                    $"\n Origen elemental: {nuevoPersonaje2.OrigenElemental} " +
                    $"\n Nivel: {nuevoPersonaje2.NivelTotal} " +
                    $"\n Arma: {nuevoPersonaje2.Arma.TipoArma} " +
                    $"\n PtsAtaque: {nuevoPersonaje2.Arma.PtsAtaque} " +
                    $"\n PtsDefensa: {nuevoPersonaje2.Arma.PtsDefensa} " +
                    $"\n ID: {nuevoPersonaje2.IdPersonaje}\n");

                    //Valido el personaje con mi INTERFAZ validadora.
                    bool resultadoValidacion2;

                    resultadoValidacion2 = ((IValidar)nuevoPersonaje).Validar(nuevoPersonaje);

                    if (resultadoValidacion2 == true)
                    {
                        Console.WriteLine("- PERSONAJE VALIDADO GRACIAS A LA INTERFAZ VALIDADORA DEL MISMO - \n");

                        // ------------- Voy a llamar a mi funcionalidad principal, enfrentamiento. ----------------------------
                        Console.WriteLine("----------- ENFRENTAMIENTO ENTRE LOS 2 PERSONAJES ------------- \n");

                        Console.WriteLine("Presione para continuar\n");
                        Console.ReadKey();

                        int resultadoBatalla;
                        resultadoBatalla = Universo.Enfrentamiento(nuevoPersonaje, nuevoPersonaje2);

                        switch (resultadoBatalla)
                        {
                            case 1:
                            {
                                Console.WriteLine($"\nEl ganador es {nuevoPersonaje.NombrePersonaje}\n");
                                break;
                             }
                            case 2:
                            {
                                Console.WriteLine($"\nEl ganador es {nuevoPersonaje2.NombrePersonaje}\n");
                                break;
                            }
                            case 0:
                            {
                                Console.WriteLine($"\nHubo un empate en el enfrentamiento entre {nuevoPersonaje.NombrePersonaje} y {nuevoPersonaje2.NombrePersonaje}\n");
                                break;
                            }
                            default:
                            {
                                throw new Exception();
                            }
                        }
                    }
                    else //Si es invalido
                    {
                        //Tiro una EXCEPCION con el mensaje de error.
                        throw new ExceptionInvalidInformation("Hubo un error en los datos del personaje \n");
                    }


                }
                else //Si es invalido
                {
                    //Tiro una EXCEPCION con el mensaje de error.
                    throw new ExceptionInvalidInformation("Hubo un error en los datos del personaje \n");
                }
            }
            catch (Exception excepcionCapturada)
            {
                if (excepcionCapturada.GetType() == typeof(Exception))
                {
                    Console.WriteLine("Ocurrio una excepcion inesperada.");
                }
                else
                {
                    Console.WriteLine(excepcionCapturada.Message);
                }
                
            }
           

        }
    }
}

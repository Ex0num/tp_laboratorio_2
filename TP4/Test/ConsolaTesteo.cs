using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Entidades;
using Excepciones;

namespace Test
{
    class ConsolaTesteo
    {
        static bool flag = false;

        static void Main(string[] args)
        {
           
            try
            {
                //VERIFICO QUE ONDA LA DB Y LOS ARCHIVOS.
                HardcodearPersonajesVerificandoExistenciaDByArchivos();

                Console.WriteLine("----------- INICIANDO CARGA DE LA DB EN OTRO HILO (CON EXPRESION LAMBDA) ------------- \n");
                
                //Creo mi delegado que contiene el metodo de carga de datos
                DelegadoCargaDB delegadoCargaDB;

                //Le asigno la tarea de traer de mi DB y un método que simule un tiempo de carga para demostrar que ocurre en otro hilo.
                delegadoCargaDB = metodo_Atrasador;
                delegadoCargaDB += DB_Stuff.TraerPersonajesDB;

                //Creo un nuevo hilo que es el de cargar personajes de la DB y lo ejecuto. Esto es realizado con un método anónimo LAMBDA.
                Task taskTraerPersonajesDB = Task.Run(() =>
                {
                    Universo.listaPersonajesExistentes = delegadoCargaDB("select * from dbo.TablaPersonajes");

                    //Pongo mi flag en true, para saber cuando el task ya terminó.
                    flag = true;

                    Console.WriteLine("-----------CARGA DE LA BASE DE DATOS EN 2DO PLANO FINALIZADA CORRECTAMENTE-------------");
                });
          
                Console.WriteLine("Presione para continuar\n");
                Console.ReadKey();

                while (flag == false)
                {
                    Console.WriteLine("----------- AUN NO SE TERMINÓ LA CARGA DE LA BASE DE DATOS. ------------- \n");
                    Console.WriteLine("Presione para reintentar\n");
                    Console.ReadKey();
                }

                Console.WriteLine("\nSE CREARAN 2 PERSONAJES\n");

                Personaje nuevoPersonaje = new Personaje("Creacion 1",50,Personaje.enumOrigenElemental.Fuego,Arma.enumTipoArma.Arco);
                ImprimirPersonaje(nuevoPersonaje);

                Personaje nuevoPersonaje2 = new Personaje("Creacion 2",80,Personaje.enumOrigenElemental.Agua,Arma.enumTipoArma.BastonMagico);
                ImprimirPersonaje(nuevoPersonaje2);

                Universo.listaPersonajesExistentes.Add(nuevoPersonaje);
                Universo.listaPersonajesExistentes.Add(nuevoPersonaje2);

                // ------------- Voy a llamar a mi funcionalidad principal, enfrentamiento. ----------------------------
                Console.WriteLine("----------- ENFRENTAMIENTO ENTRE LOS 2 PERSONAJES CREADOS (CREACION 1 vs CREACION 2) (SE UTILIZA EVENTO BATALLAR CON SU DELEGADO) ------------- \n");

                Console.WriteLine("Presione para continuar\n");
                Console.ReadKey();

                int resultadoBatalla;

                //Le asocio al evento batallar, su manejador.
                Universo.Batallar += Universo.ManejadorEventoBatallar;

                //Llamo a mi metodo enfrentamiento, que es el que invoca al evento batallar y este (ya asociado a su manejador)
                //actua en cuestion de la batalla.
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

                Console.WriteLine("----------- A CONTINUACION UN LISTADO DE TODOS LOS PERSONAJES ------------- \n");

                Console.WriteLine("Presione para continuar\n");
                Console.ReadKey();

                ImprimirTodosPersonajes();

                //--------------------------------------------------------------------------------------------------------------
                // EL ID ES DISTINTO PORQUE NO QUIERO MODIFICAR LA BASE DE DATOS E IR INSERTANDO 2 PERSONAJES POR CADA 
                // VEZ QUE SE DE PLAY LA CONSOLA DE TESTEO. ENTONCES EXPLICO ESTE HECHO Y LO CONTEMPLO. EN EL PROGRAMA REAL
                // AL DAR DE ALTA UN PERSONAJE SE SUBE EN LA BASE DE DATOS CON EL ID CORRESPONDIENTE Y AL TRAERLO DE LA DB SE 
                // CARGA CON EL ID CORRESPONDIENTE.
                //--------------------------------------------------------------------------------------------------------------

                Console.WriteLine("\nLos ultimos 2 personajes no tienen el ID correspondiente a la Database " +
                                    "ya que no los inserté. Causando así, que el primary key autoincremental " +
                                    "nunca sea incrementado ya que el persdonaje nunca fue subido a la base. Si fuera subido " +
                                    "y leído, el ID de ambos personajes (Creacion 1 y Creacion 2) serían valores " +
                                    "tomados del primary key de la base y estarían correctos y actualizados.\n");

                Console.WriteLine("----------- A CONTINUACION PORCENTAJES LOS DE ARMAS Y DE PODERES (SE UTILIZA UN METODO DE EXTENSION) ------------- \n");

                Console.WriteLine("Presione para continuar\n");
                Console.ReadKey();

                double porcentajeFuego;
                double porcentajeAgua;
                double porcentajeHielo;

                double porcentajeArco;
                double porcentajeEscudo;
                double porcentajeBaston;

                //Lamo a mi metodo de extensión para la obtencion de porcentajes indicandole que quiero el PORCENTAJE DE PODERES
                Universo.listaPersonajesExistentes.ObtenerPorcentajes(false, out porcentajeFuego, out porcentajeAgua, out porcentajeHielo);

                //Lamo a mi metodo de extensión para la obtencion de porcentajes indicandole que quiero el PORCENTAJE DE ARMAS
                Universo.listaPersonajesExistentes.ObtenerPorcentajes(true, out porcentajeArco, out porcentajeEscudo, out porcentajeBaston);

                Console.WriteLine("\nPorcentaje de poderes:\n");
                Console.WriteLine($"Fuego: {porcentajeFuego}%");
                Console.WriteLine($"Agua: {porcentajeAgua}%");
                Console.WriteLine($"Fuego: {porcentajeHielo}%");

                Console.WriteLine("\nPorcentaje de armas:\n");
                Console.WriteLine($"Fuego: {porcentajeArco}%");
                Console.WriteLine($"Agua: {porcentajeEscudo}%");
                Console.WriteLine($"Fuego: {porcentajeBaston}%");

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

        /// <summary>
        /// Imprimo por consola todos los datos del personaje recibido
        /// </summary>
        /// <param name="personaje">Personaje a imprimir</param>
        public static void ImprimirPersonaje(Personaje personaje)
        {
            Console.WriteLine
                   ($" Nombre: {personaje.NombrePersonaje} " +
                   $"\n Origen elemental: {personaje.OrigenElemental} " +
                   $"\n Nivel: {personaje.NivelTotal} " +
                   $"\n Arma: {personaje.Arma.TipoArma} " +
                   $"\n PtsAtaque: {personaje.Arma.PtsAtaque} " +
                   $"\n PtsDefensa: {personaje.Arma.PtsDefensa} " +
                   $"\n ID: {personaje.IdPersonaje}\n");
        }

        /// <summary>
        /// Imprimo por consola todos los datos de todos los personajes existentes 
        /// en la lista de Universo.ListaPersonajesExistentes.
        /// </summary>
        public static void ImprimirTodosPersonajes()
        {
            foreach (Personaje personaje in Universo.listaPersonajesExistentes)
            {
                Console.WriteLine
                   ($" Nombre: {personaje.NombrePersonaje} " +
                   $"\n Origen elemental: {personaje.OrigenElemental} " +
                   $"\n Nivel: {personaje.NivelTotal} " +
                   $"\n Arma: {personaje.Arma.TipoArma} " +
                   $"\n PtsAtaque: {personaje.Arma.PtsAtaque} " +
                   $"\n PtsDefensa: {personaje.Arma.PtsDefensa} " +
                   $"\n ID: {personaje.IdPersonaje}\n");
            }
        }

        /// <summary>
        /// Se encarga de verificar si existe el archivo que contiene los personajes y si la DB pudo traer
        /// algo. En caso de no existir el archivo, se crea y se inicializa con los personajes que trajo la base de datos. 
        /// En caso de que la base de datos no traiga nada, voy a hardcodearle los personajes pre-creados en memoria y cargados 
        /// a la lista. No cargo nada a la lista de personajes, solo verifico que el programa pueda iniciar leyendo algun personaje.
        /// </summary>
        public static void HardcodearPersonajesVerificandoExistenciaDByArchivos()
        {
            try
            {
                //Este va a ser el PATH donde voy a guardar SIEMPRE los personajes. Tanto lectura como escritura.
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                path = Path.Combine(path, "Archivos-TP3-LopezGasal");
                path = Path.Combine(path, "ArchivoPersonajes.json");

                //Me traigo lo que tengo en mi DB para ver si está o no vacía.
                Universo.listaPersonajesExistentesClonada = DB_Stuff.TraerPersonajesDB("select * from dbo.TablaPersonajes");

                //Si no existe el archivo... entonces mando los datos hardcodeados al archivo ya creado por primera vez y me los traigo del archivo.
                if (File.Exists(path) == false || Universo.listaPersonajesExistentesClonada.Count == 0)
                {

                    //Creo pjs hardcodeados
                    Personaje personaje1 = new Personaje("Federico", 90, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Arco);
                    Personaje personaje2 = new Personaje("Gabriel", 33, Personaje.enumOrigenElemental.Agua, Arma.enumTipoArma.Escudo);
                    Personaje personaje3 = new Personaje("Fran", 20, Personaje.enumOrigenElemental.Hielo, Arma.enumTipoArma.BastonMagico);
                    Personaje personaje4 = new Personaje("Julio", 40, Personaje.enumOrigenElemental.Fuego, Arma.enumTipoArma.Escudo);

                    //Los añado a la lista
                    Universo.listaPersonajesExistentes.Add(personaje1);
                    Universo.listaPersonajesExistentes.Add(personaje2);
                    Universo.listaPersonajesExistentes.Add(personaje3);
                    Universo.listaPersonajesExistentes.Add(personaje4);

                    if (File.Exists(path) == false && Universo.listaPersonajesExistentesClonada.Count > 0) //Si no existe el archivo y mi DB tiene algun personaje
                    {
                        //Voy a cargar en el archivo lo que tenga en mi DB.
                        ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentesClonada, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");
                    }
                    else if (File.Exists(path) == false && Universo.listaPersonajesExistentesClonada.Count == 0) //Si no existe el archivo, y mi DB tampoco tiene nada. HARDCODEO
                    {
                        //Creo el archivo, mandando el hardcodeo. Al igual en la DB
                        ArchivosManagement.EscribirArchivoSerializacionJSON<List<Personaje>>(Universo.listaPersonajesExistentes, "Archivos-TP3-LopezGasal", "ArchivoPersonajes.json");

                        //Mando el hardcodeo a la DB
                        DB_Stuff.MandarTodosPersonajesDB(Universo.listaPersonajesExistentes);
                    }

                    //Por las dudas, limpio la lista clonada, para no dejar basura que leí solo para hacer la comprobación de la DB.
                    Universo.listaPersonajesExistentesClonada.Clear();

                    //Los elimino de la lista
                    Universo.listaPersonajesExistentes.Clear();
                }
            }
            catch (Exception ExcepcionRecibida)
            {
                Console.WriteLine("Ocurrió una excepcion inesperada. Error guardado en el archivo de errores.", "Error inesperado");
                ArchivosManagement.EscribirArchivoTXT(ExcepcionRecibida.Message, false, "Archivos-TP3-LopezGasal//Errores", ArchivosManagement.GenerarNombreFechaHoraMntsConExtension("Error ", ".txt"));
            }
        }

        /// <summary>
        /// Delegado de tipo carga de la DB. Retorna si o si una lista de personajes y recibe un string que 
        /// es representativo a una query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public delegate List<Personaje> DelegadoCargaDB(string query);

        /// <summary>
        /// Este método solo se utiliza para mostrar como la tarea de la carga de la base de datos
        /// se ejecuta en otro hilo y no se podrá acceder a funcionalidades como 
        /// formImpresiones, formBatalla, no se podrá crear personajes, eliminar, ni modificar.
        /// Simula un tiempo de carga.
        /// </summary>
        /// <param name="stringInutil"></param>
        /// <returns></returns>
        public static List<Personaje> metodo_Atrasador(string stringInutil)
        {
            //5 SEGUNDOS DE RETRASO. AUMENTABLE SI ES NECESARIO. :)
            Thread.Sleep(5000);
            List<Personaje> listaInutil = new List<Personaje>();
            return listaInutil;
        }
    }
}

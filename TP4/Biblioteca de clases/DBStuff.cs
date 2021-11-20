using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace Entidades
{
    public class DB_Stuff
    {
        static SqlConnection conexion; //Se conecta a la instancia de SQL
        static SqlCommand comando; //LLeva la consulta
        static SqlDataReader lector; //Devuelve los datos

        /// <summary>
        /// Constructor necesario para realizar la conexión a la DB y setear el comand type.
        /// </summary>
        static DB_Stuff()
        {
            conexion = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Database=PersonajesTP4-LopezGasal;Trusted_Connection=True;");

            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.Connection = conexion;
        }

        /// <summary>
        /// Este método, al recibir una query en específico, traerá todos los personajes obtenidos de la query.
        /// Los devuelve en una lista.
        /// </summary>
        /// <param name="query">Query a consultar</param>
        /// <returns>Lista de personajes que se pudieron obtener de la consulta</returns>
        public static List<Personaje> TraerPersonajesDB (string query)
        {
            try
            {
                List<Personaje> retorno = new List<Personaje>();

                //ESTO ES LA QUERY
                comando.CommandText = query;

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                lector = comando.ExecuteReader();

                //ESTO NO PUEDE SER GENERICO. PORQUE DEPENDE DE LOS CAMPOS QUE ME TRAIGA EL LECTOR
                while (lector.Read() == true)
                {
                    //Creo el personaje vacio
                    Personaje personajeLeido = new Personaje ();
                    Arma armaDelPj = new Arma();

                    personajeLeido.Arma = armaDelPj;

                    //LE ASIGNO TODOS LOS CAMPOS

                    //Asigno nombre
                    personajeLeido.NombrePersonaje = lector["nombre"].ToString();

                    //Asigno nivel
                    personajeLeido.NivelTotal = (int) lector["nivelTotal"];

                    //Asigno origen
                    int origenLeido;
                    int.TryParse(lector["origenElemental"].ToString(), out origenLeido);

                    if (origenLeido == 0)
                    {
                        personajeLeido.OrigenElemental = Personaje.enumOrigenElemental.Fuego;
                    }
                    else if (origenLeido == 1)
                    {
                        personajeLeido.OrigenElemental = Personaje.enumOrigenElemental.Agua;
                    }
                    else if (origenLeido == 2)
                    {
                        personajeLeido.OrigenElemental = Personaje.enumOrigenElemental.Hielo;
                    }

                    //Asigno tipoArma
                    int tipoArmaLeido;
                    int.TryParse(lector["tipoArma"].ToString(), out tipoArmaLeido);

                    if (tipoArmaLeido == 0)
                    {
                        personajeLeido.Arma.TipoArma = Arma.enumTipoArma.Arco;
                    }
                    else if (tipoArmaLeido == 1)
                    {
                        personajeLeido.Arma.TipoArma = Arma.enumTipoArma.Escudo;
                    }
                    else if (tipoArmaLeido == 2)
                    {
                        personajeLeido.Arma.TipoArma = Arma.enumTipoArma.BastonMagico;
                    }

                    //Asigno ptsAtaque
                    personajeLeido.Arma.PtsAtaque = (int)lector["ptsAtaqueArma"];

                    //Asigno ptsDefensa
                    personajeLeido.Arma.PtsDefensa = (int)lector["ptsDefensaArma"];

                    //Asigno batallas jugadas
                    personajeLeido.BatallasJugadas = (int)lector["batallasJugadas"];

                    //Asigno batallas ganadas
                    personajeLeido.BatallasGanadas = (int)lector["batallasGanadas"];

                    //Asigno ID
                    personajeLeido.IdPersonaje = (int)lector["id"];

                    retorno.Add(personajeLeido);     
                }

                return retorno;

            }
            finally
            {
                conexion.Close();
            }       
        }

        /// <summary>
        /// Este método recibe una lista de personajes, que recorrida y cargada en su totalidad
        /// a la DB, mediante el comando insert.
        /// </summary>
        /// <param name="listaPersonajes">Lista de personajes a cargar en la DB</param>
        public static void MandarTodosPersonajesDB(List<Personaje> listaPersonajes)
        {
            try
            {
                List<string> auxLista = new List<string>();

                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                int cantPersonajes = listaPersonajes.Count;

                for (int i = 0; i < cantPersonajes; i++)
                {
                    //ESTO ES LA QUERY
                    comando.CommandText = "insert into TablaPersonajes values  ( @nombre, @nivelTotal, @origenElemental, @tipoArma, @ptsAtaqueArma, @ptsDefensaArma, @batallasJugadas, @batallasGanadas)";
                    comando.Parameters.Clear();

                    //Agrego el nombre
                    comando.Parameters.AddWithValue(@"nombre", listaPersonajes[i].NombrePersonaje);

                    //Agrego el nivel
                    comando.Parameters.AddWithValue(@"nivelTotal", listaPersonajes[i].NivelTotal);

                    //Agregar el indice al que pertenece el origen (en su enum).
                    if (listaPersonajes[i].OrigenElemental == Personaje.enumOrigenElemental.Fuego)
                    {
                        comando.Parameters.AddWithValue(@"origenElemental", 0);
                    }
                    else if (listaPersonajes[i].OrigenElemental == Personaje.enumOrigenElemental.Agua)
                    {
                        comando.Parameters.AddWithValue(@"origenElemental", 1);
                    }
                    else if (listaPersonajes[i].OrigenElemental == Personaje.enumOrigenElemental.Hielo)
                    {
                        comando.Parameters.AddWithValue(@"origenElemental", 2);
                    }

                    //Agregar el indice al que pertenece el tipoArma (en su enum).
                    if (listaPersonajes[i].Arma.TipoArma == Arma.enumTipoArma.Arco)
                    {
                        comando.Parameters.AddWithValue(@"tipoArma", 0);
                    }
                    else if (listaPersonajes[i].Arma.TipoArma == Arma.enumTipoArma.Escudo)
                    {
                        comando.Parameters.AddWithValue(@"tipoArma", 1);
                    }
                    else if (listaPersonajes[i].Arma.TipoArma == Arma.enumTipoArma.BastonMagico)
                    {
                        comando.Parameters.AddWithValue(@"tipoArma", 2);
                    }

                    //Agrego los puntos de ataque
                    comando.Parameters.AddWithValue(@"ptsAtaqueArma", listaPersonajes[i].Arma.PtsAtaque);

                    //Agrego los puntos de defensa
                    comando.Parameters.AddWithValue(@"ptsDefensaArma", listaPersonajes[i].Arma.PtsDefensa);

                    //Agrego las batallas jugadas
                    comando.Parameters.AddWithValue(@"batallasJugadas", listaPersonajes[i].BatallasJugadas);

                    //Agrego las batallas ganadas
                    comando.Parameters.AddWithValue(@"batallasGanadas", listaPersonajes[i].BatallasGanadas);

                    //Ejecuto la query
                    comando.ExecuteNonQuery();
                }
           
            }
            catch (Exception excepcionCapturada)
            {
                Console.WriteLine($"Excepcion: {excepcionCapturada.Message}");     
            }
            finally
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Este método recibe un personaje y lo elimina de la DB si alguno coincide con el nombre.
        /// Utiliza el comando delete.
        /// </summary>
        /// <param name="personajeAEliminar">Personaje a eliminar en la DB</param>
        public static void EliminarUnPersonajeDB(Personaje personajeAEliminar)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                //ESTO ES LA QUERY
                comando.CommandText = $"delete from dbo.TablaPersonajes where nombre = '{personajeAEliminar.NombrePersonaje}'";
                comando.Parameters.Clear();

                //Ejecuto la query
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
        }
   
        /// <summary>
        /// Este método recibe un personaje y lo modifica/updatea en la DB si alguno coincide con el nombre.
        /// Utiliza el comando update
        /// </summary>
        /// <param name="personajeAModificar">Personaje a ser modificado en la DB</param>
        public static void ModificarUnPersonajeDB(Personaje personajeAModificar, string nombre, int nivel, Personaje.enumOrigenElemental origen, Arma.enumTipoArma tipoArma, int batallasJugadas, int batallasGanadas)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                comando.CommandText = $"update dbo.TablaPersonajes set nivelTotal = {nivel} where nombre = '{personajeAModificar.NombrePersonaje}'";
                comando.Parameters.Clear();
                
                //Ejecuto la query
                comando.ExecuteNonQuery();

                //Agregar el indice al que pertenece el origen (en su enum).
                if (origen == Personaje.enumOrigenElemental.Fuego)
                {
                    comando.CommandText = $"update dbo.TablaPersonajes set origenElemental = 0 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();
                }
                else if (origen == Personaje.enumOrigenElemental.Agua)
                {
                    comando.CommandText = $"update dbo.TablaPersonajes set origenElemental = 1 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();
                }
                else if (origen == Personaje.enumOrigenElemental.Hielo)
                {
                    comando.CommandText = $"update dbo.TablaPersonajes set origenElemental = 2 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();
                }

               
                //Agregar el indice al que pertenece el origen (en su enum).
                if (tipoArma == Arma.enumTipoArma.Arco)
                {
                    comando.CommandText = $"update dbo.TablaPersonajes set tipoArma = 0 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();

                    comando.CommandText = $"update dbo.TablaPersonajes set ptsAtaqueArma = 850 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();

                    comando.CommandText = $"update dbo.TablaPersonajes set ptsDefensaArma = 500 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();
                }
                else if (tipoArma == Arma.enumTipoArma.Escudo)
                {
                    comando.CommandText = $"update dbo.TablaPersonajes set tipoArma = 1 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();

                    comando.CommandText = $"update dbo.TablaPersonajes set ptsAtaqueArma = 350 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();

                    comando.CommandText = $"update dbo.TablaPersonajes set ptsDefensaArma = 1000 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();
                }
                else if (tipoArma == Arma.enumTipoArma.BastonMagico)
                {
                    comando.CommandText = $"update dbo.TablaPersonajes set tipoArma = 2 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();

                    comando.CommandText = $"update dbo.TablaPersonajes set ptsAtaqueArma = 1000 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();

                    comando.CommandText = $"update dbo.TablaPersonajes set ptsDefensaArma = 350 where nombre = '{personajeAModificar.NombrePersonaje}'";

                    //Ejecuto la query
                    comando.Parameters.Clear();
                    comando.ExecuteNonQuery();
                }

                //Updateo las batallas jugadas
                comando.CommandText = $"update dbo.TablaPersonajes set batallasJugadas = {batallasJugadas} where nombre = '{personajeAModificar.NombrePersonaje}'";
                comando.Parameters.Clear();

                //Ejecuto la query
                comando.Parameters.Clear();
                comando.ExecuteNonQuery();

                //Updateo las batallas ganadas
                comando.CommandText = $"update dbo.TablaPersonajes set batallasGanadas = {batallasGanadas} where nombre = '{personajeAModificar.NombrePersonaje}'";
                comando.Parameters.Clear();

                //Ejecuto la query
                comando.Parameters.Clear();
                comando.ExecuteNonQuery();

                //Finalmente el nombre
                comando.CommandText = $"update dbo.TablaPersonajes set nombre = '{nombre}' where nombre = '{personajeAModificar.NombrePersonaje}'";
                comando.Parameters.Clear();

                //Ejecuto la query
                comando.ExecuteNonQuery();

            }
            finally
            {
                conexion.Close();
            }


        }

        /// <summary>
        /// Este método recibe un personaje y lo agrega a la DB. Utiliza el comando insert.
        /// </summary>
        /// <param name="personajeAAgregar">Personaje a ser agregado a la DB</param>
        public static void AgregarUnPersonajeDB(Personaje personajeAAgregar)
        {
            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                //ESTO ES LA QUERY
                comando.CommandText = $"insert into TablaPersonajes values  ( @nombre, @nivelTotal, @origenElemental, @tipoArma, @ptsAtaqueArma, @ptsDefensaArma, @batallasJugadas, @batallasGanadas )";
                comando.Parameters.Clear();

                //Agrego el nombre
                comando.Parameters.AddWithValue(@"nombre", personajeAAgregar.NombrePersonaje);

                //Agrego el nivel
                comando.Parameters.AddWithValue(@"nivelTotal", personajeAAgregar.NivelTotal);

                //Agregar el indice al que pertenece el origen (en su enum).
                if (personajeAAgregar.OrigenElemental == Personaje.enumOrigenElemental.Fuego)
                {
                    comando.Parameters.AddWithValue(@"origenElemental", 0);
                }
                else if (personajeAAgregar.OrigenElemental == Personaje.enumOrigenElemental.Agua)
                {
                    comando.Parameters.AddWithValue(@"origenElemental", 1);
                }
                else if (personajeAAgregar.OrigenElemental == Personaje.enumOrigenElemental.Hielo)
                {
                    comando.Parameters.AddWithValue(@"origenElemental", 2);
                }

                //Agregar el indice al que pertenece el tipoArma (en su enum).
                if (personajeAAgregar.Arma.TipoArma == Arma.enumTipoArma.Arco)
                {
                    comando.Parameters.AddWithValue(@"tipoArma", 0);
                }
                else if (personajeAAgregar.Arma.TipoArma == Arma.enumTipoArma.Escudo)
                {
                    comando.Parameters.AddWithValue(@"tipoArma", 1);
                }
                else if (personajeAAgregar.Arma.TipoArma == Arma.enumTipoArma.BastonMagico)
                {
                    comando.Parameters.AddWithValue(@"tipoArma", 2);
                }

                //Agrego los puntos de ataque
                comando.Parameters.AddWithValue(@"ptsAtaqueArma", personajeAAgregar.Arma.PtsAtaque);

                //Agrego los puntos de defensa
                comando.Parameters.AddWithValue(@"ptsDefensaArma", personajeAAgregar.Arma.PtsDefensa);

                //Agrego las batallas jugadas
                comando.Parameters.AddWithValue(@"batallasJugadas", personajeAAgregar.BatallasJugadas);

                //Agrego las batallas ganadas
                comando.Parameters.AddWithValue(@"batallasGanadas", personajeAAgregar.BatallasGanadas);

                //Ejecuto la query
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}

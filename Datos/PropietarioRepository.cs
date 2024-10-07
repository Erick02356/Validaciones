using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class PropietarioRepository
    {
        private string  conexionString = "Server=DESKTOP-B76V0T4\\SQLEXPRESS;DataBase=Proyecto;Integrated Security = True";
        public List<Propietario> ObtenerTodos()
        {
            var lista_propietarios = new List<Propietario> ();
            using (SqlConnection conexion = new SqlConnection (conexionString))
            {
                conexion.Open ();
                var command = new SqlCommand("Mostrar", conexion);
                var reader = command.ExecuteReader ();
                while (reader.Read())
                {
                    var propietarios = new Propietario
                    {
                        Id = (int)reader["Id"],
                        Cedula = (long)reader["Cedula"],  
                        Nombre1 = reader["Nombre_uno"].ToString(),
                        Nombre2 = reader["Nombre_dos"].ToString(),
                        Apellido1 = reader["Apellido_uno"].ToString(),
                        Apellido2 = reader["Apellido_dos"].ToString(),
                        Telefono = (int)reader["Telefono"],
                        Email = reader["Email"].ToString(),
                        NombreUsuario = reader["Usuario"].ToString(),
                        Clave = reader["Clave"].ToString(),
                        Estado = reader["Estado"].ToString(),

                    };

                    lista_propietarios.Add (propietarios);

                }
                return lista_propietarios;
            }
        }

        public void Crear (Propietario propietario)
        {
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                conexion.Open();
                var command = new SqlCommand("Insertar", conexion); // Asegúrate de que "Insertar" sea el nombre correcto del SP o query
                command.CommandType = CommandType.StoredProcedure;

                // Agregar los parámetros que necesita el SP o la consulta
                command.Parameters.AddWithValue("@Cedula", propietario.Cedula);
                command.Parameters.AddWithValue("@Nombre_uno", propietario.Nombre1);
                command.Parameters.AddWithValue("@Nombre_dos", propietario.Nombre2);
                command.Parameters.AddWithValue("@Apellido_uno", propietario.Apellido1);
                command.Parameters.AddWithValue("@Apellido_dos", propietario.Apellido2);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Email", propietario.Email);
                command.Parameters.AddWithValue("@Usuario", propietario.NombreUsuario);
                command.Parameters.AddWithValue("@Clave", propietario.Clave);
                command.Parameters.AddWithValue("@Estado", propietario.Estado);

                command.ExecuteNonQuery();
            }
        }

        public void Editar(Propietario propietario)
        {
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                conexion.Open();
                var command = new SqlCommand("Editar", conexion);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", propietario.Id);
                command.Parameters.AddWithValue("@Cedula", propietario.Cedula);
                command.Parameters.AddWithValue("@Nombre_uno", propietario.Nombre1);
                command.Parameters.AddWithValue("@Nombre_dos", propietario.Nombre2);
                command.Parameters.AddWithValue("@Apellido_uno", propietario.Apellido1);
                command.Parameters.AddWithValue("@Apellido_dos", propietario.Apellido2);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Email", propietario.Email);
                command.Parameters.AddWithValue("@Usuario", propietario.NombreUsuario);
                command.Parameters.AddWithValue("@Clave", propietario.Clave);
                command.Parameters.AddWithValue("@Estado", propietario.Estado);

                command.ExecuteNonQuery();
            }
        }

        // Método para Eliminar un propietario
        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                conexion.Open();
                var command = new SqlCommand("Eliminar", conexion);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}

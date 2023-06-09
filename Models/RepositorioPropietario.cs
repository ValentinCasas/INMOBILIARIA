using MySql.Data.MySqlClient;
namespace INMOBILIARIA.Models;

public class RepositorioPropietario
{

    string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public RepositorioPropietario()
    {
    }

    public List<Propietario> GetPropietarios()
    {
        List<Propietario> propietarios = new List<Propietario>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT Id,Dni,Nombre,Apellido,Telefono,Email
            FROM propietario";
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Propietario propietario = new Propietario()
                        {
                            Id = reader.GetInt32(nameof(Propietario.Id)),
                            Dni = reader.GetInt64(nameof(Propietario.Dni)),
                            Nombre = reader.GetString(nameof(Propietario.Nombre)),
                            Apellido = reader.GetString(nameof(Propietario.Apellido)),
                            Telefono = reader.GetInt64(nameof(Propietario.Telefono)),
                            Email = reader.GetString(nameof(Propietario.Email)),
                        };
                        propietarios.Add(propietario);
                    }
                }
            }
            connection.Close();
        }
        return propietarios;
    }

     public List<Inmueble> GetInmuebles()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT i.Id, i.Direccion, i.Uso, i.Tipo, i.CantidadAmbientes, i.Coordenadas, i.PrecioInmueble, i.Estado, i.IdPropietario,
                        p.Nombre, p.Apellido, p.Dni, p.Telefono, p.Id, p.Email
                        FROM inmueble i
                        JOIN propietario p ON i.IdPropietario = p.Id
                        ";

            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble()
                        {
                            Id = reader.GetInt32(nameof(inmueble.Id)),
                            Direccion = reader.GetString(nameof(inmueble.Direccion)),
                            Uso = reader.GetString(nameof(inmueble.Uso)),
                            Tipo = reader.GetString(nameof(inmueble.Tipo)),
                            CantidadAmbientes = reader.GetInt32(nameof(inmueble.CantidadAmbientes)),
                            Coordenadas = reader.GetString(nameof(inmueble.Coordenadas)),
                            PrecioInmueble = reader.GetDecimal(nameof(inmueble.PrecioInmueble)),
                            Estado = reader.GetString(nameof(inmueble.Estado)),
                            IdPropietario = reader.GetInt32(nameof(inmueble.IdPropietario)),
                            Propietario = new Propietario()
                            {
                                Nombre = reader.GetString(nameof(Propietario.Nombre)),
                                Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetInt64(nameof(Propietario.Dni)),
                                Telefono = reader.GetInt64(nameof(Propietario.Telefono)),
                                Id = reader.GetInt32(nameof(Propietario.Id)),
                                Email = reader.GetString(nameof(Propietario.Email)),
                            },
                        };
                        inmuebles.Add(inmueble);
                    }
                }
            }
        }
        return inmuebles;
    }

    public int Alta(Propietario propietario)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO propietario (Nombre,Apellido,Dni,Telefono,Email) VALUES (@nombre,@apellido,@dni,@telefono,@email);
        SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@email", propietario.Email);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                propietario.Id = res;
                connection.Close();
            }
        }
        return res;
    }

    public Boolean Baja(int idPropietario)
    {
        Boolean res = false;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"DELETE FROM propietario WHERE Id = @idPropietario";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idPropietario", idPropietario);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    res = true;
                }
            }
        }
        return res;
    }

    public Propietario ObtenerPorId(int id)
    {
        Propietario propietario = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {

            string query = @"SELECT * FROM propietario WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        propietario = new Propietario();
                        propietario.Id = Convert.ToInt32(reader["id"]);
                        propietario.Nombre = Convert.ToString(reader["nombre"]);
                        propietario.Apellido = Convert.ToString(reader["apellido"]);
                        propietario.Dni = Convert.ToInt64(reader["dni"]);
                        propietario.Telefono = Convert.ToInt64(reader["telefono"]);
                        propietario.Email = Convert.ToString(reader["email"]);
                        return propietario;
                    }


                }
            }
        }
        return propietario;
    }

    public Boolean Actualizar(Propietario propietario)
{
    Boolean res = false;
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        string query = @"UPDATE propietario SET Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Dni = @dni, Email = @email WHERE Id = @id";
        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            if (propietario.Nombre != null && propietario.Apellido != null && propietario.Dni > 0 && propietario.Telefono > 0 && propietario.Email != null)
            {
                command.Parameters.AddWithValue("@id", propietario.Id);
                command.Parameters.AddWithValue("@nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@dni", propietario.Dni);
                command.Parameters.AddWithValue("@telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@email", propietario.Email);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    res = true;
                }
            }
        }
    }
    return res;
}




}
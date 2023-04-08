using MySql.Data.MySqlClient;
namespace INMOBILIARIA.Models;

public class RepositorioInmueble
{

    string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public RepositorioInmueble() { }

    public Inmueble ObtenerPorId(int id)
    {
        Inmueble inmueble = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {

            string query = @"SELECT * FROM inmueble WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inmueble = new Inmueble();
                        inmueble.Id = Convert.ToInt32(reader["id"]);
                        inmueble.IdPropietario = Convert.ToInt32(reader["idPropietario"]);
                        inmueble.Direccion = Convert.ToString(reader["Direccion"]);
                        inmueble.Uso = Convert.ToString(reader["uso"]);
                        inmueble.Tipo = Convert.ToString(reader["tipo"]);
                        inmueble.CantidadAmbientes = Convert.ToInt32(reader["cantidadAmbientes"]);
                        inmueble.Coordenadas = Convert.ToString(reader["coordenadas"]);
                        inmueble.PrecioInmueble = Convert.ToDecimal(reader["precioInmueble"]);
                        inmueble.Estado = Convert.ToString(reader["estado"]);
                        return inmueble;
                    }


                }
            }
        }
        return inmueble;
    }

    public List<Inmueble> GetInmuebles()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT i.Id, i.Direccion, i.Uso, i.Tipo, i.CantidadAmbientes, i.Coordenadas, i.PrecioInmueble, i.Estado,
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

    public int Alta(Inmueble inmueble)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO Inmueble (Direccion,Estado,Uso,Tipo,CantidadAmbientes,Coordenadas, PrecioInmueble, IdPropietario)
             VALUES (@direccion,@estado,@uso,@tipo,@cantidadAmbientes,@coordenadas,@precioInmueble,@idPropietario);
        SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@estado", inmueble.Estado);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
                command.Parameters.AddWithValue("@cantidadAmbientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);

                command.Parameters.AddWithValue("@precioInmueble", inmueble.PrecioInmueble);
                command.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                inmueble.Id = res;
                connection.Close();
            }
        }
        return res;
    }

    public Boolean Baja(int id)
    {
        Boolean res = false;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"DELETE FROM inmueble WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
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

    public Boolean Actualizar(Inmueble inmueble)
    {
        Boolean res = false;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE inmueble SET Direccion = @direccion, Estado = @estado, Uso = @uso, Tipo = @tipo, CantidadAmbientes = @cantidadAmbientes, Coordenadas = @coordenadas, PrecioInmueble = @precioInmueble, IdPropietario = @idPropietario WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (inmueble.Direccion != null && inmueble.Estado != null && inmueble.Uso != null && inmueble.Tipo != null && inmueble.CantidadAmbientes > 0 && inmueble.Coordenadas != null && inmueble.PrecioInmueble > 0)
                {
                    command.Parameters.AddWithValue("@id", inmueble.Id);
                    command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@estado", inmueble.Estado);
                    command.Parameters.AddWithValue("@uso", inmueble.Uso);
                    command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
                    command.Parameters.AddWithValue("@cantidadAmbientes", inmueble.CantidadAmbientes);
                    command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);

                    command.Parameters.AddWithValue("@precioInmueble", inmueble.PrecioInmueble);
                    command.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);
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
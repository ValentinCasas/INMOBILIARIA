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
                        inmueble.IdContrato = Convert.ToInt32(reader["idContrato"]);
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
            var query = @"SELECT Id, Direccion, Uso, Tipo, CantidadAmbientes, Coordenadas, PrecioInmueble, Estado, IdPropietario, IdContrato
            FROM inmueble";

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
                            IdContrato = reader.GetInt32(nameof(inmueble.IdContrato)),
                        };
                        inmuebles.Add(inmueble);
                    }
                }
            }
            connection.Close();
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

    public List<Contrato> GetContratos()
    {
        List<Contrato> contratos = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT Id,IdInquilino,FechaInicio,FechaFinalizacion,MontoAlquilerMensual,Activo
            FROM contrato";
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato()
                        {
                            Id = reader.GetInt32(nameof(contrato.Id)),
                            IdInquilino = reader.GetInt32(nameof(contrato.IdInquilino)),
                            FechaInicio = reader.GetDateTime(nameof(contrato.FechaInicio)),
                            FechaFinalizacion = reader.GetDateTime(nameof(contrato.FechaFinalizacion)),
                            MontoAlquilerMensual = reader.GetDecimal(nameof(contrato.MontoAlquilerMensual)),
                            Activo = reader.GetBoolean(nameof(contrato.Activo)),
                        };
                        contratos.Add(contrato);
                    }
                }
            }
            connection.Close();
        }
        return contratos;
    }

    public int Alta(Inmueble inmueble)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO Inmueble (Direccion,Uso,Tipo,CantidadAmbientes,Coordenadas, PrecioInmueble, IdPropietario,IdContrato)
             VALUES (@direccion,@uso,@tipo,@cantidadAmbientes,@coordenadas,@precioInmueble,@idPropietario,@idContrato);
        SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
                command.Parameters.AddWithValue("@cantidadAmbientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);

                command.Parameters.AddWithValue("@precioInmueble", inmueble.PrecioInmueble);
                command.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);
                command.Parameters.AddWithValue("@idContrato", inmueble.IdContrato);

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
            string query = @"UPDATE inmueble SET Direccion = @direccion, Uso = @uso, Tipo = @tipo, CantidadAmbientes = @cantidadAmbientes, Coordenadas = @coordenadas, PrecioInmueble = @precioInmueble, IdPropietario = @idPropietario, IdContrato = @idContrato WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", inmueble.Id);
                command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                command.Parameters.AddWithValue("@uso", inmueble.Uso);
                command.Parameters.AddWithValue("@tipo", inmueble.Tipo);
                command.Parameters.AddWithValue("@cantidadAmbientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@coordenadas", inmueble.Coordenadas);

                command.Parameters.AddWithValue("@precioInmueble", inmueble.PrecioInmueble);
                command.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);
                command.Parameters.AddWithValue("@idContrato", inmueble.IdContrato);
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


}
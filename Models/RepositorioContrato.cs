using MySql.Data.MySqlClient;
namespace INMOBILIARIA.Models;

public class RepositorioContrato
{

    string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public RepositorioContrato() { }

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

    public List<Contrato> GetContratos()
    {
        List<Contrato> contratos = new List<Contrato>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"SELECT c.Id, c.FechaInicio, c.FechaFinalizacion, c.MontoAlquilerMensual, c.Activo, c.IdInquilino,
                        i.Nombre, i.Apellido, i.Dni, i.Telefono, i.Email, i.Id,
                        inmueble.Id, inmueble.Direccion, inmueble.Uso, inmueble.Tipo, inmueble.IdPropietario, inmueble.CantidadAmbientes, inmueble.Coordenadas, inmueble.PrecioInmueble, inmueble.Estado
                        FROM contrato c
                        JOIN inquilino i ON c.IdInquilino = i.Id
                        JOIN inmueble ON c.IdInmueble = inmueble.Id";


            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrato contrato = new Contrato()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFinalizacion = Convert.ToDateTime(reader["FechaFinalizacion"]),
                            MontoAlquilerMensual = Convert.ToDecimal(reader["MontoAlquilerMensual"]),
                            Activo = Convert.ToBoolean(reader["Activo"]),
                            IdInquilino = Convert.ToInt32(reader["IdInquilino"]),
                            Inquilino = new Inquilino()
                            {
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido"),
                                Dni = reader.GetInt64("Dni"),
                                Telefono = reader.GetInt64("Telefono"),
                                Id = reader.GetInt32("Id"),
                                Email = reader.GetString("Email"),
                            },
                            Inmueble = new Inmueble()
                            {
                                Id = reader.GetInt32("Id"),
                                Direccion = reader.GetString("Direccion"),
                                Uso = reader.GetString("Uso"),
                                IdPropietario = reader.GetInt32("IdPropietario"),
                                Tipo = reader.GetString("Tipo"),
                                CantidadAmbientes = reader.GetInt32("CantidadAmbientes"),
                                Coordenadas = reader.GetString("Coordenadas"),
                                PrecioInmueble = reader.GetDecimal("PrecioInmueble"),
                                Estado = reader.GetString("Estado"),
                            },
                        };

                        contratos.Add(contrato);
                    }
                }
            }
        }
        return contratos;
    }


    public List<Inquilino> GetInquilinos()
    {
        List<Inquilino> inquilinos = new List<Inquilino>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT Id,Dni,Nombre,Apellido,Telefono,Email
            FROM inquilino";
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inquilino inquilino = new Inquilino()
                        {
                            Id = reader.GetInt32(nameof(inquilino.Id)),
                            Dni = reader.GetInt64(nameof(inquilino.Dni)),
                            Nombre = reader.GetString(nameof(inquilino.Nombre)),
                            Apellido = reader.GetString(nameof(inquilino.Apellido)),
                            Telefono = reader.GetInt64(nameof(inquilino.Telefono)),
                            Email = reader.GetString(nameof(inquilino.Email)),
                        };
                        inquilinos.Add(inquilino);
                    }
                }
            }
            connection.Close();
        }
        return inquilinos;
    }

    public int Alta(Contrato contrato)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO contrato (FechaInicio,FechaFinalizacion,MontoAlquilerMensual,Activo,IdInquilino,IdInmueble)
                            VALUES
                            (@fechaInicio, @fechaFinalizacion,@montoAlquilerMensual,@activo,@idInquilino,@idInmueble);
                            SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@fechaFinalizacion", contrato.FechaFinalizacion);
                command.Parameters.AddWithValue("@montoAlquilerMensual", contrato.MontoAlquilerMensual);
                command.Parameters.AddWithValue("@activo", contrato.Activo);
                command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                contrato.Id = res;
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
            string query = @"DELETE FROM contrato WHERE Id = @id";
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

    public Contrato ObtenerPorId(int id)
    {
        Contrato contrato = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {

            string query = @"SELECT * FROM contrato WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrato = new Contrato();
                        contrato.Id = Convert.ToInt32(reader["id"]);
                        contrato.IdInquilino = Convert.ToInt32(reader["IdInquilino"]);
                        contrato.FechaInicio = Convert.ToDateTime(reader["FechaInicio"]);
                        contrato.FechaFinalizacion = Convert.ToDateTime(reader["FechaFinalizacion"]);
                        contrato.MontoAlquilerMensual = Convert.ToDecimal(reader["MontoAlquilerMensual"]);
                        contrato.Activo = Convert.ToBoolean(reader["Activo"]);

                        return contrato;
                    }


                }
            }
        }
        return contrato;
    }

    public Boolean Actualizar(Contrato contrato)
    {
        Boolean res = false;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE contrato SET IdInmueble = @idInmueble, IdInquilino = @idInquilino, FechaInicio = @fechaInicio, FechaFinalizacion = @fechaFinalizacion, MontoAlquilerMensual = @montoAlquilerMensual, Activo = @activo WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (contrato.FechaInicio != null && contrato.FechaFinalizacion != null && contrato.MontoAlquilerMensual > 0 && contrato.Activo != null)
                {
                    command.Parameters.AddWithValue("@id", contrato.Id);
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdInmueble);
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                    command.Parameters.AddWithValue("@fechaFinalizacion", contrato.FechaFinalizacion);
                    command.Parameters.AddWithValue("@montoAlquilerMensual", contrato.MontoAlquilerMensual);
                    command.Parameters.AddWithValue("@activo", contrato.Activo);
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
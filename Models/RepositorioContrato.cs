using MySql.Data.MySqlClient;
namespace INMOBILIARIA.Models;

public class RepositorioContrato
{

    string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public RepositorioContrato() { }

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
            string query = @"INSERT INTO contrato (FechaInicio,FechaFinalizacion,MontoAlquilerMensual,Activo,IdInquilino) VALUES (@fechaInicio,@fechaFinalizacion,@montoAlquilerMensual,@activo,@idInquilino);
        SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@fechaFinalizacion", contrato.FechaFinalizacion);
                command.Parameters.AddWithValue("@montoAlquilerMensual", contrato.MontoAlquilerMensual);
                command.Parameters.AddWithValue("@activo", contrato.Activo);
                command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
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
            string query = @"UPDATE contrato SET IdInquilino = @idInquilino, FechaInicio = @fechaInicio, FechaFinalizacion = @fechaFinalizacion, MontoAlquilerMensual = @montoAlquilerMensual, Activo = @activo, WHERE Id = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", contrato.Id);
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
        return res;
    }


}
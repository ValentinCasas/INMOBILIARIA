using MySql.Data.MySqlClient;
namespace INMOBILIARIA.Models;

public class RepositorioPago
{

    string connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";

    public RepositorioPago() { }

    public List<Pago> GetPagos()
    {
        List<Pago> pagos = new List<Pago>();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT i.Id, i.NumDePago, i.FechaDePago, i.Importe, i.IdContrato,
                        c.Id, c.FechaInicio, c.Fechafinalizacion, c.MontoAlquilerMensual, c.Activo, c.IdInquilino
                        FROM pago i
                        JOIN contrato c ON i.IdContrato = c.Id
                        ";

            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pago pago = new Pago()
                        {
                            Id = reader.GetInt32(nameof(pago.Id)),
                            NumDePago = reader.GetInt32(nameof(pago.NumDePago)),
                            FechaDePago = reader.GetDateTime(nameof(pago.FechaDePago)),
                            Importe = reader.GetDecimal(nameof(pago.Importe)),
                        
                            Contrato = new Contrato()
                            {
                                Id = reader.GetInt32(nameof(Contrato.Id)),
                                FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                                FechaFinalizacion = reader.GetDateTime(nameof(Contrato.FechaFinalizacion)),
                                MontoAlquilerMensual = reader.GetDecimal(nameof(Contrato.MontoAlquilerMensual)),
                                Activo = reader.GetBoolean(nameof(Contrato.Activo)),
                                IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                            },
                        };
                        pagos.Add(pago);
                    }
                }
            }
        }
        return pagos;
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

    public int Alta(Pago pago)
    {
        int res = 0;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"INSERT INTO pago (NumDePago,FechaDePago,Importe,IdContrato)
             VALUES (@numDePago,@fechaDePago,@importe,@idContrato);
        SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@numDePago", pago.NumDePago);
                command.Parameters.AddWithValue("@fechaDePago", pago.FechaDePago);
                command.Parameters.AddWithValue("@importe", pago.Importe);
                command.Parameters.AddWithValue("@idContrato", pago.IdContrato);

                connection.Open();
                res = Convert.ToInt32(command.ExecuteScalar());
                pago.Id = res;
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
            string query = @"DELETE FROM pago WHERE Id = @id";
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

public Pago ObtenerPorId(int id)
{
    Pago pago = null;

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        var query = @"SELECT i.Id, i.NumDePago, i.FechaDePago, i.Importe, i.IdContrato,
                    c.Id, c.FechaInicio, c.Fechafinalizacion, c.MontoAlquilerMensual, c.Activo, c.IdInquilino
                    FROM pago i
                    JOIN contrato c ON i.IdContrato = c.Id
                    WHERE i.Id = @id";

        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    pago = new Pago()
                    {
                        Id = reader.GetInt32(nameof(Pago.Id)),
                        NumDePago = reader.GetInt32(nameof(Pago.NumDePago)),
                        FechaDePago = reader.GetDateTime(nameof(Pago.FechaDePago)),
                        Importe = reader.GetDecimal(nameof(Pago.Importe)),
                    
                        Contrato = new Contrato()
                        {
                            Id = reader.GetInt32(nameof(Contrato.Id)),
                            FechaInicio = reader.GetDateTime(nameof(Contrato.FechaInicio)),
                            FechaFinalizacion = reader.GetDateTime(nameof(Contrato.FechaFinalizacion)),
                            MontoAlquilerMensual = reader.GetDecimal(nameof(Contrato.MontoAlquilerMensual)),
                            Activo = reader.GetBoolean(nameof(Contrato.Activo)),
                            IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                        },
                    };
                }
            }
        }
    }

    return pago;
}



/*
    public Boolean Actualizar(Inmueble inmueble)
    {
        Boolean res = false;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = @"UPDATE inmueble SET Direccion = @direccion, Estado = @estado, Uso = @uso, Tipo = @tipo, CantidadAmbientes = @cantidadAmbientes, Coordenadas = @coordenadas, PrecioInmueble = @precioInmueble, IdPropietario = @idPropietario, IdContrato = @idContrato WHERE Id = @id";
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
                    command.Parameters.AddWithValue("@idContrato", inmueble.IdContrato);
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

*/
}
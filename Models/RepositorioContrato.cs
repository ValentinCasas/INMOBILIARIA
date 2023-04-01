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

}
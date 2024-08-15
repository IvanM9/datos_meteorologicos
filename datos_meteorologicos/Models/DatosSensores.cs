using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace datos_meteorologicos.Models;

[Table("datos_sensores")]
public class DatosSensores
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("codigo_parametro")]
    public string CodigoParametro { get; set; }
    
    [Column("parametro_sensores_id")]
    public int ParametroSensoresId { get; set; }
    
    [Column("nombre_parametro")]
    public string NombreParametro { get; set; }
    
    [Column("fecha_dato")]
    public DateTime FechaDato { get; set; }
    
    [Column("valor_numero")]
    public double ValorNumero { get; set; }
    
    public ParametrosSensores ParametrosSensores { get; set; }
}
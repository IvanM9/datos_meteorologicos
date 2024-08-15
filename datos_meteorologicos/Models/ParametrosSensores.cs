using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace datos_meteorologicos.Models;

[Table("parametros_sensores")]
public class ParametrosSensores
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("codigo_parametro")]
    public string CodigoParametro { get; set; }
    
    [Column("descripcion_larga")]
    public string DescripcionLarga { get; set; }
    
    [Column("descripcion_corta")]
    public string DescripcionCorta { get; set; }
    
    [Column("descripcion_med")]
    public string DescripcionMed { get; set; }
    
    [Column("abreviacion")]
    public string Abreviacion { get; set; }
    
    [Column("observacion")]
    public string Observacion { get; set; }
    
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }
    
    [Column("fecha_modificacion")]
    public DateTime FechaModificacion { get; set; }
    
    [Column("estado")]
    public string Estado { get; set; }
    
    [Column("unidad")]
    public string Unidad { get; set; }
}
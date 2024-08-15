namespace datos_meteorologicos.Dtos;

public class DatosMeteorologicosResponseDto
{
    public List<string> DeviceDates { get; set; }
    public List<DeviceDataDto> DeviceData { get; set; }
}

public class DeviceDataDto
{
    public string CodigoParametro { get; set; }
    public string NombreParametro { get; set; }
    public string UnidadParametro { get; set; }
    public string AbreviacionParametro { get; set; }
    public ValuesDto Values { get; set; }
}

public class ValuesDto
{
    public List<double> AvgData { get; set; }
    public List<double> MinData { get; set; }
    public List<double> MaxData { get; set; }
}

public enum ModoEnum
{
    Month,
    Day
}
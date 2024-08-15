using datos_meteorologicos.Context;
using datos_meteorologicos.Dtos;
using datos_meteorologicos.Models;
using Microsoft.EntityFrameworkCore;

namespace datos_meteorologicos.Services;

public class DatosMeteorologicosService
{
    private AppDbContext context;

    public DatosMeteorologicosService(AppDbContext _context)
    {
        this.context = _context;
    }

    public async Task<DatosMeteorologicosResponseDto> GetDatosSensores(ModoEnum mode, DateTime firstDate, DateTime lastDate)
    {
        var data = await context.DatosSensores
            .Include(d => d.ParametrosSensores)
            .Where(d => d.FechaDato.Date >= firstDate && d.FechaDato.Date <= lastDate).ToListAsync();

        var deviceDates = new List<string>();
        var avgData = new List<double>();
        var minData = new List<double>();
        var maxData = new List<double>();

        if (mode == ModoEnum.Day)
        {
            GetDayData(data, ref deviceDates, ref avgData, ref minData, ref maxData);
        }
        else
        {
            GetMonthData(data, ref deviceDates, ref avgData, ref minData, ref maxData);
        }
        

        return new DatosMeteorologicosResponseDto
        {
            DeviceDates = deviceDates,
            DeviceData = data.Select(d => new DeviceDataDto
            {
                CodigoParametro = d.ParametrosSensores.CodigoParametro,
                NombreParametro = d.NombreParametro,
                UnidadParametro = d.ParametrosSensores.Unidad,
                AbreviacionParametro = d.ParametrosSensores.Abreviacion,
                Values = new ValuesDto
                {
                    AvgData = avgData,
                    MinData = minData,
                    MaxData = maxData
                }
            }).ToList()
        };
    }
    
    private void GetDayData(List<DatosSensores> datos, ref List<string> deviceDates, ref List<double> avgData, ref List<double> minData, ref List<double> maxData)
    {
        deviceDates = datos.Select(d => d.FechaDato.Date.ToString("yyyy-MM-dd")).Distinct().ToList();

        avgData = datos.GroupBy(d => d.FechaDato.Date)
            .Select(g => Math.Round(g.Average(d => d.ValorNumero), 2))
            .ToList();

        minData = datos.GroupBy(d => d.FechaDato.Date)
            .Select(g => g.Min(d => d.ValorNumero))
            .ToList();

        maxData = datos.GroupBy(d => d.FechaDato.Date)
            .Select(g => g.Max(d => d.ValorNumero))
            .ToList();
    }
    
    private void GetMonthData(List<DatosSensores> data, ref List<string> deviceDates, ref List<double> avgData, ref List<double> minData, ref List<double> maxData)
    {
        deviceDates = data.GroupBy(d => new { d.FechaDato.Year, d.FechaDato.Month })
            .Select(g =>
            {
                var startDate = new DateTime(g.Key.Year, g.Key.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);
                return $"{startDate:yyyy-MM-dd} – {endDate:yyyy-MM-dd}";
            })
            .ToList();

        avgData = data.GroupBy(d => new { d.FechaDato.Year, d.FechaDato.Month })
            .Select(g => Math.Round(g.Average(d => d.ValorNumero), 2))
            .ToList();

        minData = data.GroupBy(d => new { d.FechaDato.Year, d.FechaDato.Month })
            .Select(g => g.Min(d => d.ValorNumero))
            .ToList();

        maxData = data.GroupBy(d => new { d.FechaDato.Year, d.FechaDato.Month })
            .Select(g => g.Max(d => d.ValorNumero))
            .ToList();
    }

}
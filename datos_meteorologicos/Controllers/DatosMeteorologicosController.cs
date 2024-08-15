using datos_meteorologicos.Dtos;
using datos_meteorologicos.Services;
using Microsoft.AspNetCore.Mvc;

namespace datos_meteorologicos.Controllers;

[Route("api/meteo")]
public class DatosMeteorologicosController : Controller
{
    private DatosMeteorologicosService datosMeteorologicosService;

    public DatosMeteorologicosController(DatosMeteorologicosService _datosMeteorologicosService)
    {
        this.datosMeteorologicosService = _datosMeteorologicosService;
    }

    [HttpGet("month/{firstDate}/{lastDate}")]
    public Task<DatosMeteorologicosResponseDto> GetMonthData(DateTime firstDate, DateTime lastDate)
    {
        return datosMeteorologicosService.GetDatosSensores(ModoEnum.Month, firstDate, lastDate);
    }

    [HttpGet("day/{firstDate}/{lastDate}")]
    public Task<DatosMeteorologicosResponseDto> GetDayData(DateTime firstDate, DateTime lastDate)
    {
        return datosMeteorologicosService.GetDatosSensores(ModoEnum.Day, firstDate, lastDate);
    }
}
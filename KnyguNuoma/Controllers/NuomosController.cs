using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class NuomosController : ControllerBase
{
    private readonly INuomosServisas _nuomosServisas;
    private readonly IKnyguServisas _knyguServisas; // Pridėtas knygų servisas
    private readonly ILogger<NuomosController> _logger;

    public NuomosController(INuomosServisas nuomosServisas, IKnyguServisas knyguServisas, ILogger<NuomosController> logger)
    {
        _nuomosServisas = nuomosServisas;
        _knyguServisas = knyguServisas; // Inicializuojame knygų servisą
        _logger = logger;
    }

    // Gauti visas nuomas
    [HttpGet]
    public ActionResult<List<NuomosIrasas>> GetAllRentals()
    {
        _logger.LogInformation("Užklausa gauti visas nuomas.");
        var nuomos = _nuomosServisas.GautiVisasNuomas();
        if (nuomos == null || nuomos.Count == 0)
        {
            _logger.LogWarning("Nuomų nerasta.");
            return NotFound("Nuomų nerasta.");
        }
        return Ok(nuomos);
    }

    // Pridėti nuomos įrašą
    [HttpPost]
    public IActionResult AddRental([FromBody] NuomosIrasas nuoma)
    {
        _logger.LogInformation("Užklausa pridėti nuomos įrašą.");
        if (nuoma == null)
        {
            _logger.LogError("Nuomos įrašas negali būti tuščias.");
            return BadRequest("Nuomos įrašas negali būti tuščias.");
        }

        // Patikriname, ar nuoma atitinka sąlygas
        var knyga = _knyguServisas.GautiKnygaPagalId(nuoma.KnygosId);
        if (knyga == null || knyga is PopierineKnyga popierineKnyga && popierineKnyga.KopijuKiekis <= 0)
        {
            return BadRequest("Knyga negalima nuomoti, nes ji nėra prieinama.");
        }

        if (nuoma.NuomosPabaigosData <= nuoma.NuomosData)
        {
            return BadRequest("Nuomos pabaigos data turi būti vėlesnė už pradžios datą.");
        }

        _nuomosServisas.PridetiNuomosIrasa(nuoma);
        _logger.LogInformation($"Nuomos įrašas su ID {nuoma.Id} sėkmingai pridėtas.");
        return CreatedAtAction(nameof(GetAllRentals), new { id = nuoma.Id }, nuoma);
    }

    // Užbaigti nuomą pagal ID
    [HttpPut("{id}/uzbaigti")]
    public IActionResult FinishRental(int id)
    {
        _logger.LogInformation($"Užklausa užbaigti nuomą su ID: {id}.");
        var nuoma = _nuomosServisas.GautiAktyviaNuomaPagalKlienta(id);
        if (nuoma == null)
        {
            _logger.LogWarning($"Nuomos įrašas su ID {id} nerastas.");
            return NotFound($"Nuomos įrašas su ID {id} nerastas.");
        }

        _nuomosServisas.UzbaigtiNuoma(id);
        _logger.LogInformation($"Nuomos įrašas su ID {id} sėkmingai užbaigtas.");
        return NoContent();
    }

    // Gauti aktyvią nuomą pagal vartotoją
    [HttpGet("klientas/{klientasId}")]
    public ActionResult<NuomosIrasas?> GetActiveRental(int klientasId)
    {
        _logger.LogInformation($"Užklausa gauti aktyvią nuomą klientui su ID: {klientasId}.");
        var nuoma = _nuomosServisas.GautiAktyviaNuomaPagalKlienta(klientasId);
        if (nuoma == null)
        {
            _logger.LogWarning($"Aktyvi nuoma klientui su ID {klientasId} nerasta.");
            return NotFound($"Aktyvi nuoma klientui su ID {klientasId} nerasta.");
        }
        return Ok(nuoma);
    }

    // Gauti visą nuomos istoriją pagal vartotoją
    [HttpGet("istorija/{klientasId}")]
    public ActionResult<List<NuomosIrasas>> GetRentalHistory(int klientasId)
    {
        _logger.LogInformation($"Užklausa gauti nuomos istoriją klientui su ID: {klientasId}.");
        var nuomos = _nuomosServisas.GautiVisasNuomasPagalKlienta(klientasId);
        if (nuomos == null || nuomos.Count == 0)
        {
            _logger.LogWarning($"Nuomos istorija klientui su ID {klientasId} nerasta.");
            return NotFound($"Nuomos istorija klientui su ID {klientasId} nerasta.");
        }
        return Ok(nuomos);
    }
}
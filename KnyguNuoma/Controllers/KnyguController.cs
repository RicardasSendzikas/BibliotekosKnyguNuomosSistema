using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class KnyguController : ControllerBase
{
    private readonly IKnyguServisas _knyguServisas;
    private readonly ILogger<KnyguController> _logger;

    public KnyguController(IKnyguServisas knyguServisas, ILogger<KnyguController> logger)
    {
        _knyguServisas = knyguServisas;
        _logger = logger;
    }

    // Gauti visas knygas
    [HttpGet]
    public ActionResult<List<Knyga>> GetAllBooks()
    {
        _logger.LogInformation("Užklausa gauti visas knygas.");
        var knygos = _knyguServisas.GautiVisasKnygas();
        if (knygos == null || knygos.Count == 0)
        {
            _logger.LogWarning("Knygų nerasta.");
            return NotFound("Knygų nerasta.");
        }
        return Ok(knygos);
    }

    // Gauti knygas pagal kategoriją
    [HttpGet("kategorija/{kategorija}")]
    public ActionResult<List<Knyga>> GetBooksByCategory(string kategorija)
    {
        _logger.LogInformation($"Užklausa gauti knygas pagal kategoriją: {kategorija}.");
        var knygos = _knyguServisas.IeskotiPagalKategorija(kategorija);
        if (knygos == null || knygos.Count == 0)
        {
            _logger.LogWarning($"Knygų su kategorija '{kategorija}' nerasta.");
            return NotFound($"Knygų su kategorija '{kategorija}' nerasta.");
        }
        return Ok(knygos);
    }

    // Pridėti naują knygą
    [HttpPost]
    public IActionResult AddBook([FromBody] Knyga knyga)
    {
        _logger.LogInformation("Užklausa pridėti knygą.");
        if (knyga == null || string.IsNullOrWhiteSpace(knyga.Pavadinimas) ||
            string.IsNullOrWhiteSpace(knyga.Autorius) ||
            string.IsNullOrWhiteSpace(knyga.Kategorija) ||
            knyga.NuomosKaina <= 0)
        {
            _logger.LogError("Knyga negali būti tuščia ir visi laukai privalo būti užpildyti teisingai.");
            return BadRequest("Knyga negali būti tuščia ir visi laukai privalo būti užpildyti teisingai.");
        }

        _knyguServisas.PridetiKnyga(knyga);
        _logger.LogInformation($"Knyga su ID {knyga.Id} sėkmingai pridėta.");
        return CreatedAtAction(nameof(GetAllBooks), new { id = knyga.Id }, knyga);
    }

    // Ištrinti knygą pagal ID
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        _logger.LogInformation($"Užklausa ištrinti knygą su ID: {id}.");
        var knyga = _knyguServisas.GautiKnygaPagalId(id);
        if (knyga == null)
        {
            _logger.LogWarning($"Knyga su ID {id} nerasta.");
            return NotFound($"Knyga su ID {id} nerasta.");
        }

        _knyguServisas.PasalintiKnyga(id);
        _logger.LogInformation($"Knyga su ID {id} sėkmingai ištrinta.");
        return NoContent();
    }
}
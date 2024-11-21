using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class VartotojuController : ControllerBase
{
    private readonly INaudotojuRepozitorija _naudotojuRepo;
    private readonly INuomosServisas _nuomosServisas; // Pridėtas nuomos servisas
    private readonly ILogger<VartotojuController> _logger;

    public VartotojuController(INaudotojuRepozitorija naudotojuRepo, INuomosServisas nuomosServisas, ILogger<VartotojuController> logger)
    {
        _naudotojuRepo = naudotojuRepo;
        _nuomosServisas = nuomosServisas; // Inicializuojame nuomos servisą
        _logger = logger;
    }

    // Gauti visus vartotojus
    [HttpGet]
    public ActionResult<List<Vartotojas>> GetAllUsers()
    {
        _logger.LogInformation("Užklausa gauti visus vartotojus.");
        var vartotojai = _naudotojuRepo.GautiVisus();
        if (vartotojai == null || vartotojai.Count == 0)
        {
            _logger.LogWarning("Vartotojų nerasta.");
            return NotFound("Vartotojų nerasta.");
        }
        return Ok(vartotojai);
    }

    // Gauti vartotoją pagal ID
    [HttpGet("{id}")]
    public ActionResult<Vartotojas?> GetUserById(int id)
    {
        _logger.LogInformation($"Užklausa gauti vartotoją su ID: {id}.");
        var vartotojas = _naudotojuRepo.GautiPagalId(id);
        if (vartotojas == null)
        {
            _logger.LogWarning($"Vartotojas su ID {id} nerastas.");
            return NotFound($"Vartotojas su ID {id} nerastas.");
        }
        return Ok(vartotojas);
    }

    // Pridėti vartotoją
    [HttpPost]
    public IActionResult AddUser([FromBody] Vartotojas vartotojas)
    {
        _logger.LogInformation("Užklausa pridėti vartotoją.");
        if (vartotojas == null)
        {
            _logger.LogError("Vartotojas negali būti tuščias.");
            return BadRequest("Vartotojas negali būti tuščias.");
        }

        // Patikrinkite, ar vartotojas su tokiu pašto adresu jau egzistuoja.
        var esamasVartotojas = _naudotojuRepo.GautiVisus().FirstOrDefault(v => v.ElPastas == vartotojas.ElPastas);
        if (esamasVartotojas != null)
        {
            _logger.LogError("Vartotojas su šiuo el. pašto adresu jau egzistuoja.");
            return BadRequest("Vartotojas su šiuo el. pašto adresu jau egzistuoja.");
        }

        _naudotojuRepo.Prideti(vartotojas);
        _logger.LogInformation($"Vartotojas su ID {vartotojas.Id} sėkmingai pridėtas.");
        return CreatedAtAction(nameof(GetUserById), new { id = vartotojas.Id }, vartotojas);
    }

    // Gauti visus vartotojus, kurie nuomojo konkrečią knygą
    [HttpGet("knyga/{knygaId}")]
    public ActionResult<List<Vartotojas>> GetUsersByBook(int knygaId)
    {
        _logger.LogInformation($"Užklausa gauti vartotojus, kurie nuomojo knygą su ID: {knygaId}.");
        var vartotojai = _nuomosServisas.GautiVisusKlientusPagalKnyga(knygaId); // Kvietimas į nuomos servisą
        if (vartotojai == null || vartotojai.Count == 0)
        {
            _logger.LogWarning($"Vartotojų, kurie nuomojo knygą su ID {knygaId} nerasta.");
            return NotFound($"Vartotojų, kurie nuomojo knygą su ID {knygaId} nerasta.");
        }
        return Ok(vartotojai);
    }
}
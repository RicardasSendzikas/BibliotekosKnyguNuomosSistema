public class NuomosIrasas
{
    public int Id { get; set; }
    public string Pavadinimas { get; set; }
    public string Autorius { get; set; }
    public string Kategorija { get; set; }
    public double NuomosKaina { get; set; }
    public int VartotojoId { get; set; }
    public int KnygosId { get; set; } // Pridėkite šį lauką
    public DateTime NuomosData { get; set; }
    public DateTime NuomosPabaigosData { get; set; }
}
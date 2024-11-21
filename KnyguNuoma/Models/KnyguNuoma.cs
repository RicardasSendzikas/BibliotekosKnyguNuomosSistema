namespace KnyguNuoma.Models
{
    public class KnyguNuoma : Knyga
    {
        public int Id { get; set; }              // Knygos ID
        public string Pavadinimas { get; set; }  // Knygos pavadinimas
        public string Autorius { get; set; }     // Knygos autorius
        public string ISBN { get; set; }         // Knygos ISBN numeris
        public int Metai { get; set; }           // Knygos išleidimo metai
        public bool Prieinama { get; set; }      // Knygos prieinamumo statusas
    }
}
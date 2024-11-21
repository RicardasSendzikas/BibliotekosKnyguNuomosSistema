namespace KnyguNuoma.Models
{
    public class Knyga : BaseEntity
    {
        public string Pavadinimas { get; set; }
        public string Autorius { get; set; }
        public string Kategorija { get; set; }
        public decimal NuomosKaina { get; set; }
    }
}
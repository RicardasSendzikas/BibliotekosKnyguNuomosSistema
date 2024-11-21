namespace KnyguNuoma.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime SukurimoData { get; set; } = DateTime.UtcNow;
    }
}
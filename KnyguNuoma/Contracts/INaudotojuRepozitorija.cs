using KnyguNuoma.Models;

namespace KnyguNuoma.Contracts
{
    public interface INaudotojuRepozitorija
    {
        List<Vartotojas> GautiVisus();
        Vartotojas? GautiPagalId(int id);
        void Prideti(Vartotojas vartotojas); // Šis metodas buvo pridėtas
    }
}
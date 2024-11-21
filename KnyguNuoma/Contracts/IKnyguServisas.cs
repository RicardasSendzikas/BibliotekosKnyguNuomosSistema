using KnyguNuoma.Models;

namespace KnyguNuoma.Contracts
{
    public interface IKnyguServisas
    {
        List<Knyga> GautiVisasKnygas();
        Knyga? GautiKnygaPagalId(int id);
        void PridetiKnyga(Knyga knyga);
        void PasalintiKnyga(int id);
        List<Knyga> IeskotiPagalKategorija(string kategorija);
    }
}
using KnyguNuoma.Models;

namespace KnyguNuoma.Contracts
{
    public interface INuomosServisas
    {
        List<NuomosIrasas> GautiVisasNuomas();
        void PridetiNuomosIrasa(NuomosIrasas nuoma);
        void UzbaigtiNuoma(int id);
        NuomosIrasas? GautiAktyviaNuomaPagalKlienta(int klientasId);
        List<NuomosIrasas> GautiVisasNuomasPagalKlienta(int klientasId);
        List<Vartotojas> GautiVisusKlientusPagalKnyga(int knygaId);
    }
}
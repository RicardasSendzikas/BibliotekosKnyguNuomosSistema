using KnyguNuoma.Models;

namespace KnyguNuoma.Contracts
{
    public interface INuomosRepozitorija
    {
        List<NuomosIrasas> GautiVisas();
        void Prideti(NuomosIrasas nuoma);
        void Pasalinti(int id);
    }
}
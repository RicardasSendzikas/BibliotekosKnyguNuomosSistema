using KnyguNuoma.Contracts;
using KnyguNuoma.Models;

namespace KnyguNuoma.Services
{
    public class KnyguServisas : IKnyguServisas
    {
        private readonly IKnyguRepozitorija _repo;

        public KnyguServisas(IKnyguRepozitorija repo)
        {
            _repo = repo;
        }

        public List<Knyga> GautiVisasKnygas() => _repo.GautiVisas();

        public Knyga? GautiKnygaPagalId(int id) => _repo.GautiPagalId(id);

        public void PridetiKnyga(Knyga knyga) => _repo.Prideti(knyga);

        public void PasalintiKnyga(int id) => _repo.Pasalinti(id);

        public List<Knyga> IeskotiPagalKategorija(string kategorija)
        {
            return _repo.GautiVisas().Where(k => k.Kategorija == kategorija).ToList();
        }
    }
}
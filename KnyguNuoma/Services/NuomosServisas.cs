using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnyguNuoma.Services
{
    public class NuomosServisas : INuomosServisas
    {
        private readonly INuomosRepozitorija _repo;

        public NuomosServisas(INuomosRepozitorija repo)
        {
            _repo = repo;
        }

        // Gauti visas nuomas
        public List<NuomosIrasas> GautiVisasNuomas() => _repo.GautiVisas();

        // Pridėti nuomos įrašą
        public void PridetiNuomosIrasa(NuomosIrasas nuoma)
        {
            if (nuoma == null)
            {
                throw new ArgumentNullException(nameof(nuoma), "Nuomos įrašas negali būti tuščias.");
            }
            _repo.Prideti(nuoma);
        }

        // Užbaigti nuomą
        public void UzbaigtiNuoma(int id)
        {
            var nuoma = _repo.GautiVisas().FirstOrDefault(n => n.Id == id);
            if (nuoma == null)
            {
                throw new KeyNotFoundException($"Nuomos įrašas su ID {id} nerastas.");
            }
            nuoma.NuomosPabaigosData = DateTime.UtcNow; // Nustatome pabaigos datą
            _repo.Pasalinti(id); // Pašaliname seną įrašą
            _repo.Prideti(nuoma); // Pridėti su atnaujinta data
        }

        // Gauti aktyvią nuomą pagal vartotoją
        public NuomosIrasas? GautiAktyviaNuomaPagalKlienta(int klientasId)
        {
            return _repo.GautiVisas().FirstOrDefault(n => n.VartotojoId == klientasId && n.NuomosPabaigosData == null);
        }

        // Gauti visas nuomos įrašus pagal vartotoją
        public List<NuomosIrasas> GautiVisasNuomasPagalKlienta(int klientasId)
        {
            return _repo.GautiVisas().Where(n => n.VartotojoId == klientasId).ToList();
        }

        // Gauti visus klientus pagal knygą
        public List<Vartotojas> GautiVisusKlientusPagalKnyga(int knygaId)
        {
            var nuomos = _repo.GautiVisas().Where(n => n.KnygosId == knygaId).Select(n => n.VartotojoId).ToList();
            return nuomos.Select(id => GautiVartotojaPagalId(id)).ToList(); // Problema: nesusiję su Vartotojų repo
        }

        // Gauti vartotoją pagal ID
        private Vartotojas GautiVartotojaPagalId(int id)
        {
            // Jums reikėtų turėti nuorodą į vartotojų repo, kad gauti vartotoją pagal ID
            // Pvz.:
            // return _naudotojuRepo.GautiPagalId(id);
            throw new NotImplementedException("Ši funkcija dar nėra įgyvendinta.");
        }
    }
}
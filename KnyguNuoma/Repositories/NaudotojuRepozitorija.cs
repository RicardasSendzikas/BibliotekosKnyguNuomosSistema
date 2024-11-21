using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace KnyguNuoma.Repositories
{
    public class NaudotojuRepozitorija : INaudotojuRepozitorija
    {
        private readonly IMongoCollection<Vartotojas> _naudotojai;

        public NaudotojuRepozitorija(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("KnyguNuomaDB");
            _naudotojai = database.GetCollection<Vartotojas>("Naudotojai");
        }

        public List<Vartotojas> GautiVisus() => _naudotojai.Find(n => true).ToList();

        public Vartotojas? GautiPagalId(int id) => _naudotojai.Find(n => n.Id == id).FirstOrDefault();

        public void Prideti(Vartotojas vartotojas)
        {
            if (vartotojas != null)
            {
                _naudotojai.InsertOne(vartotojas); // Pridedame vartotoją į duomenų bazę
            }
        }
    }
}
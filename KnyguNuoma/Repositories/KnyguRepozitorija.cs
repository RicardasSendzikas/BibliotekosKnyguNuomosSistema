using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using MongoDB.Driver;

namespace KnyguNuoma.Repositories
{
    public class KnyguRepozitorija : IKnyguRepozitorija
    {
        private readonly IMongoCollection<Knyga> _knygos;

        public KnyguRepozitorija(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("KnyguNuomaDB");
            _knygos = database.GetCollection<Knyga>("Knygos");
        }

        public List<Knyga> GautiVisas() => _knygos.Find(k => true).ToList();

        public Knyga? GautiPagalId(int id) => _knygos.Find(k => k.Id == id).FirstOrDefault();

        public void Prideti(Knyga knyga) => _knygos.InsertOne(knyga);

        public void Pasalinti(int id) => _knygos.DeleteOne(k => k.Id == id);
    }
}
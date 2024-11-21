using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using MongoDB.Driver;

namespace KnyguNuoma.Repositories
{
    public class NuomosRepozitorija : INuomosRepozitorija
    {
        private readonly IMongoCollection<NuomosIrasas> _nuomos;

        public NuomosRepozitorija(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("KnyguNuomaDB");
            _nuomos = database.GetCollection<NuomosIrasas>("Nuomos");
        }

        public List<NuomosIrasas> GautiVisas() => _nuomos.Find(n => true).ToList();

        public void Prideti(NuomosIrasas nuoma) => _nuomos.InsertOne(nuoma);

        public void Pasalinti(int id) => _nuomos.DeleteOne(n => n.Id == id);
    }
}
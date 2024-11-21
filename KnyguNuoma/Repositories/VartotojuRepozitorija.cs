using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using System.Collections.Generic;

namespace KnyguNuoma.Repositories
{
    public class VartotojuRepozitorija : INaudotojuRepozitorija
    {
        private readonly List<Vartotojas> _vartotojai = new List<Vartotojas>();

        public List<Vartotojas> GautiVisus()
        {
            return _vartotojai;
        }

        public Vartotojas GautiPagalId(int id)
        {
            return _vartotojai.FirstOrDefault(v => v.Id == id);
        }

        public void Prideti(Vartotojas vartotojas)
        {
            if (vartotojas != null)
            {
                _vartotojai.Add(vartotojas);
            }
        }

        public void Pasalinti(int id)
        {
            var vartotojas = GautiPagalId(id);
            if (vartotojas != null)
            {
                _vartotojai.Remove(vartotojas);
            }
        }
    }
}
using KnyguNuoma.Models;
using System.Collections.Generic;
namespace KnyguNuoma.Contracts
{
    public interface IKnyguRepozitorija
    {
        List<Knyga> GautiVisas();
        Knyga? GautiPagalId(int id);
        void Prideti(Knyga knyga);
        void Pasalinti(int id);
    }
}
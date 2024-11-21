using KnyguNuoma.Contracts;
using KnyguNuoma.Models;
using KnyguNuoma.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

public class KnyguServisasTests
{
    private readonly Mock<IKnyguRepozitorija> _knyguRepoMock;
    private readonly KnyguServisas _knyguServisas;

    public KnyguServisasTests()
    {
        _knyguRepoMock = new Mock<IKnyguRepozitorija>();
        _knyguServisas = new KnyguServisas(_knyguRepoMock.Object); // Perduodame mock repo
    }

    [Fact]
    public void GautiVisasKnygas_TuretuGrazintiKnyguSarasa()
    {
        // Setup mock
        var knygos = new List<Knyga>
        {
            new Knyga { Id = 1, Pavadinimas = "Knyga 1" },
            new Knyga { Id = 2, Pavadinimas = "Knyga 2" }
        };

        _knyguRepoMock.Setup(repo => repo.GautiVisas()).Returns(knygos); // Naudokite GautiVisas

        // Call the method
        var result = _knyguServisas.GautiVisasKnygas();

        // Assert
        Assert.IsAssignableFrom<List<Knyga>>(result);
        Assert.Equal(2, result.Count);
    }
}
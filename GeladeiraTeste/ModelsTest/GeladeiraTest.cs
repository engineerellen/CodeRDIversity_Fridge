using Domain.Models;

namespace GeladeiraTeste.ModelsTest
{
    public class GeladeiraTest
    {
        private Geladeira _geladeira = new Geladeira();

        [Fact]
        public void ImprimeGeladeira_Test() =>
            Assert.NotEqual(_geladeira.ImprimeGeladeira(), string.Empty);
    }
}
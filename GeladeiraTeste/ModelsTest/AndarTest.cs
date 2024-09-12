using Domain.Models;

namespace GeladeiraTeste.ModelsTest
{
    public class AndarTest
    {
        private Andar andar = new Andar();

        [InlineData(0, "Verduras")]
        [InlineData(1, "Laticínios")]
        [InlineData(2, "Carnes")]
        public void InstanciaAndar_Test(int numAndar, string descricao)
        {
            andar = new Andar(numAndar, descricao);

            Assert.NotNull(andar);
        }

        [Fact]
        public void CriaAndar_Test() =>
            Assert.True(andar.CriaAndar().Count > 0);

        [Fact]
        public void RetornarAndares_Test() =>
                    Assert.True(andar.RetornarAndares().Count > 0);

        [Fact]
        public void RetornarAndaresParam_Test() =>
                Assert.True(andar.RetornarAndares(1).Count > 0);

        [Fact]
        public void ValidarAndares_Test() =>
           Assert.Throws<Exception>(() => andar.ValidarAndares(5, andar.RetornarAndares()));


        [Fact]
        public void ImprimirAndar_Test()
        {
            andar = new Andar(1, "Laticínios");
            Assert.NotNull(andar.ImprimirAndar());
        }

    }
}